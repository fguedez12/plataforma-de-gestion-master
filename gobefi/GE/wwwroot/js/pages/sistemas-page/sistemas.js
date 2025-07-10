var app = angular.module('sistemasApp', ['ui.router', 'cp.ngConfirm', 'ServicesModule'])
    .controller("sistemasController", function ($rootScope, $state) {
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
        $rootScope.selectedUnidadNombre = localStorage.getItem('divisionName');
        $rootScope.selectedUnidadId = localStorage.getItem('divisionId');
        $rootScope.selectedInstitucionId = localStorage.getItem('institucionId');
        $rootScope.selectedServicioId = localStorage.getItem('servicioId');
        $state.go("sistemas@home");
    })
    .controller("homeSistemasController", function ($scope, $rootScope, $http, $ngConfirm,$sistemasModule) {

        if (!$rootScope.lectura) {
            window.location.href = `/miunidad/ver/${$scope.selectedUnidadId}`
        }

        $scope.data = {};
        $http({
            method: 'GET',
            url: `/settings?sectionName=ApiConfiguration&paramName=apiGestionaEnergia`
        }).then(function (response) {
            $rootScope.APIURL = response.data;
            loadData();
        });
        $scope.submitForm = function () {
            //console.log("Submitted");
            $sistemasModule.saveSistemas($rootScope.selectedUnidadId, $scope.data).then(resp => {
                $ngConfirm({
                    title: 'Guardar',
                    content: '<p>Registro guardado correctamente<p>',
                    buttons: {
                        Cancelar: {
                            text: "Ok",
                            btnClass: "btn btn-default",
                            action: function () {
                                window.location.href = `/miunidad/ver/${$scope.selectedUnidadId}`
                            }
                        }

                    }
                });
            })
        }

        $scope.changeEquipoCalefaccion = function () {
            $scope.data.energeticosEquipo = []
            $scope.data.energeticoCalefaccionId = null;
            $scope.data.tempSeteoCalefaccionId = null;
            //console.log($scope.data.equipoCalefaccionId);
            loadEquipoCalefaccion()
        }

        function loadEquipoCalefaccion() {
            if ($scope.data.equipoCalefaccionId > 1) {
                $sistemasModule.getEnergeticosByEquipoId($scope.data.equipoCalefaccionId).then(resp => {
                    //console.log(resp)
                    $scope.data.energeticosEquipo = resp.data;
                })
            }
        }

        $scope.changeEquipoRefrigeracion = function () {
            $scope.data.energeticosEquipoRefrigeracion = []
            $scope.data.energeticoRefrigeracionId = null;
            $scope.data.tempSeteoRefrigeracionId = null;
            //console.log($scope.data.equipoCalefaccionId);
            loadEquipoRefrigeracion();
        }
        function loadEquipoRefrigeracion() {
            if ($scope.data.equipoRefrigeracionId > 1) {
                $sistemasModule.getEnergeticosByEquipoId($scope.data.equipoRefrigeracionId).then(resp => {
                    //console.log(resp)
                    $scope.data.energeticosEquipoRefrigeracion = resp.data;
                })
            }
        }

        $scope.changeEquipoAcs = function () {
            $scope.data.energeticosEquipoAcs = []
            $scope.data.energeticoAcsId = null;
            //console.log($scope.data.equipoCalefaccionId);
            loadEquipoAcs();
        }

        function loadEquipoAcs() {
            if ($scope.data.equipoAcsId > 1) {
                $sistemasModule.getEnergeticosByEquipoId($scope.data.equipoAcsId).then(resp => {
                    //console.log(resp)
                    $scope.data.energeticosEquipoAcs = resp.data;
                })
            }
        }

        $scope.changeSistemaSolarTermico = function(){
            //console.log($scope.data.sistemaSolarTermico);
            loadSistemaSolarTermico();
        }

        function loadSistemaSolarTermico() {
            if ($scope.data.sistemaSolarTermico == true) {
                //console.log("si")
                $sistemasModule.getColectores("c").then(resp => {
                    //console.log(resp);
                    $scope.data.tiposColectores = resp.data;
                });
            } 
        }

        $scope.changeImpSisFv = function () {
            //console.log($scope.data.sistemaSolarTermico);
            loadImpSisFv();
        }
        function loadImpSisFv() {
            if ($scope.data.impSisFv == true) {
                //console.log("si")
                $sistemasModule.getColectores("sf").then(resp => {
                    //console.log(resp);
                    $scope.data.tiposColectoresSf = resp.data;
                });
            }
        }


        function loadData() {
            if (!$rootScope.selectedUnidadId) {
                window.location.href = '/';
                return
            };

            $sistemasModule.getData($rootScope.selectedUnidadId).then(function (resp) {
                //console.log(resp);
                $scope.data = resp.data;
                loadEquipoCalefaccion();
                loadEquipoRefrigeracion();
                loadEquipoAcs();
                loadSistemaSolarTermico();
                loadImpSisFv();
            });
        }

    });