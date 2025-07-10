(function () {
    'use strict';

    var edificioApp = angular.module('edificioApp', ['ngMap']);

    edificioApp.controller('edificioController', function ($scope, NgMap) {
        var vm = this;
        vm.types = "['address']";
        var lat = -33.444287;
        var lng = -70.6565585;
        $scope.initialPos = [lat, lng]

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


            for (var i in vm.place.address_components)
            {
                for (var j in vm.place.address_components[i].types) {
                    if (vm.place.address_components[i].types[j] === "route") {
                        calle = vm.place.address_components[i].long_name;
                    }
                    if (vm.place.address_components[i].types[j] === "street_number") {
                        numero = vm.place.address_components[i].long_name;
                    }
                    if (vm.place.address_components[i].types[j] === "administrative_area_level_1") {
                        region = vm.place.address_components[i].long_name;
                        if (region === "Región Metropolitana") {
                            region = "Región Metropolitana de Santiago"
                        }

                        else if (region === "O'Higgins") {
                            region = "Región del Libertador Gral. Bernardo O’Higgins"
                        }

                        else if (region === "Maule" || region === "Ñuble" ) {
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
                            provincia ="Valparaíso"
                        }
                        if (provincia === "Biobio") {
                            provincia = "Biobío"
                        }
                        if (provincia === "Cautin") {
                            provincia = "Cautín"
                        }
                        if (provincia === "Diguillin") {
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
                    }
                    if (vm.place.address_components[i].types[j] === "administrative_area_level_3") {
                        comuna = vm.place.address_components[i].long_name;
                    }

                   
                }
            }

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
            }, 600)

           

            console.log(vm.place.address_components);
            console.log('location', vm.place.geometry.location);
            console.log('lat', vm.place.geometry.location.lat());
            console.log('lng', vm.place.geometry.location.lng());
            vm.map.setCenter(vm.place.geometry.location);
            $scope.initialPos = [vm.place.geometry.location.lat(), vm.place.geometry.location.lng()]
            //$rootScope.paso.edificio.latitud = vm.place.geometry.location.lat();
            //$rootScope.paso.edificio.longitud = vm.place.geometry.location.lng();
        }
        NgMap.getMap({ id: 'map1' }).then(function (map) {
            vm.map = map;
        });

    });

    
})();
