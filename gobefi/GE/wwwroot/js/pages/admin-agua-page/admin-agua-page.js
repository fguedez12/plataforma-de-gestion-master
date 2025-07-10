var app = angular.module('adminAguaApp', ['ui.router', 'cp.ngConfirm', 'ServicesModule'])
    .controller("adminAguaController", function ($rootScope, $state, $http) {
        var userId = angular.element("#input-id").val();
        var isAdmin = angular.element("#input-is-admin").val();
        const isConsulta = angular.element("#input-is-consulta").val();
        $rootScope.userId = userId;
        $rootScope.isAdmin = isAdmin == 'False' ? false : true;
        $rootScope.isConsulta = isConsulta == 'False' ? false : true;
        $rootScope.selectedUnidadNombre = localStorage.getItem('divisionName');
        $rootScope.selectedUnidadId = localStorage.getItem('divisionId');
        $rootScope.selectedInstitucionId = localStorage.getItem('institucionId');
        $rootScope.selectedServicioId = localStorage.getItem('servicioId');
        $state.go("adminAgua@home");
    })
    .controller("homeAguaController", function ($scope, $rootScope, $http, $ngConfirm, $divisionModule, $serviciosModule) {
        $http({
            method: 'GET',
            url: `/settings?sectionName=ApiConfiguration&paramName=apiGestionaEnergia`

        }).then(function (response) {
            const did = localStorage.getItem('divisionId');
            $scope.gestion = {};
            $scope.gestion.divisionId = did;
            $rootScope.APIURL = response.data;
            $serviciosModule.getDiagnostico($rootScope.selectedServicioId).then(resp => {
                if (resp.data.revisionDiagnosticoAmbiental) {
                    if (!$rootScope.isAdmin) {
                        $rootScope.isConsulta = true
                    }
                }
            });
            loadData();
            /*setServicioIdInsitutcionId();*/
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

        $scope.loadingTableConsumos = false;
        $scope.loadingTableArtefactos = false;
        $scope.consumos = [];
        $scope.artefactos = [];
        $scope.pageConsumos = 1;
        $scope.pageArtefactos = 1;
        $scope.editMode = false;
        $scope.arrayPagesConsumos = [];
        $scope.arrayPagesArtefactos = [];
        $scope.tipoArtefactos = [];
        $scope.observa = {};
        $scope.observa.checkObserva = true;
        $scope.observa.observacion = "";
        $scope.editText = "Editar";
        $scope.agredadoText = "Consumo agregado anual";
        $scope.adquicisionBidonText = "";
        $scope.selectedYear = new Date().getFullYear().toString();

        if ($rootScope.isConsulta) {
            $scope.editText = "Ver";
        } else {
            $scope.editText = "Editar";
        }



        var cantidadArtefactos = document.querySelector('#cantidadArtefactos');

        cantidadArtefactos.addEventListener('input', function () {
            var num = this.value.match(/^\d+$/);
            if (num === null) {
                this.value = "";
            }
        });

        $scope.changeYear = function () {
            loadData();
        }

        $scope.changeNoDeclaraArtefactos = function () {
            //console.log("No declara");
            $http({
                method: 'PUT',
                url: `${$rootScope.APIURL}/api/aguas/no-declara/${$rootScope.selectedUnidadId}?value=${$scope.noDeclaraArtefactos}`,
            }).then(function (response) {
                //console.log("ok");
            });
        }

        $scope.changeConsumeBidon = function () {
            //console.log($scope.consumeBidon);
            $divisionModule.patchDivisionUsaBidon($rootScope.selectedUnidadId, $scope.consumeBidon).then(function (response) {
                console.log(response);
            });
            
        }

        $scope.saveObservacion = function () {
            //console.log("save");
            //console.log($scope.justificacion);
            $http({
                method: 'PUT',
                url: `${$rootScope.APIURL}/api/divisiones/justifica-agua/${$rootScope.selectedUnidadId}`,
                data: JSON.stringify($scope.observa)

            }).then(function (response) {
                //console.log("response")
                $ngConfirm({
                    title: 'Guardar información',
                    content: '<p>Registro guardado correctamente<p>',
                    buttons: {
                        Cancelar: {
                            text: "Ok",
                            btnClass: "btn btn-default",
                            action: function () {

                            }
                        }

                    }
                });
            });

        };

        $scope.showModal = function () {
            $scope.gestion = {};
            $scope.modalTitle = "Nueva Gestión";
            $("#modal-form").modal("show");
        };

        $scope.changeTipo = function () {
            if ($scope.gestion.tipoId == "2") {
                loadTipoArtefacto();
            }
        }

        $scope.closeModal = function () {
            closeModal();
        };

        $scope.submitForm = function () {

            const did = localStorage.getItem('divisionId');
            $scope.gestion.divisionId = did;

            if ($scope.gestion.tipoId == "1") {
                if ($scope.editMode) {
                    const url = `${$rootScope.APIURL}/api/aguas/${$scope.gestion.id}`;
                    const method = 'PUT';
                    submitForm(url, method);
                } else {
                    const url = `${$rootScope.APIURL}/api/aguas`;
                    const method = 'POST';
                    submitForm(url, method);
                }
            } else {
                if ($scope.editMode) {
                    const url = `${$rootScope.APIURL}/api/artefactos/${$scope.gestion.id}`;
                    const method = 'PUT';
                    submitForm(url, method);
                } else {
                    const url = `${$rootScope.APIURL}/api/artefactos`;
                    const method = 'POST';
                    submitForm(url, method);
                }
            }
        };

        $scope.setPageConsumos = function (page) {
            $scope.pageConsumos = page;
            loadData();
        };

        $scope.prevPageConsumo = function () {

            if ($scope.pageConsumos > 1) {
                $scope.pageConsumos = $scope.pageConsumos - 1;
                loadData();
            }
        };

        $scope.nextPageConsumos = function () {

            if ($scope.pageConsumos < $scope.arrayPagesConsumos[$scope.arrayPagesConsumos.length - 1]) {
                $scope.pageConsumos = $scope.pageConsumos + 1;
                loadData();
            }
        };

        $scope.setPageArtefactos = function (page) {
            $scope.pageArtefactos = page;
            loadData();
        };

        $scope.prevPageArtefacto = function () {

            if ($scope.pageArtefactos > 1) {
                $scope.pageArtefactos = $scope.pageArtefactos - 1;
                loadData();
            }
        };

        $scope.nextPageArtefactos = function () {

            if ($scope.pageArtefactos < $scope.arrayPagesArtefactos[$scope.arrayPagesArtefactos.length - 1]) {
                $scope.pageArtefactos = $scope.pageArtefactos + 1;
                loadData();
            }
        };

        $scope.editConsumo = function (consumoId) {
            $http({
                method: 'GET',
                url: `${$rootScope.APIURL}/api/aguas/get-by-id/${consumoId}`,
            }).then(function (response) {
                if (response.data.ok) {
                    $scope.editMode = true;
                    $scope.gestion = response.data.consumo;
                    $scope.gestion.tipoId = "1";
                    $scope.gestion.tipoSuministroId = response.data.consumo.tipoSuministroId.toString();
                    if ($scope.gestion.tipoSuministroId == 1 && !$scope.gestion.compraAgregada) {

                        $scope.gestion.inicioLectura = new Date(response.data.consumo.inicioLectura);

                        $scope.gestion.finLectura = new Date(response.data.consumo.finLectura);
                    }
                    if ($scope.gestion.tipoSuministroId == 2 && !$scope.gestion.compraAgregada) {
                        $scope.gestion.fecha = new Date(response.data.consumo.fecha);
                    }
                    //const adjuntoUrl = response.data.consumo.adjuntoUrl;
                    //const adjuntoUrlArr = adjuntoUrl.split('\\');
                    //const fileName = adjuntoUrlArr[adjuntoUrlArr.length - 1];
                    //$scope.linkToDownload = `${$rootScope.FILESERVER}${$rootScope.ESTADO_VERDE_FOLDER}/${$rootScope.DOCUMENTOS_FOLDER}/${fileName}`
                    $scope.modalTitle = "Editar Gestión";
                    $("#modal-form").modal("show");
                }
            });
        };

        $scope.editArtefacto = function (artefactoId) {
            loadTipoArtefacto();
            $http({
                method: 'GET',
                url: `${$rootScope.APIURL}/api/artefactos/get-by-id/${artefactoId}`,
            }).then(function (response) {
                if (response.data.ok) {
                    $scope.editMode = true;
                    $scope.gestion = response.data.artefacto;
                    $scope.gestion.tipoId = "2";
                    $scope.gestion.tipoArtefactoId = response.data.artefacto.tipoArtefactoId.toString();
                    $scope.gestion.estadoId = response.data.artefacto.estadoId.toString();
                    $scope.modalTitle = "Editar Gestión";
                    $("#modal-form").modal("show");
                }

            });
        };

        $scope.deleteConsumo = function (id) {
            $ngConfirm({
                title: 'Eliminar Consumo',
                content: '<p>¿Esta seguro de eliminar el consumo?<p>',
                buttons: {
                    Ok: {
                        text: "Ok",
                        btnClass: "btn btn-danger",
                        action: function () {
                            $http({
                                method: 'DELETE',
                                url: `${$rootScope.APIURL}/api/aguas/${id}`
                            }).then(function () {
                                loadData();
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
        };

        $scope.deleteArtefacto = function (id) {
            $ngConfirm({
                title: 'Eliminar Artefacto',
                content: '<p>¿Esta seguro de eliminar el artefacto?<p>',
                buttons: {
                    Ok: {
                        text: "Ok",
                        btnClass: "btn btn-danger",
                        action: function () {
                            $http({
                                method: 'DELETE',
                                url: `${$rootScope.APIURL}/api/artefactos/${id}`
                            }).then(function () {
                                loadData();
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
        };

        $scope.changeInstitucion = function (id) {
            //console.log(id);
            $scope.unidades = [];
            $rootScope.selectedInstitucionId = id;
            $scope.servicios = [];
            $rootScope.selectedServicioId = '';
            loadServicios();
        }

        $scope.changeServicio = function (id) {
            if (id) {
                $rootScope.selectedServicioId = id;
                loadUnidades();
            }
            
        }

        $scope.changeUnidad = function (id) {
            if (id) {
                localStorage.setItem('institucionId', $rootScope.selectedInstitucionId);
                localStorage.setItem('servicioId', $rootScope.selectedServicioId);
                localStorage.setItem('divisionId', id);
                $rootScope.selectedUnidadId = id
                $http({
                    method: 'GET',
                    url: `${$rootScope.APIURL}/api/divisiones/${id}`,
                }).then(function (response) {
                    //console.log(response)
                    if (response.data.ok) {
                        localStorage.setItem('divisionName', response.data.division.direccion);
                        $rootScope.selectedUnidadNombre = response.data.division.direccion;
                        loadData();
                    }



                });
            }
        }

        //function setServicioIdInsitutcionId() {
        //    $http({
        //        method: 'GET',
        //        url: `${$rootScope.APIURL}/api/divisiones/${$rootScope.selectedUnidadId}`,

        //    }).then(function (response) {
        //        if (response.data.Ok) {
        //            console.log(response.data)
        //            localStorage.setItem('servicioEvId', response.data.servicio.id);
        //            localStorage.setItem('institucionId', response.data.institucion.id);
        //        }
        //    });
        //}

        function loadTipoArtefacto() {
            $http({
                method: 'GET',
                url: `${$rootScope.APIURL}/api/artefactos/get-tipos`,
            }).then(function (response) {
                if (response.data.ok) {
                    $scope.tipoArtefactos = response.data.tipoArtefactos;
                }

            });
        }

        function closeModal() {
            $scope.gestion = {};
            $scope.editMode = false;
            angular.forEach(
                angular.element("input[type='file']"),
                function (inputElem) {
                    angular.element(inputElem).val(null);
                });
            $scope.aguaForm.$setPristine();
            $scope.aguaForm.$setUntouched();
            $("#modal-form").modal("hide");
        };

        function submitForm(url, method) {
            var payload = new FormData();
            payload.append("divisionId", $scope.gestion.divisionId);
            if ($scope.gestion.tipoId == 1) {
                payload.append("tipoSuministroId", $scope.gestion.tipoSuministroId);
                payload.append("compraAgregada", $scope.gestion.compraAgregada ? $scope.gestion.compraAgregada : false);
                if ($scope.gestion.tipoSuministroId == 1 && !$scope.gestion.compraAgregada) {
                    var dateIniStr = (new Date($scope.gestion.inicioLectura)).toUTCString();
                    payload.append("inicioLectura", dateIniStr);
                    var dateFinStr = (new Date($scope.gestion.finLectura)).toUTCString();
                    payload.append("finLectura", dateFinStr);
                }
                if ($scope.gestion.tipoSuministroId == 2 && !$scope.gestion.compraAgregada) {
                    var dateStr = (new Date($scope.gestion.fecha)).toUTCString();
                    payload.append("fecha", dateStr);
                }
                if ($scope.gestion.compraAgregada) {
                    payload.append("anioAdquisicion", $scope.gestion.anioAdquisicion);
                }
                payload.append("cantidad", $scope.gestion.cantidad);
                if ($scope.gestion.costo) {
                    payload.append("costo", $scope.gestion.costo);
                }
                
                payload.append("adjunto", $scope.adjunto);
            } else {
                payload.append("tipoArtefactoId", $scope.gestion.tipoArtefactoId);
                payload.append("cantidadArtefactos", $scope.gestion.cantidadArtefactos);
                payload.append("estadoId", $scope.gestion.estadoId);
                if (!$scope.gestion.tecnologiaAhorro) {
                    payload.append("tecnologiaAhorro", false);
                } else {
                    payload.append("tecnologiaAhorro", $scope.gestion.tecnologiaAhorro);
                }

                if ($scope.gestion.tecnologiaAhorro) {
                    payload.append("tecnologiaAhorroPorcentaje", $scope.gestion.tecnologiaAhorroPorcentaje);
                }

            }

            $http({
                method: method,
                url: url,
                data: payload,
                headers: { 'Content-Type': undefined }
            }).then(function (response) {
                if (response.data.ok) {
                    loadData();
                    closeModal();
                }
            }, function (error) {
                closeModal();
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
            });
        };

        function loadData() {
            const did = localStorage.getItem('divisionId');
            //console.log(did)
            if (!did) {
                window.location.href = '/';
            };

            let years = [];
            let actualY = new Date().getFullYear();
            years.push(actualY);
            for (let i = 0; i <= 3; i++) {
                actualY = actualY - 1
                years.push(actualY);
            }

            $scope.years = years;

            $scope.loadingTableConsumos = true;
            $http({
                method: 'GET',
                url: `${$rootScope.APIURL}/api/aguas?page=${$scope.pageConsumos}&divisionId=${did}&anioDoc=${$scope.selectedYear}`,
            }).then(function (response) {
                //console.log(response)
                $scope.loadingTableConsumos = false;
                if (response.data.ok) {
                    let totalRecords = parseInt(response.headers('totalrecords'));
                    let pages = Math.ceil(totalRecords / 5);
                    const arraypages = Array(pages).fill(1).map((n, i) => n + i)
                    if (arraypages.length > 15) {
                        if ($scope.pageConsumos > 1) {
                            arraypages.splice(0, $scope.pageConsumos - 1);
                        }
                        arraypages.splice(15, arraypages.length - 15);
                    }
                    $scope.arrayPagesConsumos = arraypages;
                    $scope.consumos = response.data.consumos;
                    $scope.noDeclaraArtefactos = response.data.noDeclaraArtefactos;
                    $scope.consumeBidon = response.data.usaBidon;
                    if ($scope.consumeBidon) {
                        $scope.activeArtefactos = ""
                        $scope.activeConsumo = "active"
                    } else if ($scope.noDeclaraArtefactos) {
                        $scope.activeArtefactos = "active"
                        $scope.activeConsumo = ""
                    }
                   
                    
                    
                    if (response.data.accesoFacturaAgua) {
                        $scope.accesoFacturaAgua = response.data.accesoFacturaAgua.toString();
                    }
                    $scope.comparteMedidorAgua = response.data.comparteMedidorAgua;
                    getObservacion();
                }
            });
            $scope.loadingTableArtefactos = true;
            $http({
                method: 'GET',
                url: `${$rootScope.APIURL}/api/artefactos?page=${$scope.pageArtefactos}&divisionId=${did}`,
            }).then(function (response) {
                $scope.loadingTableArtefactos = false;
                if (response.data.ok) {
                    let totalRecords = parseInt(response.headers('totalrecords'));
                    let pages = Math.ceil(totalRecords / 5);
                    const arraypages = Array(pages).fill(1).map((n, i) => n + i)
                    if (arraypages.length > 15) {
                        if ($scope.pageArtefactos > 1) {
                            arraypages.splice(0, $scope.pageArtefactos - 1);
                        }
                        arraypages.splice(15, arraypages.length - 15);
                    }
                    $scope.arrayPagesArtefactos = arraypages;
                    $scope.artefactos = response.data.artefactos;
                }
            });

            loadServicios();
            loadUnidades();
            
        };


        $scope.changeTipoSuministro = function () {
            //console.log($scope.gestion.tipoSuministroId);
            if ($scope.gestion.tipoSuministroId == 2) {
                $scope.agredadoText = "Adquisición agregada anual";
                $scope.adquicisionBidonText = "agua bidón";
            } else {
                $scope.agredadoText = "Consumo agregado anual";
                $scope.adquicisionBidonText = "";
            }
        };

        function loadServicios() {
            $http({
                method: 'GET',
                url: `${$rootScope.APIURL}/api/servicios/getByInstitucionIdAndUserId/${$rootScope.selectedInstitucionId}/${$rootScope.userId}`,
            }).then(function (response) {
                //console.log(response.data);
                $scope.servicios = response.data;
            });
        }

        function loadUnidades() {
            $http({
                method: 'GET',
                url: `${$rootScope.APIURL}/api/divisiones/by-servicio-id?servicioId=${$rootScope.selectedServicioId}`,
            }).then(function (response) {
                //console.log(response.data);
                $scope.unidades = response.data.divisiones;
            });
        }

        function getObservacion() {
            //console.log($rootScope.selectedUnidadId)
            $http({
                method: 'GET',
                url: `${$rootScope.APIURL}/api/divisiones/observacion-agua/${$rootScope.selectedUnidadId}`,
            }).then(function (response) {
                $scope.observa.observacion = response.data.observacion;
                $scope.observa.checkObserva = response.data.checkObserva;
            });

        };

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
    ;