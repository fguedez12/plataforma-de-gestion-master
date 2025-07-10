﻿var app = angular.module('planGestionApp', ['ui.router', 'cp.ngConfirm', 'ServicesModule'])
    .controller("planGestionController", function ($rootScope, $state) {
        var userId = angular.element("#input-id").val();
        var isAdmin = angular.element("#input-is-admin").val();
        const isConsulta = angular.element("#input-is-consulta").val();
        const lectura = angular.element("#input-lectura").val();
        const escritura = angular.element("#input-escritura").val();
        $rootScope.userId = userId;
        $rootScope.isAdmin = isAdmin == 'False' ? false : true;
        $rootScope.isConsulta = isConsulta == 'False' ? false : true;
        $rootScope.lectura = lectura == 'False' ? false : true;
        $rootScope.escritura = escritura == 'False' ? false : true;
        $rootScope.selectedServicioId = localStorage.getItem('servicioEvId');
        $rootScope.selectedServicioNombre = localStorage.getItem('servicioEvNombre');
        $state.go("planGestion@home");
    })
    .controller("homePlanGestionController", function ($scope, $rootScope, $http, $ngConfirm, $planGestionModule, $state) {

        if (!$rootScope.lectura) {
            window.location.href = `/AdminMisServicios`
        }

        $scope.data = {};
        $scope.pga = {};
        $scope.searchText = "";
        $scope.unidades = [];
        $scope.unidadesSelected = [];
        $scope.brechas = [];
        $scope.brechasUnSelected = [];
        $scope.brechasSelected = [];
        $scope.modalTitle = "";
        $scope.modalTitleObjetivo = "";
        $scope.modalTitleIndicador = "";
        $scope.modalTitleTarea = "";
        $scope.modalTitlePrograma = "";
        $rootScope.titleAccion = "Agregar"
        $scope.editMode = false;
        $scope.editModeObjetivo = false;
        $scope.editModeIndicador = false;
        $scope.editModeTarea = false;
        $scope.editModeAccion = false; // Se maneja principalmente en accionController via $rootScope.editModeAccion
        $scope.editModePrograma = false;
        $scope.modoVer = false; // Nueva variable para modo Ver/Editar
        $rootScope.modoVerAccion = false; // Para pasar a accionController
        $scope.medidas = [];
        $scope.otraMedidaSelected = false;
        $scope.unidadesDeAccionSelect = [];
        $scope.tareas = [];
        $scope.accionesTareas = [];
        $scope.objetivosIndicadores = [];
        $rootScope.editModeAccion = false;
        $scope.escritura = $rootScope.escritura;

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


        $http({
            method: 'GET',
            url: `/settings?sectionName=ApiConfiguration&paramName=apiGestionaEnergia`
        }).then(function (response) {
            $rootScope.APIURL = response.data;
            loadData();
        });

        $scope.savePgaInfo = function(){
            $planGestionModule.savePgaInfo($scope.pga, $rootScope.selectedServicioId).then(function (resp) {
                // Usar función estándar de toast de app.js
                window.mostrarToast('Éxito', 'Registro creado correctamente', 'success');
                //console.log(resp);
            });
        }

        $scope.saveObs = function (item) {
            //console.log(item);
            if (!item.ingresoObservacion) {
                item.observacion = null;
            }
            if (!item.ingresoObservacionObjetivos) {
                item.observacionObjetivos = null;
            }
            if (!item.ingresoObservacionAcciones) {
                item.observacionAcciones = null;
            }
            if (!item.ingresoObservacionIndicadores) {
                item.observacionIndicadores = null;
            }
            $planGestionModule.saveObs(item, $rootScope.selectedServicioId).then(function (resp) {
                //console.log(resp);
                $ngConfirm({
                    title: 'Guardar Registro',
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

        $scope.changeIngresoObs = function (e) {
            //console.log(e.item.ingresoObservacion)
            if (!e.item.ingresoObservacion) {
                $scope.saveObs(e.item);
            }
        }
        $scope.changeIngresoObsObjetivos = function (e) {
            //console.log(e.item.ingresoObservacion)
            if (!e.item.ingresoObservacionObjetivos) {
                $scope.saveObs(e.item);
            }
        }
        $scope.changeIngresoObsAcciones = function (e) {
            //console.log(e.item.ingresoObservacionAcciones)
            if (!e.item.ingresoObservacionAcciones) {
                $scope.saveObs(e.item);
            }
        }
        $scope.changeIngresoObsIndicadores = function (e) {
            console.log(e.item.ingresoObservacionIndicadores)
            if (!e.item.ingresoObservacionIndicadores) {
                $scope.saveObs(e.item);
            }
        }
        $scope.newBrecha = function () {
            $scope.modalTitle = "Agregar";
            $scope.editMode = false;
            showModal();
        }
        $scope.newObjetivo = function () {
            $scope.modalTitleObjetivo = "Agregar";
            $scope.editModeObjetivo = false;
            showModalObjetivos();
        }
        $scope.newIndicador = function () {
            $scope.modalTitleIndicador = "Agregar";
            $scope.editModeIndicador = false;
            showModalIndicadores();
        }
        $scope.newTarea = function () {
            $scope.modalTitleTarea = "Agregar";
            $scope.editModeTarea = false;
            // Inicializar tarea con estado por defecto "No implementado"
            $scope.tarea = {
                estadoAvance: "No implementado"
            };
            // Limpiar errores de validación previos
            $scope.fechaValidationError = "";
            showModalTareas();
        }

        // Función para verificar si el estado de avance debe estar deshabilitado
        $scope.isEstadoAvanceDisabled = function() {
            // El select de estado de avance está SIEMPRE deshabilitado tanto en modo crear como editar
            return true;
        }
        $scope.newPrograma = function () {
            $scope.modalTitlePrograma = "Agregar";
            $scope.editModePrograma = false;
            showModalPrograma();
        }

        function showModal() {
            $("#modal-form").modal("show");
        }
        function showModalObjetivos() {
            $("#modal-form-objetivos").modal("show");
        }
        function showModalTareas() {
            $("#modal-form-tareas").modal("show");
        }
        function showModalIndicadores() {
            $("#modal-form-indicadores").modal("show");
        }
        function showModalPrograma() {
            $("#modal-form-programa").modal("show");
        }

        $scope.closeModal = function () {
            $scope.brecha = {};
            $scope.unidadesSelected = [];
            $scope.brechaForm.$setUntouched();
            $scope.brechaForm.$setPristine();
            hideModal()
        }
        $scope.closeModalObjetivo = function () {
            $scope.objetivo = {};
            $scope.brechasSelected = [];
            $scope.brechasUnSelected = [];
            $scope.objetivoForm.$setUntouched();
            $scope.objetivoForm.$setPristine();
            hideModalObjetivo()
        }
        $scope.closeModalIndicador = function () {
            $scope.indicador = {};
            $scope.indicadorForm.$setUntouched();
            $scope.indicadorForm.$setPristine();
            hideModalIndicador()
        }
        $scope.closeModalTarea = function () {
            $scope.tarea = {};
            $scope.fechaValidationError = "";
            $scope.tareaForm.$setUntouched();
            $scope.tareaForm.$setPristine();
            hideModalTarea()
        }
        $scope.closeModalPrograma = function () {
            $scope.programa = {};
            $scope.programaForm.$setUntouched();
            $scope.programaForm.$setPristine();
            hideModalPrograma()
        }

        function hideModal() {
            $("#modal-form").modal("hide");
        }
        function hideModalObjetivo() {
            $("#modal-form-objetivos").modal("hide");
        }
        function hideModalIndicador() {
            $("#modal-form-indicadores").modal("hide");
        }
        function hideModalTarea() {
            $("#modal-form-tareas").modal("hide");
        }
        function hideModalPrograma() {
            $("#modal-form-programa").modal("hide");
        }

        $scope.chancheTipoBrecha = function () {
            //console.log($scope.brecha.tipoBrecha)
            if ($scope.brecha.tipoBrecha == 2) {
                getUnidades();
            }
        }
        $scope.filterUnidad = function () {
            getUnidades();
        }

        $scope.unSelUnidad = function (item) {
            $scope.unidadesSelected = $scope.unidadesSelected.filter(unidad => unidad.id !== item.id);
            $scope.unidadesSelected.sort((a, b) => a.direccion.localeCompare(b.direccion));
            getUnidades();
        }
        $scope.unSelBrecha = function (item) {
            $scope.brechasSelected = $scope.brechasSelected.filter(brecha => brecha.id !== item.id);
            $scope.brechasSelected.sort((a, b) => a.titulo.localeCompare(b.titulo));
            $scope.brechasUnSelected.push(item);
            $scope.brechasUnSelected.sort((a, b) => a.titulo.localeCompare(b.titulo));

        }

        function getUnidades() {
            $planGestionModule.getUnidades($rootScope.selectedServicioId, $scope.searchText).then(function (resp) {
                //console.log(resp);
                $scope.unidades = resp.data.filter(obj1 => !$scope.unidadesSelected.some(obj2 => obj2.id === obj1.id));

            });
        }
        $scope.selUnidad = function (item) {
            //console.log(item);
            $scope.unidadesSelected.push(item);
            $scope.unidadesSelected.sort((a, b) => a.direccion.localeCompare(b.direccion));
            $scope.unidades = $scope.unidades.filter(unidad => unidad.id !== item.id);

        }

        $scope.selBrecha = function (item) {
            //console.log(item);
            $scope.brechasSelected.push(item);
            $scope.brechasSelected.sort((a, b) => a.titulo.localeCompare(b.titulo));
            $scope.brechasUnSelected = $scope.brechasUnSelected.filter(brecha => brecha.id !== item.id);
        }

        $scope.submitForm = function () {
            if ($scope.editMode) {
                $planGestionModule.editBrecha($scope.brecha.id, $scope.brecha, $scope.unidadesSelected).then(function (resp) {
                    //console.log(resp);
                    loadData();
                    $scope.closeModal();

                });
            } else {
                $planGestionModule.saveBrecha($rootScope.selectedServicioId, $scope.brecha, $scope.unidadesSelected).then(function (resp) {
                    //console.log(resp);
                    loadData();
                    $scope.closeModal();
                });
            }


        }

        $scope.submitFormObjetivo = function () {
            if ($scope.editModeObjetivo) {
                $planGestionModule.editObjetivo($scope.objetivo.id, $scope.objetivo, $scope.brechasSelected).then(function (resp) {
                    //console.log(resp);
                    loadData();
                    $scope.closeModalObjetivo();

                });
            } else {
                $planGestionModule.saveObjetivo($scope.objetivo, $scope.brechasSelected).then(function (resp) {
                    //console.log(resp);
                    loadData();
                    $scope.closeModalObjetivo();
                });
            }
        }

        $scope.edit = function (item) {
            //console.log(item);
            $scope.editMode = true; // Se usa para cargar datos y decidir lógica de guardado
            $scope.modalTitle = $scope.modoVer ? "Ver" : "Editar";
            $planGestionModule.getbrechabyId(item.id).then(function (resp) {
                //console.log(resp);
                //$scope.data = resp.data;
                $scope.brecha = resp.data;
                if (resp.data.tipoBrecha === 2) {
                    $scope.unidadesSelected = resp.data.unidades;
                    getUnidades();
                }

                showModal();
            });

        }

        $scope.editObjetivo = function (item) {
            //console.log(item);
            $scope.editModeObjetivo = true;
            $scope.modalTitleObjetivo = $scope.modoVer ? "Ver" : "Editar";
            $planGestionModule.getObjetivoById(item.id).then(function (resp) {
                //console.log(resp);
                $scope.objetivo = resp.data;
                $scope.brechasSelected = resp.data.brechasSelected;
                //if (resp.data.tipoBrecha === 2) {
                //    $scope.unidadesSelected = resp.data.unidades;
                //    getUnidades();
                //}
                $scope.changeDimObjetivo();
                showModalObjetivos();
            });

        }

        $scope.delete = function (item) {
            //console.log(item);
            $ngConfirm({
                title: 'Eliminar Registro',
                content: '<p>¿Desea eliminar el registro?<p>',
                buttons: {
                    Eliminar: {
                        text: "Eliminar",
                        btnClass: "btn btn-danger",
                        action: function () {
                            $planGestionModule.deleteBrecha(item.id).then(function (resp) {
                                //console.log(resp);
                                loadData();
                                hideModal();
                                $ngConfirm({
                                    title: false, // Oculta el título
                                    content: 'Registro Eliminado correctamente', // Mensaje del toast
                                    autoClose: 'ok|3000', // Cierra automáticamente después de 3 segundos
                                    buttons: {
                                        ok: {
                                            text: 'OK',
                                            btnClass: 'btn-blue',
                                            action: function () {
                                                // Acción opcional al cerrar el toast
                                            }
                                        }
                                    },
                                    columnClass: 'medium', // Clase para controlar el tamaño del "toast"
                                    theme: 'modern', // Tema (puedes elegir el que mejor se adapte)
                                    backgroundDismiss: true, // Permite cerrar el toast al hacer clic fuera de él
                                    boxWidth: '30%', // Ancho del "toast"
                                    useBootstrap: false, // Desactiva Bootstrap si es necesario
                                });
                            });
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

        $scope.deleteObjetivo = function (item) {
            //console.log(item);
            $ngConfirm({
                title: 'Eliminar Registro',
                content: '<p>¿Desea eliminar el registro?<p>',
                buttons: {
                    Eliminar: {
                        text: "Eliminar",
                        btnClass: "btn btn-danger",
                        action: function () {
                            $planGestionModule.deleteObjetivo(item.id).then(function (resp) {
                                //console.log(resp);
                                loadData();
                                hideModalObjetivo();
                                $ngConfirm({
                                    title: false, // Oculta el título
                                    content: 'Registro Eliminado correctamente', // Mensaje del toast
                                    autoClose: 'ok|3000', // Cierra automáticamente después de 3 segundos
                                    buttons: {
                                        ok: {
                                            text: 'OK',
                                            btnClass: 'btn-blue',
                                            action: function () {
                                                // Acción opcional al cerrar el toast
                                            }
                                        }
                                    },
                                    columnClass: 'medium', // Clase para controlar el tamaño del "toast"
                                    theme: 'modern', // Tema (puedes elegir el que mejor se adapte)
                                    backgroundDismiss: true, // Permite cerrar el toast al hacer clic fuera de él
                                    boxWidth: '30%', // Ancho del "toast"
                                    useBootstrap: false, // Desactiva Bootstrap si es necesario
                                });
                            });
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

        $scope.deleteAccion = function (item) {
            //console.log(item);
            $ngConfirm({
                title: 'Eliminar Registro',
                content: '<p>¿Desea eliminar el registro?<p>',
                buttons: {
                    Eliminar: {
                        text: "Eliminar",
                        btnClass: "btn btn-danger",
                        action: function () {
                            $planGestionModule.deleteAccion(item.id).then(function (resp) {
                                //console.log(resp);
                                loadData();
                                $ngConfirm({
                                    title: false, // Oculta el título
                                    content: 'Registro Eliminado correctamente', // Mensaje del toast
                                    autoClose: 'ok|3000', // Cierra automáticamente después de 3 segundos
                                    buttons: {
                                        ok: {
                                            text: 'OK',
                                            btnClass: 'btn-blue',
                                            action: function () {
                                                // Acción opcional al cerrar el toast
                                            }
                                        }
                                    },
                                    columnClass: 'medium', // Clase para controlar el tamaño del "toast"
                                    theme: 'modern', // Tema (puedes elegir el que mejor se adapte)
                                    backgroundDismiss: true, // Permite cerrar el toast al hacer clic fuera de él
                                    boxWidth: '30%', // Ancho del "toast"
                                    useBootstrap: false, // Desactiva Bootstrap si es necesario
                                });
                            });
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

        $scope.changeDimObjetivo = function () {
            if ($scope.objetivo.dimensionBrechaId) {
                //console.log($scope.objetivo.dimensionId);
                //console.log($scope.brechas);
                $scope.brechasUnSelected = $scope.brechas.filter(function (brecha) {
                    return brecha.dimensionBrechaId == $scope.objetivo.dimensionBrechaId && brecha.objetivoId == null;
                });

                //console.log($scope.brechasUnSelected);


            }

        }

        $scope.newAccion = function () {
            //console.log("Nueva accion")
            $rootScope.data = $scope.data;
            $rootScope.objetivos = $scope.objetivos;
            //$rootScope.objetivosAcciones = $scope.objetivosAcciones;
            $state.go("mantenedor@Accion");
        }

        $scope.editAccion = function (item) {
            //console.log(item);
            $rootScope.editModeAccion = !$rootScope.modoVerAccion; // True si es editar, false si es ver (para lógica de guardado en accionController)
            $rootScope.titleAccion = $rootScope.modoVerAccion ? "Ver" : "Editar";
            $rootScope.data = $scope.data;
            $rootScope.objetivos = $scope.objetivos;
            $planGestionModule.getAccionById(item.id).then(function (resp) {
                //console.log(resp);
                $rootScope.accion = resp.data;
                $rootScope.accion.piloto = false;
                $state.go("mantenedor@Accion");
                const adjunto = resp.data.adjuntoUrl;
                const adjuntoUrlArr = adjunto.split('\\');
                const fileName = adjuntoUrlArr[adjuntoUrlArr.length - 1];
                $rootScope.accion.linkToDownload = `${$rootScope.FILESERVER}${$rootScope.ESTADO_VERDE_FOLDER}/${$rootScope.DOCUMENTOS_FOLDER}/${fileName}`
                //$scope.brechasSelected = resp.data.brechasSelected;
                
            });
        }

        $scope.changeDimIndicador = function() {
            if ($scope.indicador.dimensionBrechaId) {
                $scope.objetivosIndicadores = $scope.objetivos.filter(function (obj) {
                    return obj.dimensionBrechaId == $scope.indicador.dimensionBrechaId;
                });
            } else {
                $scope.objetivosIndicadores = [];
            }
        }

        // Funciones para el mantenedor de Tareas basadas en el patrón de indicadores
        $scope.changeDimTarea = function() {
            if ($scope.tarea.dimensionBrechaId) {
                // Filtrar acciones por dimensión seleccionada
                $scope.accionesTareas = $scope.acciones.filter(function (accion) {
                    return accion.dimensionBrechaId == $scope.tarea.dimensionBrechaId;
                });
            } else {
                $scope.accionesTareas = [];
            }
        }

        // Función para validar fechas
        $scope.validarFechas = function () {
            $scope.fechaValidationError = "";
            
            if ($scope.tarea && $scope.tarea.fechaInicio && $scope.tarea.fechaFin) {
                var fechaInicio = new Date($scope.tarea.fechaInicio);
                var fechaFin = new Date($scope.tarea.fechaFin);
                
                if (fechaFin <= fechaInicio) {
                    $scope.fechaValidationError = "La fecha de fin debe ser mayor que la fecha de inicio";
                    return false;
                }
            }
            return true;
        }

        $scope.submitFormTarea = function () {
            // Validar fechas antes de enviar
            if (!$scope.validarFechas()) {
                return;
            }
            
            if ($scope.editModeTarea) {
                $planGestionModule.editTarea($scope.tarea.id, $scope.tarea).then(function (resp) {
                    //console.log(resp);
                    loadData();
                    $scope.closeModalTarea();
                    // Usar función estándar de toast de app.js
                    window.mostrarToast('Éxito', 'Tarea actualizada correctamente', 'success');
                });
            } else {
                $planGestionModule.saveTarea($scope.tarea).then(function (resp) {
                    //console.log(resp);
                    loadData();
                    $scope.closeModalTarea();
                    // Usar función estándar de toast de app.js
                    window.mostrarToast('Éxito', 'Tarea creada correctamente', 'success');
                });
            }
        }

        $scope.editTarea = function (item) {
            //console.log(item);
            $scope.editModeTarea = true;
            $scope.modalTitleTarea = $scope.modoVer ? "Ver" : "Editar";
            // Limpiar errores de validación previos
            $scope.fechaValidationError = "";
            $planGestionModule.getTareaById(item.id).then(function (resp) {
                //console.log(resp);
                $scope.tarea = resp.data;
                // Convertir fechas para el input date
                if ($scope.tarea.fechaInicio) {
                    $scope.tarea.fechaInicio = new Date($scope.tarea.fechaInicio);
                }
                if ($scope.tarea.fechaFin) {
                    $scope.tarea.fechaFin = new Date($scope.tarea.fechaFin);
                }
                $scope.changeDimTarea();
                showModalTareas();
            });
        }

        $scope.deleteTarea = function (item) {
            //console.log(item);
            $ngConfirm({
                title: 'Eliminar Registro',
                content: '<p>¿Desea eliminar la tarea?<p>',
                buttons: {
                    Eliminar: {
                        text: "Eliminar",
                        btnClass: "btn btn-danger",
                        action: function () {
                            $planGestionModule.deleteTarea(item.id).then(function (resp) {
                                //console.log(resp);
                                loadData();
                                // Usar función estándar de toast de app.js
                                window.mostrarToast('Éxito', 'Tarea eliminada correctamente', 'success');
                            });
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

        $scope.submitFormIndicador = function () {
            if ($scope.editModeIndicador) {
                $planGestionModule.editIndicador($scope.indicador.id, $scope.indicador).then(function (resp) {
                    //console.log(resp);
                    loadData();
                    $scope.closeModalIndicador();

                });
            } else {
                $planGestionModule.saveIndicador($scope.indicador).then(function (resp) {
                    //console.log(resp);
                    loadData();
                    $scope.closeModalIndicador();
                });
            }
        }

        $scope.editIndicador = function (item) {
            //console.log(item);
            $scope.editModeIndicador = true;
            $scope.modalTitleIndicador = $scope.modoVer ? "Ver" : "Editar";
            $planGestionModule.getIndicadorById(item.id).then(function (resp) {
                //console.log(resp);
                $scope.indicador = resp.data;
                $scope.changeDimIndicador();
                showModalIndicadores();
            });

        }

        $scope.deleteIndicador = function (item) {
            //console.log(item);
            $ngConfirm({
                title: 'Eliminar Registro',
                content: '<p>¿Desea eliminar el registro?<p>',
                buttons: {
                    Eliminar: {
                        text: "Eliminar",
                        btnClass: "btn btn-danger",
                        action: function () {
                            $planGestionModule.deleteIndicador(item.id).then(function (resp) {
                                //console.log(resp);
                                loadData();
                                $ngConfirm({
                                    title: false, // Oculta el título
                                    content: 'Registro Eliminado correctamente', // Mensaje del toast
                                    autoClose: 'ok|3000', // Cierra automáticamente después de 3 segundos
                                    buttons: {
                                        ok: {
                                            text: 'OK',
                                            btnClass: 'btn-blue',
                                            action: function () {
                                                // Acción opcional al cerrar el toast
                                            }
                                        }
                                    },
                                    columnClass: 'medium', // Clase para controlar el tamaño del "toast"
                                    theme: 'modern', // Tema (puedes elegir el que mejor se adapte)
                                    backgroundDismiss: true, // Permite cerrar el toast al hacer clic fuera de él
                                    boxWidth: '30%', // Ancho del "toast"
                                    useBootstrap: false, // Desactiva Bootstrap si es necesario
                                });
                            });
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

        $scope.submitFormPrograma = function () {
            if ($scope.editModePrograma) {
                $planGestionModule.editPrograma($scope.programa.id, $scope.programa).then(function (resp) {
                    loadData();
                    $scope.closeModalPrograma();
                });
            } else {
                $planGestionModule.savePrograma($scope.programa,$rootScope.selectedServicioId).then(function (resp) {
                    loadData();
                    $scope.closeModalPrograma();
                });
            }
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

        $scope.changeFilePrograma = async function () {

            const file = document.querySelector("#archivo").files[0];
            let b64File = await toBase64(file);
            var base64result = b64File.split(',')[1];
            $scope.programa.adjunto = base64result
            $scope.programa.adjuntoNombre = file.name
        }

        $scope.editPrograma = function (item) {
            //console.log(item);
            $scope.editModePrograma = true;
            $scope.modalTitlePrograma = $scope.modoVer ? "Ver" : "Editar";
            $planGestionModule.getProgramaById(item.id).then(function (resp) {
                const adjunto = resp.data.adjuntoUrl;
                const adjuntoUrlArr = adjunto.split('\\');
                const fileName = adjuntoUrlArr[adjuntoUrlArr.length - 1];
                $scope.linkToDownload = `${$rootScope.FILESERVER}${$rootScope.ESTADO_VERDE_FOLDER}/${$rootScope.DOCUMENTOS_FOLDER}/${fileName}`
                $scope.programa = resp.data;
                showModalPrograma();
            });

        }

        $scope.deletePrograma = function (item) {
            //console.log(item);
            $ngConfirm({
                title: 'Eliminar Registro',
                content: '<p>¿Desea eliminar el registro?<p>',
                buttons: {
                    Eliminar: {
                        text: "Eliminar",
                        btnClass: "btn btn-danger",
                        action: function () {
                            $planGestionModule.deletePrograma(item.id).then(function (resp) {
                                //console.log(resp);
                                loadData();
                                $ngConfirm({
                                    title: false, // Oculta el título
                                    content: 'Registro Eliminado correctamente', // Mensaje del toast
                                    autoClose: 'ok|3000', // Cierra automáticamente después de 3 segundos
                                    buttons: {
                                        ok: {
                                            text: 'OK',
                                            btnClass: 'btn-blue',
                                            action: function () {
                                                // Acción opcional al cerrar el toast
                                            }
                                        }
                                    },
                                    columnClass: 'medium', // Clase para controlar el tamaño del "toast"
                                    theme: 'modern', // Tema (puedes elegir el que mejor se adapte)
                                    backgroundDismiss: true, // Permite cerrar el toast al hacer clic fuera de él
                                    boxWidth: '30%', // Ancho del "toast"
                                    useBootstrap: false, // Desactiva Bootstrap si es necesario
                                });
                            });
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

        function loadData() {
            if (!$rootScope.selectedServicioId) {
                window.location.href = '/AdminMisServicios';
                return
            };

            $planGestionModule.getData($rootScope.selectedServicioId).then(function (resp) {
                //console.log(resp);
                $scope.data = resp.data;
            });
            $planGestionModule.getbrechas($rootScope.selectedServicioId).then(function (resp) {
                //console.log(resp);
                $scope.brechas = resp.data;
            });
            $planGestionModule.getObjetivos($rootScope.selectedServicioId).then(function (resp) {
                //console.log(resp);
                $scope.objetivos = resp.data;
            });
            $planGestionModule.getAcciones($rootScope.selectedServicioId).then(function (resp) {
                //console.log(resp);
                $scope.acciones = resp.data;
            });
            $planGestionModule.getindicadores($rootScope.selectedServicioId).then(function (resp) {
                //console.log(resp);
                $scope.indicadores = resp.data;
            });
            $planGestionModule.getTareas($rootScope.selectedServicioId).then(function (resp) {
                //console.log(resp);
                $scope.tareas = resp.data;
            });
            $planGestionModule.getProgramas($rootScope.selectedServicioId).then(function (resp) {
                //console.log(resp);
                $scope.programas = resp.data;
            });
            $planGestionModule.getPgaData($rootScope.selectedServicioId).then(function (resp) {
                //console.log(resp);
                $scope.pga = resp.data;
                if ($scope.pga.pgaRevisionRed && !$rootScope.isAdmin) {
                    $rootScope.escritura = false;
                }
            });

        }

        // Funciones wrapper para Brechas
        $scope.abrirModalVerBrecha = function(item) {
            $scope.modoVer = true;
            $scope.edit(item);
        }
        $scope.abrirModalEditarBrecha = function(item) {
            $scope.modoVer = false;
            $scope.edit(item);
        }

        // Funciones wrapper para Objetivos
        $scope.abrirModalVerObjetivo = function(item) {
            $scope.modoVer = true;
            $scope.editObjetivo(item);
        }
        $scope.abrirModalEditarObjetivo = function(item) {
            $scope.modoVer = false;
            $scope.editObjetivo(item);
        }

        // Funciones wrapper para Acciones
        $scope.abrirModalVerAccion = function(item) {
            $rootScope.modoVerAccion = true;
            $scope.editAccion(item);
        }
        $scope.abrirModalEditarAccion = function(item) {
            $rootScope.modoVerAccion = false;
            $scope.editAccion(item);
        }

        // Funciones wrapper para Indicadores
        $scope.abrirModalVerIndicador = function(item) {
            $scope.modoVer = true;
            $scope.editIndicador(item);
        }
        $scope.abrirModalEditarIndicador = function(item) {
            $scope.modoVer = false;
            $scope.editIndicador(item);
        }

        // Funciones wrapper para Tareas
        $scope.abrirModalVerTarea = function(item) {
            $scope.modoVer = true;
            $scope.editTarea(item);
        }
        $scope.abrirModalEditarTarea = function(item) {
            $scope.modoVer = false;
            $scope.editTarea(item);
        }

        // Funciones wrapper para Programa
        $scope.abrirModalVerPrograma = function(item) {
            $scope.modoVer = true;
            $scope.editPrograma(item);
        }
        $scope.abrirModalEditarPrograma = function(item) {
            $scope.modoVer = false;
            $scope.editPrograma(item);
        }

        $scope.esAnioActualOPosterior = function (fechaCreacion) {
            if (!fechaCreacion) {
                return true; // Si no hay fecha, se asume que es actual o posterior para mostrar botones
            }
            const anioActual = new Date().getFullYear();
            const fecha = new Date(fechaCreacion);
            return fecha.getFullYear() >= anioActual;
        };
    }).controller("accionController", function ($scope, $rootScope, $http, $ngConfirm, $planGestionModule, $state) {
        $scope.modoVer = $rootScope.modoVerAccion === true; // Heredar estado de solo lectura

        if (!$rootScope.data) {
            $state.go("planGestion@home");
        }

        if (!$rootScope.accion) {
            $rootScope.accion = { "dimensionBrechaId": null,"piloto" : false };
        } else {
            // Si es modoVer, editModeAccion debe ser false para deshabilitar guardado, pero los datos se cargan igual.
            // $rootScope.editModeAccion ya se establece en homePlanGestionController.editAccion
            loadEditData();
        }

        $scope.changeDimAccion = function () {
            changeDim();
            
        }

        function loadEditData() {
            $rootScope.objetivosAcciones = $rootScope.objetivos.filter(function (obj) {
                return obj.dimensionBrechaId == $rootScope.accion.dimensionBrechaId;
            });
            $planGestionModule.getMedidas($rootScope.accion.dimensionBrechaId).then(function (resp) {
                //console.log(resp);
                $rootScope.medidas = resp.data;
                $rootScope.medidas.forEach(obj => {
                    obj.selected = false;
                })
                if ($rootScope.accion.medidaId == 999) {
                    $scope.medidas.forEach(obj => {
                        obj.selected = false;
                    })
                    $scope.otraMedidaSelected = true;
                    $scope.accion.medidaId = 999;
                } else {
                    var obb = $rootScope.medidas.find(obj => obj.id == $rootScope.accion.medidaId);
                    obb.selected = true;
                }
                
                //console.log(obb);

            });
            $planGestionModule.getUnidadesByObjetivoId($rootScope.accion.objetivoId).then(function (resp) {

                console.log($rootScope.accion);

                $rootScope.unidadesDeAccionUnselect = resp.data.filter(obj1 => $rootScope.accion.unidades.some(obj2 => obj2.id != obj1.id));
                $rootScope.accion.unidadesDeAccionSelect = $rootScope.accion.unidades;
                //console.log($scope.unidadesDeAccionUnselect);
            });
        }

        function changeDim() {
            //console.log($rootScope.accion.dimensionId);
            $rootScope.otraMedidaSelected = false;
            $rootScope.accion.medidaId = null;
            if ($rootScope.accion.dimensionBrechaId) {
                $rootScope.objetivosAcciones = $rootScope.objetivos.filter(function (obj) {
                    return obj.dimensionBrechaId == $rootScope.accion.dimensionBrechaId;
                });
                $planGestionModule.getMedidas($rootScope.accion.dimensionBrechaId).then(function (resp) {
                    //console.log(resp);
                    $rootScope.medidas = resp.data;
                });

            } else {
                $rootScope.objetivosAcciones = [];
                $rootScope.medidas = [];
            }
        }

        $scope.selectMedida = function (item) {

            if ($rootScope.escritura) {
                $scope.medidas.forEach(obj => {
                    obj.selected = false;
                })
                $scope.otraMedidaSelected = false;
                item.selected = true;
                $scope.accion.medidaId = item.id;
            }
            //console.log(item);
            
            //console.log(item);
        }

        $scope.otraMedida = function () {
            $scope.medidas.forEach(obj => {
                obj.selected = false;
            })
            $scope.otraMedidaSelected = true;
            $scope.accion.medidaId = 999;
        }



        $scope.goToS2 = function () {
            let objetivo = $rootScope.objetivosAcciones.find(function (obj) {
                return obj.id == $rootScope.accion.objetivoId
            })

            
            if (objetivo.tieneBrechaPorUnidad) {
                $rootScope.accion.brechaPorUnidad = true;
                $state.go("mantenedor2@Accion");
            } else {
                $state.go("mantenedor3@Accion");
                $rootScope.accion.brechaPorUnidad = false;
            }


        }
        $scope.backToPlan = function () {
            $rootScope.accion = null;
            $state.go("planGestion@home");
        }

        $scope.changeObjetovoAccion = function () {
            //console.log($scope.accion.objetivoAccion)
            if ($rootScope.accion.objetivoId) {
                $planGestionModule.getUnidadesByObjetivoId($rootScope.accion.objetivoId).then(function (resp) {

                    $rootScope.unidadesDeAccionUnselect = resp.data;
                    //console.log($scope.unidadesDeAccionUnselect);
                });
            } else {
                $rootScope.unidadesDeAccionUnselect = [];
            }
        }
    }).controller("accion2Controller", function ($scope, $rootScope, $http, $ngConfirm, $planGestionModule, $state) {
        $scope.modoVer = $rootScope.modoVerAccion === true; // Heredar estado de solo lectura

        if (!$rootScope.accion) {
            $state.go("planGestion@home");
        }
        if ($rootScope.editModeAccion) {
            $rootScope.unidadesDeAccionUnselect = $rootScope.unidadesDeAccionUnselect.filter(servicioUnselect =>
                !$rootScope.accion.unidadesDeAccionSelect.some(servicioSelect =>
                    servicioSelect.id === servicioUnselect.id));
        }
        $scope.backToAccion = function () {
            $state.go("mantenedor@Accion");
        }
        $scope.backToPlan = function () {
            $rootScope.accion = null;
            $state.go("planGestion@home");
        }

        $scope.selUnidadAccion = function (item) {
            //console.log(item)
            $rootScope.unidadesDeAccionUnselect = $rootScope.unidadesDeAccionUnselect.filter(unidad => unidad.id !== item.id);
            $rootScope.unidadesDeAccionUnselect.sort((a, b) => a.direccion.localeCompare(b.direccion));
            if (!$rootScope.accion.unidadesDeAccionSelect) {
                $rootScope.accion.unidadesDeAccionSelect = [];
            }
            $rootScope.accion.unidadesDeAccionSelect.push(item);
            $rootScope.accion.unidadesDeAccionSelect.sort((a, b) => a.direccion.localeCompare(b.direccion));
        }

        $scope.unSelUnidadAccion = function (item) {
            $rootScope.accion.unidadesDeAccionSelect = $scope.accion.unidadesDeAccionSelect.filter(unidad => unidad.id !== item.id);
            $scope.accion.unidadesDeAccionSelect.sort((a, b) => a.direccion.localeCompare(b.direccion));
            $rootScope.unidadesDeAccionUnselect.push(item);
            $rootScope.unidadesDeAccionUnselect.sort((a, b) => a.direccion.localeCompare(b.direccion));
        }

        $scope.goToS3 = function () {
            $state.go("mantenedor3@Accion");
        }


    })
    .controller("accion3Controller", function ($scope, $rootScope, $http, $ngConfirm, $planGestionModule, $state) {
        $scope.modoVer = $rootScope.modoVerAccion === true; // Heredar estado de solo lectura
        
        console.log($rootScope.accion);
        if (!$rootScope.accion) {
            $state.go("planGestion@home");
            return;
        }

        $rootScope.accion.nivelMedida = "Implementacion";

        if (!$rootScope.accion.cobertura) {
            $rootScope.accion.cobertura = "Edificio";
        }
        if (!$rootScope.accion.nivelMedida) {
            $rootScope.accion.nivelMedida = "Implementacion";
        }
        if (!$rootScope.accion.otroServicio) {
            $rootScope.accion.otroServicio = "No";
            $scope.serviciosUnselect = [];
        } else if ($rootScope.accion.otroServicio == "Si") {
            $planGestionModule.getOtrosServicios($rootScope.selectedServicioId).then(function (resp) {
                $scope.serviciosUnselect = resp.data.filter(servicio => $rootScope.accion.serviciosSelected
                    .some(servicioSeleccionado => servicioSeleccionado.id != servicio.id));
            });
        }

        if (!$rootScope.accion.serviciosSelected) {
            $rootScope.accion.serviciosSelected = [];
        }

        if ($rootScope.accion.brechaPorUnidad) {
            $scope.btnText = "Siguiente"
        } else {
            $scope.btnText = "Guardar"
        }

        if ($rootScope.editModeAccion) {
            $planGestionModule.getOtrosServicios($rootScope.selectedServicioId).then(function (resp) {
                $scope.serviciosUnselect = resp.data.filter(servicio => $rootScope.accion.servicios
                    .some(servicioSeleccionado => servicioSeleccionado.id != servicio.id));
            });

            $rootScope.accion.serviciosSelected = $rootScope.accion.servicios;

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

        $scope.changeFileDocumentoAccion = async function () {

            const file = document.querySelector("#documentoAccion").files[0];
            let b64File = await toBase64(file);
            var base64result = b64File.split(',')[1];
            $scope.accion.adjunto = base64result
            $scope.accion.adjuntoNombre = file.name
        }

        $planGestionModule.getUsuariosByServicioId($rootScope.selectedServicioId).then(function (resp) {
            //console.log(resp);
            $scope.gestores = resp.data;
        });

        $scope.rdOtroServicioSi = function () {
            $rootScope.accion.serviciosSelected = [];
            $planGestionModule.getOtrosServicios($rootScope.selectedServicioId).then(function (resp) {
                //console.log(resp);
                $scope.serviciosUnselect = resp.data;
            });
        }

        $scope.rdOtroServicioNo = function () {

            $rootScope.accion.serviciosSelected = [];
        }

        $scope.selServicio = function (item) {
            //console.log(item)
            $scope.serviciosUnselect = $scope.serviciosUnselect.filter(servicio => servicio.id !== item.id);
            $scope.serviciosUnselect.sort((a, b) => a.nombre.localeCompare(b.nombre));
            if (!$rootScope.accion.serviciosSelected) {
                $rootScope.accion.serviciosSelected = [];
            }
            $rootScope.accion.serviciosSelected.push(item);
            $rootScope.accion.serviciosSelected.sort((a, b) => a.nombre.localeCompare(b.nombre));
        }

        $scope.unSelServicio = function (item) {
            $rootScope.accion.serviciosSelected = $rootScope.accion.serviciosSelected.filter(servicio => servicio.id !== item.id);
            $rootScope.accion.serviciosSelected.sort((a, b) => a.nombre.localeCompare(b.nombre));
            $scope.serviciosUnselect.push(item);
            $scope.serviciosUnselect.sort((a, b) => a.nombre.localeCompare(b.nombre));
        }

        $scope.backToPlan = function () {
            $rootScope.accion = null;
            $state.go("planGestion@home");
        }

        $scope.goToS4 = function () {

            if ($rootScope.accion.brechaPorUnidad) {
                $state.go("mantenedor4@Accion");
            } else {
                //TODO Guardar
                if ($rootScope.editModeAccion) {
                    $planGestionModule.editAccion($rootScope.accion).then(function (resp) {
                        //console.log(resp);
                        $ngConfirm({
                            title: 'Editar Registro',
                            content: '<p>Registro guardado correctamente<p>',
                            buttons: {
                                Cancelar: {
                                    text: "Ok",
                                    btnClass: "btn btn-default",
                                    action: function () {
                                        $rootScope.accion = null;
                                        $state.go("planGestion@home");
                                    }
                                }
                            }
                        });
                    });
                } else {
                    $planGestionModule.saveAccion($rootScope.accion).then(function (resp) {
                        //console.log(resp);
                        $ngConfirm({
                            title: 'Guardar Registro',
                            content: '<p>Registro guardado correctamente<p>',
                            buttons: {
                                Cancelar: {
                                    text: "Ok",
                                    btnClass: "btn btn-default",
                                    action: function () {
                                        $rootScope.accion = null;
                                        $state.go("planGestion@home");
                                    }
                                }

                            }
                        });
                    });
                }
            }
            
        }
        $scope.backToAccion2 = function () {
            if ($rootScope.accion.brechaPorUnidad) {
                $state.go("mantenedor2@Accion");
            } else {
                $state.go("mantenedor@Accion");
            }
            
        }
    })
    .controller("accion4Controller", function ($scope, $rootScope, $http, $ngConfirm, $planGestionModule, $state) {
        $scope.modoVer = $rootScope.modoVerAccion === true; // Heredar estado de solo lectura

        if (!$rootScope.accion) {
            $state.go("planGestion@home");
        }

        $scope.backToPlan = function () {
            $rootScope.accion = null;
            $state.go("planGestion@home");
        }

        $scope.backToAccion3 = function () {
            $state.go("mantenedor3@Accion");
        }

        $scope.saveAccion = function () {
            $scope.isLoadingSaveAccion = true;
            if ($rootScope.editModeAccion) {
                $planGestionModule.editAccion($rootScope.accion).then(function (resp) {
                    $scope.isLoadingSaveAccion = false;
                    //console.log(resp);
                    $ngConfirm({
                        title: 'Editar Registro',
                        content: '<p>Registro guardado correctamente<p>',
                        buttons: {
                            Cancelar: {
                                text: "Ok",
                                btnClass: "btn btn-default",
                                action: function () {
                                    $rootScope.accion = null;
                                    $state.go("planGestion@home");
                                }
                            }
                        }
                    });
                });
            } else {
                $planGestionModule.saveAccion($rootScope.accion).then(function (resp) {
                    $scope.isLoadingSaveAccion = false;
                    //console.log(resp);
                    $ngConfirm({
                        title: 'Guardar Registro',
                        content: '<p>Registro guardado correctamente<p>',
                        buttons: {
                            Cancelar: {
                                text: "Ok",
                                btnClass: "btn btn-default",
                                action: function () {
                                    $rootScope.accion = null;
                                    $state.go("planGestion@home");
                                }
                            }

                        }
                    });
                });
            }

        };

        const toBase64 = file => new Promise((resolve, reject) => {
            if (file == null) {
                return;
            }
            const reader = new FileReader();
            reader.readAsDataURL(file);
            reader.onload = () => resolve(reader.result);
            reader.onerror = reject;
        });

        $scope.changeFileDocumentoAccion = async function () {

            const file = document.querySelector("#documentoAccion").files[0];
            let b64File = await toBase64(file);
            var base64result = b64File.split(',')[1];
            $scope.accion.adjunto = base64result
            $scope.accion.adjuntoNombre = file.name
        }

    });

app.filter('truncate', function () {
    return function (input, limit) {
        if (!input) return '';
        if (input.length <= limit) return input;
        return input.substring(0, limit) + '...';
    };
});

app.directive('fileMaxSize', function () {
    return {
        restrict: 'A',
        link: function (scope, element, attrs) {
            var maxSize = parseInt(attrs.fileMaxSize, 10); // Tamaño máximo en bytes
            element.bind('change', function (event) {
                var file = event.target.files[0];
                if (file && file.size > maxSize) {
                    scope.$apply(function () {
                        scope.fileSizeError = true;
                    });
                } else {
                    scope.$apply(function () {
                        scope.fileSizeError = false;
                    });
                }
            });
        }
    };
});