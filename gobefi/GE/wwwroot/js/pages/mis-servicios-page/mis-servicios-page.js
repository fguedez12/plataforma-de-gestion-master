var app = angular.module('adminMisServiciosApp', ['cp.ngConfirm'])
    .controller("adminMisServiciosController", function ($rootScope,$scope, $state, $http) {
        var userId = angular.element("#input-id").val();
        var isAdmin = angular.element("#input-is-admin").val();
        const isConsulta = angular.element("#input-is-consulta").val();
        $rootScope.userId = userId;
        $rootScope.isAdmin = isAdmin == 'False' ? false : true;
        $rootScope.isConsulta = isConsulta == 'False' ? false : true;
        $scope.page = 1;
        $scope.loadingTable = false;
        $scope.insitucionIdFilter = '';

        $http({
            method: 'GET',
            url: `/settings?sectionName=ApiConfiguration&paramName=apiGestionaEnergia`

        }).then(function (response) {
            $rootScope.APIURL = response.data;
            loadServicios($scope.page, $scope.insitucionIdFilter);
        });

        $scope.setPage = function (page) {
            $scope.page = page;
            loadServicios(page, $scope.insitucionIdFilter);
        }

        $scope.nextPage = function () {

            if ($scope.page < $scope.arrayPages[$scope.arrayPages.length - 1]) {
                $scope.page = $scope.page + 1;
                loadServicios($scope.page, $scope.insitucionIdFilter);
            }
        }

        $scope.prevPage = function () {

            if ($scope.page > 1) {
                $scope.page = $scope.page - 1;
                loadServicios($scope.page, $scope.insitucionIdFilter);
            }
        }

        $scope.setServicio = function (servicio) {
            //console.log(servicio);
            localStorage.setItem('servicioEvId', servicio.id);
            localStorage.setItem('servicioEvNombre', servicio.nombre);
            window.location.href = `/AdminServicioInfo?servicioidEv=${servicio.id}`;
        }

        $scope.changeInstitucion = function (id) {
            $scope.insitucionIdFilter = id;
            loadServicios($scope.page, $scope.insitucionIdFilter);
        };


        function loadServicios(page, institucionId) {
            $scope.loadingTable = true;
            $http({
                method: 'GET',
                url: `${$rootScope.APIURL}/api/servicios/getByUserIdPagin?page=${page}&institucionId=${institucionId}&pmg=true`
            }).then(function (response) {
                if (response.data.ok) {
                    $scope.loadingTable = false;
                    let totalRecords = parseInt(response.headers('totalrecords'));
                    let pages = Math.ceil(totalRecords / 5);
                    const arraypages = Array(pages).fill(1).map((n, i) => n + i)
                    if (arraypages.length > 15) {
                        if ($scope.page > 1) {
                            arraypages.splice(0, $scope.page - 1);
                        }
                        arraypages.splice(15, arraypages.length - 15);
                    }
                    $scope.page = page;
                    $scope.arrayPages = arraypages;
                    $scope.servicios = response.data.servicios;

                }
            })

        }
    });