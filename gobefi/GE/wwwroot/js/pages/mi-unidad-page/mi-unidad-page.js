var app = angular.module("miUnidadApp", ["ui.router"])
    .controller("miUnidadController", function ($rootScope,$state) {
        var userId = angular.element("#input-id").val();
        $rootScope.userId = userId;
        $state.go("miUnidad@home");

    })
    .controller("homeMiUnidadController", function ($scope, $rootScope, $http, $timeout, $state) {
        $scope.unidades = [];
        $scope.loading = false;
        $scope.institucionId = '';
        $scope.regionId = '';
        $scope.servicioId = '';
        $scope.data = {};
        $scope.loading = true;

        $http.get(`/api/unidad/getasociadosbyuser/${$rootScope.userId}`)
            .then(function (result) {
                $scope.loading = false;
                $scope.unidades = result.data;
                intitDataTable($timeout);
            });


        $scope.changeInstitucion = function (institucionId) {
            $scope.institucionId = institucionId;
            $scope.getFilter();
        };
        $scope.changeServicio = function (servicioId) {
            $scope.servicioId = servicioId;
            $scope.getFilter();
        };
        $scope.changeRegion = function (regionId) {
            $scope.regionId = regionId;
            $scope.getFilter();
        };
        $scope.newClick = function () {
            console.log("newClick");
            $state.go("test");
        };
        $scope.backClick = function () {
            console.log("backClick");
        };

        $scope.getFilter = function () {
            $scope.loading = true;
            $http({
                method: 'GET',
                url: `/api/unidad/getbyfilter?userId=${$scope.userId}&institucionId=${$scope.institucionId}&servicioId=${$scope.servicioId}&regionId=${$scope.regionId}`,
            }).then(function (response) {
                $scope.loading = false;
                $scope.unidades = response.data;
                intitDataTable($timeout);
            });
        };

        function intitDataTable($timeout) {
            $('#unidadesTable').DataTable().destroy();
            $timeout(function () {
                //console.log(vm.unidades)
                $('#unidadesTable').DataTable({
                    "searching": true,
                    "lengthChange": true,
                    "language": {
                        "sProcessing": "Procesando...",
                        "sLengthMenu": "Mostrar _MENU_ registros",
                        "sZeroRecords": "No se encontraron resultados",
                        "sEmptyTable": "Ningún dato disponible en esta tabla",
                        "sInfo": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
                        "sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
                        "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
                        "sInfoPostFix": "",
                        "sSearch": "Buscar:",
                        "sUrl": "",
                        "sInfoThousands": ".",
                        "sLoadingRecords": "Cargando...",
                        "oPaginate": {
                            "sFirst": "Primero",
                            "sLast": "Último",
                            "sNext": "Siguiente",
                            "sPrevious": "Anterior"
                        }
                    }
                });
            }, 200);
        }
    });