var app = angular.module('adminResiduosApp', ['ui.router', 'cp.ngConfirm', 'ServicesModule'])
    .controller("adminResiduosController", function ($rootScope, $state, $http) {
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
        $state.go("adminResiduos@home");
    })
    .controller("homeResiduosController", function ($scope, $rootScope, $http, $ngConfirm, $serviciosModule) {
        /*Paramters*/
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
        });
        /*Variables*/
        $scope.loadingTableResiduos = false;
        $scope.loadingTableResiduosNoReciclados = false;
        $scope.loadingTableContenedores = false;
        $scope.residuos = [];
        $scope.residuosNoReciclados = [];
        $scope.contenedores = [];
        $scope.pageResiduos = 1;
        $scope.pageResiduosNoReciclados = 1;
        $scope.pageContenedores = 1;
        $scope.editMode = false;
        $scope.arrayPagesResiduos = [];
        $scope.arrayPagesResiduosNoReciclados = [];
        $scope.arrayPagesContenedores = [];
        $scope.tipoResiduos = [];
        $scope.observa = {};
        $scope.observa.checkObserva = false;
        $scope.observa.observacion = "";
        $scope.reporta = {};
        $scope.reporta.checkReporta = true;
        $scope.reporta.justificacion = "";
        $scope.editText = "Editar";
        $scope.procedimientos = [];
        $scope.editResiduoEnable = false;
        $scope.selectedYear = new Date().getFullYear().toString();
        $scope.selectedYearRes = new Date().getFullYear().toString();

        if ($rootScope.isConsulta) {
            $scope.editText = "Ver";
        } else {
            $scope.editText = "Editar";
        }

        /*Methods*/

        $scope.changeYear = function () {
            loadData();
        }

        $scope.saveObservacion = function () {
            //console.log("save");
            //console.log($scope.justificacion);
            $http({
                method: 'PUT',
                url: `${$rootScope.APIURL}/api/divisiones/observa-residuos/${$rootScope.selectedUnidadId}`,
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
        }

        $scope.saveJustificacion = function () {
            //console.log($scope.reporta);
            $http({
                method: 'PUT',
                url: `${$rootScope.APIURL}/api/divisiones/reporta-residuos/${$rootScope.selectedUnidadId}`,
                data: JSON.stringify($scope.reporta)

            }).then(function (response) {
                //console.log("response")
                $ngConfirm({
                    title: 'Guardar información',
                    content: '<p>Resgitro guardado correctamente<p>',
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

        $scope.saveJustificacionNoReciclados = function () {
            //console.log($scope.reporta);
            $http({
                method: 'PUT',
                url: `${$rootScope.APIURL}/api/divisiones/reporta-residuos-no-reciclados/${$rootScope.selectedUnidadId}`,
                data: JSON.stringify($scope.reporta)

            }).then(function (response) {
                //console.log("response")
                $ngConfirm({
                    title: 'Guardar información',
                    content: '<p>Resgitro guardado correctamente<p>',
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

        $scope.changeNoDeclaraContenedores = function () {
            console.log("No declara");
            $http({
                method: 'PUT',
                url: `${$rootScope.APIURL}/api/residuos/no-declara/${$rootScope.selectedUnidadId}?value=${$scope.noDeclaraContenedores}`,
            }).then(function (response) {
                console.log("ok");
            });
        }


        $scope.showModal = function () {
            $scope.editResiduoEnable = false;
            $scope.gestion = {};
            $scope.modalTitle = "Nueva Gestión";
            loadProcedimientos();
            $("#modal-form").modal("show");
        };

        $scope.closeModal = function () {
            $scope.editResiduoEnable = false;
            closeModal();
        };


        $scope.changeTipo = function () {
            if ($scope.gestion.tipoId == "3") {
                $scope.gestion.tipoResiduo = "Residuos NO reciclados";
            } else {
                $scope.gestion.tipoResiduo = "";
            }
        }


        $scope.submitForm = function () {

            const did = localStorage.getItem('divisionId');
            $scope.gestion.divisionId = did;

            if ($scope.gestion.tipoId == "1" || $scope.gestion.tipoId == "3" ) {
                if ($scope.editMode) {
                    const url = `${$rootScope.APIURL}/api/residuos/${$scope.gestion.id}`;
                    const method = 'PUT';
                    submitForm(url, method);
                } else {
                    const url = `${$rootScope.APIURL}/api/residuos`;
                    const method = 'POST';
                    submitForm(url, method);
                }
            } else {
                if ($scope.editMode) {
                    const url = `${$rootScope.APIURL}/api/contenedores/${$scope.gestion.id}`;
                    const method = 'PUT';
                    submitForm(url, method);
                } else {
                    const url = `${$rootScope.APIURL}/api/contenedores`;
                    const method = 'POST';
                    submitForm(url, method);
                }
            }
        };

        $scope.setPageResiduos = function (page) {
            $scope.pageResiduos = page;
            loadData();
        };

        $scope.setPageResiduosNoReciclados = function (page) {
            $scope.pageResiduosNoReciclados = page;
            loadData();
        };

        $scope.prevPageResiduos = function () {

            if ($scope.pageResiduos > 1) {
                $scope.pageResiduos = $scope.pageResiduos - 1;
                loadData();
            }
        };

        $scope.prevPageResiduosNoReciclados = function () {

            if ($scope.pageResiduosNoReciclados > 1) {
                $scope.pageResiduosNoReciclados = $scope.pageResiduosNoReciclados - 1;
                loadData();
            }
        };

        $scope.nextPageResiduos = function () {

            if ($scope.pageResiduos < $scope.arrayPagesResiduos[$scope.arrayPagesResiduos.length - 1]) {
                $scope.pageResiduos = $scope.pageResiduos + 1;
                loadData();
            }
        };

        $scope.nextPageResiduosNoReciclados = function () {

            if ($scope.pageResiduosNoReciclados < $scope.arrayPagesResiduosNoReciclados[$scope.arrayPagesResiduosNoReciclados.length - 1]) {
                $scope.pageResiduosNoReciclados = $scope.pageResiduosNoReciclados + 1;
                loadData();
            }
        };

        $scope.setPageContenedores = function (page) {
            $scope.pageContenedores = page;
            loadData();
        };

        $scope.prevPageContenedores = function () {

            if ($scope.pageContenedores > 1) {
                $scope.pageContenedores = $scope.pageContenedores - 1;
                loadData();
            }
        };

        $scope.nextPageContenedores = function () {

            if ($scope.pageContenedores < $scope.arrayPagesContenedores[$scope.arrayPagesContenedores.length - 1]) {
                $scope.pageContenedores = $scope.pageContenedores + 1;
                loadData();
            }
        };

        $scope.editResiduo = function (residuoId) {
            $http({
                method: 'GET',
                url: `${$rootScope.APIURL}/api/residuos/get-by-id/${residuoId}`,
            }).then(function (response) {
                if (response.data.ok) {
                    $scope.editResiduoEnable = true;
                    $scope.editMode = true;
                    $scope.gestion = response.data.residuo;
                    console.log($scope.gestion);
                    if ($scope.gestion.tipoResiduo == "Residuos NO reciclados") {
                        $scope.gestion.tipoId = "3";
                    } else if ($scope.gestion.tipoResiduo == "Contenedores") {
                        $scope.gestion.tipoId = "2";
                    }
                    else {
                        $scope.gestion.tipoId = "1";
                    }
                   
                   
                    if (!$scope.gestion.ingresoAgregado) {
                        $scope.gestion.fecha = new Date(response.data.residuo.fecha);
                    };

                    if (response.data.residuo.procedimientoId) {
                        $scope.gestion.procedimientoId = response.data.residuo.procedimientoId.toString();
                    }
                    loadProcedimientos();
                    $scope.modalTitle = "Editar Gestión";
                    $("#modal-form").modal("show");
                }
            });
        };

        $scope.editContenedor = function (contenedorId) {
            $http({
                method: 'GET',
                url: `${$rootScope.APIURL}/api/contenedores/get-by-id/${contenedorId}`,
            }).then(function (response) {
                if (response.data.ok) {
                    
                    $scope.editMode = true;
                    $scope.gestion = response.data.contenedor;
                    $scope.gestion.tipoId = "2";
                    $scope.modalTitle = "Editar Gestión";
                    $("#modal-form").modal("show");
                }

            });
        };

        $scope.deleteResiduo = function (id) {
            $ngConfirm({
                title: 'Eliminar Residuo',
                content: '<p>¿Esta seguro de eliminar el Residuo?<p>',
                buttons: {
                    Ok: {
                        text: "Ok",
                        btnClass: "btn btn-danger",
                        action: function () {
                            $http({
                                method: 'DELETE',
                                url: `${$rootScope.APIURL}/api/residuos/${id}`
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

        $scope.deleteContenedor = function (id) {
            $ngConfirm({
                title: 'Eliminar Contenedor',
                content: '<p>¿Esta seguro de eliminar el Contenedor?<p>',
                buttons: {
                    Ok: {
                        text: "Ok",
                        btnClass: "btn btn-danger",
                        action: function () {
                            $http({
                                method: 'DELETE',
                                url: `${$rootScope.APIURL}/api/contenedores/${id}`
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
                    if (response.data.ok) {
                        localStorage.setItem('divisionName', response.data.division.direccion);
                        $rootScope.selectedUnidadNombre = response.data.division.direccion;
                        loadData();
                    }



                });
            }
        };


        /*Validations*/

        var anio = document.querySelector('#anio');

        anio.addEventListener('input', function () {
            var num = this.value.match(/^\d+$/);
            if (num === null) {
                this.value = "";
            }
        });

        var cantidad = document.querySelector('#cantidad');

        cantidad.addEventListener('input', function () {
            var num = this.value.match(/^\d+$/);
            if (num === null) {
                this.value = "";
            }
        });

        var nRecipientes = document.querySelector('#nRecipientes');

        nRecipientes.addEventListener('input', function () {
            var num = this.value.match(/^\d+$/);
            if (num === null) {
                this.value = "";
            }
        });

       
        /*Functions*/

        function getObservacion() {
            $http({
                method: 'GET',
                url: `${$rootScope.APIURL}/api/divisiones/observacion-residuos/${$rootScope.selectedUnidadId}`,
            }).then(function (response) {
                $scope.observa.observacion = response.data.observacion;
                $scope.observa.checkObserva = response.data.checkObserva;
            });

        }


        function getJustificacion() {
            $http({
                method: 'GET',
                url: `${$rootScope.APIURL}/api/divisiones/reporta-residuos/${$rootScope.selectedUnidadId}`,
            }).then(function (response) {
                console.log(response.data);
                $scope.reporta.checkReporta = response.data.checkReporta;
                $scope.reporta.justificacion = response.data.justificacion;
                $scope.reporta.checkReportaNoReciclados = response.data.checkReportaNoReciclados;
                $scope.reporta.justificacionNoReciclados = response.data.justificacionNoReciclados;
            });

        }




        function closeModal() {
            $scope.gestion = {};
            $scope.editMode = false;
            $scope.residuosForm.$setPristine();
            $scope.residuosForm.$setUntouched()
            $("#modal-form").modal("hide");
        };


        function submitForm(url, method) {
            $http({
                method: method,
                url: url,
                data: JSON.stringify($scope.gestion)
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
            if (!did) {
                window.location.href = '/';
            };
            $scope.loadingTableResiduos = true;


            let years = [];
            let actualY = new Date().getFullYear();
            years.push(actualY);
            for (let i = 0; i <= 3; i++) {
                actualY = actualY - 1
                years.push(actualY);
            }
            $scope.years = years;


            $http({
                method: 'GET',
                url: `${$rootScope.APIURL}/api/residuos?page=${$scope.pageResiduos}&divisionId=${did}&anioDoc=${$scope.selectedYearRes}`,
            }).then(function (response) {
                $scope.loadingTableResiduos = false;
                if (response.data.ok) {
                    let totalRecords = parseInt(response.headers('totalrecords'));
                    let pages = Math.ceil(totalRecords / 5);
                    const arraypages = Array(pages).fill(1).map((n, i) => n + i)
                    if (arraypages.length > 15) {
                        if ($scope.pageResiduos > 1) {
                            arraypages.splice(0, $scope.pageResiduos - 1);
                        }
                        arraypages.splice(15, arraypages.length - 15);
                    }
                    $scope.arrayPagesResiduos = arraypages;
                    $scope.residuos = response.data.residuos;
                    $scope.noDeclaraContenedores = response.data.noDeclaraContenedores;
                    getObservacion();
                    getJustificacion();

                }
            });
            $scope.loadingTableResiduosNoReciclados = true;
            $http({
                method: 'GET',
                url: `${$rootScope.APIURL}/api/residuos/no-reciclados?page=${$scope.pageResiduosNoReciclados}&divisionId=${did}&anioDoc=${$scope.selectedYear}`,
            }).then(function (response) {
                $scope.loadingTableResiduosNoReciclados = false;
                if (response.data.ok) {
                    let totalRecords = parseInt(response.headers('totalrecords'));
                    let pages = Math.ceil(totalRecords / 5);
                    const arraypages = Array(pages).fill(1).map((n, i) => n + i)
                    if (arraypages.length > 15) {
                        if ($scope.pageResiduosNoReciclados > 1) {
                            arraypages.splice(0, $scope.pageResiduosNoReciclados - 1);
                        }
                        arraypages.splice(15, arraypages.length - 15);
                    }
                    $scope.arrayPagesResiduosNoReciclados = arraypages;
                    $scope.residuosNoReciclados = response.data.residuos;
                }
            });
            $scope.loadingTableContenedores = true;
            $http({
                method: 'GET',
                url: `${$rootScope.APIURL}/api/contenedores?page=${$scope.pageContenedores}&divisionId=${did}`,
            }).then(function (response) {
                $scope.loadingTableContenedores = false;
                if (response.data.ok) {
                    let totalRecords = parseInt(response.headers('totalrecords'));
                    let pages = Math.ceil(totalRecords / 5);
                    const arraypages = Array(pages).fill(1).map((n, i) => n + i)
                    if (arraypages.length > 15) {
                        if ($scope.pageContenedores > 1) {
                            arraypages.splice(0, $scope.pageContenedores - 1);
                        }
                        arraypages.splice(15, arraypages.length - 15);
                    }
                    $scope.arrayPagesContenedores = arraypages;
                    $scope.contenedores = response.data.contenedores;
                }
            });

            loadServicios();
            loadUnidades();
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

        function loadProcedimientos() {
            $http({
                method: 'GET',
                url: `${$rootScope.APIURL}/api/residuos/procedimientos-residuos/${$rootScope.selectedUnidadId}`,
            }).then(function (response) {
                //console.log(response.data);
                $scope.procedimientos = response.data;
            });
        }
    });