var app = angular.module('adminDiagnosticoApp', ['ui.router', 'cp.ngConfirm', 'ServicesModule'])
    .controller("adminDiagnosticoController", function ($rootScope, $state, $http) {
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

        $state.go("adminDiagnostico@home");
    }).controller("homeDiagnosticoController", function ($scope, $rootScope, $http, $ngConfirm, $serviciosModule) {
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


        $scope.aprobar =function(){
            $ngConfirm({
                title: 'Aprobar diagónstico',
                content: '<p>¿Desea aprobar la Revisión del Diagnóstico de Gestión Ambiental?<p>',
                buttons: {
                    Cancelar: {
                        text: "Cancelar",
                        btnClass: "btn btn-default"
                    },
                    Aprobar: {
                        text: "Aprobar",
                        btnClass: "btn btn-default",
                        action: function () {
                            $serviciosModule.setDiagnosticoAmbiental($rootScope.selectedServicioId).then(resp => {
                                loadData();
                            });
                        }
                    }

                }
            });
        }

        function loadData() {
            $serviciosModule.getDiagnostico($rootScope.selectedServicioId).then(resp => {
                //console.log(resp.data);
                $scope.diagnostico.revisionDiagnosticoAmbiental = resp.data;
            });
        }
    });