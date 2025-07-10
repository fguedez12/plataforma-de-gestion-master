﻿var app = angular.module('adminDocumentosApp', ['ui.router', 'cp.ngConfirm', 'ServicesModule'])
    .config(['$compileProvider', '$httpProvider', '$provide', function($compileProvider, $httpProvider, $provide) {
        // Configurar cabeceras HTTP para evitar caché
        $httpProvider.defaults.headers.get = {
            'Cache-Control': 'no-cache, no-store, must-revalidate',
            'Pragma': 'no-cache',
            'Expires': '0'
        };
        
        // Permitir enlaces seguros
        $compileProvider.aHrefSanitizationWhitelist(/^\s*(https?|ftp|mailto|chrome-extension):/);
        
        // Decorator para $templateCache que evita el caché de templates
        $provide.decorator('$templateCache', ['$delegate', function($delegate) {
            var originalGet = $delegate.get;
            $delegate.get = function(key) {
                if (key.startsWith('wwwroot/')) {
                    return null; // Forzar recarga del template
                }
                return originalGet.apply(this, arguments);
            };
            return $delegate;
        }]);

        // Agregar timestamp a URLs de templates y prevenir caché
        $provide.decorator('$templateRequest', ['$delegate', '$templateCache', function($delegate, $templateCache) {
            var fn = function(tpl) {
                if (tpl.startsWith('wwwroot/')) {
                    // Limpiar esta URL del cache
                    $templateCache.remove(tpl);
                    // Agregar timestamp único para forzar recarga
                    tpl += (tpl.indexOf('?') === -1 ? '?' : '&') + '_t=' + new Date().getTime();
                }
                return $delegate.call(this, tpl);
            };
            return angular.extend(fn, $delegate);
        }]);
    }])
    .controller("adminDocumentosController", function ($rootScope, $state, $http, $timeout) {
        var userId = angular.element("#input-id").val();
        var isAdmin = angular.element("#input-is-admin").val();
        $rootScope.etapa = angular.element("#input-etapa").val();       
        const isConsulta = angular.element("#input-is-consulta").val();
        $rootScope.userId = userId;
        $rootScope.isAdmin = isAdmin == 'False' ? false : true;
        $rootScope.isConsulta = isConsulta == 'False' ? false : true;
        $rootScope.servicioEvId = localStorage.getItem('servicioEvId');
        if (!$rootScope.servicioEvId) {
            window.location.href = '/AdminMisServicios';
        }
        $state.go("adminDocumentos@home");
    })
    .controller("homeDocumentosController", function ($scope, $rootScope, $http, $ngConfirm, $state, $serviciosModule, $timeout) {
        $scope.showAlertDisponibilidad = false;
        $scope.alertDisponibilidadText = "El adjunto estará disponible a partir del 1° de noviembre del 2025.";

        $http({
            method: 'GET',
            url: `/settings?sectionName=ApiConfiguration&paramName=apiGestionaEnergia`

        }).then(function (response) {
            $rootScope.APIURL = response.data;
            $serviciosModule.getDiagnostico($rootScope.servicioEvId).then(resp => {
                if (resp.data.revisionDiagnosticoAmbiental) {
                    if (!$rootScope.isAdmin) {
                        $rootScope.isConsulta = true
                    }
                }
            });
            loadAllData(1, $rootScope.servicioEvId);
        });

        $http({
            method: 'GET',
            url: `/settings?sectionName=DirectorioArchivos&paramName=DisenioPasivoPubFiles`

        }).then(function (response) {
            $rootScope.FILESERVER = response.data;
        });
        $http({
            method: 'GET',
            url: `/settings?sectionName=DirectorioArchivos&paramName=EstadoVerdeFolder`

        }).then(function (response) {
            $rootScope.ESTADO_VERDE_FOLDER = response.data;
        });
        $http({
            method: 'GET',
            url: `/settings?sectionName=DirectorioArchivos&paramName=DocumentosFolder`

        }).then(function (response) {
            $rootScope.DOCUMENTOS_FOLDER = response.data;
        });
        //Declarations
        const TIPO_DOCUMENTO_ACTA = 1;
        const TIPO_DOCUMENTO_REUNION = 2;
        const TIPO_DOCUMENTO_LISTA_INTEGRANTES = 3;
        const TIPO_DOCUMENTO_POLITICA = 4;
        const TIPO_DOCUMENTO_DIFUSION = 5;
        const TIPO_DOCUMENTO_PROC_PAPEL = 6;
        const TIPO_DOCUMENTO_PROC_RESIDUO = 1005;
        const TIPO_DOCUMENTO_PROC_RESIDUO_SISTEMA = 1006;
        const TIPO_DOCUMENTO_PROC_BAJA_INMUEBLES = 1007;
        const TIPO_DOCUMENTO_PROC_COMPRA_SUSTENTABLE = 1008;
        const TIPO_DOCUMENTO_CHARLAS = 1009;
        const TIPO_DOCUMENTO_LISTADO_COLABORADORES = 1010;
        const TIPO_DOCUMENTO_PROC_REUTILIZACION_PAPEL = 1011;
        const TIPO_DOCUMENTO_CAPACITADOS_MP = 1012;
        const TIPO_DOCUMENTO_GESTION_COMPRA_SUSTENTABLE = 1013;
        const TIPO_DOCUMENTO_PAC_E3 = 1015;
        const TIPO_DOCUMENTO_INFORME_DA = 1016;
        const TIPO_DOCUMENTO_RESOLUCION_APRUEBA_PLAN = 1017;
        $scope.tipoDocumentos = [];
        $scope.Comites = [];
        $scope.politicas = [];
        $scope.pga = [];
        $scope.procedimientos = [];
        $scope.charlas = [];
        $scope.comprasustentables = [];
        $scope.page = 1;
        $scope.pagePoliticas = 1;
        $scope.pagePga = 1;
        $scope.pageProcedimientos = 1;
        $scope.pageCharlas = 1;
        $scope.pageCompraSustentable = 1;
        $scope.modalTitle = "";
        $scope.editMode = false;
        $scope.loadingTable = false;
        $scope.loadingTablePolitica = false;
        $scope.loadingTableProcedimientos = false;
        $scope.loadingTableCharlas = false;
        $scope.loadingTableCompraSustentables = false;
        $scope.actasLista = [];
        $scope.nombreServicio = localStorage.getItem('servicioEvNombre');
        $scope.sizeModal = "";
        $scope.colLista = "";
        $scope.listaIntegrantes = [];
        $scope.integrante = { nombre: "", rol: "", email: "", marca: false };
        $scope.lblReduccion = "Reducción";
        $scope.lblReutilizacion = "Reutilización";
        $scope.lblReciclaje = "Reciclaje";
        $scope.editText = "Editar";
        $scope.showDocumentoActa = false;
        $scope.anioDoc = "0";
        $scope.anioDocInt = 0;
        $scope.fechaText = "Fecha:";
        $scope.filetypes = "image/*,.pdf,.rar,.zip";
        $scope.fechaMIn = "";
        $scope.adjuntoText = "Adjuntar documento:";
        $scope.showDocumentoRespaldo = false;
        $scope.validationDocPACE3 = false;
        $scope.showAlertGCSE1 = false;
        $scope.showAlertDisponibilidad = false;
        $scope.alertDisponibilidadText = "El adjunto estará disponible a partir del 1° de noviembre del 2025.";
        $scope.showTemplatePoliticaE2 = false;
        $scope.showConsulta = false;

        // Calculate years for the select dropdown
        const currentYear = new Date().getFullYear();
        $scope.yearsForSelect = [
            currentYear,
            currentYear - 1,
            currentYear - 2
        ];
        // anioDoc is already initialized to "0" earlier, keeping it

        if ($rootScope.isConsulta) {
            $scope.editText = "Ver";
        } else {
            $scope.editText = "Editar";
        }


        $scope.changeFile = async function () {
            
            const file = document.querySelector("#adjunto").files[0];
            let b64File = await toBase64(file);
            var base64result = b64File.split(',')[1];
            $scope.documento.adjunto = base64result
            $scope.$apply(function () {
                if ($scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_PAC_E3) {
                    if ($scope.documento.adjuntoRespaldoPathCompromiso) {
                        $scope.validationDocPACE3 = true;
                    } else {
                        $scope.validationDocPACE3 = false;
                    }
                } else {
                    $scope.validationDocPACE3 = false;
                }
            })
        }

        $scope.clearFile = function () {
            var fileInput = document.querySelector("#adjunto");
            fileInput.value = ""; // Limpiar el valor del input
            $scope.documento.adjuntoPath = null; // Limpiar el modelo
            $scope.documento.adjunto = null; // Limpiar el modelo base64
            if ($scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_PAC_E3) {
                if ($scope.editMode) {
                    $scope.validationDocPACE3 = false;
                } else {
                    if (!$scope.documento.adjuntoRespaldoPathCompromiso) {
                        $scope.validationDocPACE3 = true;
                    } else {
                        $scope.validationDocPACE3 = false;
                    }
                }
                
            } else {
                $scope.validationDocPACE3 = false;
            }
            $scope.$apply(); // Aplicar los cambios al scope
        }

        $scope.changeFileCompraFuera = async function () {

            const file = document.querySelector("#adjuntoCompraFuera").files[0];
            let b64File = await toBase64(file);
            var base64result = b64File.split(',')[1];
            $scope.documento.adjuntoCompraFuera = base64result
        }

        $scope.changeFileCompraSustentableAnt = async function () {

            const file = document.querySelector("#adjuntoCompraSustentableAnt").files[0];
            let b64File = await toBase64(file);
            var base64result = b64File.split(',')[1];
            $scope.documento.adjuntoCompraSustentableAnt = base64result
        }
        $scope.changeFileRespaldoParticipativo = async function () {

            const file = document.querySelector("#adjuntoRespaldoParticipativo").files[0];
            let b64File = await toBase64(file);
            var base64result = b64File.split(',')[1];
            $scope.documento.adjuntoRespaldoParticipativo = base64result
        }
        $scope.changeFileRespaldoCompromiso = async function () {

            const file = document.querySelector("#adjuntoRespaldoCompromiso").files[0];
            let b64File = await toBase64(file);
            var base64result = b64File.split(',')[1];
            $scope.documento.adjuntoRespaldoCompromiso = base64result
            $scope.$apply(function () {
                if ($scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_PAC_E3) {
                    if ($scope.documento.adjuntoPath) {
                        $scope.validationDocPACE3 = true;
                    } else {
                        $scope.validationDocPACE3 = false;
                    }
                } else {
                    $scope.validationDocPACE3 = false;
                }
            })
        }

        $scope.clearFileCompromiso = function () {
            var fileInput = document.querySelector("#adjuntoRespaldoCompromiso");
            fileInput.value = ""; // Limpiar el valor del input
            $scope.documento.adjuntoRespaldoPathCompromiso = null; // Limpiar el modelo
            $scope.documento.adjuntoRespaldoCompromiso = null; // Limpiar el modelo base64
            if ($scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_PAC_E3) {
                if ($scope.editMode) {
                    $scope.validationDocPACE3 = false;
                } else {
                    if (!$scope.documento.adjuntoPath) {
                        $scope.validationDocPACE3 = true;
                    } else {
                        $scope.validationDocPACE3 = false;
                    }
                }
                
            } else {
                $scope.validationDocPACE3 = false;
            }
            $scope.$apply(); // Aplicar los cambios al scope
        }
        $scope.changeFileRespaldo = async function () {
            const file = document.querySelector("#adjuntoRespaldo").files[0];
            let b64File = await toBase64(file);
            var base64result = b64File.split(',')[1];
            $scope.documento.adjuntoRespaldo = base64result
            
        }

        const toBase64 = file => new Promise((resolve, reject) => {
            if (file == null) {
                return;
            }
            const reader = new FileReader();
            reader.readAsDataURL(file);
            reader.onload = () => resolve(reader.result);
            reader.onerror = reject;
        });

        $scope.checkPoliticaAmbiental = function () {
            patchServicio("NoRegistraPoliticaAmbiental", $scope.politicaAmbientalCheck);
        }
        $scope.checkActividadInterna = function () {
            patchServicio("NoRegistraActividadInterna", $scope.actividadInternaCheck);
        }
        $scope.checkReutilizacionPapel = function () {
            patchServicio("NoRegistraReutilizacionPapel", $scope.reutilizacionPapelCheck);
        }
        $scope.checkProcFormalPapel = function () {
            patchServicio("NoRegistraProcFormalPapel", $scope.procFormalPapelCheck);
        }
        $scope.checkDocResiduosCertificados = function () {
            patchServicio("NoRegistraDocResiduosCertificados", $scope.docResiduosCertificadosCheck);
        }
        $scope.checkDocResiduosSistemas = function () {
            patchServicio("NoRegistraDocResiduosSistemas", $scope.docResiduosSistemasCheck);
        }
        $scope.checkProcBajaBienesMuebles = function () {
            patchServicio("NoRegistraProcBajaBienesMuebles", $scope.procBajaBienesMueblesCheck);
        }
        $scope.checkProcComprasSustentables = function () {
            patchServicio("NoRegistraProcComprasSustentables", $scope.procComprasSustentablesCheck);
        }

        function patchServicio(field,value) {
            payload = [{
                "path": `/${field}`,
                "op": "replace",
                "value": value
            }]

            $http({
                method: 'PATCH',
                url: `${$rootScope.APIURL}/api/servicios/${$rootScope.servicioEvId}`,
                data: angular.fromJson(payload),
            }).then(function (response) {
                //console.log(response)
            });
        }

        $scope.changeServicio = function (id) {
            if (id) {
                loadAllData(1, id); 
            } else {
                loadAllData(1, $rootScope.servicioEvId);
            }
            
        };

        $scope.changeAnioDoc = function () {
            let anioDocInt = parseInt($scope.anioDoc);
            $scope.anioDocInt = anioDocInt;
            loadAllData(1, $rootScope.servicioEvId);
           
        }

        $scope.changeInstitucion = function (id) {
            $scope.servicios=[];
            if (id) {
                $http({
                    method: 'GET',
                    url: `${$rootScope.APIURL}/api/servicios/getByInstitucionIdAndUserId/${id}/${$rootScope.userId}`
                }).then(function (response) {
                    //console.log(response.data);
                    $scope.servicios = response.data;
                })
            }
        }

        $scope.listaIntegrantesValid = function () {
            if ($scope.documento) {
                if ($scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_LISTA_INTEGRANTES) {
                    if ($scope.listaIntegrantes.length > 0) {
                        return false;
                    } else {
                        return true;
                    }
                } else {
                    return false;
                }
            }
            
        }

        $scope.newDocument = function () {
            $scope.editMode = false;
            $scope.modalTitle = "Agregar Documento";
            $scope.showModal();
        }

        $scope.showModal = function () {
            $scope.showAlertGCSE1 = false;
            $scope.showAlertDisponibilidad = false;
            $http({
                method: 'GET',
                url: `${$rootScope.APIURL}/api/tipoDocumentos/${$rootScope.etapa}`
            }).then(function (response) {
                if (response.data.ok) {
                    $scope.tipoDocumentos = response.data.tipoDocumentos;
                }
            });
            $("#modal-form").modal("show");
        }

        $scope.edit = function (id) {
            $scope.editMode = true;
            const url = `${$rootScope.APIURL}/api/documentos`
            httpGet(url, id, processEditDocumentoResponse);
            
        }

        $scope.validateIntegrante = function () {
            if ($scope.integrante.nombre != "" && $scope.integrante.areaInst != "" && $scope.integrante.rol != "" && $scope.integrante.email != "")  {
                return true;
            } else {
                return false;
            }
        }

        $scope.addIntegrante = function () {

            console.log($scope.integrante);
            $scope.listaIntegrantes.push($scope.integrante);
            $scope.integrante = { nombre: "", rol: "", email: "", marca: false };
        }

        $scope.remIntegrante = function (integrante) {
            console.log(integrante);
            $scope.listaIntegrantes = $scope.listaIntegrantes.filter(function (obj) {
                return obj.email != integrante.email;
            })
        }


        function processEditDocumentoResponse(response) {
            let doc;
            if (response.data.acta) {
                doc = response.data.acta
                $scope.showObservaciones = true;
                $scope.sizeModal = "";
                $scope.colLista = "";
            }
            if (response.data.reunion) {
                const url = `${$rootScope.APIURL}/api/documentos/actas-comite-lista/${$rootScope.servicioEvId}`;
                httpGet(url, null, setActasLista);
                doc = response.data.reunion;
                doc.actaComiteId = doc.actaComiteId.toString();
                $scope.showObservaciones = true;
            }
            if (response.data.listaIntegrante) {
                const url = `${$rootScope.APIURL}/api/documentos/actas-comite-lista/${$rootScope.servicioEvId}`;
                httpGet(url, null, setActasLista);
                doc = response.data.listaIntegrante;
                doc.actaComiteId = doc.actaComiteId.toString();
                $scope.listaIntegrantes = response.data.listaIntegrante.integrantes;
                //$scope.showObservaciones = true;
                $scope.sizeModal = "modal-lg";
                $scope.colLista = "col-lg-6";
               
            }
            if (response.data.politica) {
                doc = response.data.politica
                doc.cobertura = doc.cobertura.toString();
                $scope.showObservaciones = false;
                $scope.sizeModal = "";
                $scope.colLista = "";

                if (doc.adjuntoRespaldoUrlParticipativo) {
                    const adjuntoRespaldoParticipativo = doc.adjuntoRespaldoUrlParticipativo;
                    const adjuntoUrlArrRespaldoParticipativo = adjuntoRespaldoParticipativo.split('\\');
                    const fileNameRespaldoParticipativo = adjuntoUrlArrRespaldoParticipativo[adjuntoUrlArrRespaldoParticipativo.length - 1];
                    $scope.linkToDownloadRespaldoParticipativo = `${$rootScope.FILESERVER}${$rootScope.ESTADO_VERDE_FOLDER}/${$rootScope.DOCUMENTOS_FOLDER}/${fileNameRespaldoParticipativo}`
                }
                
                if (doc.adjuntoRespaldoUrl) {
                    const adjuntoRespaldoUrl = doc.adjuntoRespaldoUrl;
                    const adjuntoRespaldoUrlArr = adjuntoRespaldoUrl.split('\\');
                    const fileNameRespaldo = adjuntoRespaldoUrlArr[adjuntoRespaldoUrlArr.length - 1];
                    $scope.linkToDownloadRespaldo = `${$rootScope.FILESERVER}${$rootScope.ESTADO_VERDE_FOLDER}/${$rootScope.DOCUMENTOS_FOLDER}/${fileNameRespaldo}`
                }
            }
            if (response.data.difusion) {
                const url = `${$rootScope.APIURL}/api/documentos/politicas-lista/${$rootScope.servicioEvId}`;
                httpGet(url, null, setPoliticasLista);
                doc = response.data.difusion
                doc.politicaId = doc.politicaId.toString();
                $scope.showObservaciones = false;
                $scope.sizeModal = "";
                $scope.colLista = "";
            }
            if (response.data.procedimientoPapel) {
                const url = `${$rootScope.APIURL}/api/documentos/politicas-lista/${$rootScope.servicioEvId}`;
                httpGet(url, null, setPoliticasLista);
                doc = response.data.procedimientoPapel
                if (doc.politicaId) {
                    doc.politicaId = doc.politicaId.toString();
                } else {
                    doc.politicaId = '';
                }
                
                doc.cobertura = doc.cobertura.toString();
                $scope.showObservaciones = false;
                $scope.sizeModal = "";
                $scope.colLista = "";
            }
            if (response.data.procedimientoResiduo) {
                const url = `${$rootScope.APIURL}/api/documentos/politicas-lista/${$rootScope.servicioEvId}`;
                httpGet(url, null, setPoliticasLista);
                doc = response.data.procedimientoResiduo
                if (doc.politicaId) {
                    doc.politicaId = doc.politicaId.toString();
                } else {
                    doc.politicaId = '';
                }
                $scope.showObservaciones = false;
                $scope.sizeModal = "";
                $scope.colLista = "";
            }
            if (response.data.procedimientoResiduoSistema) {
                const url = `${$rootScope.APIURL}/api/documentos/politicas-lista/${$rootScope.servicioEvId}`;
                httpGet(url, null, setPoliticasLista);
                doc = response.data.procedimientoResiduoSistema
                if (doc.politicaId) {
                    doc.politicaId = doc.politicaId.toString();
                } else {
                    doc.politicaId = '';
                }
                $scope.showObservaciones = false;
                $scope.sizeModal = "";
                $scope.colLista = "";
            }
            if (response.data.procedimientoBajaBienes) {
                const url = `${$rootScope.APIURL}/api/documentos/politicas-lista/${$rootScope.servicioEvId}`;
                httpGet(url, null, setPoliticasLista);
                doc = response.data.procedimientoBajaBienes
                if (doc.politicaId) {
                    doc.politicaId = doc.politicaId.toString();
                } else {
                    doc.politicaId = '';
                }
                $scope.showObservaciones = false;
                $scope.sizeModal = "";
                $scope.colLista = "";
            }
            if(response.data.procedimientoCompraSustentable) {
                const url = `${$rootScope.APIURL}/api/documentos/politicas-lista/${$rootScope.servicioEvId}`;
                httpGet(url, null, setPoliticasLista);
                doc = response.data.procedimientoCompraSustentable
                if (doc.politicaId) {
                    doc.politicaId = doc.politicaId.toString();
                } else {
                    doc.politicaId = '';
                }
                $scope.showObservaciones = false;
                $scope.sizeModal = "";
                $scope.colLista = "";
            }
            if (response.data.procedimientoReutilizacionPapel) {
                doc = response.data.procedimientoReutilizacionPapel
                $scope.showObservaciones = false;
                $scope.sizeModal = "";
                $scope.colLista = "";
            }
            if (response.data.charla) {
                doc = response.data.charla;
                $scope.showObservaciones = false;
                $scope.sizeModal = "";
                $scope.colLista = "";
            }
            if (response.data.capacitadoMP) {
                doc = response.data.capacitadoMP;
                $scope.showObservaciones = false;
                $scope.sizeModal = "";
                $scope.colLista = "";
            }
            if (response.data.pacE3) {
                doc = response.data.pacE3;
                $scope.showObservaciones = false;
                $scope.sizeModal = "";
                $scope.colLista = "";
                if (doc.adjuntoRespaldoUrlCompromiso) {
                    const adjuntoRespaldoCompromiso = doc.adjuntoRespaldoUrlCompromiso;
                    const adjuntoUrlArrRespaldoCompromiso = adjuntoRespaldoCompromiso.split('\\');
                    const fileNameRespaldoCompromiso = adjuntoUrlArrRespaldoCompromiso[adjuntoUrlArrRespaldoCompromiso.length - 1];
                    $scope.linkToDownloadRespaldoCompromiso = `${$rootScope.FILESERVER}${$rootScope.ESTADO_VERDE_FOLDER}/${$rootScope.DOCUMENTOS_FOLDER}/${fileNameRespaldoCompromiso}`
                }
            }
            if (response.data.resolucionApruebaPlanDTO) {
                doc = response.data.resolucionApruebaPlanDTO;
                $scope.sizeModal = "";
                $scope.colLista = "";
            }
            if (response.data.listadoColaborador) {
                doc = response.data.listadoColaborador;
                $scope.showObservaciones = false;
                $scope.sizeModal = "";
                $scope.colLista = "";
            }
            if (response.data.compraSustentable) {
                doc = response.data.compraSustentable;
                $scope.sizeModal = "";
                $scope.colLista = "";
                if (doc.adjuntoUrlCompraSustentableAnt) {
                    const adjuntoUrlCompraSustentableAnt = doc.adjuntoUrlCompraSustentableAnt;
                    const adjuntoUrlArrCompraSustentableAnt = adjuntoUrlCompraSustentableAnt.split('\\');
                    const fileNameCompraSustentableAnt = adjuntoUrlArrCompraSustentableAnt[adjuntoUrlArrCompraSustentableAnt.length - 1];
                    $scope.linkToDownloadCompraSustentableAnt = `${$rootScope.FILESERVER}${$rootScope.ESTADO_VERDE_FOLDER}/${$rootScope.DOCUMENTOS_FOLDER}/${fileNameCompraSustentableAnt}`
                }
                if (doc.adjuntoUrlCompraFuera) {
                    const adjuntoUrlCompraFuera = doc.adjuntoUrlCompraFuera;
                    const adjuntoUrlArrCompraFuera = adjuntoUrlCompraFuera.split('\\');
                    const fileNameCompraFuera = adjuntoUrlArrCompraFuera[adjuntoUrlArrCompraFuera.length - 1];
                    $scope.linkToDownloadCompraFuera = `${$rootScope.FILESERVER}${$rootScope.ESTADO_VERDE_FOLDER}/${$rootScope.DOCUMENTOS_FOLDER}/${fileNameCompraFuera}`
                }
            }
            doc.fecha = new Date(doc.fecha);
            doc.tipoDocumentoId = doc.tipoDocumentoId.toString();
            $scope.documento = doc;
            if (doc.adjuntoUrl) {
                const adjuntoUrl = doc.adjuntoUrl;
                const adjuntoUrlArr = adjuntoUrl.split('\\');
                const fileName = adjuntoUrlArr[adjuntoUrlArr.length - 1];
                $scope.linkToDownload = `${$rootScope.FILESERVER}${$rootScope.ESTADO_VERDE_FOLDER}/${$rootScope.DOCUMENTOS_FOLDER}/${fileName}`
            }
           
            //if (response.data.charla) {
            //    const adjuntoUrlInvitacion = doc.adjuntoUrlInvitacion;
            //    const adjuntoUrlArrInvitacion = adjuntoUrlInvitacion.split('\\');
            //    const fileNameInvitacion = adjuntoUrlArrInvitacion[adjuntoUrlArrInvitacion.length - 1];
            //    $scope.linkToDownloadInvitacion = `${$rootScope.FILESERVER}${$rootScope.ESTADO_VERDE_FOLDER}/${$rootScope.DOCUMENTOS_FOLDER}/${fileNameInvitacion}`;
            //}
            $scope.modalTitle = "Editar Documento";
            showFields();
            showFieds2();
            $scope.showModal();
            return;
        }

        $scope.closeModal = function () {
            $scope.showPuestaEnMarchaCEV = false;
            $scope.showDefinePropuestaConcientizados = false;
            $scope.showAlertDisponibilidad = false;
            $scope.documento = {};
            $scope.sizeModal = "";
            $scope.colLista = "";
            $scope.showListaIntegrantes = false;
            $scope.listaIntegrantes = [];
            $scope.integrante = { nombre: "", rol: "", email: "", marca: false };
            angular.element("input[type='file']").val(null);
            $scope.validationDocPACE3 = false;
            $scope.documentoForm.$setPristine();
            $scope.documentoForm.$setUntouched()
            $("#modal-form").modal("hide");
            
        }

        $scope.testValid = function () {
            console.log($scope.documentoForm.$error);
        }

        $scope.changeTipoDocumento = function () {
            $scope.showAlertDisponibilidad = false
            showFields();
            showFieds2();
            if ($scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_PAC_E3) {
                if (!$scope.documento.adjuntoPath && !$scope.documento.adjuntoRespaldoPathCompromiso) {
                    $scope.validationDocPACE3 = true;
                } else {
                    $scope.validationDocPACE3 = false;
                }
            } else {
                $scope.validationDocPACE3 = false;
            }
            //if ($scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_ACTA) {
            //    $scope.fechaText = "Fecha Resolución Comité:";
            //} else {
            //    $scope.fechaText = "Fecha:";
            //}

            if ($scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_REUNION) {
                setInitValuesReuniones();
            }

            if ($scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_LISTA_INTEGRANTES) {
                $scope.sizeModal = "modal-lg";
                $scope.colLista = "col-lg-6";
            } else {
                $scope.sizeModal = "";
                $scope.colLista = "";
            }
            if ($scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_REUNION ||
                $scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_LISTA_INTEGRANTES
                ) {
                const url = `${$rootScope.APIURL}/api/documentos/actas-comite-lista/${$rootScope.servicioEvId}`;
                httpGet(url, null, setActasLista);
            }
            if ($scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_POLITICA) {
                setInitValuesPolicy();
            }
            if ($scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_DIFUSION ||
                $scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_PROC_PAPEL ||
                $scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_PROC_RESIDUO ||
                $scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_PROC_RESIDUO_SISTEMA ||
                $scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_PROC_BAJA_INMUEBLES ||
                $scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_PROC_COMPRA_SUSTENTABLE

            ) {
                const url = `${$rootScope.APIURL}/api/documentos/politicas-lista/${$rootScope.servicioEvId}`;
                httpGet(url, null, setPoliticasLista);
                if ($scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_PROC_PAPEL) {
                    setInitValuesProcPapel();
                }
                if ($scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_PROC_RESIDUO) {
                    setInitValuesProcResiduos();
                }
                if ($scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_PROC_RESIDUO_SISTEMA) {
                    setInitValuesProcResiduoSistema();
                }
                if ($scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_PROC_BAJA_INMUEBLES) {
                    setInitValuesProcBajaBienes();
                }
                if ($scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_PROC_COMPRA_SUSTENTABLE) {
                    setInitValuesProcCompraSustentable();
                }
            }
        }

        function setInitValuesProcCompraSustentable() {
            if (!$scope.documento.prodBajoImpactoAmbiental) {
                $scope.documento.prodBajoImpactoAmbiental = false;
            }
            if (!$scope.documento.procesoGestionSustentable) {
                $scope.documento.procesoGestionSustentable = false;
            }
            if (!$scope.documento.estandaresSustentabilidad) {
                $scope.documento.estandaresSustentabilidad = false;
            }
        }

        function setInitValuesReuniones() {
            if (!$scope.documento.revisionPoliticaAmbiental) {
                $scope.documento.revisionPoliticaAmbiental = false;
            }
            if (!$scope.documento.revisionProcBienesMuebles) {
                $scope.documento.revisionProcBienesMuebles = false;
            }
        }

        function setInitValuesProcBajaBienes() {
            if (!$scope.documento.reduccion) {
                $scope.documento.reduccion = false;
            }
            if (!$scope.documento.reciclaje) {
                $scope.documento.reciclaje = false;
            }
            if (!$scope.documento.reutilizacion) {
                $scope.documento.reutilizacion = false;
            }
            if (!$scope.documento.bajaBienesFormalizado) {
                $scope.documento.bajaBienesFormalizado = false;
            }
            if (!$scope.documento.donacion) {
                $scope.documento.donacion = false;
            }
            if (!$scope.documento.destruccion) {
                $scope.documento.destruccion = false;
            }
            if (!$scope.documento.reparacion) {
                $scope.documento.reparacion = false;
            }
        }

        function setInitValuesProcResiduoSistema() {
            if (!$scope.documento.reduccion) {
                $scope.documento.reduccion = false;
            }
            if (!$scope.documento.reutilizacion) {
                $scope.documento.reutilizacion = false;
            }
            if (!$scope.documento.reciclaje) {
                $scope.documento.reciclaje = false;
            }
        }

        function setInitValuesProcResiduos() {
            if (!$scope.documento.entregaCertificadoRegistroRetiro) {
                $scope.documento.entregaCertificadoRegistroRetiro = false;
            }
            if (!$scope.documento.entregaCertificadoRegistroDisposicion) {
                $scope.documento.entregaCertificadoRegistroDisposicion = false;
            }
            if (!$scope.documento.entregaCertificadoRegistroCantidades) {
                $scope.documento.entregaCertificadoRegistroCantidades = false;
            }
        }

        function setInitValuesProcPapel() {
            if (!$scope.documento.cobertura) {
                $scope.documento.cobertura = "1";
            }
            if (!$scope.documento.implementacion) {
                $scope.documento.implementacion = false;
            }
            if (!$scope.documento.reduccion) {
                $scope.documento.reduccion = false;
            }
            if (!$scope.documento.reutilizacion) {
                $scope.documento.reutilizacion = false;
            }
            if (!$scope.documento.impresionDobleCara) {
                $scope.documento.impresionDobleCara = false;
            }

            if (!$scope.documento.bajoConsumoTinta) {
                $scope.documento.bajoConsumoTinta = false;
            }

            if (!$scope.documento.formatoDocumento) {
                $scope.documento.formatoDocumento = false;
            }
        }

        function setInitValuesPolicy() {
            if (!$scope.documento.cobertura) {
                $scope.documento.cobertura = "1";
            }
            if (!$scope.documento.reduccion) {
                $scope.documento.reduccion = false;
            }
            if (!$scope.documento.reciclaje) {
                $scope.documento.reciclaje = false;
            }
            if (!$scope.documento.eficienciaEnergetica) {
                $scope.documento.eficienciaEnergetica = false;
            }
            if (!$scope.documento.reutilizacion) {
                $scope.documento.reutilizacion = false;
            }
            if (!$scope.documento.gestionPapel) {
                $scope.documento.gestionPapel = false;
            }
            if (!$scope.documento.comprasSustentables) {
                $scope.documento.comprasSustentables = false;
            }
            if (!$scope.documento.prodBajoImpactoAmbiental) {
                $scope.documento.prodBajoImpactoAmbiental = false;
            }
            if (!$scope.documento.procesoGestionSustentable) {
                $scope.documento.procesoGestionSustentable = false;
            }
            if (!$scope.documento.estandaresSustentabilidad) {
                $scope.documento.estandaresSustentabilidad = false;
            }
        }

        function setActasLista(response) {
            $scope.actasLista = response.data.actas
        }
        function setPoliticasLista(response) {
            $scope.politicasLista = response.data.politicaLista
        }

        $scope.submitForm = function () {
            var payload = new FormData();
            let url;
            payload.append("tipoDocumentoId", $scope.documento.tipoDocumentoId);
            var datestr = (new Date($scope.documento.fecha)).toUTCString();
            if ($scope.documento.tipoDocumentoId != TIPO_DOCUMENTO_CHARLAS) {
                payload.append("fecha", datestr);
            }
            
            payload.append("etapaSEV_docs", $rootScope.etapa);
            if ($scope.documento.adjunto) {
                payload.append("adjunto", $scope.documento.adjunto);
                payload.append("adjuntoPath", $scope.documento.adjuntoPath);
            }
            if ($scope.documento.adjuntoRespaldoParticipativo) {
                payload.append("adjuntoRespaldoParticipativo", $scope.documento.adjuntoRespaldoParticipativo);
                payload.append("adjuntoRespaldoPathParticipativo", $scope.documento.adjuntoRespaldoPathParticipativo);
            }
            if ($scope.documento.adjuntoRespaldoCompromiso) {
                payload.append("adjuntoRespaldoCompromiso", $scope.documento.adjuntoRespaldoCompromiso);
                payload.append("adjuntoRespaldoPathCompromiso", $scope.documento.adjuntoRespaldoPathCompromiso);
            }
            if ($scope.documento.adjuntoRespaldo) {
                payload.append("adjuntoRespaldo", $scope.documento.adjuntoRespaldo);
                payload.append("adjuntoRespaldoPath", $scope.documento.adjuntoRespaldoPath);
            }

            if ($scope.documento.adjuntoCompraSustentableAnt) {
                payload.append("adjuntoCompraSustentableAnt", $scope.documento.adjuntoCompraSustentableAnt);
                payload.append("adjuntoPathCompraSustentableAnt", $scope.documento.adjuntoPathCompraSustentableAnt);
            }
            if ($scope.documento.adjuntoCompraFuera) {
                payload.append("adjuntoCompraFuera", $scope.documento.adjuntoCompraFuera);
                payload.append("adjuntoPathCompraFuera", $scope.documento.adjuntoPathCompraFuera);
            }

            //payload.append("adjuntoRespaldo", $scope.adjuntoRespaldo);
            payload.append("servicioId", $rootScope.servicioEvId);
            if ($scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_ACTA) {
                payload.append("nresolucion", $scope.documento.nresolucion);
                if ($scope.documento.observaciones) {
                    payload.append("observaciones", $scope.documento.observaciones);
                }
                if ($scope.editMode) {
                    url = `${$rootScope.APIURL}/api/documentos/acta-comite/${$scope.documento.id}`
                } else {
                    url = `${$rootScope.APIURL}/api/documentos/acta-comite`
                }
            }
            if ($scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_REUNION) {
                if ($scope.documento.observaciones) {
                    payload.append("observaciones", $scope.documento.observaciones);
                }
                payload.append("actaComiteId", $scope.documento.actaComiteId);
                payload.append("titulo", $scope.documento.titulo);
                payload.append("definePolitica", $scope.documento.definePolitica);
                if ($scope.documento.apruebaAlcanceGradualSEV) {
                    payload.append("apruebaAlcanceGradualSEV", $scope.documento.apruebaAlcanceGradualSEV);
                }
                if ($scope.documento.revisionPoliticaAmbiental) {
                    payload.append("revisionPoliticaAmbiental", $scope.documento.revisionPoliticaAmbiental);
                }
                if ($scope.documento.detActDeConcientizacion) {
                    payload.append("detActDeConcientizacion", $scope.documento.detActDeConcientizacion);
                }
                if ($scope.documento.revisionProcBienesMuebles) {
                    payload.append("revisionProcBienesMuebles", $scope.documento.revisionProcBienesMuebles);
                }
                if ($scope.documento.apruebaDiagnostico) {
                    payload.append("apruebaDiagnostico", $scope.documento.apruebaDiagnostico);
                }
                if ($scope.documento.puestaEnMarchaCEV) {
                    payload.append("puestaEnMarchaCEV", $scope.documento.puestaEnMarchaCEV);
                }
                if ($scope.documento.definePropuestaConcientizados) {
                    payload.append("definePropuestaConcientizados", $scope.documento.definePropuestaConcientizados);
                }
                
                if ($scope.editMode) {
                    url = `${$rootScope.APIURL}/api/documentos/reunion-comite/${$scope.documento.id}`
                } else {
                    url = `${$rootScope.APIURL}/api/documentos/reunion-comite`
                }
            }

            if ($scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_LISTA_INTEGRANTES) {
                payload.append("titulo", $scope.documento.titulo);
                for (var i = 0; i < $scope.listaIntegrantes.length; i++) {
                    var integrante = $scope.listaIntegrantes[i];
                    payload.append(`Integrantes[${i}].nombre`, integrante.nombre);
                    payload.append(`Integrantes[${i}].areaInst`, integrante.areaInst);
                    payload.append(`Integrantes[${i}].rol`, integrante.rol);
                    payload.append(`Integrantes[${i}].email`, integrante.email);
                    payload.append(`Integrantes[${i}].marca`, integrante.marca);
                }

                
                payload.append("actaComiteId", $scope.documento.actaComiteId);
                if ($scope.editMode) {
                    url = `${$rootScope.APIURL}/api/documentos/lista-integrantes-comite/${$scope.documento.id}`
                } else {
                    url = `${$rootScope.APIURL}/api/documentos/lista-integrantes-comite`
                }
            }
            if ($scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_POLITICA) {
                payload.append("nResolucionPolitica", $scope.documento.nResolucionPolitica);
                payload.append("cobertura", $scope.documento.cobertura);
                payload.append("reduccion", $scope.documento.reduccion);
                payload.append("reciclaje", $scope.documento.reciclaje);
                payload.append("eficienciaEnergetica", $scope.documento.eficienciaEnergetica);
                payload.append("reutilizacion", $scope.documento.reutilizacion);
                payload.append("gestionPapel", $scope.documento.gestionPapel);
                payload.append("comprasSustentables", $scope.documento.comprasSustentables);
                payload.append("prodBajoImpactoAmbiental", $scope.documento.prodBajoImpactoAmbiental);
                payload.append("procesoGestionSustentable", $scope.documento.procesoGestionSustentable);
                payload.append("estandaresSustentabilidad", $scope.documento.estandaresSustentabilidad);
                
                if ($scope.documento.otras) {
                    payload.append("otras", $scope.documento.otras);
                }
                if ($scope.etapa == 2) {
                    if(!$scope.documento.consultaPersonal){
                        payload.append("consultaPersonal", false);    
                    }else{
                        payload.append("consultaPersonal", $scope.documento.consultaPersonal);
                    }
                    
                    if ($scope.documento.e1O1RT2) {
                        payload.append("E1O1RT2", $scope.documento.e1O1RT2);
                    }
                    if ($scope.documento.definicionesEstrategicas) {
                        payload.append("definicionesEstrategicas", $scope.documento.definicionesEstrategicas);
                    }
                    if ($scope.documento.consultiva) {
                        payload.append("consultiva", $scope.documento.consultiva);
                    }
                }
                if ($scope.documento.elaboraPolitica) {
                    payload.append("elaboraPolitica", $scope.documento.elaboraPolitica);
                }
                if ($scope.documento.actualizaPolitica) {
                    payload.append("actualizaPolitica", $scope.documento.actualizaPolitica);
                }
                if ($scope.documento.mantienePolitica) {
                    payload.append("mantienePolitica", $scope.documento.mantienePolitica);
                }
                

                if ($scope.editMode) {
                    url = `${$rootScope.APIURL}/api/documentos/politica/${$scope.documento.id}`
                } else {
                    url = `${$rootScope.APIURL}/api/documentos/politica`
                }
            }
            if ($scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_DIFUSION) {
                payload.append("politicaId", $scope.documento.politicaId);
                payload.append("titulo", $scope.documento.titulo);
                if ($scope.editMode) {
                    url = `${$rootScope.APIURL}/api/documentos/difusion/${$scope.documento.id}`
                } else {
                    url = `${$rootScope.APIURL}/api/documentos/difusion`
                }
            }

            if ($scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_PROC_PAPEL) {
                if ($scope.documento.politicaId) {
                    payload.append("politicaId", $scope.documento.politicaId);
                }
                
                payload.append("titulo", $scope.documento.titulo);
                payload.append("cobertura", $scope.documento.cobertura);
                payload.append("implementacion", $scope.documento.implementacion);
                payload.append("reduccion", $scope.documento.reduccion);
                payload.append("reutilizacion", $scope.documento.reutilizacion);
                payload.append("impresionDobleCara", $scope.documento.impresionDobleCara);
                payload.append("bajoConsumoTinta", $scope.documento.bajoConsumoTinta);
                payload.append("nResolucionProcedimiento", $scope.documento.nResolucionProcedimiento);
                payload.append("formatoDocumento", $scope.documento.formatoDocumento);
                if ($scope.editMode) {
                    url = `${$rootScope.APIURL}/api/documentos/procedimiento-papel/${$scope.documento.id}`
                } else {
                    url = `${$rootScope.APIURL}/api/documentos/procedimiento-papel`
                }
            }

            if ($scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_PROC_RESIDUO) {
                if ($scope.documento.politicaId) {
                    payload.append("politicaId", $scope.documento.politicaId);
                }
                payload.append("titulo", $scope.documento.titulo);
                payload.append("entregaCertificadoRegistroRetiro", $scope.documento.entregaCertificadoRegistroRetiro);
                payload.append("entregaCertificadoRegistroDisposicion", $scope.documento.entregaCertificadoRegistroDisposicion);
                payload.append("entregaCertificadoRegistroCantidades", $scope.documento.entregaCertificadoRegistroCantidades);
                payload.append("nResolucionProcedimiento", $scope.documento.nResolucionProcedimiento);
                if ($scope.editMode) {
                    url = `${$rootScope.APIURL}/api/documentos/procedimiento-residuo/${$scope.documento.id}`
                } else {
                    url = `${$rootScope.APIURL}/api/documentos/procedimiento-residuo`
                }
            }

            if ($scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_PROC_RESIDUO_SISTEMA) {
                if ($scope.documento.politicaId) {
                    payload.append("politicaId", $scope.documento.politicaId);
                }
                payload.append("titulo", $scope.documento.titulo);
                payload.append("reduccion", $scope.documento.reduccion);
                payload.append("reutilizacion", $scope.documento.reutilizacion);
                payload.append("reciclaje", $scope.documento.reciclaje);
                payload.append("nResolucionProcedimiento", $scope.documento.nResolucionProcedimiento);
                if ($scope.editMode) {
                    url = `${$rootScope.APIURL}/api/documentos/procedimiento-residuo-sistema/${$scope.documento.id}`
                } else {
                    url = `${$rootScope.APIURL}/api/documentos/procedimiento-residuo-sistema`
                }
            }

            if ($scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_PROC_BAJA_INMUEBLES) {
                if ($scope.documento.politicaId) {
                    payload.append("politicaId", $scope.documento.politicaId);
                }
                payload.append("titulo", $scope.documento.titulo);
                payload.append("reduccion", $scope.documento.reduccion);
                payload.append("reciclaje", $scope.documento.reciclaje);
                payload.append("reutilizacion", $scope.documento.reutilizacion);
                payload.append("bajaBienesFormalizado", $scope.documento.bajaBienesFormalizado);
                payload.append("nResolucionProcedimiento", $scope.documento.nResolucionProcedimiento);
                payload.append("donacion", $scope.documento.donacion);
                payload.append("destruccion", $scope.documento.destruccion);
                payload.append("reparacion", $scope.documento.reparacion);
                if ($scope.editMode) {
                    url = `${$rootScope.APIURL}/api/documentos/procedimiento-baja-bienes/${$scope.documento.id}`
                } else {
                    url = `${$rootScope.APIURL}/api/documentos/procedimiento-baja-bienes`
                }
            }

            if ($scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_PROC_COMPRA_SUSTENTABLE) {
                if ($scope.documento.politicaId) {
                    payload.append("politicaId", $scope.documento.politicaId);
                }
                payload.append("titulo", $scope.documento.titulo);
                payload.append("prodBajoImpactoAmbiental", $scope.documento.prodBajoImpactoAmbiental);
                payload.append("procesoGestionSustentable", $scope.documento.procesoGestionSustentable);
                payload.append("estandaresSustentabilidad", $scope.documento.estandaresSustentabilidad);
                payload.append("nResolucionProcedimiento", $scope.documento.nResolucionProcedimiento);
                if ($scope.editMode) {
                    url = `${$rootScope.APIURL}/api/documentos/procedimiento-compra-sustentable/${$scope.documento.id}`
                } else {
                    url = `${$rootScope.APIURL}/api/documentos/procedimiento-compra-sustentable`
                }
            }
            if ($scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_CHARLAS) {
                payload.append("titulo", $scope.documento.titulo);
                //payload.append("nParticipantes", $scope.documento.nParticipantes);
                //payload.append("adjuntoInvitacion", $scope.adjuntoInvitacion);
                //payload.append("materia", $scope.documento.materia);
                if ($scope.editMode) {
                    url = `${$rootScope.APIURL}/api/documentos/charlas/${$scope.documento.id}`
                } else {
                    url = `${$rootScope.APIURL}/api/documentos/charlas`
                }
                if ($scope.documento.observaciones) {
                    payload.append("observaciones", $scope.documento.observaciones);
                }
                if ($scope.documento.gestionEnergia) {
                    payload.append("gestionEnergia", $scope.documento.gestionEnergia);
                }
                if ($scope.documento.gestionVehiculosTs) {
                    payload.append("gestionVehiculosTs", $scope.documento.gestionVehiculosTs);
                }
                if ($scope.documento.trasladoSustentable) {
                    payload.append("trasladoSustentable", $scope.documento.trasladoSustentable);
                }
                if ($scope.documento.gestionAgua) {
                    payload.append("gestionAgua", $scope.documento.gestionAgua);
                }
                if ($scope.documento.materiaGestionPapel) {
                    payload.append("materiaGestionPapel", $scope.documento.materiaGestionPapel);
                }
                if ($scope.documento.gestionResiduosEc) {
                    payload.append("gestionResiduosEc", $scope.documento.gestionResiduosEc);
                }
                if ($scope.documento.gestionComprasS) {
                    payload.append("gestionComprasS", $scope.documento.gestionComprasS);
                }
                if ($scope.documento.gestionBajaBs) {
                    payload.append("gestionBajaBs", $scope.documento.gestionBajaBs);
                }
                if ($scope.documento.huellaC) {
                    payload.append("huellaC", $scope.documento.huellaC);
                }
                if ($scope.documento.cambioClimatico) {
                    payload.append("cambioClimatico", $scope.documento.cambioClimatico);
                }
                if ($scope.documento.otraMateria) {
                    payload.append("otraMateria", $scope.documento.otraMateria);
                }
            }

            if ($scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_LISTADO_COLABORADORES) {
                if ($scope.documento.observaciones) {
                    payload.append("observaciones", $scope.documento.observaciones);
                }
                if ($scope.documento.totalColaboradoresConcientizados) {
                    payload.append("totalColaboradoresConcientizados", $scope.documento.totalColaboradoresConcientizados);
                }
                if ($scope.documento.totalColaboradoresCapacitados) {
                    payload.append("totalColaboradoresCapacitados", $scope.documento.totalColaboradoresCapacitados);
                }
                if ($scope.documento.funcionariosDesignados) {
                    payload.append("funcionariosDesignados", $scope.documento.funcionariosDesignados);
                }
               
                if ($scope.documento.porcentajeConcientizadosEtapa2) {
                    payload.append("porcentajeConcientizadosEtapa2", $scope.documento.porcentajeConcientizadosEtapa2);
                }
                if ($scope.documento.propuestaPorcentaje) {
                    payload.append("propuestaPorcentaje", $scope.documento.propuestaPorcentaje);
                }
                if ($scope.documento.actividadesCI) {
                    payload.append("actividadesCI", $scope.documento.actividadesCI);
                }
                if ($scope.documento.propuestaTemasCI) {
                    payload.append("propuestaTemasCI", $scope.documento.propuestaTemasCI);
                }
                if ($scope.editMode) {
                    url = `${$rootScope.APIURL}/api/documentos/listado-colaboradores/${$scope.documento.id}`
                } else {
                    url = `${$rootScope.APIURL}/api/documentos/listado-colaboradores`
                }
            }
            if ($scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_CAPACITADOS_MP) {
                if ($scope.documento.observaciones) {
                    payload.append("observaciones", $scope.documento.observaciones);
                }
                if ($scope.documento.totalColaboradoresCapacitados) {
                    payload.append("totalColaboradoresCapacitados", $scope.documento.totalColaboradoresCapacitados);
                }
                if ($scope.documento.funcionariosDesignados) {
                    payload.append("funcionariosDesignados", $scope.documento.funcionariosDesignados);
                }
                if ($scope.documento.funcionariosDesignadosNum) {
                    payload.append("funcionariosDesignadosNum", $scope.documento.funcionariosDesignadosNum);
                }
                if ($scope.documento.porcentajeCapacitadosEtapa2) {
                    payload.append("porcentajeCapacitadosEtapa2", $scope.documento.porcentajeCapacitadosEtapa2);
                }
                if ($scope.editMode) {
                    url = `${$rootScope.APIURL}/api/documentos/capacitados-mp/${$scope.documento.id}`
                } else {
                    url = `${$rootScope.APIURL}/api/documentos/capacitados-mp`
                }
            }


            if ($scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_PROC_REUTILIZACION_PAPEL) {
                payload.append("titulo", $scope.documento.titulo);
                if ($scope.editMode) {
                    url = `${$rootScope.APIURL}/api/documentos/procedimiento-reutilizacion-papel/${$scope.documento.id}`
                } else {
                    url = `${$rootScope.APIURL}/api/documentos/procedimiento-reutilizacion-papel`
                }
            }

            if ($scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_GESTION_COMPRA_SUSTENTABLE) {
                if ($scope.documento.observaciones) {
                    payload.append("observaciones", $scope.documento.observaciones);
                }
                if ($scope.documento.nComprasCriterios) {
                    payload.append("nComprasCriterios", $scope.documento.nComprasCriterios);
                }
                // if ($scope.documento.nComprasCriterios2 || $scope.documento.nComprasCriterios2>=0) {
                //     payload.append("nComprasCriterios2", $scope.documento.nComprasCriterios2);
                // }
                if ($scope.documento.nComprasCriteriosFuera) {
                    payload.append("nComprasCriteriosFuera", $scope.documento.nComprasCriteriosFuera);
                }
                if ($scope.documento.nComprasRubros) {
                    payload.append("nComprasRubros", $scope.documento.nComprasRubros);
                }
                // if ($scope.documento.nComprasRubros2 || $scope.documento.nComprasRubros2>=0) {
                //     payload.append("nComprasRubros2", $scope.documento.nComprasRubros2);
                // }
                if ($scope.documento.nComprasRubrosFuera) {
                    payload.append("nComprasRubrosFuera", $scope.documento.nComprasRubrosFuera);
                }
                if ($scope.editMode) {
                    url = `${$rootScope.APIURL}/api/documentos/gestion-compra-sustentable/${$scope.documento.id}`
                } else {
                    url = `${$rootScope.APIURL}/api/documentos/gestion-compra-sustentable`
                }
            }

            if ($scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_PAC_E3) {
                if ($scope.editMode) {
                    url = `${$rootScope.APIURL}/api/documentos/pac-e3/${$scope.documento.id}`
                } else {
                    url = `${$rootScope.APIURL}/api/documentos/pac-e3`
                }
            }

            if ($scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_INFORME_DA) {
                if ($scope.editMode) {
                    url = `${$rootScope.APIURL}/api/documentos/informe-da/${$scope.documento.id}`
                } else {
                    url = `${$rootScope.APIURL}/api/documentos/informe-da`
                }
            }
            if ($scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_RESOLUCION_APRUEBA_PLAN) {
                payload.append("nresolucion", $scope.documento.nresolucion);
                if ($scope.documento.observaciones) {
                    payload.append("observaciones", $scope.documento.observaciones);
                }
                if ($scope.editMode) {
                    url = `${$rootScope.APIURL}/api/documentos/resolucion-aprueba-plan/${$scope.documento.id}`
                } else {
                    url = `${$rootScope.APIURL}/api/documentos/resolucion-aprueba-plan`
                }
            }

            submitForm($scope.editMode, payload, url);
            
        }

        $scope.setPage = function (page) {
            $scope.page = page;
            LoadData(page, $rootScope.servicioEvId);
        }
        $scope.setPagePoliticas = function (page) {
            $scope.pagePoliticas = page;
            LoadDataPoliticas(page, $rootScope.servicioEvId);
        }
        $scope.setPagePga = function (page) {
            $scope.pagePga = page;
            LoadDataPga(page, $rootScope.servicioEvId);
        }
        $scope.setPageProcedimientos = function (page) {
            $scope.pageProcedimientos = page;
            LoadDataProcedimientos(page, $rootScope.servicioEvId);
        }
        $scope.setPageCharlas = function (page) {
            $scope.pageCharlas = page;
            LoadDataCharlas(page, $rootScope.servicioEvId);
        }

        $scope.setPageCapacitadosMP = function (page) {
            $scope.pageCapacitadosMP = page;
            LoadDataCharlas(page, $rootScope.servicioEvId);
        }
        $scope.setPagePacE3 = function (page) {
            $scope.pagePacE3 = page;
            LoadDataPacE3(page, $rootScope.servicioEvId);
        }

        $scope.nextPage = function () {
           
            if ($scope.page < $scope.arrayPages[$scope.arrayPages.length-1]) {
                $scope.page = $scope.page + 1;
                LoadData($scope.page, $rootScope.servicioEvId);
            }
        }
        $scope.nextPagePoliticas = function () {

            if ($scope.pagePoliticas < $scope.arrayPagesPoliticas[$scope.arrayPagesPoliticas.length - 1]) {
                $scope.pagePoliticas = $scope.pagePoliticas + 1;
                LoadDataPoliticas($scope.pagePoliticas, $rootScope.servicioEvId);
            }
        }
        $scope.nextPagePga = function () {

            if ($scope.pagePga < $scope.arrayPagesPga[$scope.arrayPagesPga.length - 1]) {
                $scope.pagePga = $scope.pagePga + 1;
                LoadDataPga($scope.pagePga, $rootScope.servicioEvId);
            }
        }
        $scope.nextPageProcedimientos = function () {

            if ($scope.pageProcedimientos < $scope.arrayPagesProcedimientos[$scope.arrayPagesProcedimientos.length - 1]) {
                $scope.pageProcedimientos = $scope.pageProcedimientos + 1;
                LoadDataProcedimientos($scope.pageProcedimientos, $rootScope.servicioEvId);
            }
        }
        $scope.nextPageCharlas = function () {

            if ($scope.pageCharlas < $scope.arrayPagesCharlas[$scope.arrayPagesCharlas.length - 1]) {
                $scope.pageCharlas = $scope.pageCharlas + 1;
                LoadDataCharlas($scope.pageCharlas, $rootScope.servicioEvId);
            }
        }
        $scope.nextPageCapacitadosMP = function () {

            if ($scope.pageCapacitadosMP < $scope.arrayPagesCapacitadosMP[$scope.arrayPagesCapacitadosMP.length - 1]) {
                $scope.pageCapacitadosMP = $scope.pageCapacitadosMP + 1;
                LoadDataCharlas($scope.pageCapacitadosMP, $rootScope.servicioEvId);
            }
        }

        $scope.nextPagePacE3 = function () {

            if ($scope.pagePacE3 < $scope.arrayPagesPacE3[$scope.arrayPagesPacE3.length - 1]) {
                $scope.pagePacE3 = $scope.pagePacE3 + 1;
                LoadDataPacE3($scope.pagePacE3, $rootScope.servicioEvId);
            }
        }


        $scope.prevPage = function () {

            if ($scope.page >1) {
                $scope.page = $scope.page - 1;
                LoadData($scope.page, $rootScope.servicioEvId);
            }
        }
        $scope.prevPagePoliticas = function () {

            if ($scope.pagePoliticas > 1) {
                $scope.pagePoliticas = $scope.pagePoliticas - 1;
                LoadDataPoliticas($scope.pagePoliticas, $rootScope.servicioEvId);
            }
        }
        $scope.prevPagePga = function () {

            if ($scope.pagePga > 1) {
                $scope.pagePga = $scope.pagePga - 1;
                LoadDataPga($scope.pagePga, $rootScope.servicioEvId);
            }
        }
        $scope.prevPageProcedimientos = function () {

            if ($scope.pageProcedimientos > 1) {
                $scope.pageProcedimientos = $scope.pageProcedimientos - 1;
                LoadDataProcedimientos($scope.pageProcedimientos, $rootScope.servicioEvId);
            }
        }
        $scope.prevPageCharlas = function () {

            if ($scope.pageCharlas > 1) {
                $scope.pageCharlas = $scope.pageCharlas - 1;
                LoadDataCharlas($scope.pageCharlas, $rootScope.servicioEvId);
            }
        }

        $scope.prevPageCapacitadosMP = function () {

            if ($scope.pageCapacitadosMP > 1) {
                $scope.pageCapacitadosMP = $scope.pageCapacitadosMP - 1;
                LoadDataCapacitadosMp($scope.pageCapacitadosMP, $rootScope.servicioEvId);
            }
        }

        $scope.prevPagePacE3 = function () {

            if ($scope.pagePacE3 > 1) {
                $scope.pagePacE3 = $scope.pagePacE3 - 1;
                LoadDataPacE3($scope.pagePacE3, $rootScope.servicioEvId);
            }
        }

        $scope.deleteDocumento = function(id){
            $ngConfirm({
                title: 'Eliminar Documento',
                content: '<p>¿Esta seguro de eliminar el documento?<p>',
                buttons: {
                    Ok: {
                        text: "Ok",
                        btnClass: "btn btn-danger",
                        action: function () {
                            $http({
                                method: 'DELETE',
                                url: `${$rootScope.APIURL}/api/documentos/${id}`
                            }).then(function () {
                                loadAllData(1, $rootScope.servicioEvId);
                            })

                        }
                    },
                    Cancelar: {
                        text: "Cancelar",
                        btnClass: "btn btn-default",
                        action: function () {

                        }
                    }

                }
            });
        }

        function showFieds2() {

            // Resetear estado del campo adjunto al inicio
            const adjuntoInput = document.querySelector("#adjunto");
            if (adjuntoInput) {
                adjuntoInput.disabled = false;
            }

            $scope.filetypes = "image/*,.pdf,.rar,.zip";
            $scope.showAlertDocumentoUnico = false;
            $scope.alertDocumentoUnicoText = "";
            $scope.showFecha = true;
            $scope.fechaText = "Fecha";
            $scope.fechaMIn = "";
            $scope.adjuntoText = "Adjuntar documento";
            $scope.showDocumentoRespaldo = false;
            $scope.showDocumentoRespaldoParticipativo = false;
            $scope.showDocumentoRespaldoCompromiso = false;
            $scope.showConcientizadosSEV = false;
            $scope.showCapacitadosMP = false;
            $scope.showTemplateCapacitadosMP = false;
            $scope.obsText = "Observaciones";
            $scope.showNComprasRubros = false;
            $scope.showNComprasRubros2 = false;
            $scope.showNComprasRubrosFuera = false;
            $scope.showNComprasCriterios = false;
            $scope.showNComprasCriterios2 = false;
            $scope.showNComprasCriteriosFuera = false;
            $scope.showComprasAnteriores = false;
            $scope.showComprasFuera = false;
            $scope.showTemplateCompraSustentable = false;
            $scope.showTemplateCompraSustentableAnt = false;
            $scope.showTemplateActasReunionesE1 = false;
            $scope.showTemplateCompraFuera = false;
            $scope.showCheckPolitica = false;
            $scope.showAlertPoliticaE2 = false;
            $scope.showChekPoliticaE2 = false;
            $scope.showPorcentajeConcientizadosEtapa2 = false;
            $scope.showPropuestaPorcentaje = false;
            $scope.showPorcentajeCapacitadosEtapa2 = false;
            $scope.showUploadDoc = true;
            $scope.showAlertGCSE1 = false;
            $scope.showPuestaEnMarchaCEV = false;
            $scope.showDefinePropuestaConcientizados = false;
            $scope.showCobertura = false;
            $scope.showConsulta = false;
            $scope.showdefinePolitica = false;
            $scope.showTemplatePoliticaE2 = false;
            $scope.showRevPoliticaAmbiental = false;
            $scope.showActividadesCI = false;
            $scope.showPropuestaTemasCI = false;
            $scope.esFechaMenorOIgualAlLimite=false
            $scope.showTemplateAIC=false;

            if ($scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_ACTA) {
                $scope.fechaText = "Fecha Resolución Comité";
                $scope.filetypes = ".pdf";
                $scope.showAlertDocumentoUnico = false;
                $scope.alertDocumentoUnicoText = "NOTA: se permite subir un solo archivo en formato PDF, este deberá corresponder a la Resolución y sus modificaciones si existen. CUIDADO: en el caso de subir un nuevo documento, este reemplazará al anterior.";                
            } else if ($scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_REUNION) {
                $scope.fechaText = "Fecha de reunión CEV";
                $scope.fechaMIn = "2023-11-01";
                if ($rootScope.etapa == 2) {
                    $scope.cb1Text = "Pone en marcha CEV para etapa 2";
                    $scope.cb2Text = "Política Ambiental Etapa 2";
                    $scope.cb3Text = "Coordina campaña de concientización y capacitación MP";
                    $scope.cb4Text = "Determina % concientizados SEV/CC y capacitados MP Etapa 2";
                    $scope.cb5Text = "Acuerda versión final del Plan de Gestión Ambiental";
                    $scope.showPuestaEnMarchaCEV = false;
                    $scope.showDefinePropuestaConcientizados = false;
                    $scope.showRevPoliticaAmbiental = true;
                    $scope.showdefinePolitica = true;
                    $scope.showTemplateActasReunionesE1 = true;
                } else {
                    $scope.showRevPoliticaAmbiental = true
                    $scope.showdefinePolitica = false;
                    $scope.cb1Text = "Aprueba alcance gradual SEV";
                    $scope.cb2Text = "Revisa existencia Política Ambiental";
                    $scope.cb3Text = "Coordina actividades de concientización";
                    $scope.cb4Text = "Revisa existencia procedimientos de bienes muebles";
                    $scope.cb5Text = "Aprueba Diagnostico de Gestión Ambiental";
                    $scope.showPuestaEnMarchaCEV = true;
                    $scope.showDefinePropuestaConcientizados = true;
                    $scope.showTemplateActasReunionesE1 = true;
                    $scope.filetypes = ".doc,.docx";
                }
            } else if ($scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_POLITICA) {
                
                $scope.fechaText = "Fecha Resolución Política Ambiental";
                $scope.adjuntoText = "Adjuntar copia Resolución. Política";
                $scope.showDocumentoRespaldo = false;
                if ($rootScope.etapa == 2) {
                    $scope.showCheckPolitica = true;
                    $scope.showAlertPoliticaE2 = true;
                    $scope.showDocumentoRespaldoParticipativo = true;
                    $scope.showChekPoliticaE2 = true;
                    $scope.showTemplatePoliticaE2 = true;
                    $scope.filetypes = ".docx,.doc";
                    $scope.showConsulta = true;
                } else {
                    $scope.showAlertDocumentoUnico = true;
                    $scope.alertDocumentoUnicoText = "NOTA: se permite subir un solo archivo, este deberá corresponder a la Política y sus modificaciones si existen. CUIDADO: en el caso de subir un nuevo documento, este reemplazará al anterior.";
                    $scope.showCheckPolitica = false;
                    $scope.showAlertPoliticaE2 = false;
                    $scope.showTemplatePoliticaE2 = false;
                    $scope.showConsulta = false;
                }
                
            } else if ($scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_CHARLAS) {
                // $scope.fechaText = "Fecha ejecución actividad interna de concientización";
                // $scope.fechaMIn = "2023-11-01";
                $scope.showTemplateAIC=true;
                $scope.showFecha= false;
                $scope.adjuntoText = "Respaldo actividad interna de concientización (lista de asistencia, presentaciones, contenidos, invitación, otros)";
                $scope.filetypes = ".pdf";
                $scope.showAlertDocumentoUnico = false;
                $scope.alertDocumentoUnicoText = "NOTA: Se permite subir un solo archivo PDF. CUIDADO: en el caso de subir un nuevo documento, este reemplazará al anterior.";
                $scope.obsText = "Breve descripción de la actividad (opcional)";
            } else if ($scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_LISTADO_COLABORADORES) {
                $scope.esFechaMenorOIgualAlLimite = (new Date() <= new Date(2025, 6, 19)); // 19 de Julio de 2025 (mes 6 es Julio)
                $scope.fechaText = "Fecha de carga";
                $scope.showConcientizadosSEV = true;
                $scope.showAlertDocumentoUnico = true;
                if($scope.esFechaMenorOIgualAlLimite){
                    $scope.alertDocumentoUnicoText = "NOTA: Hasta el 18 de Julio, fecha en que la Red entregará OTF a propuesta de porcentaje de concientización y temas de actividades internas, los campos de información \"Total de colaboradores concientizados SEV\" y el archivo a adjuntar, no serán obligatorios. Posterior a la fecha indicada estos campos de información deberán incluirse en este documento. Se permite subir un solo archivo, respetando formato EXCEL, facilitado por la Red. CUIDADO: en el caso de subir un nuevo documento, este reemplazará al anterior";
                }else{
                    $scope.alertDocumentoUnicoText = "NOTA: Se permite subir un solo archivo, respetando formato EXCEL, facilitado por la Red. CUIDADO: en el caso de subir un nuevo documento, este reemplazará al anterior";
                }
                
                $scope.filetypes = ".xlsx,.xls";
                $scope.showPorcentajeConcientizadosEtapa2 = true;
                $scope.showPropuestaPorcentaje = true;
                $scope.showActividadesCI = true;
                $scope.showPropuestaTemasCI = true;
                $scope.adjuntoText = "Adjuntar listado de colaboradores concientizados";
                if ($scope.etapa == 2) {
                    $scope.showTemplateAIC=false;
                    $scope.showUploadDoc = false;
                }
            } else if ($scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_CAPACITADOS_MP) {
                $scope.fechaText = "Fecha de carga";
                $scope.showCapacitadosMP = true;
                $scope.showTemplateCapacitadosMP = true;
                $scope.showAlertDocumentoUnico = true;
                $scope.alertDocumentoUnicoText = "NOTA: Se permite subir un solo archivo, respetando formato EXCEL, facilitado por la Red. CUIDADO: en el caso de subir un nuevo documento, este reemplazará al anterior";
                $scope.filetypes = ".xlsx,.xls";
                if ($scope.etapa == 2) {
                    $scope.showPorcentajeCapacitadosEtapa2 = true;
                }
            } else if ($scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_PROC_PAPEL) {
                $scope.fechaText = "Fecha Procedimiento formal de papel";
                $scope.showAlertDocumentoUnico = true;
                $scope.alertDocumentoUnicoText = "NOTA: Se permite subir un solo archivo PDF. CUIDADO: en el caso de subir un nuevo documento, este reemplazará al anterior.";
                $scope.filetypes = ".pdf";
            } else if ($scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_PROC_RESIDUO) {
                $scope.fechaText = "Fecha Proceso con certificado";
                $scope.showAlertDocumentoUnico = false;
                $scope.alertDocumentoUnicoText = "NOTA: Se permite subir un solo archivo PDF. CUIDADO: en el caso de subir un nuevo documento, este reemplazará al anterior.";
                $scope.filetypes = ".pdf";
            } else if ($scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_PROC_RESIDUO_SISTEMA) {
                $scope.fechaText = "Fecha Proceso sin certificado";
                $scope.showAlertDocumentoUnico = false;
                $scope.alertDocumentoUnicoText = "NOTA: Se permite subir un solo archivo PDF. CUIDADO: en el caso de subir un nuevo documento, este reemplazará al anterior.";
                $scope.filetypes = ".pdf";
            } else if ($scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_PROC_REUTILIZACION_PAPEL) {
                $scope.fechaText = "Fecha Procedimiento para Practicas de reutilización de papel";
                $scope.showAlertDocumentoUnico = true;
                $scope.alertDocumentoUnicoText = "NOTA: Se permite subir un solo archivo PDF. CUIDADO: en el caso de subir un nuevo documento, este reemplazará al anterior.";
                $scope.filetypes = ".pdf";
            } else if ($scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_PROC_BAJA_INMUEBLES) {
                $scope.fechaText = "Fecha Procedimiento para dar de baja los bienes muebles";
                $scope.showAlertDocumentoUnico = true;
                $scope.alertDocumentoUnicoText = "NOTA: Se permite subir un solo archivo PDF. CUIDADO: en el caso de subir un nuevo documento, este reemplazará al anterior.";
                $scope.filetypes = ".pdf";
            } else if ($scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_PROC_COMPRA_SUSTENTABLE) {
                $scope.fechaText = "Fecha Procedimiento para Compras Sustentables";
                $scope.showAlertDocumentoUnico = true;
                $scope.alertDocumentoUnicoText = "NOTA: Se permite subir un solo archivo PDF. CUIDADO: en el caso de subir un nuevo documento, este reemplazará al anterior.";
                $scope.filetypes = ".pdf";
            } else if ($scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_GESTION_COMPRA_SUSTENTABLE) {
                $scope.showUploadDoc = false;
                $scope.showTemplateCompraSustentable = false;
                $scope.fechaText = "Fecha de carga";
                $scope.showComprasAnteriores = true;
                $scope.showNComprasRubros = true;
                $scope.showNComprasRubrosFuera = true;
                $scope.showNComprasCriterios = true;                
                $scope.showNComprasCriteriosFuera = true;
                $scope.showAlertDocumentoUnico = false;
                
                // $scope.filetypes = ".xlsx,.xls";
                if ($scope.etapa == 1) {
                    $scope.showNComprasRubros2 = true;
                    $scope.showNComprasCriterios2 = true;
                    $scope.adjuntoText = "Adjuntar documento";
                    $scope.showAlertDocumentoUnico = true;
                    $scope.alertDocumentoUnicoText = "Nota: Se permite mantener una sola versión de este documento, por lo que en caso de crearse más de una versión solo se guardará la última.";
                    $scope.showTemplateCompraFuera = true;
                    $scope.showComprasFuera = true;
                    $scope.obsText = "Observaciones (justificar 0 en numerador o denominador del indicador)";
                    // $scope.showAlertGCSE1 = true;
                }
                
            } else if ($scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_PAC_E3) {
                $scope.fechaText = "Fecha de carga";
                $scope.adjuntoText = "Adjuntar documento con el Plan Anual de capacitación para Etapa 3";
                $scope.showDocumentoRespaldoCompromiso = true;
            } else if ($scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_INFORME_DA) {
                $scope.fechaText = "Fecha de elaboración del informe de análisis del diagnóstico de gestión ambiental​";
                $scope.alertDocumentoUnicoText = "Nota: Se permite subir un solo archivo PDF. CUIDADO: en el caso de subir un nuevo documento, este reemplazará al anterior.​ Recordatorio: Solo se considerarán informes cargados a partir del 1° de noviembre, luego del cierre del período t";
                
                // Validación de fecha para bloquear adjunto
                const fechaActual = new Date();
                const fechaLimite = new Date('2025-11-01');
                const adjuntoInput = document.querySelector("#adjunto");
                $scope.showAlertDisponibilidad = fechaActual < fechaLimite;
                $scope.showAlertDocumentoUnico =  fechaActual >= fechaLimite;
                adjuntoInput.disabled = $scope.showAlertDisponibilidad;
            } else if ($scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_RESOLUCION_APRUEBA_PLAN) {
                $scope.fechaText = "Fecha​";
            }
            else {
                // Para cualquier otro tipo de documento, asegurar que el campo adjunto esté habilitado
                const adjuntoInput = document.querySelector("#adjunto");
                if (adjuntoInput) {
                    adjuntoInput.disabled = false;
                }
            }
        }

        function showFields() {
            if ($scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_LISTADO_COLABORADORES) {
                $scope.showTotalColaboradoresConcientizados = true;
                $scope.showTotalColaboradoresCapacitados = true;
                $scope.showFuncionariosDesignados = true;
            } else {
                $scope.showTotalColaboradoresConcientizados = false;
                $scope.showTotalColaboradoresCapacitados = false;
                $scope.showFuncionariosDesignados = false;
            }

            if ($scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_PROC_BAJA_INMUEBLES) {
                $scope.showBajaBienesInmueble = true;
            } else {
                $scope.showBajaBienesInmueble = false;
            }

            if ($scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_LISTADO_COLABORADORES) {
                $scope.showTemplate = true;
            } else {
                $scope.showTemplate = false;
            };

            if ($scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_LISTA_INTEGRANTES) {
                $scope.showListaIntegrantes = true;
            } else {
                $scope.showListaIntegrantes = false;
            };

            if ($scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_REUNION) {
               
                $scope.showRevProcBienesMuebles = true
                $scope.showApruebaAlcanceGradualSEV = true
                $scope.showDetActDeConcientizacion = true
                $scope.showApruebaDiagnostico = true
            } else {
               
                $scope.showRevProcBienesMuebles = false
                $scope.showApruebaAlcanceGradualSEV = false
                $scope.showDetActDeConcientizacion = false
                $scope.showApruebaDiagnostico = false
            }

            if ($scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_REUNION ||
                $scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_DIFUSION ||
                $scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_PROC_PAPEL ||
                $scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_PROC_RESIDUO ||
                $scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_PROC_RESIDUO_SISTEMA ||
                $scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_PROC_BAJA_INMUEBLES ||
                $scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_PROC_COMPRA_SUSTENTABLE ||
                $scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_LISTA_INTEGRANTES
                || $scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_CHARLAS 
                || $scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_PROC_REUTILIZACION_PAPEL 
            ) {
                $scope.showTitulo = true;
            } else {
                $scope.showTitulo = false;
            };

            if ($scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_REUNION ||
                $scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_LISTA_INTEGRANTES
            ) {
                $scope.showActaComiteSel = true;
            } else {
                $scope.showActaComiteSel = false;
            };

            if ($scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_POLITICA
                || $scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_PROC_COMPRA_SUSTENTABLE
                || $scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_PROC_PAPEL
                || $scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_PROC_RESIDUO
                || $scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_PROC_RESIDUO_SISTEMA
            ) {
                $scope.showTemasConsideradosLabel = true;
            } else {
                $scope.showTemasConsideradosLabel = false;
            };

            if ($scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_POLITICA
            ) {
                $scope.showNResolucionPolitica = true;
            } else {
                $scope.showNResolucionPolitica = false;
            }

            if ($scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_ACTA ||
                $scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_REUNION ||
                $scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_LISTADO_COLABORADORES ||
                $scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_CAPACITADOS_MP ||
                $scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_CHARLAS ||
                $scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_GESTION_COMPRA_SUSTENTABLE || 
                $scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_RESOLUCION_APRUEBA_PLAN
                ) {
                $scope.showObservaciones = true;
            } else {
                $scope.showObservaciones = false;
            };
           
            // if ($scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_PROC_PAPEL) {
            //     $scope.showFormatoDocumento = true;
            // } else {
            //     $scope.showFormatoDocumento = false;
            // };
            if ($scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_PROC_PAPEL) {
                $scope.showImplementacion = true;
            } else {
                $scope.showImplementacion = false;
            };
            if ($scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_DIFUSION
               
            ) {
                $scope.showPolitica = true;
                $scope.reqPolitica = false;
                //if ($scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_DIFUSION) {
                //    $scope.reqPolitica = true;
                //} else {
                //    $scope.reqPolitica = false;
                //}
            } else {
                $scope.showPolitica = false;
                $scope.reqPolitica = false;
            };

            

            if ($scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_POLITICA ||
                $scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_PROC_PAPEL ||
                $scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_PROC_RESIDUO_SISTEMA
            ) {
                $scope.showReduccion = true;
                if ($scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_POLITICA) {
                    $scope.lblReduccion = "Gestión de residuos y economía circular (3R)";
                }
                if ($scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_PROC_PAPEL) {
                    $scope.lblReduccion = "Reducción de consumo papel";
                }
            } else {
                $scope.showReduccion = false;
                $scope.lblReduccion = "Reducción";
            };
            if (
                $scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_POLITICA ||
                $scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_PROC_RESIDUO_SISTEMA ||
                $scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_PROC_BAJA_INMUEBLES
            ) {
                $scope.showReciclaje = true;
                if ($scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_POLITICA) {
                    $scope.lblReciclaje ="Gestión hídrica"
                } else {
                    $scope.lblReciclaje = "Reciclaje"
                }


            } else {
                $scope.showReciclaje = false;
            };
            if ($scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_POLITICA) {
                $scope.showEficienciaEnergetica = true;
            } else {
                $scope.showEficienciaEnergetica = false;
            };
            if ($scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_POLITICA ||
                $scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_PROC_RESIDUO_SISTEMA ||
                $scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_PROC_BAJA_INMUEBLES
            ) {
                $scope.showReutilizacion = true;

                if ($scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_POLITICA) {
                    $scope.lblReutilizacion = 'Mecanismos de concientización ambiental'
                } else {
                    $scope.lblReutilizacion = 'Reutilización '
                }



            } else {
                $scope.showReutilizacion = false;
            };
            if ($scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_PROC_PAPEL) {
                $scope.showImpresionDobleCara = true;
                $scope.showBajoConsumoTinta = true;
            } else {
                $scope.showImpresionDobleCara = false;
                $scope.showBajoConsumoTinta = false;
            };
            if ($scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_POLITICA) {
                $scope.showGestionPapel = true;
            } else {
                $scope.showGestionPapel = false;
            };
            if ($scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_POLITICA) {
                $scope.showCompraSustentable = true;
            } else {
                $scope.showCompraSustentable = false;
            };
            if ( $scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_PROC_COMPRA_SUSTENTABLE
            ) {
                $scope.showProdBajoImpactoAmbiental = true;
            } else {
                $scope.showProdBajoImpactoAmbiental = false;
            };
            if (
                $scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_PROC_COMPRA_SUSTENTABLE
            ) {
                $scope.showProcesoGestionSustentable = true;
            } else {
                $scope.showProcesoGestionSustentable = false;
            };
            if (
                $scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_PROC_COMPRA_SUSTENTABLE
            ) {
                $scope.showEstandaresSustentabilidad = true;
            } else {
                $scope.showEstandaresSustentabilidad = false;
            };
            if ($scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_POLITICA
            ) {
                $scope.showOtras = true;
            } else {
                $scope.showOtras = false;
            };
            if ($scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_PROC_RESIDUO) {
                $scope.showEntregaCertificadoRegistroRetiro = true;
            } else {
                $scope.showEntregaCertificadoRegistroRetiro = false;
            };
            if ($scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_PROC_RESIDUO) {
                $scope.showEntregaCertificadoRegistroDisposicion = true;
            } else {
                $scope.showEntregaCertificadoRegistroDisposicion = false;
            };
            if ($scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_PROC_RESIDUO) {
                $scope.showEntregaCertificadoRegistroCantidades = true;
            } else {
                $scope.showEntregaCertificadoRegistroCantidades = false;
            };
            if ($scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_PROC_BAJA_INMUEBLES) {
                $scope.showBajaBienesFormalizado = true;
            } else {
                $scope.showBajaBienesFormalizado = false;
            };
            if ($scope.documento.tipoDocumentoId == TIPO_DOCUMENTO_CHARLAS) {
                //$scope.showNparticipantes = true;
                $scope.showMateria = true;
            } else {
                //$scope.showNparticipantes = false;
                $scope.showMateria = false;
            };
        };


        function loadAllData(page,servicioId) {

            LoadData(page, servicioId);
            LoadDataPoliticas(page, servicioId);
            LoadDataPga(page, servicioId);
            LoadDataProcedimientos(page, servicioId);
            LoadDataCharlas(page, servicioId, $scope.anioDoc);
            LoadDataCapacitadosMp(page, servicioId);
            LoadDataPacE3(page, servicioId);
            LoadDataCompraSustentalbes(page, servicioId);
        }

        function httpGet(url, id, processResponse) {
            let urlStr = id ? `${url}/${id}` : url;
            $http({
                method: 'GET',
                url: urlStr
            }).then(function (response) {
                if (response.data.ok) {
                    //Process the ok response
                    processResponse(response);
                } else {
                    $ngConfirm({
                        title: 'Error',
                        content: `<p>${response.data.msj}<p>`,
                        buttons: {
                            Ok: {
                                text: "Ok",
                                btnClass: "btn btn-default",
                                action: function () {
                                }
                            }
                        }
                    });
                }
            })
        }

        function submitForm(editMode, payload, url) {
            let method = editMode ? 'PUT' : 'POST'
            $http({
                method: method,
                url: url,
                data: payload,
                headers: { 'Content-Type': undefined },
                //transformRequest: angular.identity
            }).then(function (response) {
                if (response.data.ok) {
                    $scope.adjunto = null;
                    $scope.adjuntoRespaldo = null;
                    $scope.closeModal();
                    loadAllData($scope.page, $rootScope.servicioEvId);
                } else {
                    $ngConfirm({
                        title: 'Error',
                        content: `<p>${response.data.msj}<p>`,
                        buttons: {
                            Ok: {
                                text: "Ok",
                                btnClass: "btn btn-default",
                                action: function () {
                                }
                            }
                        }
                    });
                }
                
            }, function (error) {
                $ngConfirm({
                    title: 'Error',
                    content: `<p>Ocurrió un error, favor contactese con el administrador del sistema<p>`,
                    buttons: {
                        Ok: {
                            text: "Ok",
                            btnClass: "btn btn-default",
                            action: function () {
                            }
                        }
                    }
                });
            })
        }

        function LoadData(page,servicioId) {
            $scope.loadingTable = true;
            $http({
                method: 'GET',
                url: `${$rootScope.APIURL}/api/documentos/comite?page=${page}&recordsPerPage=10&servicioId=${servicioId}&anioDoc=${$scope.anioDocInt}&etapa=${$rootScope.etapa}`
            }).then(function (response) {
                $scope.loadingTable = false;
                if (response.data.ok) {
                    let totalRecords = parseInt(response.headers('totalrecords'));
                    let pages = Math.ceil(totalRecords / 10);
                    const arraypages = Array(pages).fill(1).map((n, i) => n + i)
                    $scope.page = page;
                    $scope.arrayPages = arraypages;
                    $scope.Comites = response.data.comites
                    $scope.politicaAmbientalCheck = response.data.noRegistraPoliticaAmbiental;
                    $scope.actividadInternaCheck = response.data.noRegistraActividadInterna;
                    $scope.reutilizacionPapelCheck = response.data.noRegistraReutilizacionPapel;
                    $scope.procFormalPapelCheck = response.data.noRegistraProcFormalPapel;
                    $scope.docResiduosCertificadosCheck = response.data.noRegistraDocResiduosCertificados;
                    $scope.docResiduosSistemasCheck = response.data.noRegistraDocResiduosSistemas;
                    $scope.procBajaBienesMueblesCheck = response.data.noRegistraProcBajaBienesMuebles;
                    $scope.procComprasSustentablesCheck = response.data.noRegistraProcComprasSustentables;
                    
                }
            })
        };

        function LoadDataPoliticas(pagePoliticas, servicioId) {
            $scope.loadingTablePolitica = true;
            $http({
                method: 'GET',
                url: `${$rootScope.APIURL}/api/documentos/politica?page=${pagePoliticas}&recordsPerPage=10&servicioId=${servicioId}&anioDoc=${$scope.anioDocInt}&etapa=${$rootScope.etapa}`
            }).then(function (response) {
                $scope.loadingTablePolitica = false;
                if (response.data.ok) {
                    let totalRecords = parseInt(response.headers('totalrecords'));
                    let pages = Math.ceil(totalRecords / 10);
                    const arraypages = Array(pages).fill(1).map((n, i) => n + i)
                    $scope.pagePoliticas = pagePoliticas;
                    $scope.arrayPagesPoliticas = arraypages;
                    $scope.politicas = response.data.politicaLista;
                }
            })
        };

        function LoadDataPga(pagePga, servicioId) {
            $scope.loadingTablePga = true;
            $http({
                method: 'GET',
                url: `${$rootScope.APIURL}/api/documentos/pga?page=${pagePga}&recordsPerPage=10&servicioId=${servicioId}&anioDoc=${$scope.anioDocInt}&etapa=${$rootScope.etapa}`
            }).then(function (response) {
                $scope.loadingTablePga = false;
                if (response.data.ok) {
                    let totalRecords = parseInt(response.headers('totalrecords'));
                    let pages = Math.ceil(totalRecords / 10);
                    const arraypages = Array(pages).fill(1).map((n, i) => n + i)
                    $scope.pagePga = pagePga;
                    $scope.arrayPagesPga = arraypages;
                    $scope.pga = response.data.pgaLista;
                }
            })
        };

        function LoadDataProcedimientos(pageProcedimientos, servicioId) {
            $scope.loadingTableProcedimientos = true;
            $http({
                method: 'GET',
                url: `${$rootScope.APIURL}/api/documentos/procedimientos?page=${pageProcedimientos}&recordsPerPage=10&servicioId=${servicioId}&anioDoc=${$scope.anioDocInt}`
            }).then(function (response) {
                $scope.loadingTableProcedimientos = false;
                if (response.data.ok) {
                    let totalRecords = parseInt(response.headers('totalrecords'));
                    let pages = Math.ceil(totalRecords / 10);
                    const arraypages = Array(pages).fill(1).map((n, i) => n + i)
                    $scope.pageProcedimientos = pageProcedimientos;
                    $scope.arrayPagesProcedimientos = arraypages;
                    $scope.procedimientos = response.data.procedimientos;
                }
            })
        };
        function LoadDataCharlas(pageCharlas, servicioId) {
            $scope.loadingTableCharlas = true;
            $http({
                method: 'GET',
                url: `${$rootScope.APIURL}/api/documentos/charlas?page=${pageCharlas}&recordsPerPage=10&servicioId=${servicioId}&anioDoc=${$scope.anioDocInt}&etapa=${$rootScope.etapa}`
            }).then(function (response) {
                $scope.loadingTableCharlas = false;
                if (response.data.ok) {
                    let totalRecords = parseInt(response.headers('totalrecords'));
                    let pages = Math.ceil(totalRecords / 10);
                    const arraypages = Array(pages).fill(1).map((n, i) => n + i)
                    $scope.pageCharlas = pageCharlas;
                    $scope.arrayPagesCharlas = arraypages;
                    $scope.charlas = response.data.charlas;
                }
            })
        };

        function LoadDataCapacitadosMp(pageCapacitadosMP, servicioId) {
            $scope.loadingTableCapacitadosMP = true;
            $http({
                method: 'GET',
                url: `${$rootScope.APIURL}/api/documentos/capacitados-mp?page=${pageCapacitadosMP}&recordsPerPage=10&servicioId=${servicioId}&anioDoc=${$scope.anioDocInt}&etapa=${$rootScope.etapa}`
            }).then(function (response) {
                $scope.loadingTableCapacitadosMP = false;
                if (response.data.ok) {
                    let totalRecords = parseInt(response.headers('totalrecords'));
                    let pages = Math.ceil(totalRecords / 10);
                    const arraypages = Array(pages).fill(1).map((n, i) => n + i)
                    $scope.pageCapacitadosMP = pageCapacitadosMP;
                    $scope.arrayPagesCapacitadosMP = arraypages;
                    $scope.capacitadosMP = response.data.capacitadosMP;
                }
            })
        };

        function LoadDataPacE3(pagePacE3, servicioId) {
            $scope.loadingTablePacE3 = true;
            $http({
                method: 'GET',
                url: `${$rootScope.APIURL}/api/documentos/pac-e3?page=${pagePacE3}&recordsPerPage=10&servicioId=${servicioId}&anioDoc=${$scope.anioDocInt}&etapa=${$rootScope.etapa}`
            }).then(function (response) {
                $scope.loadingTablePacE3 = false;
                if (response.data.ok) {
                    let totalRecords = parseInt(response.headers('totalrecords'));
                    let pages = Math.ceil(totalRecords / 10);
                    const arraypages = Array(pages).fill(1).map((n, i) => n + i)
                    $scope.pagePacE3 = pagePacE3;
                    $scope.arrayPagesPacE3 = arraypages;
                    $scope.pacE3 = response.data.pacE3s;
                }
            })
        };

        function LoadDataCompraSustentalbes(pageCompraSustentable, servicioId) {
            $scope.loadingTableCompraSustentables = true;
            $http({
                method: 'GET',
                url: `${$rootScope.APIURL}/api/documentos/compra-sustentables?page=${pageCompraSustentable}&recordsPerPage=10&servicioId=${servicioId}&anioDoc=${$scope.anioDocInt}`
            }).then(function (response) {
                $scope.loadingTableCompraSustentables = false;
                if (response.data.ok) {
                    let totalRecords = parseInt(response.headers('totalrecords'));
                    let pages = Math.ceil(totalRecords / 10);
                    const arraypages = Array(pages).fill(1).map((n, i) => n + i)
                    $scope.pageCompraSustentable = pageCompraSustentable;
                    $scope.arrayPagesCompraSustentable = arraypages;
                    $scope.comprasustentables = response.data.compraSustentables;
                }
            })
        };
    })
    .controller("selunidadDocumentosController", function ($scope, $rootScope, $http, $ngConfirm, $state) {
        $scope.page = 1;
        $scope.loadingTable = false;
        $scope.insitucionIdFilter = '';

        $http({
            method: 'GET',
            url: `/settings?sectionName=ApiConfiguration&paramName=apiGestionaEnergia`

        }).then(function (response) {
            $rootScope.APIURL = response.data;
            loadServicios($scope.page, $scope.insitucionIdFilter);
        });

        $scope.setPage = function (page) {
            $scope.page = page;
            loadServicios(page, $scope.insitucionIdFilter);
        }

        $scope.nextPage = function () {

            if ($scope.page < $scope.arrayPages[$scope.arrayPages.length - 1]) {
                $scope.page = $scope.page + 1;
                loadServicios($scope.page, $scope.insitucionIdFilter);
            }
        }

        $scope.prevPage = function () {

            if ($scope.page > 1) {
                $scope.page = $scope.page - 1;
                loadServicios($scope.page, $scope.insitucionIdFilter);
            }
        }

        $scope.setServicio = function (servicio) {
            $rootScope.servicioSelected = servicio;
            $state.go("adminDocumentos@home");
        }

        $scope.changeInstitucion = function (id) {
            $scope.insitucionIdFilter = id;
            loadServicios($scope.page, $scope.insitucionIdFilter);
        };

        function loadServicios(page,institucionId) {
            $scope.loadingTable = true;
            $http({
                method: 'GET',
                url: `${$rootScope.APIURL}/api/servicios/getByUserIdPagin?page=${page}&institucionId=${institucionId}`
            }).then(function (response) {
                if (response.data.ok) {
                    $scope.loadingTable = false;
                    let totalRecords = parseInt(response.headers('totalrecords'));
                    let pages = Math.ceil(totalRecords / 5);
                    const arraypages = Array(pages).fill(1).map((n, i) => n + i)
                    if (arraypages.length > 15) {
                        if ($scope.page > 1) {
                            arraypages.splice(0, $scope.page-1);
                        }
                        arraypages.splice(15, arraypages.length-15 );
                    }
                    $scope.page = page;
                    $scope.arrayPages = arraypages;
                    $scope.servicios = response.data.servicios;

                }
            })

        }
    })
    .directive('validFile', function () {
        return {
            require: 'ngModel',
            link: function (scope, el, attrs, ngModel) {
                el.bind('change', function () {
                    scope.$apply(function () {
                        ngModel.$setViewValue(el.val());
                        ngModel.$render();
                    })
                })
            }
        }

    })
    .directive('demoFileModel', function ($parse) {
        return {
            restrict: 'A', //the directive can be used as an attribute only

            /*
             link is a function that defines functionality of directive
             scope: scope associated with the element
             element: element on which this directive used
             attrs: key value pair of element attributes
             */
            link: function (scope, element, attrs) {
                var model = $parse(attrs.demoFileModel),
                    modelSetter = model.assign; //define a setter for demoFileModel

                //Bind change event on the element
                element.bind('change', function () {
                    //Call apply on scope, it checks for value changes and reflect them on UI
                    scope.$apply(function () {
                        //set the model value
                        modelSetter(scope, element[0].files[0]);
                    });
                });
            }
        };
    })
    .directive('adjuntoFileModel', function ($parse) {
        return {
            restrict: 'A', //the directive can be used as an attribute only

            /*
             link is a function that defines functionality of directive
             scope: scope associated with the element
             element: element on which this directive used
             attrs: key value pair of element attributes
             */
            link: function (scope, element, attrs) {
                var model = $parse(attrs.adjuntoFileModel),
                    modelSetter = model.assign; //define a setter for demoFileModel

                //Bind change event on the element
                element.bind('change', function () {
                    //Call apply on scope, it checks for value changes and reflect them on UI
                    scope.$apply(function () {
                        //set the model value
                        modelSetter(scope, element[0].files[0]);
                    });
                });
            }
        };
    })
    .config([
        '$compileProvider',
        function ($compileProvider) {
            $compileProvider.aHrefSanitizationWhitelist(/^\s*(https?|ftp|mailto|chrome-extension):/);
            // Angular before v1.2 uses $compileProvider.urlSanitizationWhitelist(...)
        }
    ]);
