var app = angular.module('adminPapelApp', ['ui.router', 'cp.ngConfirm', 'ServicesModule'])
    .controller("adminPapelController", function ($rootScope, $state, $http) {
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
        $state.go("adminPapel@home");
    })
    .controller("homePapelController", function ($scope, $rootScope, $http, $ngConfirm, $serviciosModule) {
        $http({
            method: 'GET',
            url: `/settings?sectionName=ApiConfiguration&paramName=apiGestionaEnergia`

        }).then(function (response) {
            const did = localStorage.getItem('divisionId');
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

       

        $scope.loadingTableImpresoras = false;
        $scope.loadingTableResmas = false;
        $scope.impresoras = [];
        $scope.resmas = [];
        $scope.page = 1;
        $scope.pageResmas = 1;
        $scope.editMode = false;
        $scope.observa = {};
        $scope.observa.checkObserva = false;
        $scope.observa.observacion = "";
        $scope.editText = "Editar";
        $scope.selectedYear = new Date().getFullYear().toString();

        if ($rootScope.isConsulta) {
            $scope.editText = "Ver";
        } else {
            $scope.editText = "Editar";
        }
        //console.log($scope.justificacion);

        //var numInput = document.querySelector('#potencia');

        //numInput.addEventListener('input', function () {
        //    var num = this.value.match(/^\d+$/);
        //    if (num === null) {
        //        this.value = "";
        //    }
        //});

        //var numInput = document.querySelector('#potencia');

        //numInput.addEventListener('input', function () {
        //    var num = this.value.match(/^\d+$/);
        //    if (num === null) {
        //        this.value = "";
        //    }
        //});

        $scope.changeYear = function () {
            loadData();
        }

        var numeroImpresiones = document.querySelector('#numeroImpresiones');

        numeroImpresiones.addEventListener('input', function () {
            var num = this.value.match(/^\d+$/);
            if (num === null) {
                this.value = "";
            }
        });


        var numInputCantidadResmas = document.querySelector('#cantidadResmas');

        numInputCantidadResmas.addEventListener('input', function () {
            var num = this.value.match(/^\d+$/);
            if (num === null) {
                this.value = "";
            }
        });

        var numInputCostoEstimado = document.querySelector('#costoEstimado');

        numInputCostoEstimado.addEventListener('input', function () {
            var num = this.value.match(/^\d+$/);
            if (num === null) {
                this.value = "";
            }
        });

        $scope.changeNoDeclaraImpresoras = function () {
            //console.log("No declara");
            $http({
                method: 'PUT',
                url: `${$rootScope.APIURL}/api/impresoras/no-declara/${$rootScope.selectedUnidadId}?value=${$scope.noDeclaraImpresoras}`,
            }).then(function (response) {
                console.log("ok");
            });
        }

        $scope.saveObservacion = function () {
            //console.log("save");
            //console.log($scope.justificacion);
            $http({
                method: 'PUT',
                url: `${$rootScope.APIURL}/api/divisiones/observacion-papel/${$rootScope.selectedUnidadId}`,
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

        $scope.showModal = function () {
            $scope.gestion = {};
            $scope.modalTitle = "Nueva Gestión";
            $("#modal-form").modal("show");
        }

        $scope.closeModal = function () {
            closeModal();
        }

        $scope.submitForm = function () {

            const did = localStorage.getItem('divisionId');
            $scope.gestion.divisionId = did;

            if ($scope.gestion.tipoId == "1") {
                if ($scope.editMode) {
                    const url = `${$rootScope.APIURL}/api/impresoras/${$scope.gestion.id}`;
                    const method = 'PUT';
                    submitForm(url, method);
                } else {
                    const url = `${$rootScope.APIURL}/api/impresoras`;
                    const method = 'POST';
                    submitForm(url, method);
                }
            } else {
                if (!$scope.gestion.agregada) {
                    $scope.gestion.agregada  = false;
                }
                if ($scope.editMode) {
                    const url = `${$rootScope.APIURL}/api/resmas/${$scope.gestion.id}`;
                    const method = 'PUT';
                    submitForm(url, method);
                } else {
                    const url = `${$rootScope.APIURL}/api/resmas`;
                    const method = 'POST';
                    submitForm(url, method);
                }
            }
        }

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

        $scope.setPageImpresoras = function (page) {
            $scope.page = page;
            loadData();
        }

        $scope.nextPageImpresoras = function () {

            if ($scope.page < $scope.arrayPages[$scope.arrayPages.length - 1]) {
                $scope.page = $scope.page + 1;
                loadData();
            }
        }

        $scope.prevPageImpresoras = function () {

            if ($scope.page > 1) {
                $scope.page = $scope.page - 1;
                loadData();
            }
        }

        $scope.setPageResmas = function (page) {
            $scope.pageResmas = page;
            loadData();
        }

        $scope.nextPageResmas = function () {

            if ($scope.pageResmas < $scope.arrayPagesResmas[$scope.arrayPagesResmas.length - 1]) {
                $scope.pageResmas = $scope.pageResmas + 1;
                loadData();
            }
        }

        $scope.prevPageResmas = function () {

            if ($scope.pageResmas > 1) {
                $scope.pageResmas = $scope.pageResmas - 1;
                loadData();
            }
        }

        $scope.edit = function (impresoraId) {
            $http({
                method: 'GET',
                url: `${$rootScope.APIURL}/api/impresoras/get-impresora-by-id/${impresoraId}`,
            }).then(function (response) {
                if (response.data.ok) {
                    $scope.editMode = true;
                    $scope.gestion = response.data.impresora;
                    $scope.gestion.tipoId = "1";
                    $scope.modalTitle = "Editar Gestión";
                    $("#modal-form").modal("show");
                }

            });
        };

        $scope.editResma = function (resmaId) {
            $http({
                method: 'GET',
                url: `${$rootScope.APIURL}/api/resmas/get-by-id/${resmaId}`,
            }).then(function (response) {
                if (response.data.ok) {
                    $scope.editMode = true;
                    $scope.gestion = response.data.resma;
                    $scope.gestion.fechaAdquisicion = new Date(response.data.resma.fechaAdquisicion);
                    $scope.gestion.tipoId = "2";
                    $scope.modalTitle = "Editar Gestión";
                    $("#modal-form").modal("show");
                }

            });
        };

        $scope.deleteImpresora = function (id) {
            $ngConfirm({
                title: 'Eliminar Impresora',
                content: '<p>¿Esta seguro de eliminar la impresora?<p>',
                buttons: {
                    Ok: {
                        text: "Ok",
                        btnClass: "btn btn-danger",
                        action: function () {
                            $http({
                                method: 'DELETE',
                                url: `${$rootScope.APIURL}/api/impresoras/${id}`
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

        $scope.deleteResma = function (id) {
            $ngConfirm({
                title: 'Eliminar Consumo de Papel',
                content: '<p>¿Esta seguro de eliminar el consumo de papel?<p>',
                buttons: {
                    Ok: {
                        text: "Ok",
                        btnClass: "btn btn-danger",
                        action: function () {
                            $http({
                                method: 'DELETE',
                                url: `${$rootScope.APIURL}/api/resmas/${id}`
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
        }

        function closeModal() {
            $scope.gestion = {};
            $scope.editMode = false;
            $scope.papelForm.$setPristine();
            $scope.papelForm.$setUntouched()
            $("#modal-form").modal("hide");
        }

        function getObservacion() {
            $http({
                method: 'GET',
                url: `${$rootScope.APIURL}/api/divisiones/observacion-papel/${$rootScope.selectedUnidadId}`,
            }).then(function (response) {
                $scope.observa.observacion = response.data.observacion;
                $scope.observa.checkObserva = response.data.checkObserva;
            });
                
        }

        function loadData() {
            const did = localStorage.getItem('divisionId');

            if (!did) {
                window.location.href = '/';
            }
            let years = [];
            let actualY = new Date().getFullYear();
            years.push(actualY);
            for (let i = 0; i <= 3; i++) {
                actualY = actualY -1
                years.push(actualY);
            }

            $scope.years = years;
            //console.log(years);

            $scope.loadingTableImpresoras = true;
            $scope.loadingTableResmas = true;
            $http({
                method: 'GET',
                url: `${$rootScope.APIURL}/api/impresoras?page=${$scope.page}&divisionId=${did}`,
            }).then(function (response) {
                $scope.loadingTableImpresoras = false;
                if (response.data.ok) {
                    let totalRecords = parseInt(response.headers('totalrecords'));
                    let pages = Math.ceil(totalRecords / 5);
                    const arraypages = Array(pages).fill(1).map((n, i) => n + i)
                    if (arraypages.length > 15) {
                        if ($scope.page > 1) {
                            arraypages.splice(0, $scope.page - 1);
                        }
                        arraypages.splice(15, arraypages.length - 15);
                    }
                    $scope.arrayPages = arraypages;
                    $scope.impresoras = response.data.impresoras;
                    $scope.noDeclaraImpresoras = response.data.noDeclaraImpresora;
                    getObservacion();
                }
                
            });

            $http({
                method: 'GET',
                url: `${$rootScope.APIURL}/api/resmas?page=${$scope.pageResmas}&divisionId=${did}&anioDoc=${$scope.selectedYear}`,
            }).then(function (response) {
                $scope.loadingTableResmas = false;
                if (response.data.ok) {
                    let totalRecords = parseInt(response.headers('totalrecords'));
                    let pages = Math.ceil(totalRecords / 5);
                    const arraypages = Array(pages).fill(1).map((n, i) => n + i)
                    if (arraypages.length > 15) {
                        if ($scope.pageResmas > 1) {
                            arraypages.splice(0, $scope.page - 1);
                        }
                        arraypages.splice(15, arraypages.length - 15);
                    }
                    $scope.arrayPagesResmas = arraypages;
                    $scope.resmas = response.data.resmas;
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

    });