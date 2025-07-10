var app = angular.module('adminAgregadaApp', ['ui.router', 'cp.ngConfirm', 'ServicesModule'])
    .controller("adminAgregadaController", function ($rootScope, $state, $http) {
        var userId = angular.element("#input-id").val();
        var isAdmin = angular.element("#input-is-admin").val();
        const isConsulta = angular.element("#input-is-consulta").val();
        $rootScope.userId = userId;
        $rootScope.isAdmin = isAdmin == 'False' ? false : true;
        $rootScope.isConsulta = isConsulta == 'False' ? false : true;
        $rootScope.selectedUnidadNombre = localStorage.getItem('divisionName');
        $rootScope.selectedUnidadId = localStorage.getItem('divisionId');
        $rootScope.selectedInstitucionId = localStorage.getItem('institucionId');
        $rootScope.selectedServicioId = localStorage.getItem('servicioEvId');
        $rootScope.selectedServicioNombre = localStorage.getItem('servicioEvNombre');
        $state.go("adminAgregada@home");
    }).controller("homeAgregadaController", function ($scope, $rootScope, $http, $ngConfirm, $serviciosModule) {
        $http({
            method: 'GET',
            url: `/settings?sectionName=ApiConfiguration&paramName=apiGestionaEnergia`

        }).then(function (response) {
            const did = localStorage.getItem('divisionId');
            $scope.gestion = {};
            $scope.gestion.divisionId = did;
            $rootScope.APIURL = response.data;
            loadData();
            /*setServicioIdInsitutcionId();*/
        });

        $scope.submitForm = function () {
            $serviciosModule.setColaboradoresModAlcance($rootScope.selectedServicioId, $scope.agregada.colaboradoresModAlcance, $scope.agregada.modificacioAlcance)
                .then(function(response) {
                    console.log(response)
                    $ngConfirm({
                        title: 'Actualizar',
                        content: '<p>Registro guardado correctamente<p>',
                        buttons: {
                            Cancelar: {
                                text: "Ok",
                                btnClass: "btn btn-default"
                            }

                        }
                    });
                })
        }

        function loadData() {
            $http({
                method: 'GET',
                url: `${$rootScope.APIURL}/api/agregada/${$rootScope.selectedServicioId}`
            }).then(function (response) {
                //console.log(response);
                $scope.agregada = response.data;
            });
        }
    });