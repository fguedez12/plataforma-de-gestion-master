var app = angular.module('reporteConsumoApp', ['ui.router', 'cp.ngConfirm'])
    .controller("reporteConsumoController", function ($rootScope, $state) {
        var userId = angular.element("#input-id").val();
        console.log(userId);
        $rootScope.userId = userId;
        var isConsulta = angular.element("#input-consulta").val();
        var isAdmin = angular.element("#input-admin").val();
        //console.log(isConsulta);
        $rootScope.isConsulta = isConsulta;
        $rootScope.isAdmin = isAdmin;
        $state.go("reporteConsumo@home");
    })
    .controller("homeReporteConsumoController", function ($scope, $rootScope, $http, $timeout, $state, $ngConfirm) {

       

        getData();

        $scope.showModal = function (item) {
            $scope.justifica = {};
            if (item.compromiso2022 == null) {
                $scope.justifica.check = true;
            } else {
                $scope.justifica.check = item.compromiso2022;
            }
           
            $scope.justifica.text = item.justificacion;
            $scope.justifica.compromiso2022 = item.compromiso2022;
            $scope.justifica.estadoCompromiso2022 = item.estadoCompromiso2022;
            $scope.justifica.observacionCompromiso2022 = item.observacionCompromiso2022;
            $scope.justifica.UnidadId = item.id;
            $scope.unidad = item;
            $scope.unidad.accesoFactura = item.accesoFactura.toString();
            //if (item.servicioResponsableId > 0) {
            //    $http.get(`/api/servicios/${item.servicioResponsableId}`)
            //        .then(function (result) {
            //            $scope.unidad.servicio = result.data.nombre;
            //            //console.log($scope.servicio);

            //        })
            //}
            
            //console.log($scope.unidad);
            angular.element("#modalJustifica").modal("show");
        };

        $scope.saveJustifica = function ($state) {
            $http({
                method: 'POST',
                url: '/api/divisiones/justificacion',
                data: JSON.stringify($scope.justifica)
            }).then(function (response) {
                angular.element("#modalJustifica").modal("hide");
                $scope.model.unidades = [];
                $timeout(function () { $('#reporteConsumosTable').DataTable().destroy(); },500)
                $('#reporteConsumosTable').DataTable().destroy();
                getData();
            });

        };

        $scope.observarJustificacion = function () {
            $http({
                method: 'POST',
                url: '/api/divisiones/observar-justificacion',
                data: JSON.stringify($scope.justifica)
            }).then(function (response) {
                angular.element("#modalJustifica").modal("hide");
                $scope.model.unidades = [];
                $timeout(function () { $('#reporteConsumosTable').DataTable().destroy(); }, 500)
                $('#reporteConsumosTable').DataTable().destroy();
                getData();
            });
        }

        $scope.validarJustificacion = function () {
            $http({
                method: 'POST',
                url: '/api/divisiones/validar-justificacion',
                data: JSON.stringify($scope.justifica)
            }).then(function (response) {
                angular.element("#modalJustifica").modal("hide");
                $scope.model.unidades = [];
                $timeout(function () { $('#reporteConsumosTable').DataTable().destroy(); }, 500)
                $('#reporteConsumosTable').DataTable().destroy();
                getData();
            });
        }

        function getData() {
            $scope.loading = true;
            $scope.model = null;
            $http.get(`/api/divisiones/reporte-consumo`)
                .then(function (result) {
                    $scope.loading = false;
                    console.log(result.data);
                    $scope.model = result.data;
                    intitDataTable($timeout);
                    
                });
        }

        function intitDataTable($timeout) {
            $('#reporteConsumosTable').DataTable().destroy();
            $timeout(function () {
                //console.log(vm.unidades)
                $('#reporteConsumosTable').DataTable({
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
            }, 1000);
        }

    })
    ;