﻿var app = angular.module('adminColaboradoresApp', ['ui.router', 'cp.ngConfirm','ServicesModule'])
    .controller("adminColaboradoresController", function ($rootScope, $state, $http) {
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

        $state.go("adminColaboradores@home");
    }).controller("homeColaboradoresController", function ($scope, $rootScope, $http, $ngConfirm, $viajesModule, $serviciosModule) {
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
            $scope.loadData();
            /*setServicioIdInsitutcionId();*/
        });

        $scope.vp = {};
        $scope.vc = {};
        $scope.tp = {};
        $scope.bici = {};
        $scope.otra = {};
        $scope.totalEncuestados = 0;
        $scope.viajes = [];
        $scope.loadingTable = false;
        $scope.types = "['locality']";
        $scope.distanciaEstimada = "";
        const currentYear = new Date().getFullYear();
        $scope.years = [];
        for(let year = 2024; year <= currentYear; year++) {
            $scope.years.push(year);
        }
        $scope.year = currentYear;

        $scope.viaje = {
            periodo: $scope.year.toString(),
            ciudadOrigen: "",
            ciudadDestino: "",
            nViajesSoloIdaRetorno: 0,
            nViajesIdaVuelta: 0,
            nViajesTotales: 0,
            distanciaEstimada: 0,
            distancia:0
        }
        $scope.editMode = false;
        $scope.bloqOD = false;
        $scope.page = 1;
        $scope.arrayPages = [];
        $scope.modalTitle = "Agregar cometido en avión";
        $scope.editText = "Editar";

        if ($rootScope.isConsulta) {
            $scope.editText = "Ver";
        } else {
            $scope.editText = "Editar";
        }

        $scope.saveVp = function () {
            //console.log($scope.vp);
            $scope.vp.totalEncuestados = $scope.totalEncuestados;
            $http({
                method: 'POST',
                url: `${$rootScope.APIURL}/api/encuestaColaborador/vehiculo-propio/${$rootScope.selectedServicioId}/${$scope.year}`,
                data: JSON.stringify($scope.vp)
            }).then(function (response) {
                console.log("ok");
                $ngConfirm({
                    title: 'Vehiculo Propio',
                    content: '<p>Registro guardado correctamente<p>',
                    buttons: {
                        Cancelar: {
                            text: "Ok",
                            btnClass: "btn btn-default",
                            action: function () {

                            }
                        }

                    }
                });
                $scope.loadData();
            });
        };

        $scope.saveVc = function () {
            //console.log($scope.vp);
            $scope.vc.totalEncuestados = $scope.totalEncuestados;
            $http({
                method: 'POST',
                url: `${$rootScope.APIURL}/api/encuestaColaborador/vehiculo-compartido/${$rootScope.selectedServicioId}/${$scope.year}`,
                data: JSON.stringify($scope.vc)
            }).then(function (response) {
                console.log("ok");
                $ngConfirm({
                    title: 'Vehiculo Compartido',
                    content: '<p>Registro guardado correctamente<p>',
                    buttons: {
                        Cancelar: {
                            text: "Ok",
                            btnClass: "btn btn-default",
                            action: function () {

                            }
                        }

                    }
                });
                $scope.loadData();
            });
        }

        $scope.changeNoDeclaraVuelos = function () {
            //console.log($scope.noDeclaraVuelos);
            $http({
                method: 'PUT',
                url: `${$rootScope.APIURL}/api/viajes/no-declara/${$rootScope.selectedServicioId}/${$scope.year}?value=${$scope.noDeclaraVuelos}`,
            }).then(function (response) {
                console.log("ok");
            });

        }

        $scope.changeNViajes = function () {
            //console.log(1);
            if ($scope.viaje.nViajesSoloIdaRetorno > 0 & $scope.viaje.nViajesIdaVuelta>0) {
                $scope.viaje.distanciaEstimada = $scope.viaje.distancia * ($scope.viaje.nViajesSoloIdaRetorno + 2 * $scope.viaje.nViajesIdaVuelta);
                $scope.viaje.nViajesTotales = $scope.viaje.nViajesSoloIdaRetorno + 2 * $scope.viaje.nViajesIdaVuelta
            }
            if ($scope.viaje.nViajesSoloIdaRetorno > 0 & $scope.viaje.nViajesIdaVuelta==0) {
                $scope.viaje.distanciaEstimada = $scope.viaje.distancia * $scope.viaje.nViajesSoloIdaRetorno 
                $scope.viaje.nViajesTotales = $scope.viaje.nViajesSoloIdaRetorno
            }
            if ($scope.viaje.nViajesSoloIdaRetorno == 0 & $scope.viaje.nViajesIdaVuelta > 0) {
                $scope.viaje.distanciaEstimada = $scope.viaje.distancia * 2 * $scope.viaje.nViajesIdaVuelta
                $scope.viaje.nViajesTotales = 2 * $scope.viaje.nViajesIdaVuelta
            }
            if ($scope.viaje.nViajesSoloIdaRetorno == 0 & $scope.viaje.nViajesIdaVuelta == 0) {
                $scope.viaje.distanciaEstimada = 0;
                $scope.viaje.nViajesTotales = 0;
            }
            
        }

        $scope.saveTp = function () {
            //console.log($scope.vp);
            $scope.tp.totalEncuestados = $scope.totalEncuestados;
            $http({
                method: 'POST',
                url: `${$rootScope.APIURL}/api/encuestaColaborador/transporte-publico/${$rootScope.selectedServicioId}/${$scope.year}`,
                data: JSON.stringify($scope.tp)
            }).then(function (response) {
                console.log("ok");
                $ngConfirm({
                    title: 'Transporte público',
                    content: '<p>Registro guardado correctamente<p>',
                    buttons: {
                        Cancelar: {
                            text: "Ok",
                            btnClass: "btn btn-default",
                            action: function () {

                            }
                        }

                    }
                });
                $scope.loadData();
            });
        };

        $scope.saveBici = function () {
            //console.log($scope.vp);
            $scope.tp.totalEncuestados = $scope.totalEncuestados;
            $http({
                method: 'POST',
                url: `${$rootScope.APIURL}/api/encuestaColaborador/bicicleta/${$rootScope.selectedServicioId}/${$scope.year}`,
                data: JSON.stringify($scope.bici)
            }).then(function (response) {
                console.log("ok");
                $ngConfirm({
                    title: 'Bicicleta',
                    content: '<p>Registro guardado correctamente<p>',
                    buttons: {
                        Cancelar: {
                            text: "Ok",
                            btnClass: "btn btn-default",
                            action: function () {

                            }
                        }

                    }
                });
                $scope.loadData();
            });
        };
        $scope.saveMoto = function () {
            //console.log($scope.vp);
            $scope.tp.totalEncuestados = $scope.totalEncuestados;
            $http({
                method: 'POST',
                url: `${$rootScope.APIURL}/api/encuestaColaborador/motocicleta/${$rootScope.selectedServicioId}/${$scope.year}`,
                data: JSON.stringify($scope.moto)
            }).then(function (response) {
                console.log("ok");
                $ngConfirm({
                    title: 'Motocibleta',
                    content: '<p>Registro guardado correctamente<p>',
                    buttons: {
                        Cancelar: {
                            text: "Ok",
                            btnClass: "btn btn-default",
                            action: function () {

                            }
                        }

                    }
                });
                $scope.loadData();
            });
        };
        $scope.saveOtra = function () {
            //console.log($scope.vp);
            $scope.tp.totalEncuestados = $scope.totalEncuestados;
            $http({
                method: 'POST',
                url: `${$rootScope.APIURL}/api/encuestaColaborador/otra/${$rootScope.selectedServicioId}/${$scope.year}`,
                data: JSON.stringify($scope.otra)
            }).then(function (response) {
                console.log("ok");
                $ngConfirm({
                    title: 'Otras Formas',
                    content: '<p>Registro guardado correctamente<p>',
                    buttons: {
                        Cancelar: {
                            text: "Ok",
                            btnClass: "btn btn-default",
                            action: function () {

                            }
                        }

                    }
                });
                $scope.loadData();
            });
        };

        $scope.newVuelo = function () {
            $scope.modalTitle = "Agregar Viajes en avión";
            $scope.editMode = false;
            showModal();
        };

        $scope.changeLocationOrigen = function () {
            //console.log("change")
            const place = this.getPlace();
            $scope.viaje.latOrigen = place.geometry.location.lat();
            $scope.viaje.lngOrigen = place.geometry.location.lng();
        }

        $scope.changeLocationDestino = function () {
            //console.log("change")
            const place = this.getPlace();
            $scope.viaje.latDestino = place.geometry.location.lat();
            $scope.viaje.lngDestino = place.geometry.location.lng();
            calcDistancia();
        }

        $scope.submitFormViajes = function () {
            if ($scope.editMode) {

                $http({
                    method: 'PUT',
                    url: `${$rootScope.APIURL}/api/v2/viajes/${$scope.viaje.id}/${$scope.year}`,
                    data: JSON.stringify($scope.viaje)
                }).then(function (response) {
                    $scope.loadData();
                    $scope.closeModal();
                }, function (error) {
                    closeModal();
                    $ngConfirm({
                        title: 'Error',
                        content: `<p>Ocurrió un error, favor contactese con el administrador del sistema<p>`,
                        buttons: {
                            Ok: {
                                text: "Ok",
                                btnClass: "btn btn-default",
                                action: function () {
                                }
                            }
                        }
                    });
                });

            } else {
                $http({
                    method: 'POST',
                    url: `${$rootScope.APIURL}/api/v2/viajes/${$rootScope.selectedServicioId}/${$scope.year}`,
                    data: JSON.stringify($scope.viaje)
                }).then(function (response) {
                    $scope.loadData();
                    $scope.closeModal();
                }, function (error) {
                    closeModal();
                    $ngConfirm({
                        title: 'Error',
                        content: `<p>Ocurrió un error, favor contactese con el administrador del sistema<p>`,
                        buttons: {
                            Ok: {
                                text: "Ok",
                                btnClass: "btn btn-default",
                                action: function () {
                                }
                            }
                        }
                    });
                });
            }
            
        }

        $scope.delete = function (id) {
            $ngConfirm({
                title: 'Eliminar viaje',
                content: '<p>¿Esta seguro de eliminar el viaje?<p>',
                buttons: {
                    Ok: {
                        text: "Ok",
                        btnClass: "btn btn-danger",
                        action: function () {
                            $http({
                                method: 'DELETE',
                                url: `${$rootScope.APIURL}/api/viajes/${id}`
                            }).then(function () {
                                $scope.loadData();
                            })

                        }
                    },
                    Cancelar: {
                        text: "Cancelar",
                        btnClass: "btn btn-default",
                        action: function () {

                        }
                    }

                }
            });
        };

        function calcDistancia() {
            var origin = new google.maps.LatLng($scope.viaje.latOrigen, $scope.viaje.lngOrigen);
            var destination = new google.maps.LatLng($scope.viaje.latDestino, $scope.viaje.lngDestino);
            var distance = google.maps.geometry.spherical.computeDistanceBetween(origin, destination);
            const distanceEst = Math.round(distance / 1000)
            //console.log(distanceEst);
            $scope.viaje.distancia = distanceEst;
            $scope.changeNViajes();
            //service.getDistanceMatrix(
            //    {
            //        origins: [origin],
            //        destinations: [destination],
            //        travelMode: 'TRANSIT',
            //        //transitOptions: TransitOptions,
            //        //drivingOptions: DrivingOptions,
            //        //unitSystem: UnitSystem,
            //        //avoidHighways: Boolean,
            //        //avoidTolls: Boolean,
            //    }, callback);

            //function callback(response, status) {
            //    // See Parsing the Results for
            //    // the basics of a callback function.
            //    console.log(response);
            //    
            //    console.log($scope.distanciaEstimada)
            //}  
        }

        function showModal() {
            $viajesModule.getPaisList().then(resp => {
                $scope.paises = resp.data;
                $("#modal-form").modal("show");
            }); 
        }

        $scope.changePaisOrigen = function () {
            if ($scope.viaje.paisOrigenId) {
                $viajesModule.getAeropuertoByPaisId($scope.viaje.paisOrigenId).then(resp => {
                    $scope.aeropuertos = resp.data;
                });
            } else {
                $scope.aeropuertos = [];
            }
        }

        $scope.changePaisDestino = function () {
            if ($scope.viaje.paisDestinoId) {
                $viajesModule.getAeropuertoByPaisId($scope.viaje.paisDestinoId).then(resp => {
                    $scope.aeropuertosDestino = resp.data;
                });
            } else {
                $scope.aeropuertosDestino = [];
            }
        }

        $scope.closeModal = function () {
            $scope.editMode = false;
            $scope.bloqOD = false;
            $scope.vuelosForm.$setPristine();
            $scope.vuelosForm.$setUntouched();
            $scope.viaje = {
                periodo: $scope.year.toString(),
                ciudadOrigen: "",
                ciudadDestino: "",
                nViajesSoloIdaRetorno: 0,
                nViajesIdaVuelta: 0,
                nViajesTotales: 0,
                distanciaEstimada: 0,
                distancia: 0
            }
            $("#modal-form").modal("hide");

        };

        $scope.setPage = function (page) {
            $scope.page = page;
            $scope.loadData();
        };

        $scope.prevPage = function () {

            if ($scope.page > 1) {
                $scope.page = $scope.page - 1;
                $scope.loadData();
            }
        };

        $scope.nextPage = function () {

            if ($scope.page < $scope.arrayPages[$scope.arrayPages.length - 1]) {
                $scope.page = $scope.page + 1;
                $scope.loadData();
            }
        };

        $scope.edit = function (id) {
            //console.log("Edit");

            $scope.modalTitle = "Editar viajes en avión";
            $scope.editMode = true;
            $scope.bloqOD = true;
            $http({
                method: 'GET',
                url: `${$rootScope.APIURL}/api/viajes/get-by-id/${id}`,
            }).then(function (response) {
                $scope.viaje = response.data;
                $scope.viaje.periodo = response.data.periodo.toString();
                $viajesModule.getPaisList().then(resp => {
                    $scope.paises = resp.data;
                    $("#modal-form").modal("show");
                });
                $scope.changePaisOrigen();
                $scope.changePaisDestino();

            });
        };

        $scope.editOD = function () {
            $scope.bloqOD = false;
            $scope.viaje.ciudadOrigen = "";
            $scope.viaje.ciudadDestino = "";
            $scope.viaje.distancia = 0;
        }

        /*Validations*/

        //var entero = document.querySelector('.entero');

        //entero.addEventListener('input', function () {
        //    var num = this.value.match(/^\d+$/);
        //    if (num === null) {
        //        this.value = "";
        //    }
        //});
        //entero.addEventListener('blur', function () {
        //    var num = this.value.match(/^\d+$/);
        //    if (num === null) {
        //        this.value = "0";
        //    }
        //});



        //var decimal = document.querySelector('.decimal');

        //decimal.addEventListener('input', function () {
        //    var num = this.value.match(/^[1-9]\d*(\.\d+)?$/);
        //    if (num === null) {
        //        this.value = "0";
        //    }
        //});
        //decimal.addEventListener('blur', function () {
        //    var num = this.value.match(/^[1-9]\d*(\.\d+)?$/);
        //    if (num === null) {
        //        this.value = "0";
        //    }
        //});

        $scope.loadData = function() {
            console.log("loadData called");
            $scope.loadingTable = true;
            $http({
                method: 'GET',
                url: `${$rootScope.APIURL}/api/encuestaColaborador/${$rootScope.selectedServicioId}/${$scope.year}`
            }).then(function (response) {
                //console.log(response);
                $scope.totalEncuestados = response.data.totalEncuestados;
                $scope.vp = response.data.vehiculoPropio;
                $scope.vc = response.data.vehiculoCompartido;
                $scope.tp = response.data.transportePublico;
                $scope.bici = response.data.bicicleta;
                $scope.moto = response.data.motocicleta;
                $scope.otra = response.data.otrasFormas;
            });

            $http({
                method: 'GET',
                url: `${$rootScope.APIURL}/api/viajes/${$rootScope.selectedServicioId}/${$scope.year}?page=${$scope.page}&recordsPerPage=20`
            }).then(function (response) {
                //console.log(response.data);
                
                let totalRecords = parseInt(response.headers('totalrecords'));
                let pages = Math.ceil(totalRecords / 20);
                const arraypages = Array(pages).fill(1).map((n, i) => n + i)
                if (arraypages.length > 15) {
                    if ($scope.page > 1) {
                        arraypages.splice(0, $scope.page - 1);
                    }
                    arraypages.splice(15, arraypages.length - 15);
                }
                $scope.arrayPages = arraypages;
                $scope.viajes = response.data.viajes;
                $scope.noDeclaraVuelos = response.data.noDeclaraViajeAvion;
                $scope.loadingTable = false;

            });
        }
    })
    .directive('entero', function () {
        return {
            restrict: 'C', // call the directive on DOM elements with the class 'btn'
            link: function (scope, elem, attr) {
                         elem.bind('input', function () {
                                var num = this.value.match(/^\d+$/);
                                if (num === null) {
                                    this.value = "";
                                }
                         });
                        elem.bind('blur', function () {
                               var num = this.value.match(/^\d+$/);
                                if (num === null) {
                                    this.value = 0;
                                }
                            });
                }
            }

    })
    .directive('decimal', function () {
        return {
            restrict: 'C', // call the directive on DOM elements with the class 'btn'
            link: function (scope, elem, attr) {
                elem.bind('input', function () {
                    var num = this.value.match(/^[1-9]\d*(\.\d+)?$/);
                    if (num === null) {
                        this.value = "";
                    }
                });
                elem.bind('blur', function () {
                    var num = this.value.match(/^[1-9]\d*(\.\d+)?$/);
                    if (num === null) {
                        this.value = 0;
                    }
                });
            }
        }

    });