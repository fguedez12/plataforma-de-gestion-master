var app = angular.module('actualizacionUnidadApp', ['ui.router', 'cp.ngConfirm','ServicesModule'])
    .controller("actualizacionUnidadController", function ($rootScope, $state, $http) {
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
        $state.go("actualizacionUnidad@home");
       
    })
    .controller("homeActualizacionUnidadController", function ($scope, $rootScope, $http, $ngConfirm, $institucionModule, $serviciosModule, $divisionModule) {
        
        $scope.data = {}
        $scope.instituciones = []
        $scope.servicios = []
        
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

        $scope.changeSinRol = function () {

            if (!$scope.data.sinRol) {
                $scope.data.justificaRol=null
            }
        }

        $scope.changeAccesoFacturaAgua = function () {
            
            if ($scope.data.accesoFacturaAgua == 2) {
                $institucionModule.getList().then(function (response) {
                    $scope.instituciones = response.data
                })
            } else {
                $scope.data.institucionResponsableAguaId = null;
                $scope.data.servicioResponsableAguaId = null;
            }
        }

        $scope.changeInstitucion = function () {
            //console.log($scope.data.institucionResponsableAguaId)
            if ($scope.data.institucionResponsableAguaId) {
                $serviciosModule.getByInstitucionId($scope.data.institucionResponsableAguaId).then(function (response) {
                    console.log(response)
                    $scope.servicios = response.data;
                })
            }
        }

        $scope.submitForm = function () {
            console.log($scope.data)
            $divisionModule.patchDivision($rootScope.selectedUnidadId, $scope.data).then(function (response) {
                console.log(response)
                $ngConfirm({
                    title: 'Actualizar',
                    content: '<p>Registro guardado correctamente<p>',
                    buttons: {
                        Cancelar: {
                            text: "Ok",
                            btnClass: "btn btn-default",
                            action: function () {
                                window.location.href = `/AdminEstadoVerde/index/${$scope.selectedUnidadId}`
                            }
                        }

                    }
                });
            })
        }

        function loadData() {
            if (!$rootScope.selectedUnidadId) {
                window.location.href = '/';
                return
            };

            $http({
                method: 'GET',
                url: `${$rootScope.APIURL}/api/divisiones/actualizacion-unidad/${$rootScope.selectedUnidadId}`,
            }).then(function (response) {
                console.log(response.data)    
                $scope.data = response.data;
                $scope.changeAccesoFacturaAgua();
                $scope.changeInstitucion();
                //$(".chosen-select").chosen({ no_results_text: "No se encuentra la patente" });
            });

        }
    })