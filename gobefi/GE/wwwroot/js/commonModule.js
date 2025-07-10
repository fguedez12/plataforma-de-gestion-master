(function () {
    'use strict';
    var common = angular.module('common', []);

    common.controller('findLocationController', function ($scope, $rootScope, NgMap, $http) {

        var vm = this;
        vm.types = "['address']";
        var lat = -33.444287;
        var lng = -70.6565585;
        $scope.initialPos = [lat, lng]
        $scope.data = {};
        $rootScope.inmuebleListSearch = [];
        
      
        $rootScope.direccion = {};
        loadRegiones();

        function loadRegiones() {
            $http({
                method: 'GET',
                url: '/api/regiones/getregiones'
            }).then(function (response) {
                $scope.data.regiones = response.data;
            });
        };
        function loadProvincias() {
            $http({
                method: 'GET',
                url: `/api/provincia/getByRegionId/${$scope.direccion.regionId}`
            }).then(function (response) {
                $scope.data.provincias = response.data;
            });
        };
        function loadComunas() {
            $http({
                method: 'GET',
                url: `/api/comuna/getByProvinciaId/${$scope.direccion.provinciaId}`
            }).then(function (response) {
                $scope.data.comunas = response.data;
            });
        };

        $scope.changeRegion = function () {
            if ($scope.direccion.regionId == null) {
                $scope.direccion.provinciaId = null;
                $scope.direccion.comunaId = null;
            } else {
               
                loadProvincias();
            }
        };

        $scope.changeProvincia = function () {
            if ($scope.direccion.provinciaId == null) {
               
                $scope.direccion.comunaId = null;
            } else {
               
                loadComunas();
            }
        };

        $scope.changeLocation = function () {

            var calle = '';
            var numero = 'S/N ';
            var region = '';
            var comuna = '';
            var provincia = '';
            var $calle = angular.element('#Calle');
            var $numero = angular.element('#Numero');
            var $lat = angular.element('#Lat');
            var $lng = angular.element('#Lng');

            vm.place = this.getPlace();
            console.log(vm.place);

            for (var i in vm.place.address_components) {
                for (var j in vm.place.address_components[i].types) {
                    if (vm.place.address_components[i].types[j] === "route") {
                        calle = vm.place.address_components[i].long_name;
                    }
                    if (vm.place.address_components[i].types[j] === "street_number") {
                        numero = vm.place.address_components[i].long_name;
                    }
                    if (vm.place.address_components[i].types[j] === "administrative_area_level_1") {
                        region = vm.place.address_components[i].long_name;
                        console.log(region);
                        if (region === "Región Metropolitana") {
                            region = "Región Metropolitana de Santiago"
                        }

                        else if (region === "O'Higgins") {
                            region = "Región del Libertador Gral. Bernardo O’Higgins"
                        }

                        else if (region === "Maule" || region === "Ñuble") {
                            region = "Región del " + region;
                        }
                        else if (region === "Bío Bío") {
                            region = "Región del Biobío";
                        }
                        else if (region === "Araucanía") {
                            region = "Región de la " + region;
                        }
                        else if (region === "Aysén") {
                            region = "Región Aysén del Gral. Carlos Ibáñez del Campo";
                        }
                        else if (region === "Magallanes y la Antártica Chilena") {
                            region = "Región de Magallanes y de la Antártica Chilena";
                        }

                        else {
                            region = "Región de " + region;
                        }

                        console.log(region);

                    }
                    if (vm.place.address_components[i].types[j] === "administrative_area_level_2") {
                        provincia = vm.place.address_components[i].long_name;

                        if (provincia === "Capitan Prat") {
                            provincia = "Capitán Prat"
                        }
                        if (provincia === "Antartica Chilena") {
                            provincia = "Antártica Chilena"
                        }
                        if (provincia === "Chiloe") {
                            provincia = "Chiloé"
                        }

                        if (provincia === "Aysen") {
                            provincia = "Aysén"
                        }
                        if (provincia === "Limari") {
                            provincia = "Limarí"
                        }

                        if (provincia === "Valparaiso") {
                            provincia = "Valparaíso"
                        }
                        if (provincia === "Biobio") {
                            provincia = "Biobío"
                        }
                        if (provincia === "Cautin") {
                            provincia = "Cautín"
                        }
                        if (provincia === "Provincia de Diguillín") {
                            provincia = "Diguillín"
                        }
                        if (provincia === "Copiapo") {
                            provincia = "Copiapó"
                        }
                        if (provincia === "Curico") {
                            provincia = "Curicó"
                        }
                        if (provincia === "Concepcion") {
                            provincia = "Concepción"
                        }
                        if (provincia === "Ultima Esperanza") {
                            provincia = "Última Esperanza"
                        }
                        if (provincia === "Provincia de Punilla") {
                            provincia = "Punilla"
                        }
                    }
                    if (vm.place.address_components[i].types[j] === "administrative_area_level_3") {

                        comuna = vm.place.address_components[i].long_name;
                        console.log(comuna);
                        if (comuna == "Cabo de Hornos") {
                            comuna = "Cabo de Hornos (Ex Navarino)";
                            console.log(comuna);
                        }
                    }


                }
            }

            //En caso de usar ng-model
            $rootScope.direccion.adress = $rootScope.adress;
            $rootScope.direccion.direccionCompleta = `${calle} ${numero}, ${comuna}`
            $rootScope.direccion.calle = calle;
            $rootScope.direccion.numero = numero;
            $rootScope.direccion.comunaId = null;
            $rootScope.direccion.region = region;
            $rootScope.direccion.provincia = provincia;
            $rootScope.direccion.comuna = comuna;
            $rootScope.direccion.latitud = vm.place.geometry.location.lat();
            $rootScope.direccion.longitud = vm.place.geometry.location.lng();

            //en caso de usar controles mvc
            $calle.val(calle);
            $numero.val(numero);
            $lat.val(vm.place.geometry.location.lat());
            $lng.val(vm.place.geometry.location.lng());

            $("#RegionId option").filter(function () {
                //may want to use $.trim in here
                return $(this).text() == region;
            }).prop('selected', true).change();

            setTimeout(function () {
                $("#ProvinciaId option").filter(function () {
                    //may want to use $.trim in here
                    return $(this).text() == provincia;
                }).prop('selected', true).change();
            }, 300)

            setTimeout(function () {
                $("#ComunaId option").filter(function () {
                    //may want to use $.trim in here
                    return $(this).text() == comuna;
                }).prop('selected', true).change();
                //console.log($rootScope.direccion);
                $rootScope.$broadcast('address-changed', $rootScope.direccion);
            }, 600)



            //console.log(vm.place.address_components);
            //console.log('location', vm.place.geometry.location);
            //console.log('lat', vm.place.geometry.location.lat());
            //console.log('lng', vm.place.geometry.location.lng());
            vm.map.setCenter(vm.place.geometry.location);
            $scope.initialPos = [vm.place.geometry.location.lat(), vm.place.geometry.location.lng()]
           
        }
        NgMap.getMap({ id: 'map1' }).then(function (map) {
            vm.map = map;
        });
        

    });

    common.controller('inmuebleSeachController', function ($http,$scope, $rootScope) {
        $rootScope.inmuebleListSearch = [];
        $scope.$on('address-changed', function (evt, direccion) {
            setTimeout(() => {
                $http({
                    method: 'POST',
                    url: '/api/inmueble/getbyaddress',
                    data: JSON.stringify(direccion)
                }).then(function (response) {
                    $rootScope.inmuebleSearched = true;
                    $rootScope.inmuebleListSearch = response.data.inmuebles;
                    //console.log($rootScope.inmuebleSearched);
                    //console.log($rootScope.inmuebleListSearch);
                });

            }, 1500);
            
        });

        $scope.selInmueble = function (inmueble) {
            $rootScope.$broadcast('selInmueble', inmueble);
        }

        $scope.addinmueble = function () {
            $rootScope.$broadcast('addinmueble');
        }

    });
})();