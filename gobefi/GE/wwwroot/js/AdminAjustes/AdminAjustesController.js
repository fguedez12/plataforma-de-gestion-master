
(function () {
    'use strict';

    var ajustesApp = angular.module('adminAjustesApp', []);

    ajustesApp.controller('adminAjustesController', function ($scope, $http, $rootScope) {

        $http({
            method: 'GET',
            url: `/settings?sectionName=ApiConfiguration&paramName=apiGestionaEnergia`

        }).then(function (response) {
            const did = localStorage.getItem('divisionId');
            $scope.gestion = {};
            $scope.gestion.divisionId = did;
            $rootScope.APIURL = response.data;
            loadData();
        });

        $scope.loading = false;

        function getAuthToken() {
            return localStorage.getItem('token'); // Ajusta esto según cómo almacenes el token
        }

        $scope.EditUniDadPmg = function () {
            $http({
                method: 'POST',
                url: `${$rootScope.APIURL}/api/ajustes/editUnidadPMG`,
                data: JSON.stringify(!$scope.editUnidadPMG)
            }).then(function () {


            });
        };

        $scope.ActiveAlcanceModulo = function () {
            $http({
                method: 'POST',
                url: `${$rootScope.APIURL}/api/ajustes/activeAlcanceModulo`,
                data: JSON.stringify(!$scope.activeAlcance)
            }).then(function () {


            });
        };

        $scope.createUniDadPmg = function () {

            $http({
                method: 'POST',
                url: `${$rootScope.APIURL}/api/ajustes/createUnidadPMG`,
                data: JSON.stringify(!$scope.createUnidadPMG)
            }).then(function () {


            });
        }

        $scope.DeleteUniDadPmg = function () {
            $http({
                method: 'POST',
                url: `${$rootScope.APIURL}/api/ajustes/deleteUnidadPMG`,
                data: JSON.stringify(!$scope.deleteUnidadPMG)
            }).then(function () {


            });
        };

        $scope.ComprasServicio = function () {
            $http({
                method: 'POST',
                url: `${$rootScope.APIURL}/api/ajustes/comprasServicio`,
                data: JSON.stringify(!$scope.comprasServicio)
            }).then(function () {


            });
        };

        $scope.BloqueoInfoServicio = function () {

            if (confirm("¿Estás seguro de que deseas bloquear la información del servicio, no podra deshacer esta acción?")) {
                $scope.loading = true;
                $http({
                    method: 'GET',
                    url: `${$rootScope.APIURL}/api/account/user-id`
                }).then(function (res) {
                    console.log(res.data);
                    var datos = {}
                    datos.servicioId = res.data
                    $http({
                        method: 'GET',
                        url: `/adminajustes/BloqueoInfoServicio/${res.data}`,
                    }).then(function () {
                        alert("La información del servicio ha sido bloqueada exitosamente.");
                        $scope.loading = false;
                    });
                });
            }


        }

        function loadData() {
            $http({
                method: 'GET',
                url: `${$rootScope.APIURL}/api/ajustes/`
            }).then(function (res) {
                //console.log("res")
                $scope.editUnidadPMG = res.data.editUnidadPMG;
                $scope.deleteUnidadPMG = res.data.deleteUnidadPMG;
                $scope.comprasServicio = res.data.comprasServicio;
                $scope.createUnidadPMG = res.data.createUnidadPMG;
                $scope.activeAlcance = res.data.activeAlcanceModule;
            });

            $http({
                method: 'GET',
                url: `${$rootScope.APIURL}/api/ajustes/bloqueo-info-servicio`
            }).then(function (res) {
                console.log(res)
                $scope.serviciosABloquear = res.data;
                //$scope.bloqInfo = res.data;
            });
        }


    });


})();
