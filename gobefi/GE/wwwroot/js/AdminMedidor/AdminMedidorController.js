
(function () {
    'use strict';

    var medidoresApp = angular.module('adminMedidoresApp', []);

    medidoresApp.controller('adminMedidoresController', function ($scope, $http, $interval, $sce) {
        
        $scope.disableServicios = true;

        loadData(1);

        $scope.goToPage = function (page) {
            loadData(page);
        };

        $scope.nextPage = function () {
            if ($scope.pageIndex < $scope.stopPageIndex) {
                loadData($scope.pageIndex + 1);
            }

        };
        $scope.prevPage = function () {
            if ($scope.pageIndex > $scope.startPageIndex) {
                loadData($scope.pageIndex - 1);
            }

        };

        $scope.changeMinisterio = function () {
            if ($scope.filtroMinisterio != null) {
                $scope.filteredServicios = $scope.serviciosList.filter(function (f) {
                    return f.ministerioId == $scope.filtroMinisterio
                });

                $scope.disableServicios = false;
            } else {
                $scope.disableServicios = true;
            }

        };

        $scope.findById = function () {
            loadData($scope.pageIndex);
        }

        $scope.changeServicio = function () {
            loadData($scope.pageIndex);
        }

        $scope.editMedidor = function (medidor) {
            $scope.medidorEdit = medidor;
            angular.element("#modalEdit").modal("show");

        }

        $scope.updateMedidor = function () {
            var filtroMinisterio = ""
            if ($scope.filtroMinisterio) {
                filtroMinisterio = $scope.filtroMinisterio
            }
            var filtroServicio = ""
            if ($scope.filtroServicio) {
                filtroServicio = $scope.filtroServicio
            }
            $http({
                method: 'POST',
                url: `/api/amedidor?page=${$scope.pageIndex}&filtroMinisterio=${filtroMinisterio}&filtroServicio=${filtroServicio}`,
                data: JSON.stringify($scope.medidorEdit)
            }).then(function () {

                angular.element("#modalEdit").modal("hide");
            });
        }

        function loadData(page) {
            var filtroMinisterio = ""
            if ($scope.filtroMinisterio) {
                filtroMinisterio = $scope.filtroMinisterio
            }
            var filtroServicio = ""
            if ($scope.filtroServicio) {
                filtroServicio = $scope.filtroServicio
            }
            var filtroId = ""
            if ($scope.filtroId) {
                filtroId = $scope.filtroId
            }
            $scope.loadingTable = true;
            $http({
                method: 'GET',
                url: `/api/amedidor?page=${page}&filtroMinisterio=${filtroMinisterio}&filtroServicio=${filtroServicio}&id=${filtroId}`
            }).then(function (response) {
                //console.log(response)
                $scope.loadingTable = false;
                $scope.ministeriosList = response.data.medidoresPorPagina.ministerios;
                $scope.serviciosList = response.data.medidoresPorPagina.servicios;
                $scope.medidores = response.data.medidoresPorPagina.medidores;
                $scope.pageIndex = response.data.medidoresPorPagina.pageIndex;
                $scope.startPageIndex = response.data.medidoresPorPagina.startPageIndex;
                $scope.stopPageIndex = response.data.medidoresPorPagina.stopPageIndex;
            });
        };

    });


})();
