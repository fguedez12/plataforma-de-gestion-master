Dropzone.autoDiscover = false;

var disenioApp = angular.module('disenioApp', ['ngMaterial', 'md-steppers', 'cp.ngConfirm', 'ngMap', 'thatisuday.dropzone']);

var divisionId = localStorage.getItem("divisionId");
var servicioId = localStorage.getItem("servicioId");


function selectFile(id,seccion) {

    var scope = angular.element(document.getElementById('dz' + seccion)).scope();
    scope.$apply(function () {
        scope.selectFile(id, seccion);
    });

    //$('#modalArchivo').modal("show");
};

function deleteFile(id, seccion) {

    var scope = angular.element(document.getElementById('dz' + seccion)).scope();
    scope.$apply(function () {
        scope.confirmDeleteFile(id, seccion);
    });

    //$('#modalArchivo').modal("show");
};

disenioApp.config(['$compileProvider', '$qProvider', 'dropzoneOpsProvider', function ($compileProvider, $qProvider, dropzoneOpsProvider) {
    $compileProvider.aHrefSanitizationWhitelist(/^\s*(|blob|):/);
    $qProvider.errorOnUnhandledRejections(false);
    dropzoneOpsProvider.setOptions({
        addRemoveLinks: false,
        clickable: false,
        dictRemoveFile: 'Eliminar Archivo',
    });
}]);

disenioApp.controller('disenioController', function ($scope, $rootScope, $http) {

    $('.filtro-unidad').hide();
    $rootScope.suelo_tmp = {};
    $rootScope.ventana_tmp = {};
    $rootScope.puerta_tmp = {};
    $rootScope.paso = {};
    $rootScope.paso.edificio = {};
    var divisionId = localStorage.getItem('divisionId');
    
    $rootScope.latLngPasoUno = [-33.44425710000001, -70.65648650000001];

    $scope.testDivision = function () {
        //if (servicioId == 638 || servicioId == 546 || servicioId == 709) {
            return true;
        //} else {
        //    return false;
        //}
    }

    $rootScope.step1 = {
        disabled: false,
        completed: true
    };
    $rootScope.step2 = {
        disabled: true,
        completed: false
    };
    $rootScope.step3 = {
        disabled: true,
        completed: false
    };
    $rootScope.step4 = {
        disabled: true,
        completed: false
    };

    //$rootScope.paso2Complete = false;

    //$rootScope.getEstructura = function () {

    //    $http({
    //        method: 'GET',
    //        url: '/api/Estructura'
    //    })
    //        .then(function (response) {
    //            $rootScope.estructuras = response.data;
    //            for (var i in $rootScope.estructuras) {
    //                if ($rootScope.estructuras[i].nombre == ESTRUCTURA_TECHOS) {
    //                    $rootScope.techos = $rootScope.estructuras[i];
    //                }
    //                if ($rootScope.estructuras[i].nombre == ESTRUCTURA_MUROS) {
    //                    $rootScope.muros = $rootScope.estructuras[i];
    //                }
    //                if ($rootScope.estructuras[i].nombre == ESTRUCTURA_PISOS) {
    //                    $rootScope.pisos = $rootScope.estructuras[i];
    //                }
    //                if ($rootScope.estructuras[i].nombre == ESTRUCTURA_VENTANAS) {
    //                    $rootScope.ventanas = $rootScope.estructuras[i];
    //                    $rootScope.tipo_cierre = $rootScope.ventanas.aislaciones.filter(function (el) {
    //                        return el.aislacion.subNivel == 0;
    //                    });
    //                    $rootScope.tipo_marco = $rootScope.ventanas.aislaciones.filter(function (el) {
    //                        return el.aislacion.subNivel == 1;
    //                    });


    //                }
    //                if ($rootScope.estructuras[i].nombre == ESTRUCTURA_PUERTAS) {
    //                    $rootScope.puertas = $rootScope.estructuras[i];
    //                }
    //                if ($rootScope.estructuras[i].nombre == ESTRUCTURA_CIMIENTOS) {
    //                    $rootScope.cimientos = $rootScope.estructuras[i];
    //                }
    //            }
    //        }, function error(response) {

    //            $scope.loadAgrupamiento = '--Seleccione--';
    //            $ngConfirm({
    //                title: 'Error!',
    //                content: '<strong>Ha ocurrido un error: ' + response.data + '</strong>',
    //                buttons: {
    //                    Cerrar: function () {
    //                        // closes the modal
    //                    },
    //                }
    //            });
    //        }

    //    );


    //    $http({
    //        method: 'GET',
    //        url: '/api/Estructura/espesores'
    //    })
    //        .then(function (response) {
    //            $rootScope.espesores = response.data;
    //        }, function error(response) {

    //            $scope.loadAgrupamiento = '--Seleccione--';
    //            $ngConfirm({
    //                title: 'Error!',
    //                content: '<strong>Ha ocurrido un error: ' + response.data + '</strong>',
    //                buttons: {
    //                    Cerrar: function () {
    //                        // closes the modal
    //                    },
    //                }
    //            });
    //        }

    //    );
    //};

    //$rootScope.getDivision = function () {
    //    $http({
    //        method: 'GET',
    //        url: '/api/diseniopasivo/division/' + divisionId
    //    }).then(function (response) {

    //        $rootScope.loading = false;
    //        $rootScope.paso = response.data;
    //        $rootScope.bajoPisos = response.data.pisos.filter(function (piso) {
    //            return piso.tipoNivelId == 1;
    //        });
    //        $rootScope.sobrePisos = response.data.pisos.filter(function (piso) {
    //            return piso.tipoNivelId == 2;
    //        });

    //        if ($rootScope.paso.edificio.tipoAgrupamiento != null) {
    //            $rootScope.latLngPasoUno = [$rootScope.paso.edificio.latitud, $rootScope.paso.edificio.longitud];
    //            $rootScope.initialPos = [$rootScope.paso.edificio.latitud, $rootScope.paso.edificio.longitud];
    //            //$rootScope.disableForm = true;
    //        };

    //        if ($rootScope.paso.pisos.length > 0) {
    //            if ($rootScope.paso.pisos[0].muros.length > 0) {
    //                $rootScope.paso2Complete = true;
    //                $rootScope.muro_tmp = $rootScope.paso.pisos[0].muros[0];
    //                if ($rootScope.paso.pisos[0].muros[0].ventanas.length > 0) {
                       
    //                    if ($rootScope.paso.pisos[0].muros[0].ventanas[0].tipoMarcoId == 28) {
    //                        $rootScope.paso.pisos[0].muros[0].ventanas[0].tipoMarcoId=null
    //                    }
    //                    if ($rootScope.paso.pisos[0].muros[0].ventanas[0].tipoCierreId == 28) {
    //                        $rootScope.paso.pisos[0].muros[0].ventanas[0].tipoCierreId = null
    //                    }
    //                    $rootScope.ventana_tmp = $rootScope.paso.pisos[0].muros[0].ventanas[0];
    //                };
    //                if ($rootScope.paso.pisos[0].muros[0].puertas.length > 0) {
    //                    if ($rootScope.paso.pisos[0].muros[0].puertas[0].tipoMarcoId == 28) {
    //                        $rootScope.paso.pisos[0].muros[0].puertas[0].tipoMarcoId = null
    //                    }
    //                    $rootScope.puerta_tmp = $rootScope.paso.pisos[0].muros[0].puertas[0];
    //                };

    //            }
    //            if ($rootScope.paso.pisos[0].suelos.length>0) {
    //                if ($rootScope.paso.pisos[0].suelos[0].aislacionId == 28) {
    //                    $rootScope.paso.pisos[0].suelos[0].aislacionId = null
    //                }
    //                if ($rootScope.paso.pisos[0].suelos[0].espesorId == 0) {
    //                    $rootScope.paso.pisos[0].suelos[0].espesorId = null
    //                }
    //            }
    //            if ($rootScope.paso.pisos[0].suelos.length > 0) {
    //                $rootScope.suelo_tmp = $rootScope.paso.pisos[0].suelos[0];
    //            }

    //            $rootScope.paso.pisos.sort(function (a, b) {
    //                if (a.pisoNumero > b.pisoNumero) {
    //                    return 1;
    //                }
    //                if (a.pisoNumero < b.pisoNumero) {
    //                    return -1;
    //                }
    //                return 0;
    //            });

    //            $rootScope.level2_piso_index_sel = $rootScope.paso.pisos.length - 1;
    //            $rootScope.level2_piso_sel = $rootScope.paso.pisos[0];
    //            $rootScope.level2_piso_sel.muro_sel = $rootScope.paso.pisos[0].muros[0];

    //            for (var i in $rootScope.paso.pisos) {

    //                if ($rootScope.paso.pisos[i].suelos.length == 0) {
    //                    var newSuelo = {};
    //                    $rootScope.paso.pisos[i].suelos.push(newSuelo);
    //                }
    //                for (var j in $rootScope.paso.pisos[i].muros) {

    //                    $rootScope.paso.pisos[i].muros[j].nombreMuro = 'Muro ' + (parseInt(j) + 1);

    //                }
    //            };
    //        };

    //        if (response.data.nivelPaso3 == 0 || response.data.nivelPaso3 == 1) {

    //            $rootScope.nivel1Paso3 = true;
    //            $rootScope.nivel2Paso3 = false;
    //        } else {
    //            $rootScope.nivel2Paso3 = true;
    //            $rootScope.nivel1Paso3 = false;
    //            $rootScope.lv2Suelos = true;
    //            $rootScope.lv2Muros = false;
    //            $rootScope.lv2Ventanas = false;
    //        };

    //        if ($rootScope.paso.edificio.latitud === 0 ||
    //            $rootScope.paso.edificio.longitud === 0 ||
    //            $rootScope.paso.edificio.tipoAgrupamiento == null ||
    //            $rootScope.paso.edificio.entorno == null ||
    //            $rootScope.paso.pisos.length === 0) {

    //            $scope.step2.completed = false;
    //            $scope.step2.disabled = true;
    //            $scope.step3.completed = false;
    //            $scope.step3.disabled = true;
    //            $scope.step4.completed = false;
    //            $scope.step4.disabled = true;
    //        };

    //        if ($rootScope.paso.pisos.length > 0) {
    //            if ($rootScope.paso.pisos[0].muros.length === 0) {
    //                $scope.step2.completed = false;
    //                $scope.step2.disabled = false;
    //                $scope.step3.completed = false;
    //                $scope.step3.disabled = true;
    //                $scope.step4.completed = false;
    //                $scope.step4.disabled = true;
    //            };
    //        }

    //        //$rootScope.$emit('divisionReady', null);     
    //    })




    //};

    //$rootScope.getEstructura();
    //$rootScope.getDivision();


    $rootScope.polySvg = function () {
        var myPath = $rootScope.polyPath;

        //function latLng2point(latLng) {

        //    return {
        //        x: (latLng.lng + 180) * (256 / 360),
        //        y: (256 / 2) - (256 * Math.log(Math.tan((Math.PI / 4) + ((latLng.lat * Math.PI / 180) / 2))) / (2 * Math.PI))
        //    };
        //}

        //function poly_gm2svg(gmPaths, fx) {

        //    var point,
        //        gmPath,
        //        svgPath,
        //        svgPaths = [],
        //        minX = 256,
        //        minY = 256,
        //        maxX = 0,
        //        maxY = 0;

        //    for (var pp = 0; pp < gmPaths.length; ++pp) {
        //        gmPath = gmPaths[pp], svgPath = [];
        //        for (var p = 0; p < gmPath.length; ++p) {
        //            point = latLng2point(fx(gmPath[p]));
        //            minX = Math.min(minX, point.x);
        //            minY = Math.min(minY, point.y);
        //            maxX = Math.max(maxX, point.x);
        //            maxY = Math.max(maxY, point.y);
        //            svgPath.push([point.x, point.y].join(','));
        //        }


        //        svgPaths.push(svgPath.join(' '))


        //    }
        //    return {
        //        path: 'M' + svgPaths.join('z M') + 'z',
        //        x: minX,
        //        y: minY,
        //        width: maxX - minX,
        //        height: maxY - minY
        //    };

        //}

        //function drawPoly(node, props) {

        //    var svg = node.cloneNode(false),
        //        g = document.createElementNS("http://www.w3.org/2000/svg", 'g'),
        //        path = document.createElementNS("http://www.w3.org/2000/svg", 'path');

        //    g.setAttribute("id", "svgg");
        //    node.parentNode.replaceChild(svg, node);
        //    path.setAttribute('d', props.path);
        //    g.appendChild(path);
        //    svg.appendChild(g);
        //    svg.setAttribute('viewBox', [props.x, props.y, props.width, props.height].join(' '));


        //}

        //function init() {

        //    //for (var i = 0; i < paths.length; ++i) {
        //    //    paths[i] = google.maps.geometry.encoding.decodePath(paths[i]);
        //    //}

        //    svgProps = poly_gm2svg(paths, function (latLng) {
        //        return {
        //            lat: latLng.lat(),
        //            lng: latLng.lng()
        //        }
        //    });
        //    drawPoly(document.getElementById('svg'), svgProps)
        //}
        //var paths = [];
        //paths.push(myPath.g);
        //init();
    };


    //$rootScope.getPasoTres();
});

disenioApp.controller('pasoUnoController', ["$scope", "$rootScope", "$ngConfirm", "NgMap", "$http", function ($scope, $rootScope, $ngConfirm, NgMap, $http) {

    $scope.pisosIguales = false;
    $scope.porPiso = '';
    $scope.loading = true;
    $scope.loadAgrupamiento = '--Seleccione--';
    $scope.loadEntorno = '--Seleccione--';
    $scope.tipoAgrupamientoDisabled = true;
    $scope.disableForm = false;
    $scope.loadTipoNivelPiso = '--Seleccione--';
    $scope.loadNumeroPiso = '--Seleccione--';
    $scope.bajoPisos = [];
    $scope.sobrePisos = [];

    $scope.pstep1 = function () {
        LoadData();
    };

    LoadData();

    function LoadData() {

        $scope.loadAgrupamiento = 'Cargando..';
        $http({
            method: 'GET',
            url: '/api/TipoAgrupamiento'
        })
            .then(function success(response) {

                $scope.loadAgrupamiento = '--Seleccione--';
                $scope.data.tipoAgrupamiento = response.data;
                $scope.tipoAgrupamientoDisabled = false;
            }, function error(response) {

                $scope.loadAgrupamiento = '--Seleccione--';
                $ngConfirm({
                    title: 'Error!',
                    content: '<strong>Ha ocurrido un error: ' + response.data + '</strong>',
                    buttons: {
                        Cerrar: function () {
                            // closes the modal
                        },
                    }
                });
            });

        $scope.loadEntorno = 'Cargando..';
        $http({
            method: 'GET',
            url: '/api/entorno'
        })
            .then(function success(response) {
                $scope.loadEntorno = '--Seleccione--';
                $scope.data.entorno = response.data;

            }, function error(response) {
                $scope.loadEntorno = '--Seleccione--';
                $ngConfirm({
                    title: 'Error!',
                    content: '<strong>Ha ocurrido un error: ' + response.data + '</strong>',
                    buttons: {
                        Cerrar: function () {
                            // closes the modal
                        },
                    }
                });
            });

        $scope.loadTipoNivelPiso = 'Cargando..';
        $http({
            method: 'GET',
            url: '/api/TipoNivelPiso'
        })
            .then(function success(response) {
                $scope.loadTipoNivelPiso = '--Seleccione--';
                $scope.data.tipoNivelPiso = response.data;

            }, function error(response) {
                $scope.loadTipoNivelPiso = '--Seleccione--';
                $ngConfirm({
                    title: 'Error!',
                    content: '<strong>Ha ocurrido un error: ' + response.data + '</strong>',
                    buttons: {
                        Cerrar: function () {
                            // closes the modal
                        },
                    }
                });
            });

        
        $scope.loadNumeroPiso = 'Cargando..';
        $http({
            method: 'GET',
            url: '/api/NumeroPiso'
        })
            .then(function success(response) {
                $scope.loadNumeroPiso = '--Seleccione--';
                $scope.data.numeroPiso = response.data;

            }, function error(response) {
                $scope.loadNumeroPiso = '--Seleccione--';
                $ngConfirm({
                    title: 'Error!',
                    content: '<strong>Ha ocurrido un error: ' + response.data + '</strong>',
                    buttons: {
                        Cerrar: function () {
                            // closes the modal
                        },
                    }
                });
            });
        

        $http({
            method: 'GET',
            url: '/api/diseniopasivo/pasouno/' + divisionId
        }).then(function (response) {
            $rootScope.loading = false;
            $scope.pasoUno = response.data;
            if (response.data.dpSt2) {
                $rootScope.step2 = {
                    disabled: false,
                    completed: true
                };
                $rootScope.$broadcast('pasoUnoComplete');
            }
            if (response.data.dpSt3) {
                $rootScope.step3 = {
                    disabled: false,
                    completed: true
                };
                $rootScope.$broadcast('pasoDosComplete');
            }
            if (response.data.dpSt4) {
                $rootScope.step4 = {
                    disabled: false,
                    completed: true
                };
                $rootScope.$broadcast('pasoTresComplete');
            }


            $scope.bajoPisos = response.data.pisos.filter(function (piso) {
                return piso.tipoNivelId == 1;
            });

            $scope.bajoPisos.sort(comparePisos);
            $scope.sobrePisos = response.data.pisos.filter(function (piso) {
                return piso.tipoNivelId == 2;
            });
            $scope.sobrePisos.sort(comparePisos);
            if (response.data.tipoAgrupamientoId != null) {
                $scope.disableForm = true;
                
            };
            if ($scope.pasoUno.latitud == 0 && $scope.pasoUno.longitud == 0) {
                var dir = localStorage.getItem("divisionName");
                var dirArr = dir.split(",");
                var nDir = dirArr[1].replace("Nro. ", "");
                var dirSearch = dirArr[0].trim() + " " + nDir.trim() + ", " + dirArr[3].trim();
                var geocoder = new google.maps.Geocoder();
                var address = dirSearch;
                geocoder.geocode({ 'address': address }, function (results, status) {
                    if (status == google.maps.GeocoderStatus.OK) {

                        var lat = results[0].geometry.location.lat();
                        var lng = results[0].geometry.location.lng();
                        $scope.initialPos = [lat, lng]

                        $scope.pasoUno.latitud = lat;
                        $scope.pasoUno.longitud = lng;
                        NgMap.getMap().then(function (map) {
                            map.setCenter(new google.maps.LatLng(lat, lng));
                        });
                    } else {
                        var lat = -33.444287;
                        var lng = -70.6565585;
                        $scope.initialPos = [lat, lng]
                        $scope.pasoUno.latitud = lat;
                        $scope.pasoUno.longitud = lng;
                        NgMap.getMap().then(function (map) {
                            map.setCenter(new google.maps.LatLng(lat, lng));
                        });
                    }
                });
            } else {
                $scope.initialPos = [$scope.pasoUno.latitud, $scope.pasoUno.longitud];
                $rootScope.latLngPasoUno = [$scope.pasoUno.latitud, $scope.pasoUno.longitud]
            }

        }, function (response) {
                $scope.loading = false;
                $ngConfirm({
                    title: 'Error!',
                    content: '<strong>Ha ocurrido un error: ' + response.data + '</strong>',
                    buttons: {
                        Cerrar: function () {
                            // closes the modal
                        },
                    }
                });
        });

    };

    function comparePisos(a, b) {
        if (a.pisoNumero < b.pisoNumero) {
            return -1;
        }
        if (a.pisoNumero > b.pisoNumero) {
            return 1;
        }
        return 0;
    }

    var vm = this;
    vm.types = "['address']";
    
    $scope.changeLocation= function () {
        vm.place = this.getPlace();
        console.log('location', vm.place.geometry.location);
        console.log('lat', vm.place.geometry.location.lat());
        console.log('lng', vm.place.geometry.location.lng());
        vm.map.setCenter(vm.place.geometry.location);
        $scope.initialPos = [vm.place.geometry.location.lat(), vm.place.geometry.location.lng()]
        $scope.pasoUno.latitud = vm.place.geometry.location.lat();
        $scope.pasoUno.longitud = vm.place.geometry.location.lng();
    }
    NgMap.getMap({ id: 'map1' }).then(function (map) {
        vm.map = map;
    });

    $scope.checkPisosIguales = function () {
        if (!$scope.piso.pisosIguales) {
            $scope.porPiso = 'por piso'
        } else {
            $scope.porPiso = '';
        }
    };

    $scope.countPisos = function () {
        if ($scope.piso.tipoNivelId == 1 && $scope.bajoPisos.length == 0) {
            $scope.pisosIguales = true;
        }
        if ($scope.piso.tipoNivelId == 2 && $scope.sobrePisos.length == 0) {
            $scope.pisosIguales = true;
        }
    };

    $scope.showModalConfirm = function (idPiso, nomPiso) {
        var element = angular.element("#modalConfirm");
        $scope.nomPiso = nomPiso;
        $scope.idPiso = idPiso;
        element.modal("show");
    };

    $scope.hideConfirm = function () {
        var element = angular.element("#modalConfirm");
        element.modal("hide");
    };

    //if (!$rootScope.initialPos) {
    //    if (navigator.geolocation) {
    //        navigator.geolocation.getCurrentPosition(function (position) {
    //            var lat = position.coords.latitude;
    //            var long = position.coords.longitude;
    //            $rootScope.initialPos = [lat, long]
    //        });
    //    } else {
    //        $rootScope.initialPos = [-33.444654876727505, -70.6564262509346]
    //    }
    //}

    //var divisionSelect = document.getElementById('bh-divisiones');
    //divisionSelect.addEventListener('change', function () {
    //    $rootScope.getDivision();
    //});

    //$scope.getPasoUno = function () {
    //    var divisionId = localStorage.getItem('divisionId');
    //    $http({
    //        method: 'GET',
    //        url: '/api/DisenioPasivo/pasouno/' + divisionId
    //    })
    //        .then(function (response) {
    //            if (response.data.tipoAgrupamientoId > 0) {
    //                $scope.general.entornoId = response.data.entornoId;
    //                $scope.general.inerciaTermicaId = response.data.inerciaTermicaId;
    //                $scope.general.latitud = response.data.latitud;
    //                $scope.general.longitud = response.data.longitud;
    //                $scope.general.tipoAgrupamientoId = response.data.tipoAgrupamientoId;
    //                $rootScope.latLngPasoUno = [response.data.latitud, response.data.longitud];
    //                $rootScope.initialPos = [response.data.latitud, response.data.longitud];
    //                $rootScope.check = response.data.pisosIguales;
    //                $scope.disableForm = true;
    //                $scope.getPisos();
    //            }
    //            else {
    //                $scope.disableForm = false;
    //                $scope.general = {};
    //                $scope.generalForm.$setUntouched();
    //                $scope.generalForm.$setPristine();
    //                $scope.ubicacionForm.$setUntouched();
    //                $scope.ubicacionForm.$setPristine();
    //                $scope.getPisos();
    //                $scope.setInitialPos();

    //            }
    //        });
    //};

    

    

    
    $scope.loadInercia = '--Seleccione--';
    $scope.getInercia = function () {
        $scope.loadInercia = 'Cargando..';
        $http({
            method: 'GET',
            url: '/api/inerciatermica'
        })
            .then(function success(response) {
                $scope.loadInercia = '--Seleccione--';
                $scope.data.inerciaTermica = response.data;

            }, function error(response) {
                $scope.loadInercia = '--Seleccione--';
                $ngConfirm({
                    title: 'Error!',
                    content: '<strong>Ha ocurrido un error: ' + response.data + '</strong>',
                    buttons: {
                        Cerrar: function () {
                            // closes the modal
                        },
                    }
                });
            });
    };
    $scope.showModalPiso = function () {
        $scope.editMode = false;
        $scope.modalTitulo = "Agregar";
        var element = angular.element('#modalPiso');
        element.modal('show');
    };

    $scope.hideModalPiso = function () {
        $scope.piso = {};
        $scope.tipoNivel = {};
        $scope.pisosForm.$setUntouched();
        $scope.pisosForm.$setPristine();

        var element = angular.element('#modalPiso');
        element.modal('hide');
    };

    


    
    //$scope.getPisos = function () {
    //    var divisionId = localStorage.getItem('divisionId');
    //    $http({
    //        method: 'GET',
    //        url: '/api/Piso/bydivision/' + divisionId
    //    })
    //        .then(function success(response) {
    //            //console.log(response.data)
    //            function compare(a, b) {
    //                if (a.pisoNumero > b.pisoNumero) {
    //                    return -1;
    //                }
    //                if (a.pisoNumero < b.pisoNumero) {
    //                    return 1;
    //                }
    //                return 0;
    //            };
    //            $rootScope.paso.pisos = response.data;
    //            $rootScope.paso.pisos.sort(compare);
    //            $rootScope.level2_piso_index_sel = $rootScope.paso.pisos.length - 1;
    //            $rootScope.level2_piso_sel = $rootScope.paso.pisos[$rootScope.paso.pisos.length - 1];
    //            //var pisosSobre = response.data.filter(function (piso) {
    //            //    return piso.tipoNivelId == 2;
    //            //});
    //            //var pisosBajo = response.data.filter(function (piso) {
    //            //    return piso.tipoNivelId == 1;
    //            //});
    //            //$scope.pisosBajo = pisosBajo;
    //            //$scope.pisosSobre = pisosSobre;

    //            if ($rootScope.paso.pisos[0].muros) {
    //                if ($rootScope.paso.pisos[0].muros.length > 0) {

    //                    $rootScope.paso2Complete = true;
    //                }
    //            }


    //        }, function error(response) {
    //            $ngConfirm({
    //                title: 'Error!',
    //                content: '<strong>Ha ocurrido un error: ' + response.data + '</strong>',
    //                buttons: {
    //                    Cerrar: function () {
    //                        // closes the modal
    //                    },
    //                }
    //            });
    //        });
    //};




    $scope.editPiso = function (option) {

        $scope.piso.id = option.id;
        $scope.piso.numeroPisoNombre = option.numeroPisoNombre;
        $scope.piso.divisionId = option.divisionId;
        $scope.piso.tipoNivelId = option.tipoNivelId;
        $scope.piso.numeroPisoId = option.numeroPisoId;
        $scope.piso.superficie = option.superficie;
        $scope.piso.altura = option.altura;
        $scope.showModalPiso();
        $scope.editMode = true;
        $scope.modalTitulo = "Editar";
    };

    $scope.deletePiso = function (id) {
        var id = $scope.idPiso;

        if (id) {
            $http({
                method: 'DELETE',
                url: '/api/Piso/' + id
            })
                .then(function (response) {
                    if (response.data.ok) {

                        $scope.bajoPisos = response.data.pisoPasoUnoList.filter(function (piso) {
                            return piso.tipoNivelId == 1;
                        });

                        $scope.bajoPisos.sort(comparePisos);
                        $scope.sobrePisos = response.data.pisoPasoUnoList.filter(function (piso) {
                            return piso.tipoNivelId == 2;
                        });
                        $scope.sobrePisos.sort(comparePisos);

                    } else {
                        angular.element('#modalSession').modal('show');
                    }
                    

                }), function error(response) {

                    $ngConfirm({
                        title: 'Error!',
                        theme: 'bootstrap',
                        content: '<strong>Ha ocurrido un error: ' + response.data + '</strong>',
                        buttons: {
                            Cerrar: function () {
                                // closes the modal
                            },
                        }
                    });
                };

            $scope.hideConfirm();
        }
    };

    $scope.pasoUnoSave = function () {
        $rootScope.$broadcast('pasoUnoComplete');
        $rootScope.latLngPasoUno = $scope.initialPos;
        var divisionId = localStorage.getItem('divisionId');
        $scope.general.unidadId = divisionId;

        $http({
            method: 'PUT',
            url: '/api/DisenioPasivo/pasouno/' + divisionId,
            data: JSON.stringify($scope.pasoUno)
        }).then(function (response) {
            if (response.data.ok) {
                if ($rootScope.step2.disabled) {
                    setTimeout(function () {
                        showModalInfoP2();
                    }, 500);
                }
                $scope.editPos = false;
                $scope.$parent.step2.disabled = false;
                $scope.$parent.step1.completed = true;
                $scope.$parent.selected = 1;
                
                //if ($rootScope.paso.pisos[0].muros.length == 0) {
                //    setTimeout(function () {
                //        showModalInfoP2();
                //    }, 500);
                //}
            } else {
                angular.element('#modalSession').modal('show');
            }


        }, function error(response) {

            $scope.loadAgrupamiento = '--Seleccione--';
            $ngConfirm({
                title: 'Error!',
                content: '<strong>Ha ocurrido un error: ' + response.data + '</strong>',
                buttons: {
                    Cerrar: function () {
                        // closes the modal
                    },
                }
            });
        }

        );
        //if (!$scope.disableForm || $scope.editPos) {
            
        //} else {
        //    $scope.$parent.step2.disabled = false;
        //    $scope.$parent.step1.completed = true;
        //    $scope.$parent.selected = 1;
        //    if (!$rootScope.dpSt2) {
        //        setTimeout(function () {
        //            showModalInfoP2();
        //        }, 500);
        //    }
        //};
       
    };

    function showModalInfoP2() {
        var $modal = angular.element("#modalInfoP2")
        $modal.modal('show');
    };

    $scope.general = {};
    $scope.piso = {};
    $scope.havePisos = function () {
        if ($scope.sobrePisos.length > 0 || $scope.bajoPisos.length > 0) {
            return false;
        } else {

            return true;
        }
    }


    //$scope.nearme = function () {
    //    if (navigator.geolocation) {
    //        navigator.geolocation.getCurrentPosition(function (position) {
    //            $scope.latlng = [position.coords.latitude, position.coords.longitude]
    //        });
    //    }
    //};

    $scope.editPos = false;

    $scope.latlng = [];
    $scope.getpos = function (event) {
        $scope.latlng = [event.latLng.lat(), event.latLng.lng()];
        $scope.pasoUno.latitud = $scope.latlng[0];
        $scope.pasoUno.longitud = $scope.latlng[1];
        //$rootScope.latLngPasoUno = $scope.latlng;
        $scope.editPos = true;
    };

    $scope.chekPiso = function () {
        if ($scope.piso.numeroPisoId == undefined) {
            $scope.pisosForm.npiso.$setValidity("check", true);
            return;
        }
        let pisosList = $scope.piso.tipoNivelId == 2 ? $scope.sobrePisos : $scope.bajoPisos;

        var found = false;
        for (var i = 0; i < pisosList.length; i++) {
            if (pisosList[i].numeroPisoId == $scope.piso.numeroPisoId) {
                found = true;
                $scope.pisosForm.npiso.$setValidity("check", false);
                break;
            }
        };
        if (!found) {
            $scope.pisosForm.npiso.$setValidity("check", true);
        }
    };

    $scope.totales = function () {

        if ($scope.sobrePisos && $scope.bajoPisos) {
            let supTotal = 0;
            let volTotal = 0;
            let pisosSobreList = $scope.sobrePisos;
            let pisosBajosList = $scope.bajoPisos;

            for (var i = 0; i < pisosSobreList.length; i++) {
                var piso = pisosSobreList[i];
                supTotal = supTotal + parseFloat(piso.superficie);
                volTotal = volTotal + parseFloat(piso.altura * piso.superficie);
            };
            for (var i = 0; i < pisosBajosList.length; i++) {
                var piso = pisosBajosList[i];
                supTotal = supTotal + parseFloat(piso.superficie);
                volTotal = volTotal + parseFloat(piso.altura * piso.superficie);
            };

            let totales = {
                supTotal: supTotal,
                volTotal: volTotal
            };
            return totales;
        }

    };

    $scope.submitPisoForm = function () {

        const ENTRE_PISO_NOMBRE = "Entre Piso";
        if ($scope.editMode) {
            $scope.piso.divisionId = divisionId;
            $http({
                method: 'PUT',
                url: '/api/Piso/' + $scope.piso.id,
                data: JSON.stringify($scope.piso)
            })
                .then(function (response) {

                    if (response.data.ok) {
                        $scope.bajoPisos = response.data.pisoPasoUnoList.filter(function (piso) {
                            return piso.tipoNivelId == 1;
                        });

                        $scope.bajoPisos.sort(comparePisos);
                        $scope.sobrePisos = response.data.pisoPasoUnoList.filter(function (piso) {
                            return piso.tipoNivelId == 2;
                        });
                        $scope.sobrePisos.sort(comparePisos);

                        $scope.piso = {};
                        $scope.pisosIguales = false;
                        $scope.pisosForm.$setUntouched();
                        $scope.pisosForm.$setPristine();
                        $scope.hideModalPiso();
                    } else {
                        $scope.hideModalPiso();
                        angular.element('#modalSession').modal('show');
                    }

                    

                }), function () {
                    $ngConfirm({
                        title: 'Error!',
                        content: '<strong>Ha ocurrido un error: ' + response.data + '</strong>',
                        buttons: {
                            Cerrar: function () {
                                // closes the modal
                            },
                        }
                    });
                    $scope.piso = {};
                    $scope.pisosIguales = false;
                    $scope.pisosForm.$setUntouched();
                    $scope.pisosForm.$setPristine();
                    $scope.hideModalPiso();
                }

        } else {
            $scope.piso.divisionId = divisionId;
            $http({
                method: 'POST',
                url: '/api/Piso',
                data: JSON.stringify($scope.piso)
            })
                .then(function (response) {
                    if (response.data.ok) {

                        $scope.bajoPisos = response.data.pisoPasoUnoList.filter(function (piso) {
                            return piso.tipoNivelId == 1;
                        });

                        $scope.bajoPisos.sort(comparePisos);
                        $scope.sobrePisos = response.data.pisoPasoUnoList.filter(function (piso) {
                            return piso.tipoNivelId == 2;
                        });
                        $scope.sobrePisos.sort(comparePisos);

                        $scope.piso = {};
                        $scope.pisosIguales = false;
                        $scope.pisosForm.$setUntouched();
                        $scope.pisosForm.$setPristine();
                        $scope.hideModalPiso();
                        $rootScope.level2_piso_index_sel = $rootScope.paso.pisos.length - 1;
                        $rootScope.level2_piso_sel = $rootScope.paso.pisos[0];

                        
                    } else {
                        $scope.hideModalPiso();
                        angular.element('#modalSession').modal('show');
                    } 
                }), function () {
                    $ngConfirm({
                        title: 'Error!',
                        content: '<strong>Ha ocurrido un error: ' + response.data + '</strong>',
                        buttons: {
                            Cerrar: function () {
                                // closes the modal
                            },
                        }
                    });
                    $scope.piso = {};
                    $scope.pisosIguales = false;
                    $scope.pisosForm.$setUntouched();
                    $scope.pisosForm.$setPristine();
                    $scope.hideModalPiso();
                }
        }
    };

    $scope.data = {
        tipoAgrupamiento: [],
        entorno: [],
        inerciaTermica: [],
        tipoNivelPiso: [],
        numeroPiso: [],
    }


}]);

disenioApp.controller('pasoDosController', function ($scope, $rootScope, NgMap, $http) {
    var hasData = false;
    $scope.hasInnerData = false;
    $scope.paso = {};
    $scope.paso.pisos = [];
    $scope.overrideMode = null;
    $scope.overrideText = "";
    var vm = this;
    var poly = null;
    var holePloy = null;
    var innerPoly = null;
    var innerPolyPoints = [];
    //$scope.check = $rootScope.check;
    $scope.pisosPoints = {};
    $scope.pisosPoints.pisos = [];
    $scope.muros = [];
    $scope.points = [];
    $scope.markers = [];
    $scope.paso2Complete = false;

    $scope.pstep2 = function () {
        LoadData();
    };

    function LoadData() {
        $http({
            method: 'GET',
            url: '/api/diseniopasivo/pasodos/' + divisionId
        }).then(function (response) {
            $scope.paso = response.data;
            deletePoly();
            $scope.drawPoly();
        }, function error(response) {

            $scope.loadAgrupamiento = '--Seleccione--';
            $ngConfirm({
                title: 'Error!',
                content: '<strong>Ha ocurrido un error: ' + response.data + '</strong>',
                buttons: {
                    Cerrar: function () {
                        // closes the modal
                    },
                }
            });
        }

        );
    }



    $scope.$on('pasoUnoComplete', function () {

        LoadData();
       
    });


    NgMap.getMap({ id: 'mapy' }).then(function (map) {

        function CenterControl(controlDiv, map) {

            // Set CSS for the control border.
            var controlUI = document.createElement('div');
            controlUI.style.backgroundColor = '#ffc107';
            controlUI.style.border = '2px solid #ffc107';
            controlUI.style.borderRadius = '3px';
            controlUI.style.boxShadow = '0 2px 6px rgba(0,0,0,.3)';
            controlUI.style.cursor = 'pointer';
            controlUI.style.marginBottom = '22px';
            controlUI.style.textAlign = 'center';
            controlUI.title = 'Borrar Polígono';
            controlDiv.appendChild(controlUI);

            // Set CSS for the control interior.
            var controlText = document.createElement('div');
            controlText.style.color = 'rgb(25,25,25)';
            controlText.style.fontFamily = 'Roboto,Arial,sans-serif';
            controlText.style.fontSize = '16px';
            controlText.style.lineHeight = '38px';
            controlText.style.paddingLeft = '5px';
            controlText.style.paddingRight = '5px';
            controlText.innerHTML = 'Borrar Poligono';
            controlUI.appendChild(controlText);

            // Setup the click event listeners: simply set the map to Chicago.
            controlUI.addEventListener('click', function () {
                if ($scope.paso.pisosIguales) {
                    if ($scope.paso.pisos[0].muros.length > 0) {
                        var mode = "deleteButton";
                        var text = "Ya existen datos de muros ingresados para los pisos de este edificio, si continua se sobre escribiran los datos";
                        showAlertOverride(mode, text);
                        $scope.paso2Complete = false;
                    }
                } else {
                    if (!$scope.level) {
                        var mode = "alertLevel";
                        var text = "Primero debe seleccionar la herramienta polígono del piso que desea eliminar";
                        showAlertOverride(mode, text);
                        return;
                    } else {
                        var mode = "deleteButton";
                        var text = "Ya existen datos de muros ingresados para los pisos de este edificio, si continua se sobre escribiran los datos";
                        showAlertOverride(mode, text);
                        $scope.paso2Complete = false;
                    }
                }
            });;
        }

        var centerControlDiv = document.createElement('div');
        var centerControl = new CenterControl(centerControlDiv, map);

        centerControlDiv.index = 1;
        map.controls[google.maps.ControlPosition.BOTTOM_CENTER].push(centerControlDiv);


    });

    vm.onMapOverlayCompleted = function (e) {
        if (!$scope.polygons) {
            $scope.polygons = [];
        }
        var newPolygon = e.overlay;
        $scope.polygons.push(newPolygon);

        if (!$scope.paso.pisosIguales && $scope.level == undefined) {
            var mode = "alertLevel";
            var text = "Primero debe seleccionar la herramienta polígono del piso que desea dibujar";
            showAlertOverride(mode, text);
            return;
        }

        if ($scope.paso.pisosIguales) {
            if ($scope.polygons.length == 1) {
                processPolygon(newPolygon);
                return;
            }
            //Cuando hay mas de un poligono
            if ($scope.polygons.length > 1) {

                // si ya hay otro interno se elimina
                if ($scope.hasInnerData) {
                    $scope.polygons[1].setMap(null);
                    return;
                }
                //Si es interno se procesa
                if (checkInnerPoly($scope.polygons[0], $scope.polygons[1])) {
                    $scope.polygons[0].setMap(null);
                    $scope.polygons[1].setMap(null);
                    processInnerPolygon($scope.polygons[0], $scope.polygons[1]);
                } else {
                    //Si no es interno se pregunta si se desea borrar
                    var mode = "alertLevel";
                    var text = "Primero debe borrar el poligono existente";
                    showAlertOverride(mode, text);
                    return;
                }
                return;
            }
        } else {
            if ($scope.level) {
                if ($scope.paso.pisos[$scope.level].muros) {
                    if ($scope.paso.pisos[$scope.level].muros.length == 0) {
                        processPolygon(newPolygon);
                    } else {
                        // si ya hay otro interno se elimina
                        if ($scope.hasInnerData) {
                            $scope.polygons[1].setMap(null);
                            return;
                        }
                        //Si es interno se procesa
                        if (checkInnerPoly($scope.polygons[0], $scope.polygons[1])) {
                            $scope.polygons[0].setMap(null);
                            $scope.polygons[1].setMap(null);
                            processInnerPolygon($scope.polygons[0], $scope.polygons[1]);
                        } else {
                            //Si no es interno se pregunta si se desea borrar
                            var mode = "alertLevel";
                            var text = "Primero debe borrar el poligono existente";
                            showAlertOverride(mode, text);
                            return;
                        }
                    }
                }
            }
        }

       
    };

    $scope.deleteMuros = function () {
        
            deletePoly();

       
            deleteDataMuros();

       
            hideAlertOverride();

       
        
        
    };

    $scope.cancelDelete = function () {
        hideAlertOverride();
    };

    $scope.sameFloor = function () {
       $http({
            method: 'POST',
            url: '/api/divisiones/pisosiguales/' + divisionId + '/' + !$scope.paso.pisosIguales
       }).then(function (response) {
           if (response.data.ok) {

           } else {
               angular.element('#modalSession').modal('show');
           }
       }, function error(response) {

           $scope.loadAgrupamiento = '--Seleccione--';
           $ngConfirm({
               title: 'Error!',
               content: '<strong>Ha ocurrido un error: ' + response.data + '</strong>',
               buttons: {
                   Cerrar: function () {
                       // closes the modal
                   },
               }
           });
       }
       );

    };

    $scope.cancelSameFloor = function () {
        $rootScope.paso.pisosIguales = !$rootScope.paso.pisosIguales;
        hideAlertOverride();
    };
    $scope.continueSameFloor = function () {
        deletePoly();
        deleteDataMuros();
        hideAlertOverride();
    };

    $scope.cancelDraw = function () {

        deletePoly();
        $scope.drawPoly();
    };

    $scope.continueDraw = function () {
        setPolygon($rootScope.polygons[$rootScope.polygons.length - 1]);
        hideAlertOverride();
    };

    $scope.infoCheck = false;

    $scope.switcherInfo = function () {

        $scope.infoCheck = !$scope.infoCheck;
    }

    $scope.updateMuro = function () {
        var pisoIndex = $scope.editedMuro.pisoIndex;
        var muroIndex = $scope.editedMuro.muroIndex;


        $rootScope.paso.pisos[pisoIndex].muros[muroIndex] = $scope.editedMuro;

        for (var i in $scope.tipoSombreados) {

            if ($scope.tipoSombreados[i].id == $scope.editedMuro.tipoSombreadoId) {

                $scope.editedMuro.tipoSombreado = $scope.tipoSombreados[i];
            }
        }


        var $modal = angular.element("#modalMuro")
        $modal.modal('hide');
        $http({
            method: 'PUT',
            url: '/api/Muro/' + $scope.editedMuro.id,
            data: JSON.stringify($scope.editedMuro)
        }).then(function () {
            $scope.editedMuro = null;
        }, function error(response) {

            $scope.loadAgrupamiento = '--Seleccione--';
            $ngConfirm({
                title: 'Error!',
                content: '<strong>Ha ocurrido un error: ' + response.data + '</strong>',
                buttons: {
                    Cerrar: function () {
                        // closes the modal
                    },
                }
            });
        }
        );

    };

    $scope.editMuro = function (j, pisoNumero) {

        var pisoIndex = 0;
        for (var i in $rootScope.paso.pisos) {
            if ($rootScope.paso.pisos[i].pisoNumero == pisoNumero) {
                pisoIndex = i;
            }
        }

        var editMuro = angular.copy($rootScope.paso.pisos[pisoIndex].muros[j], $scope.editedMuro);
        var frontis = $rootScope.paso.pisos[i].frontisIndex;
        editMuro.pisoIndex = pisoIndex;
        editMuro.muroIndex = j;
        editMuro.nombre = "Muro " + parseInt(j + 1);
        editMuro.frontis = frontis;
        editMuro.checkFrontis = frontis == j ? true : false;
        $scope.editedMuro = editMuro;
        $scope.getTipoSombreado();

        var $modal = angular.element("#modalMuro")
        $modal.modal('show');
    };



    $scope.getTipoSombreado = function () {
        $scope.loadTipoSombreado = 'Cargando..';
        $http({
            method: 'GET',
            url: '/api/tipoSombreado'
        })
            .then(function success(response) {
                $scope.tipoSombreados = response.data;
            }, function error(response) {
                $scope.loadTipoSombreado = '--Seleccione--';
                $ngConfirm({
                    title: 'Error!',
                    content: '<strong>Ha ocurrido un error: ' + response.data + '</strong>',
                    buttons: {
                        Cerrar: function () {
                            // closes the modal
                        },
                    }
                });
            });
    };

    $scope.drawPoly = function () {
        let level = 0;
        if ($scope.level) {
            level = $scope.level;
            hasData = true;
        } else {
            for (var i in $scope.paso.pisos) {
                if ($scope.paso.pisos[i].muros) {
                    if ($scope.paso.pisos[i].muros.length > 0) {
                        hasData = true;
                        break;
                    }
                }
            }
        }
       
        if (hasData) {
            var outerCoords = [];
            var innerCoords = [];
            $scope.paso.pisos[level].muros.sort(function (a, b) {
                return parseInt(a.order) - parseInt(b.order);
            });

            for (var i in $scope.paso.pisos[level].muros) {
                if ($scope.paso.pisos[level].muros[i].tipo == "Muro externo") {
                    outerCoords.push({
                        lat: $scope.paso.pisos[level].muros[i].lat,
                        lng: $scope.paso.pisos[level].muros[i].lng,
                    })
                } else {
                    innerCoords.push({
                        lat: $scope.paso.pisos[level].muros[i].lat,
                        lng: $scope.paso.pisos[level].muros[i].lng,
                    })
                }

            }

            if (innerCoords.length > 0) {
                $scope.hasInnerData = true;
            } else {
                $scope.hasInnerData = false;
            }

            //var stPoint = outerCoords[0];
            //outerCoords.push(stPoint);
            $scope.polypoints = outerCoords;
            newPoly = new google.maps.Polygon({
                paths: [outerCoords, innerCoords],
                fillColor: 'green',
                fillOpacity: 0.35,
            });

            NgMap.getMap({ id: 'mapy' }).then(function (map) {
                newPoly.setMap(map);
            });

            $scope.polygons = [];
            $scope.polygons.push(newPoly);

            var muros = $scope.paso.pisos[0].muros;
            var index = 0;
            for (var i in muros) {
                if (muros[i].id == $scope.paso.frontisId) {
                    index = i;
                }
            }

            $scope.setMarker(index);

            $scope.polyPath = newPoly.getPath();
            $scope.polySvg();
        }
    }

    $scope.changeMarker = function (index) {
        NgMap.getMap({ id: 'mapy' }).then(function (map) {
            for (var i in $scope.markers) {
                $scope.markers[i].setMap(null);
            }

            $scope.setMarker(index);

        });
    };


    $scope.setMarker = function (index) {
        var frontisIndex = parseInt(index)
        var outerCoords = $scope.polypoints;

        if ($scope.paso.pisos[0].muros) {

            if (frontisIndex + 1 > $scope.paso.pisos[0].muros.length) {
                frontisIndex = 0;
            }

            if (frontisIndex + 1 == $scope.paso.pisos[0].muros.length) {
                var latMidPoint = (outerCoords[frontisIndex].lat + outerCoords[0].lat) / 2;
                var lngMidPoint = (outerCoords[frontisIndex].lng + outerCoords[0].lng) / 2;
            } else {
                if (outerCoords.length>0) {
                    var latMidPoint = (outerCoords[frontisIndex].lat + outerCoords[frontisIndex + 1].lat) / 2;
                    var lngMidPoint = (outerCoords[frontisIndex].lng + outerCoords[frontisIndex + 1].lng) / 2;
                }
     
            }
        } else {
            var latMidPoint = (outerCoords[frontisIndex].lat + outerCoords[frontisIndex + 1].lat) / 2;
            var lngMidPoint = (outerCoords[frontisIndex].lng + outerCoords[frontisIndex + 1].lng) / 2;
        }


        var latlng = new google.maps.LatLng(latMidPoint, lngMidPoint);

        var marker = new google.maps.Marker({
            icon: 'https://maps.google.com/mapfiles/ms/icons/flag.png'
        });

        marker.setPosition(latlng);
        $scope.markers.push(marker);

        NgMap.getMap({ id: 'mapy' }).then(function (map) {
            marker.setMap(map);
        });
    }

    //$scope.polygons = [];


    function deleteDataMuros() {

        if ($scope.paso.pisosIguales) {
            for (var i in $scope.paso.pisos) {
                $scope.paso.pisos[i].muros = [];
            }

            $http({
                method: 'PUT',
                url: '/api/Muro/murodisable',
                data: JSON.stringify($scope.paso)
            })
        } else {
            if ($scope.level) {
                $scope.paso.pisos[$scope.level].muros = [];
                let pisoId = $scope.paso.pisos[$scope.level].id;
                $http({
                    method: 'PUT',
                    url: `/api/Muro/murodisable/${pisoId}`
                    
                })
            }
        }
    }
    function deletePoly() {
            $scope.paths = "";
            for (var i in $scope.polygons) {
                $scope.polygons[i].setMap(null);
            }
            $scope.polygons = [];
            $scope.markers.forEach(function (marker, i) {
                marker.setMap(null);
            });
        $scope.markers = [];
        $scope.hasInnerData = false;
       
    };

    function showAlertOverride(mode, text) {
        $scope.overrideMode = mode;
        $scope.overrideText = text;

        var element = angular.element("#modalOverride");
        element.modal("show");
    };
    function hideAlertOverride() {
        $scope.overrideMode = null;
        $scope.overrideText = null;
        var element = angular.element("#modalOverride");
        element.modal("hide");;
    };



    function setPolygon(poly) {

        if (!$rootScope.polygons) {
            $rootScope.polygons = [];
        }
        //si hay mas de dos poligono se cancela la  accion

        if ($rootScope.polygons.length > 2) {
            var lastPolyIndex = $rootScope.polygons.length - 1
            $rootScope.polygons[lastPolyIndex].setMap(null);
            $rootScope.polygons[lastPolyIndex] = null;
            $rootScope.polygons.splice(lastPolyIndex, 1);

            return;
        }
        //si solo hay un poligono se procesa
        if ($rootScope.polygons.length == 1) {
            processPolygon($rootScope.polygons[0]);
        } else {
            //si hay mas de un poligono verificar si es interno
            if (($rootScope.polygons.length > 1)) {
                if (checkInnerPoly($rootScope.polygons[0], $rootScope.polygons[1])) {
                    //si es interno 
                    $rootScope.polygons[0].setMap(null);
                    $rootScope.polygons[1].setMap(null);
                    processInnerPolygon($rootScope.polygons[0], $rootScope.polygons[1]);
                } else {
                    //si no es interno borrar el anterior y reprocesar el nuevo
                    $rootScope.polygons[0].setMap(null);
                    $rootScope.polygons[0] = null;
                    $rootScope.polygons.splice(0, 1);
                    deleteDataMuros();
                    //si hay marcadores del primer poligono se borran
                    if ($scope.markers.length > 0) {
                        $scope.markers[0].setMap(null);
                        $scope.markers = [];
                    }
                    processPolygon($rootScope.polygons[0]);
                }
            }
        }
    }

    function checkInnerPoly(poly, innerPoly) {

        var innerPolygonBounds = innerPoly.getPath();
        for (var a = 0; a < innerPolygonBounds.length; a++) {
            var gPoint = new google.maps.LatLng(innerPolygonBounds.getAt(a).lat(), innerPolygonBounds.getAt(a).lng());
            var containPoint = google.maps.geometry.poly.containsLocation(gPoint, poly)
            if (!containPoint) {
                return false;
            }
        }
        return true;
    }

    function processInnerPolygon(poly, innerPoly) {
        var polygonBounds = poly.getPath();
        
        var points = [];
        for (var a = 0; a < polygonBounds.length; a++) {
            var point = {
                lat: polygonBounds.getAt(a).lat(),
                lng: polygonBounds.getAt(a).lng()
            };
            points.push(point);
        }
        var innerPolygonBounds = innerPoly.getPath();
        var innerPoints = [];
        for (var a = 0; a < innerPolygonBounds.length; a++) {
            var point = {
                lat: innerPolygonBounds.getAt(a).lat(),
                lng: innerPolygonBounds.getAt(a).lng()
            };
            innerPoints.push(point);
        }

        // Construct the polygon, including both paths.
        holePloy = new google.maps.Polygon({
            paths: [points, innerPoints],
            fillColor: 'green',
            fillOpacity: 0.35
        });

        NgMap.getMap({ id: 'mapy' }).then(function (map) {
            holePloy.setMap(map);
        });

        $scope.polygons = [];
        $scope.polygons.push(holePloy);

        for (var i = 0; i < innerPoints.length; i++) {
            if (i == innerPoints.length - 1) {
                var lat1 = innerPoints[i].lat;
                var lon1 = innerPoints[i].lng;
                var lat2 = innerPoints[0].lat;
                var lon2 = innerPoints[0].lng;
                var point1 = turf.point([lon1, lat1]);
                var point2 = turf.point([lon2, lat2]);

            } else {
                var lat1 = innerPoints[i].lat;
                var lon1 = innerPoints[i].lng;
                var lat2 = innerPoints[i + 1].lat;
                var lon2 = innerPoints[i + 1].lng;
                var point1 = turf.point([lon1, lat1]);
                var point2 = turf.point([lon2, lat2]);
            }

            var bearing = turf.bearing(point1, point2);
            var azimut = null
            if (bearing > -180 && bearing < -90) {
                azimut = bearing - 90 + 360;
            } else {
                azimut = bearing - 90;
            }
            innerPoints[i].frontis = false;
            innerPoints[i].vanos = 0;
            innerPoints[i].bearing = azimut;
            innerPoints[i].tipo = "Muro interno";
            innerPoints[i].orientation = $scope.getOrientation(azimut);
            innerPoints[i].distance = turf.distance(point1, point2, { units: 'meters' });

        };
        var totalPoints = $scope.points.concat(innerPoints);
        $scope.points = totalPoints;
        if ($scope.paso.pisosIguales) {
            for (var i in $scope.paso.pisos) {
                if (hasData) {
                    $scope.paso.pisos[i].muros = $scope.paso.pisos[i].muros.concat(totalPoints);
                } else {
                    $scope.paso.pisos[i].muros = totalPoints;
                }
            }
        } else {
            if ($scope.level >= 0) {
                $scope.paso.level = $scope.level;
                $scope.paso.pisos[$scope.level].muros = totalPoints;
            }
        }

        $http({
            method: 'POST',
            url: '/api/Muro/internos',
            data: JSON.stringify($scope.paso)
        }).then(function (response) {
            if (response.data.ok) {
                $scope.paso.pisos = response.data.muroList;
                $scope.hasInnerData = true;
            } else {
                angular.element('#modalSession').modal('show');
            }
           
        }, function error(response) {

            $scope.loadAgrupamiento = '--Seleccione--';
            $ngConfirm({
                title: 'Error!',
                content: '<strong>Ha ocurrido un error: ' + response.data + '</strong>',
                buttons: {
                    Cerrar: function () {
                        // closes the modal
                    },
                }
            });
        }
        );

    }

    function processPolygon(poly) {
        var polygonBounds = poly.getPath();
        console.log(polygonBounds);

        var points = [];
        $scope.points = [];

        for (var a = 0; a < polygonBounds.length; a++) {
            var point = {
                lat: polygonBounds.getAt(a).lat(),
                lng: polygonBounds.getAt(a).lng()
            };
            points.push(point);
        }

        $scope.polypoints = points;

        $scope.setMarker(0);

        for (var i = 0; i < points.length; i++) {
            if (i == points.length - 1) {
                var lat1 = points[i].lat;
                var lon1 = points[i].lng;
                var lat2 = points[0].lat;
                var lon2 = points[0].lng;
                var point1 = turf.point([lon1, lat1]);
                var point2 = turf.point([lon2, lat2]);

            } else {
                var lat1 = points[i].lat;
                var lon1 = points[i].lng;
                var lat2 = points[i + 1].lat;
                var lon2 = points[i + 1].lng;
                var point1 = turf.point([lon1, lat1]);
                var point2 = turf.point([lon2, lat2]);
            }

            var bearing = turf.bearing(point1, point2);
            var azimut = null
            if (bearing > -180 && bearing < -90) {
                azimut = bearing - 90 + 360;
            } else {
                azimut = bearing - 90;
            }
            points[i].vanos = 0;
            points[i].bearing = azimut;
            points[i].tipo = "Muro externo";
            points[i].orientation = $scope.getOrientation(azimut);
            points[i].distance = turf.distance(point1, point2, { units: 'meters' });
            points[i].order = i;
        };
        $scope.points = points;
        if ($scope.paso.pisosIguales) {
          
            for (var i in $scope.paso.pisos) {
                $scope.paso.pisos[i].muros = points;
               
            }
        } else {
            if ($scope.level >= 0) {
                $scope.paso.level = $scope.level;
                $scope.paso.pisos[$scope.level].muros = $scope.points

            }
        }

        //$scope.paso2Complete = true;

        $http({
            method: 'POST',
            url: '/api/Muro',
            data: JSON.stringify($scope.paso)
        }).then(function (response) {
            if (response.data.ok) {
                $scope.paso.pisos = response.data.muroList;
                $scope.paso.frontisId = response.data.muroList[0].muros[0].id;
            } else {
                angular.element('#modalSession').modal('show');
;            }
            
        }, function error(response) {

            $scope.loadAgrupamiento = '--Seleccione--';
            $ngConfirm({
                title: 'Error!',
                content: '<strong>Ha ocurrido un error: ' + response.data + '</strong>',
                buttons: {
                    Cerrar: function () {
                        // closes the modal
                    },
                }
            });
        }
        );
    }

    $scope.specificLevel = function (pisoNumero) {
        for (var i in $scope.paso.pisos) {
            if ($scope.paso.pisos[i].pisoNumero == pisoNumero) {
                $scope.level = i;
                break;
            }
        };
        $scope.drawPiso = $scope.paso.pisos[$scope.level].numeroPisoNombre;
        deletePoly();
        $scope.drawPoly();
        $(window).scrollTop();
    };

    $scope.firstCheck = function (value) {
        if (value == 0) {
            return true;
        }
        else {
            return false;
        }
    }

    $scope.getOrientation = function (bearing) {

        if (-22.5 < bearing && bearing <= 22.5) {
            return 'Norte'
        }
        if (22.5 < bearing && bearing <= 67.5) {
            return 'Noreste'
        }
        if (67.5 < bearing && bearing <= 112.5) {
            return 'Este'
        }
        if (112.5 < bearing && bearing <= 157.5) {
            return 'Sureste'
        }
        if (157.5 < bearing && bearing <= 180) {
            return 'Sur'
        }
        if (-180 < bearing && bearing <= -157.5) {
            return 'Sur'
        }
        if (-157.5 < bearing && bearing <= -112.5) {
            return 'Suroeste'
        }
        if (-112.5 < bearing && bearing <= -67.5) {
            return 'Oeste'
        }
        if (-67.5 < bearing && bearing <= -22.5) {
            return 'Noroeste'
        }
    };
    $scope.collapse = function (i) {
        var $collapse = angular.element('#collapse' + i);
        if ($collapse.hasClass('show')) {
            angular.element('#minus' + i).addClass('d-none');
            angular.element('#plus' + i).removeClass('d-none');
        } else {
            angular.element('.fa-minus').addClass('d-none');
            angular.element('.fa-plus').removeClass('d-none');
            angular.element('#plus' + i).addClass('d-none');
            angular.element('#minus' + i).removeClass('d-none');
        }
        $collapse.collapse('toggle');
    };

    $scope.paso2Save = function () {


        $http({
            method: 'PUT',
            url: '/api/DisenioPasivo/pasodoscomplete/' + divisionId
        }).then(function (response) {
            $rootScope.$broadcast('pasoDosComplete');
        }, function error(response) {

            $scope.loadAgrupamiento = '--Seleccione--';
            $ngConfirm({
                title: 'Error!',
                content: '<strong>Ha ocurrido un error: ' + response.data + '</strong>',
                buttons: {
                    Cerrar: function () {
                        // closes the modal
                    },
                }
            });
        }

        );

        $scope.$parent.step3.disabled = false;
        $scope.$parent.step2.completed = true;
        $scope.$parent.selected = 2;

    }

    $scope.setFrontis = function (index,muroId) {

        $scope.changeMarker(index);

        $http({
            method: 'PUT',
            url: '/api/DisenioPasivo/setFrontis/' + divisionId + '/' + muroId
        }).then(function (response) {
            if (response.data.ok) {

            } else {
                angular.element('#modalSession').modal('show');
            }

        }, function error(response) {

            $scope.loadAgrupamiento = '--Seleccione--';
            $ngConfirm({
                title: 'Error!',
                content: '<strong>Ha ocurrido un error: ' + response.data + '</strong>',
                buttons: {
                    Cerrar: function () {
                        // closes the modal
                    },
                }
            });
        }
        );
        
    }
});

disenioApp.controller('pasoTresController', function ($scope, $http, $rootScope, NgMap) {
    const ESTRUCTURA_TECHOS = 'Techos';
    const ESTRUCTURA_MUROS = 'Muros';
    const ESTRUCTURA_PISOS = 'Pisos';
    const ESTRUCTURA_VENTANAS = 'Ventanas';
    const ESTRUCTURA_PUERTAS = 'Puertas';
    const ESTRUCTURA_CIMIENTOS = 'Cimientos';

    $scope.paso = {};
    $scope.data = {};

    $scope.$on('pasoDosComplete', function () {

        LoadData();

    });


    function LoadData() {

        $http({
                method: 'GET',
                url: '/api/Estructura'
        })
            .then(function (response) {
                $scope.data.estructuras = response.data;
                for (var i in $scope.data.estructuras) {
                    if ($scope.data.estructuras[i].nombre == ESTRUCTURA_TECHOS) {
                        $scope.data.techos = $scope.data.estructuras[i];
                    }
                    if ($scope.data.estructuras[i].nombre == ESTRUCTURA_MUROS) {
                        $scope.data.muros = $scope.data.estructuras[i];
                    }
                    if ($scope.data.estructuras[i].nombre == ESTRUCTURA_PISOS) {
                        $scope.data.pisos = $scope.data.estructuras[i];
                    }
                    if ($scope.data.estructuras[i].nombre == ESTRUCTURA_VENTANAS) {
                        $scope.data.ventanas = $scope.data.estructuras[i];
                        $scope.data.tipo_cierre = $scope.data.ventanas.aislaciones.filter(function (el) {
                            return el.aislacion.subNivel == 0;
                        });
                        $scope.data.tipo_marco = $scope.data.ventanas.aislaciones.filter(function (el) {
                            return el.aislacion.subNivel == 1;
                        });
                    }

                    if ($scope.data.estructuras[i].nombre == ESTRUCTURA_PUERTAS) {
                        $scope.data.puertas = $scope.data.estructuras[i];
                    }
                    if ($scope.data.estructuras[i].nombre == ESTRUCTURA_CIMIENTOS) {
                        $scope.data.cimientos = $scope.data.estructuras[i];
                    }
                }
            }, function error(response) {

                $scope.loadAgrupamiento = '--Seleccione--';
                $ngConfirm({
                    title: 'Error!',
                    content: '<strong>Ha ocurrido un error: ' + response.data + '</strong>',
                    buttons: {
                        Cerrar: function () {
                            // closes the modal
                        },
                    }
                });
            }

        );


        $http({
            method: 'GET',
            url: '/api/Estructura/espesores'
        })
            .then(function (response) {
                $scope.data.espesores = response.data;
            }, function error(response) {

                $scope.loadAgrupamiento = '--Seleccione--';
                $ngConfirm({
                    title: 'Error!',
                    content: '<strong>Ha ocurrido un error: ' + response.data + '</strong>',
                    buttons: {
                        Cerrar: function () {
                            // closes the modal
                        },
                    }
                });
            }

        );

        $http({
            method: 'GET',
            url: '/api/diseniopasivo/pasotres/' + divisionId
        })
            .then(function (response) {
                $scope.paso = response.data;
            }, function error(response) {

                $scope.loadAgrupamiento = '--Seleccione--';
                $ngConfirm({
                    title: 'Error!',
                    content: '<strong>Ha ocurrido un error: ' + response.data + '</strong>',
                    buttons: {
                        Cerrar: function () {
                            // closes the modal
                        },
                    }
                });
            }

            );

    }

    

    $scope.resetAislacion = function () {
        if ($scope.paso.piso.aislacionId == null) {
            $scope.paso.piso.espesorId = null
        }

        if ($scope.paso.muro.aislacionIntId == null) {
            $scope.paso.muro.espesorId = null
        }
        if ($scope.paso.techo.aislacionId == null) {
            $scope.paso.techo.espesorId = null
        }
    };

    var divisionId = localStorage.getItem('divisionId');
    $scope.pasoTresSave = function () {
        $rootScope.loading = true;
        //if ($rootScope.muro_tmp) {
        //    $rootScope.paso.pisos[0].muros[0].materialidadId = $rootScope.muro_tmp.materialidadId;
        //    $rootScope.paso.pisos[0].muros[0].aislacionIntId = $rootScope.muro_tmp.aislacionIntId;
        //    $rootScope.paso.pisos[0].muros[0].aislacionExtId = $rootScope.muro_tmp.aislacionExtId;
        //    $rootScope.paso.pisos[0].muros[0].superficie = $scope.muro_tmp.superficie;
        //}
        //if (!$rootScope.suelo_tmp.materialidadId) {
        //    for (var i in $rootScope.paso.pisos) {
        //        $rootScope.paso.pisos[i].suelos = [];
        //    }
        //} else {
        //    if ($rootScope.paso.pisos[0].suelos.length > 0) {
        //        $rootScope.paso.pisos[0].suelos[0].materialidadId = $rootScope.suelo_tmp.materialidadId;
        //        if (!$rootScope.suelo_tmp.aislacionId || $rootScope.suelo_tmp.aislacionId == null) {
        //            $rootScope.paso.pisos[0].suelos[0].aislacionId = 28
        //        } else {
        //            $rootScope.paso.pisos[0].suelos[0].aislacionId = $rootScope.suelo_tmp.aislacionId;
        //        }

        //        $rootScope.paso.pisos[0].suelos[0].superficie = $rootScope.suelo_tmp.superficie;
        //    } else {
        //        if (!$rootScope.suelo_tmp.aislacionId || $rootScope.suelo_tmp.aislacionId == null) {
        //            $rootScope.suelo_tmp.aislacionId = 28
        //        } 
        //        $rootScope.paso.pisos[0].suelos.push($rootScope.suelo_tmp);
        //    }
        //}
       
        //if (!$rootScope.ventana_tmp.materialidadId) {
        //    for (var i in $rootScope.paso.pisos) {
        //        for (var j in $rootScope.paso.pisos[i].muros) {
        //            $rootScope.paso.pisos[i].muros[j].ventanas = [];
        //        }
        //    }
        //} else {
        //    if ($rootScope.paso.pisos[0].muros[0].ventanas.length > 0) {
        //        $rootScope.paso.pisos[0].muros[0].ventanas[0].materialidadId = $rootScope.ventana_tmp.materialidadId;
        //        if (!$rootScope.ventana_tmp.tipoCierreId || $rootScope.ventana_tmp.tipoCierreId == null) {
        //            $rootScope.paso.pisos[0].muros[0].ventanas[0].tipoCierreId = 28
        //        } else {
        //            $rootScope.paso.pisos[0].muros[0].ventanas[0].tipoCierreId = $rootScope.ventana_tmp.tipoCierreId;
        //        }
        //        if (!$rootScope.paso.pisos[0].muros[0].ventanas[0].tipoMarcoId || $rootScope.paso.pisos[0].muros[0].ventanas[0].tipoMarcoId == null) {
        //            $rootScope.paso.pisos[0].muros[0].ventanas[0].tipoMarcoId = 28
        //        } else {
        //            $rootScope.paso.pisos[0].muros[0].ventanas[0].tipoMarcoId = $rootScope.ventana_tmp.tipoMarcoId;
        //        }
               
        //        $rootScope.paso.pisos[0].muros[0].ventanas[0].superficie = $rootScope.ventana_tmp.superficie;
        //    } else {
        //        if (!$rootScope.ventana_tmp.tipoCierreId || $rootScope.ventana_tmp.tipoCierreId == null) {
        //            $rootScope.ventana_tmp.tipoCierreId = 28
        //        }
        //        if (!$rootScope.ventana_tmp.tipoMarcoId || $rootScope.ventana_tmp.tipoMarcoId == null) {
        //            $rootScope.ventana_tmp.tipoMarcoId = 28
        //        } 
        //        $rootScope.paso.pisos[0].muros[0].ventanas.push($rootScope.ventana_tmp);
        //    }
        //}

        //if (!$rootScope.puerta_tmp.materialidadId) {
        //    for (var i in $rootScope.paso.pisos) {
        //        for (var j in $rootScope.paso.pisos[i].muros) {
        //            $rootScope.paso.pisos[i].muros[j].puertas = [];
        //        }
        //    }
        //} else {
        //    if ($rootScope.paso.pisos[0].muros[0].puertas.length > 0) {
        //        $rootScope.paso.pisos[0].muros[0].puertas[0].materialidadId = $rootScope.puerta_tmp.materialidadId;
        //        if (!$rootScope.puerta_tmp.tipoMarcoId || $rootScope.puerta_tmp.tipoMarcoId == null) {
        //            $rootScope.puerta_tmp.tipoMarcoId = 28
        //        } 
        //        $rootScope.paso.pisos[0].muros[0].puertas[0].tipoMarcoId = $rootScope.puerta_tmp.tipoMarcoId;
        //        $rootScope.paso.pisos[0].muros[0].puertas[0].superficie = $rootScope.puerta_tmp.superficie;
        //    } else {
        //        if (!$rootScope.puerta_tmp.tipoMarcoId || $rootScope.puerta_tmp.tipoMarcoId == null) {
        //            $rootScope.puerta_tmp.tipoMarcoId = 28
        //        }
        //        $rootScope.paso.pisos[0].muros[0].puertas.push($rootScope.puerta_tmp);
        //    }
        //}

        $http({
            method: 'POST',
            url: '/api/DisenioPasivo/pasotres/' + divisionId,
            data: JSON.stringify($scope.paso)
        }).then(function (response) {
            if (response.data.ok) {
                $rootScope.$broadcast('pasoTresComplete');
                $rootScope.loading = false;
                $scope.$parent.step4.disabled = false;
                $scope.$parent.step3.completed = true;
                $scope.$parent.selected = 3;
            } else {
                angular.element('#modalSession').modal('show');
            }
           
        }, function error(response) {

            $scope.loadAgrupamiento = '--Seleccione--';
            $ngConfirm({
                title: 'Error!',
                content: '<strong>Ha ocurrido un error: ' + response.data + '</strong>',
                buttons: {
                    Cerrar: function () {
                        // closes the modal
                    },
                }
            });
        })

    };

    $scope.back_to_lv1 = function () {
        $http({
            method: 'POST',
            url: '/api/DisenioPasivo/paso3level/' + divisionId,

        }).then(function () {
            $rootScope.nivel1Paso3 = true;
            $rootScope.nivel2Paso3 = false;
        })
    };

    $scope.suelosLv2 = function () {
        $http({
            method: 'POST',
            url: '/api/DisenioPasivo/paso3level/' + divisionId,

        }).then(function () {
            $rootScope.polySvg();
            $rootScope.nivel1Paso3 = false;
            $rootScope.nivel2Paso3 = true;
            $rootScope.lv2Suelos = true;
        })
    };

    $scope.murosLv2 = function () {
        $http({
            method: 'POST',
            url: '/api/DisenioPasivo/paso3level/' + divisionId,

        }).then(function () {
            $rootScope.polySvg();
            $rootScope.nivel1Paso3 = false;
            $rootScope.nivel2Paso3 = true;
            $rootScope.lv2Suelos = false;
            $rootScope.lv2Muros = true;
        })
    };

    $scope.sel_piso = function (i, piso) {
        $rootScope.level2_piso_index_sel = i;
        $rootScope.level2_piso_sel = piso;
        $rootScope.level2_piso_sel.muro_sel = piso.muros[0];
    };

    $scope.addSueloForm = function () {
        var newSueloForm = {};
        $rootScope.level2_piso_sel.suelos.push(newSueloForm);

    };
    $scope.addVentanaForm = function () {
        var newVentanaForm = {};
        $rootScope.level2_piso_sel.muro_sel.ventanas.push(newVentanaForm);

    };

    $scope.remSueloForm = function (i) {
        $rootScope.level2_piso_sel.suelos.splice(i, 1);
    };
    $scope.remVentanaForm = function (i) {
        $rootScope.level2_piso_sel.muro_sel.ventanas.splice(i, 1);
    };

    $scope.suelos_lv2_save = function () {

        $http({
            method: 'POST',
            url: '/api/DisenioPasivo/sueloslv2/' + divisionId,
            data: JSON.stringify($rootScope.paso)
        }).then(function () {
            $rootScope.lv2Suelos = false;
            $rootScope.lv2Muros = true;
        })
    };
    $scope.muros_lv2_save = function () {

        $http({
            method: 'POST',
            url: '/api/DisenioPasivo/muroslv2/' + divisionId,
            data: JSON.stringify($rootScope.paso)
        }).then(function () {
            $rootScope.lv2Suelos = false;
            $rootScope.lv2Muros = false;
            $rootScope.lv2Ventanas = true;
        })
    };

    $scope.ventanas_lv2_save = function () {

        $http({
            method: 'POST',
            url: '/api/DisenioPasivo/ventanaslv2/' + divisionId,
            data: JSON.stringify($rootScope.paso)
        }).then(function () {
            //$rootScope.lv2Suelos = false;
            //$rootScope.lv2Muros = true;
        })
    };

    $scope.back_to_suelos = function () {
        $rootScope.lv2Suelos = true;
        $rootScope.lv2Muros = false;
    }
    $scope.back_to_muros = function () {
        $rootScope.lv2Ventanas = false;
        $rootScope.lv2Muros = true;
    }
});

disenioApp.controller('pasoCuatroController', function ($scope, $http, $rootScope, NgMap) {
    const SECCION_ENVOLVENTES = 'Envolventes';
    const SECCION_DETALLES = 'Detalles';
    const SECCION_PROBLEMAS = 'Problemas';
    const SECCION_ARQUITECTURA = 'Arquitectura';
    const SECCION_ELEVACIONES = 'Elevaciones';
    const SECCION_ESTRUCTURALES = 'Estructurales';
    const SECCION_ESPECIALIDAD = 'Especialidad';
    $scope.envolventes = [];
    $scope.detalles = [];
    $scope.problemas = [];
    $scope.arquitectura = [];
    $scope.elevaciones = [];
    $scope.estructurales = [];
    $scope.especialidad = [];
    $scope.files = [];
    $scope.editMode = false;
    $scope.editModeText = 'Editar';
    $scope.selectedSection = null;
    $scope.paso = {};

    $scope.$on('pasoTresComplete', function () {

        LoadData();

    });

    function LoadData() {
        $scope.serverFilesEnv = [];
        $scope.serverFilesDet = [];
        $scope.serverFilesProb = [];
        $scope.serverFilesArq = [];
        $scope.serverFilesEle = [];
        $scope.serverFilesEst = [];
        $scope.serverFilesEsp = [];


        $http({
            method: 'GET',
            url: '/api/diseniopasivo/pasocuatro/' + divisionId
        }).then(function (response) {
            $scope.paso = {};
            $scope.paso.archivos = response.data.archivos;
            for (var i in $scope.paso.archivos) {
                var serverFile = {
                    id: $scope.paso.archivos[i].id,
                    name: $scope.paso.archivos[i].nombre,
                    filename: $scope.paso.archivos[i].nombreArchivo,
                    description: $scope.paso.archivos[i].descripcion,
                    size: $scope.paso.archivos[i].peso,
                    serverImgUrl: $scope.paso.archivos[i].fileUrl,
                    fecha: $scope.paso.archivos[i].fecha,
                    loaded: true,
                    seccion: $scope.paso.archivos[i].seccion
                };
                switch ($scope.paso.archivos[i].seccion) {
                    case SECCION_ENVOLVENTES:
                        $scope.serverFilesEnv.push(serverFile);
                        break;
                    case SECCION_DETALLES:
                        $scope.serverFilesDet.push(serverFile);
                        break;
                    case SECCION_PROBLEMAS:
                        $scope.serverFilesProb.push(serverFile);
                        break;
                    case SECCION_ARQUITECTURA:
                        $scope.serverFilesArq.push(serverFile);
                        break;
                    case SECCION_ELEVACIONES:
                        $scope.serverFilesEle.push(serverFile);
                        break;
                    case SECCION_ESTRUCTURALES:
                        $scope.serverFilesEst.push(serverFile);
                        break;
                    case SECCION_ESPECIALIDAD:
                        $scope.serverFilesEsp.push(serverFile);
                        break;
                }
            }
            $scope.myDz = $scope.dzMethodsEnv.getDropzone();
            $scope.myDzDet = $scope.dzMethodsDetalles.getDropzone();
            $scope.myDzProb = $scope.dzMethodsProblemas.getDropzone();
            $scope.myDzArq = $scope.dzMethodsArq.getDropzone();
            $scope.myDzEle = $scope.dzMethodsElevaciones.getDropzone();
            $scope.myDzEst = $scope.dzMethodsEstructurales.getDropzone();
            $scope.myDzEsp = $scope.dzMethodsEspecialidad.getDropzone();
            if ($scope.myDz.files.length == 0) {
                $scope.serverFilesEnv.forEach(function (serverFile) {
                    $scope.myDz.emit('addedfile', serverFile);
                    $scope.myDz.files.push(serverFile);
                });
            }
            if ($scope.myDzDet.files == 0) {
                $scope.serverFilesDet.forEach(function (serverFile) {
                    $scope.myDzDet.emit('addedfile', serverFile);
                    $scope.myDzDet.files.push(serverFile);
                });
            }

            if ($scope.myDzProb.files.length == 0) {
                $scope.serverFilesProb.forEach(function (serverFile) {
                    $scope.myDzProb.emit('addedfile', serverFile);
                    $scope.myDzProb.files.push(serverFile);
                });
            }
            if ($scope.myDzArq.files.length == 0) {
                $scope.serverFilesArq.forEach(function (serverFile) {
                    $scope.myDzArq.emit('addedfile', serverFile);
                    $scope.myDzArq.files.push(serverFile);
                });
            }
            if ($scope.myDzEle.files.length == 0) {
                $scope.serverFilesEle.forEach(function (serverFile) {
                    $scope.myDzEle.emit('addedfile', serverFile);
                    $scope.myDzEle.files.push(serverFile);
                });
            }
            if ($scope.myDzEst.files.length == 0) {
                $scope.serverFilesEst.forEach(function (serverFile) {
                    $scope.myDzEst.emit('addedfile', serverFile);
                    $scope.myDzEst.files.push(serverFile);
                });
            }
            if ($scope.myDzEsp.files.length == 0) {
                $scope.serverFilesEsp.forEach(function (serverFile) {
                    $scope.myDzEsp.emit('addedfile', serverFile);
                    $scope.myDzEsp.files.push(serverFile);
                });
            }


        }, function () {
                $scope.loading = false;
                $ngConfirm({
                    title: 'Error!',
                    content: '<strong>Ha ocurrido un error: ' + response.data + '</strong>',
                    buttons: {
                        Cerrar: function () {
                            // closes the modal
                        },
                    }
                });
        });

        
    };

    $scope.toogleEditMode = function () {
        if ($scope.editMode) {

            $http({
                method: 'POST',
                url: '/api/DisenioPasivo/updatefile',
                data: JSON.stringify($scope.selectedFile)
            }).then(function (response) {
                if (response.data.ok) {
                    var text = angular.element("#namefile" + $scope.selectedFile.id);
                    text[0].innerText = $scope.selectedFile.nombre;
                } else {
                    angular.element('#modalSession').modal('show');
                }

            })

        };

        $scope.editMode = !$scope.editMode;
        if ($scope.editMode) {
            $scope.editModeText = 'Guardar';
        } else {
            $scope.editModeText = 'Editar';
        }
    };

    $scope.selectFile = function (id, seccion) {
        switch (seccion) {
            case SECCION_ENVOLVENTES:
                $scope.selectedSection = $scope.envolventes;
                $scope.selectedSection.section = SECCION_ENVOLVENTES;
                break;
            case SECCION_DETALLES:
                $scope.selectedSection = $scope.detalles;
                $scope.selectedSection.section = SECCION_DETALLES;
                break;
            case SECCION_PROBLEMAS:
                $scope.selectedSection = $scope.problemas;
                $scope.selectedSection.section = SECCION_PROBLEMAS;
                break;
            case SECCION_ARQUITECTURA:
                $scope.selectedSection = $scope.arquitectura;
                $scope.selectedSection.section = SECCION_ARQUITECTURA;
                break;
            case SECCION_ELEVACIONES:
                $scope.selectedSection = $scope.elevaciones;
                $scope.selectedSection.section = SECCION_ELEVACIONES;
                break;
            case SECCION_ESTRUCTURALES:
                $scope.selectedSection = $scope.estructurales;
                $scope.selectedSection.section = SECCION_ESTRUCTURALES;
                break;
            case SECCION_ESPECIALIDAD:
                $scope.selectedSection = $scope.especialidad;
                $scope.selectedSection.section = SECCION_ESPECIALIDAD;
                break;
        };
        if (seccion == SECCION_ARQUITECTURA || seccion == SECCION_ELEVACIONES || seccion == SECCION_ESTRUCTURALES || seccion == SECCION_ESPECIALIDAD) {
           
                for (var i = 0; i < $scope.selectedSection.length;i++ ) {

                    var extension = $scope.selectedSection[i].nombreArchivo.split('.')[1];
                    if (extension.match('pdf')) {
                        $scope.selectedSection[i].fileUrl = "/images/Recurso 12PDF.svg";
                    }
                    if (extension.match('jpeg') || extension.match('jpg')) {
                        $scope.selectedSection[i].fileUrl = "/images/Recurso 13JPG.svg";
                    }

                    if (extension.match('dwg')) {
                        $scope.selectedSection[i].fileUrl = "/images/Recurso 14DWG.svg";
                    }
                }
                
        }
        for (var i in $scope.selectedSection) {
            if ($scope.selectedSection[i].id == id) {
                $scope.selectedFile = $scope.selectedSection[i];
            }
        }
        var modal = angular.element('#modalArchivo');
        modal.modal('show');
    }

    $scope.confirmDeleteFile = function (id,seccion) {
        if (id > 0) {
            switch (seccion) {
                case SECCION_ENVOLVENTES:
                    $scope.selectedSection = $scope.envolventes
                    $scope.selectedSection.section = SECCION_ENVOLVENTES;
                    break;
                case SECCION_DETALLES:
                    $scope.selectedSection = $scope.detalles;
                    $scope.selectedSection.section = SECCION_DETALLES;
                    break;
                case SECCION_PROBLEMAS:
                    $scope.selectedSection = $scope.problemas;
                    $scope.selectedSection.section = SECCION_PROBLEMAS;
                    break;
                case SECCION_ARQUITECTURA:
                    $scope.selectedSection = $scope.arquitectura;
                    $scope.selectedSection.section = SECCION_ARQUITECTURA;
                    break;
                case SECCION_ELEVACIONES:
                    $scope.selectedSection = $scope.elevaciones;
                    $scope.selectedSection.section = SECCION_ELEVACIONES;
                    break;
                case SECCION_ESTRUCTURALES:
                    $scope.selectedSection = $scope.estructurales;
                    $scope.selectedSection.section = SECCION_ESTRUCTURALES;
                    break;
                case SECCION_ESPECIALIDAD:
                    $scope.selectedSection = $scope.especialidad;
                    $scope.selectedSection.section = SECCION_ESPECIALIDAD;
                    break;
            };
            for (var i in $scope.selectedSection) {
                if ($scope.selectedSection[i].id == id) {
                    $scope.selectedFile = $scope.selectedSection[i];
                }
            }
        }
        var modal = angular.element('#modalConfirmFiles');
        modal.modal('show');
    }

    $scope.deleteFile = function () {
        $http({
            method: 'POST',
            url: '/api/DisenioPasivo/deletefile/' + $scope.selectedFile.id
        }).then(function (response) {
            if (response.data.ok) {
                var $dzm = null;
                switch ($scope.selectedSection.section) {
                    case SECCION_ENVOLVENTES:
                        $dzm = $scope.dzMethodsEnv;
                        break;
                    case SECCION_DETALLES:
                        $dzm = $scope.dzMethodsDetalles;
                        break;
                    case SECCION_PROBLEMAS:
                        $dzm = $scope.dzMethodsProblemas;
                        break;
                    case SECCION_ARQUITECTURA:
                        $dzm = $scope.dzMethodsArq;
                        break;
                    case SECCION_ELEVACIONES:
                        $dzm = $scope.dzMethodsElevaciones;
                        break;
                    case SECCION_ESTRUCTURALES:
                        $dzm = $scope.dzMethodsEstructurales;
                        break;
                    case SECCION_ESPECIALIDAD:
                        $dzm = $scope.dzMethodsEspecialidad;
                        break;
                }

                for (var i in $scope.selectedSection) {
                    if ($scope.selectedSection[i].id == $scope.selectedFile.id) {

                        if ($scope.selectedSection.length == 1) {
                            $scope.selectedSection.splice(0, 1);
                            for (var i in $scope.files) {
                                if ($scope.files[i].id == $scope.selectedFile.id) {
                                    $dzm.removeFile($scope.files[i]);
                                    $scope.files.splice(i, 1);
                                    var modal = angular.element('#modalConfirmFiles');
                                    modal.modal('hide');
                                    var modal = angular.element('#modalArchivo');
                                    modal.modal('hide');
                                    break;
                                }
                            }
                        }

                        if ($scope.selectedSection.length > 1) {
                            $scope.selectedSection.splice(i, 1);
                            for (var i in $scope.files) {
                                if ($scope.files[i].id == $scope.selectedFile.id) {
                                    $dzm.removeFile($scope.files[i]);
                                    $scope.files.splice(i, 1);
                                    var modal = angular.element('#modalConfirmFiles');
                                    modal.modal('hide');
                                    break;

                                }
                            }
                            $scope.selectedFile = $scope.selectedSection[0];
                        }
                    }
                }

                switch ($scope.selectedFile.section) {
                    case SECCION_ENVOLVENTES:
                        $scope.envolventes = $scope.selectedSection;
                        break;
                    case SECCION_DETALLES:
                        $scope.detalles = $scope.selectedSection;
                        break;
                    case SECCION_PROBLEMAS:
                        $scope.problemas = $scope.selectedSection;
                        break;
                    case SECCION_ARQUITECTURA:
                        $scope.arquitectura = $scope.selectedSection;
                        break;
                    case SECCION_ELEVACIONES:
                        $scope.elevaciones = $scope.selectedSection;
                        break;
                    case SECCION_ESTRUCTURALES:
                        $scope.estructurales = $scope.selectedSection;
                        break;
                    case SECCION_ESPECIALIDAD:
                        $scope.especialidad = $scope.selectedSection;
                        break;
                }

            } else {
                angular.element('#modalSession').modal('show');
            }
            

        })
    };

    $scope.dzOptionsEnv = {
        previewTemplate: "<div class=\"dz-preview dz-file-preview\">\n <div class=\"dz-image\"><img data-dz-thumbnail /></div>\n <div class=\"dz-details\">\n <div class=\"dz-filename text-center\"><span data-dz-name></span></div>\n   <div class=\"dz-size\"><span data-dz-size></span></div>\n </div> \n  <div class=\"dz-progress\"><span class=\"dz-upload\" data-dz-uploadprogress></span></div> \n  <div class=\"dz-error-message\"><span data-dz-errormessage></span></div>\n  <div class=\"dz-success-mark\"></div>\n  <div class=\"dz-error-mark\">\n    </div>\n</div>",
        url: '/api/DisenioPasivo/paso4archivos/' + divisionId + '/envolventes',
        dictDefaultMessage: "Arrastra tus fotos acá"
    };

    $scope.dzOptionsDetalles = {
        previewTemplate: "<div class=\"dz-preview dz-file-preview\">\n <div class=\"dz-image\"><img data-dz-thumbnail /></div>\n <div class=\"dz-details\">\n <div class=\"dz-filename text-center\"><span data-dz-name></span></div>\n   <div class=\"dz-size\"><span data-dz-size></span></div>\n </div> \n  <div class=\"dz-progress\"><span class=\"dz-upload\" data-dz-uploadprogress></span></div> \n  <div class=\"dz-error-message\"><span data-dz-errormessage></span></div>\n  <div class=\"dz-success-mark\"></div>\n  <div class=\"dz-error-mark\">\n    </div>\n</div>",
        url: '/api/DisenioPasivo/paso4archivos/' + divisionId + '/detalles',
        dictDefaultMessage: "Arrastra tus fotos acá"
    };
    $scope.dzOptionsProblemas = {
        previewTemplate: "<div class=\"dz-preview dz-file-preview\">\n <div class=\"dz-image\"><img data-dz-thumbnail /></div>\n <div class=\"dz-details\">\n <div class=\"dz-filename text-center\"><span data-dz-name></span></div>\n   <div class=\"dz-size\"><span data-dz-size></span></div>\n </div> \n  <div class=\"dz-progress\"><span class=\"dz-upload\" data-dz-uploadprogress></span></div> \n  <div class=\"dz-error-message\"><span data-dz-errormessage></span></div>\n  <div class=\"dz-success-mark\"></div>\n  <div class=\"dz-error-mark\">\n    </div>\n</div>",
        url: '/api/DisenioPasivo/paso4archivos/' + divisionId + '/problemas',
        dictDefaultMessage: "Arrastra tus fotos acá"
    };
    $scope.dzOptionsArq = {
        previewTemplate: "<div class=\"dz-preview dz-file-preview\">\n <div class=\"dz-image\"><img data-dz-thumbnail /></div>\n <div class=\"dz-details\">\n <div class=\"dz-filename text-center\"><span data-dz-name></span></div>\n   <div class=\"dz-size\"><span data-dz-size></span></div>\n </div> \n  <div class=\"dz-progress\"><span class=\"dz-upload\" data-dz-uploadprogress></span></div> \n  <div class=\"dz-error-message\"><span data-dz-errormessage></span></div>\n  <div class=\"dz-success-mark\"></div>\n  <div class=\"dz-error-mark\">\n    </div>\n</div>",
        url: '/api/DisenioPasivo/paso4archivos/' + divisionId + '/arquitectura',
        dictDefaultMessage: "Arrastra tus archivos acá",
        thumbnailWidth: 100
    };
    $scope.dzOptionsElevaciones = {
        previewTemplate: "<div class=\"dz-preview dz-file-preview\">\n <div class=\"dz-image\"><img data-dz-thumbnail /></div>\n <div class=\"dz-details\">\n <div class=\"dz-filename text-center\"><span data-dz-name></span></div>\n   <div class=\"dz-size\"><span data-dz-size></span></div>\n </div> \n  <div class=\"dz-progress\"><span class=\"dz-upload\" data-dz-uploadprogress></span></div> \n  <div class=\"dz-error-message\"><span data-dz-errormessage></span></div>\n  <div class=\"dz-success-mark\"></div>\n  <div class=\"dz-error-mark\">\n    </div>\n</div>",
        url: '/api/DisenioPasivo/paso4archivos/' + divisionId + '/elevaciones',
        dictDefaultMessage: "Arrastra tus archivos acá",
        thumbnailWidth: 100
    };
    $scope.dzOptionsEstructurales = {
        previewTemplate: "<div class=\"dz-preview dz-file-preview\">\n <div class=\"dz-image\"><img data-dz-thumbnail /></div>\n <div class=\"dz-details\">\n <div class=\"dz-filename text-center\"><span data-dz-name></span></div>\n   <div class=\"dz-size\"><span data-dz-size></span></div>\n </div> \n  <div class=\"dz-progress\"><span class=\"dz-upload\" data-dz-uploadprogress></span></div> \n  <div class=\"dz-error-message\"><span data-dz-errormessage></span></div>\n  <div class=\"dz-success-mark\"></div>\n  <div class=\"dz-error-mark\">\n    </div>\n</div>",
        url: '/api/DisenioPasivo/paso4archivos/' + divisionId + '/estructurales',
        dictDefaultMessage: "Arrastra tus archivos acá",
        thumbnailWidth: 100
    };
    $scope.dzOptionsEspecialidad = {
        previewTemplate: "<div class=\"dz-preview dz-file-preview\">\n <div class=\"dz-image\"><img data-dz-thumbnail /></div>\n <div class=\"dz-details\">\n <div class=\"dz-filename text-center\"><span data-dz-name></span></div>\n   <div class=\"dz-size\"><span data-dz-size></span></div>\n </div> \n  <div class=\"dz-progress\"><span class=\"dz-upload\" data-dz-uploadprogress></span></div> \n  <div class=\"dz-error-message\"><span data-dz-errormessage></span></div>\n  <div class=\"dz-success-mark\"></div>\n  <div class=\"dz-error-mark\">\n    </div>\n</div>",
        url: '/api/DisenioPasivo/paso4archivos/' + divisionId + '/especialidad',
        dictDefaultMessage: "Arrastra tus archivos acá",
        thumbnailWidth: 100
    };

    $scope.dzCallbacks = {
        'addedfile': function (file) {
            if (file.loaded) {
                switch (file.seccion) {
                    case SECCION_ENVOLVENTES:
                        $scope.myDz.createThumbnailFromUrl(file, file.serverImgUrl, null, true);
                        break;
                    case SECCION_DETALLES:
                        $scope.myDzDet.createThumbnailFromUrl(file, file.serverImgUrl, null, true);
                        break;
                    case SECCION_PROBLEMAS:
                        $scope.myDzProb.createThumbnailFromUrl(file, file.serverImgUrl, null, true);
                        break;
                    case SECCION_ARQUITECTURA:
                        var extension = file.filename.split('.')[1];
                        if (extension.match('pdf')) {
                            $scope.myDzArq.emit("thumbnail", file, "/images/Recurso 12PDF.svg");
                        }
                        if (extension.match('jpeg') || extension.match('jpg')) {
                            $scope.myDzArq.emit("thumbnail", file, "/images/Recurso 13JPG.svg");
                        }
                        
                        if (extension.match('dwg')) {
                            $scope.myDzArq.emit("thumbnail", file, "/images/Recurso 14DWG.svg");
                        }
                        break;
                    case SECCION_ELEVACIONES:
                        var extension = file.filename.split('.')[1];
                        if (extension.match('pdf')) {
                            $scope.myDzEle.emit("thumbnail", file, "/images/Recurso 12PDF.svg");
                        }
                        if (extension.match('jpeg') || extension.match('jpg')) {
                            $scope.myDzEle.emit("thumbnail", file, "/images/Recurso 13JPG.svg");
                        }

                        if (extension.match('dwg')) {
                            $scope.myDzEle.emit("thumbnail", file, "/images/Recurso 14DWG.svg");
                        }
                        break;
                    case SECCION_ESTRUCTURALES:
                        var extension = file.filename.split('.')[1];
                        if (extension.match('pdf')) {
                            $scope.myDzEst.emit("thumbnail", file, "/images/Recurso 12PDF.svg");
                        }
                        if (extension.match('jpeg') || extension.match('jpg')) {
                            $scope.myDzEst.emit("thumbnail", file, "/images/Recurso 13JPG.svg");
                        }

                        if (extension.match('dwg')) {
                            $scope.myDzEst.emit("thumbnail", file, "/images/Recurso 14DWG.svg");
                        }
                        break;
                    case SECCION_ESPECIALIDAD:
                        var extension = file.filename.split('.')[1];
                        if (extension.match('pdf')) {
                            $scope.myDzEst.emit("thumbnail", file, "/images/Recurso 12PDF.svg");
                        }
                        if (extension.match('jpeg') || extension.match('jpg')) {
                            $scope.myDzEst.emit("thumbnail", file, "/images/Recurso 13JPG.svg");
                        }

                        if (extension.match('dwg')) {
                            $scope.myDzEst.emit("thumbnail", file, "/images/Recurso 14DWG.svg");
                        }
                        break;
                }
                $scope.loadFile(file, null);
            }
        },
        'success': function (file, responseText) {
            if (responseText.seccion == SECCION_ARQUITECTURA || responseText.seccion == SECCION_ELEVACIONES || responseText.seccion == SECCION_ESTRUCTURALES || responseText.seccion == SECCION_ESPECIALIDAD ) {
                
                    
                    if (file.type.match('application/pdf')) {
                        $scope.myDzArq.emit("thumbnail", file, "/images/Recurso 12PDF.svg");
                    }
                    if (file.type.match('image/jpeg')) {
                        $scope.myDzArq.emit("thumbnail", file, "/images/Recurso 13JPG.svg");
                    }
                    var extension = responseText.nombreArchivo.split('.')[1];
                    if (extension.match('dwg')) {
                        $scope.myDzArq.emit("thumbnail", file, "/images/Recurso 14DWG.svg");
                    }

            }
            $scope.loadFile(file, responseText);
        }
    };

    $scope.collapse = function (i) {
        var $collapse = angular.element('#' + i);
        if ($collapse.hasClass('show')) {
            angular.element('.fa-minus').addClass('d-none');
            angular.element('.fa-plus').removeClass('d-none');
        } else {
            angular.element('.fa-plus').removeClass('d-none');
            angular.element('.fa-minus').addClass('d-none');
            angular.element('#minus-' + i).removeClass('d-none');
            angular.element('#plus-' + i).addClass('d-none');
        }
        $collapse.collapse('toggle');
    };

    $scope.loadFile = function (file, responseText) {
        if (!responseText) {
            responseText = {};
            responseText.id = file.id;
            responseText.nombre = file.name;
            responseText.nombreArchivo = file.filename;
            responseText.descripcion = file.description;
            responseText.fileUrl = file.serverImgUrl;
            responseText.fecha = file.fecha;
            responseText.seccion = file.seccion;
        } else {
            file.id = responseText.id;
            file.seccion = responseText.seccion;
        };

        if (responseText.nombre == null) {
            responseText.nombre = 'Archivo ' + responseText.id;
        };
        var newFile = {
            nombre: responseText.nombre,
            nombreArchivo: responseText.nombreArchivo,
            descripcion: responseText.descripcion,
            fileUrl: responseText.fileUrl,
            id: responseText.id,
            fecha: responseText.fecha
        };
        switch (responseText.seccion) {
            case SECCION_ENVOLVENTES:
                $scope.envolventes.push(newFile);
                break;
            case SECCION_DETALLES:
                $scope.detalles.push(newFile);
                break;
            case SECCION_PROBLEMAS:
                $scope.problemas.push(newFile);
                break;
            case SECCION_ARQUITECTURA:
                $scope.arquitectura.push(newFile);
                break;
            case SECCION_ELEVACIONES:
                $scope.elevaciones.push(newFile);
                break;
            case SECCION_ESTRUCTURALES:
                $scope.estructurales.push(newFile);
                break;
            case SECCION_ESPECIALIDAD:
                $scope.especialidad.push(newFile);
                break;
        }

        $scope.files.push(file);


        var name = document.createElement('div');
        name.className = 'text-center';
        name.setAttribute('id', 'namefile' + responseText.id);
        var name_text = document.createTextNode(responseText.nombre)
        name.appendChild(name_text);
        file.previewTemplate.insertBefore(name, file.previewTemplate.children[2]);

        var date = document.createElement('div');
        date.className = 'text-center';
        var date_small = document.createElement('small');
        var date_strong = document.createElement('strong');

        var dateFormat = new Date(responseText.fecha);
        var dd = dateFormat.getDate();
        var mm = dateFormat.getMonth() + 1; //January is 0!

        var yyyy = dateFormat.getFullYear();
        if (dd < 10) {
            dd = '0' + dd;
        }
        if (mm < 10) {
            mm = '0' + mm;
        }
        var dateText = dd + '/' + mm + '/' + yyyy;


        var date_text = document.createTextNode(dateText);

        date_strong.appendChild(date_text);
        date_small.appendChild(date_strong);
        date.appendChild(date_small);
        file.previewTemplate.insertBefore(date, file.previewTemplate.children[3]);
        var row_buttons = document.createElement('div');
        row_buttons.className = 'row ml-3';

        var a_edit = document.createElement('a');
        a_edit.className = 'btn btn-primary dz-edit-link'
        //var link = document.createTextNode('Editar archivo');
        var icon_edit = document.createElement('i');
        icon_edit.className = 'fa fa-edit'
        icon_edit.setAttribute('title', 'Editar');
        a_edit.appendChild(icon_edit);
        a_edit.href = 'javascript:selectFile(' + responseText.id + ',"' + responseText.seccion+'")';

        var a_delete = document.createElement('a');
        a_delete.className = 'btn btn-danger'
        //var link = document.createTextNode('Editar archivo');
        var icon_delete = document.createElement('i');
        icon_delete.className = 'fa fa-trash'
        icon_delete.setAttribute('title', 'Eliminar');
        a_delete.appendChild(icon_delete);
        a_delete.href = 'javascript:deleteFile(' + responseText.id + ',"' + responseText.seccion + '")';
        //file.previewTemplate.appendChild(document.createTextNode(responseText));

        row_buttons.appendChild(a_edit);
        row_buttons.appendChild(a_delete);
        file.previewTemplate.appendChild(row_buttons);
        var row_check = document.createElement("div");
        row_check.className = "row ml-3";
        var i_check = document.createElement("INPUT");
        i_check.setAttribute("type", "checkbox");
        i_check.setAttribute("id", "sel" + responseText.id);
        i_check.setAttribute("style", "margin-left:30px;");
        i_check.setAttribute("file-id", responseText.id);
        if (file.seccion == SECCION_ENVOLVENTES || file.seccion == SECCION_DETALLES || file.seccion == SECCION_PROBLEMAS) {
            i_check.className = "form-check-input chk-photos";
        } else {
            i_check.className = "form-check-input chk-files";
        }

        row_check.appendChild(i_check);
        file.previewTemplate.appendChild(row_check);
        //console.log(file);

    }

    $scope.nextImg = function () {
        for (var i in $scope.selectedSection) {
            var pos = parseInt(i);
            if ($scope.selectedSection[pos].id == $scope.selectedFile.id) {
                if (pos + 1 == $scope.selectedSection.length) {
                    $scope.selectedFile = $scope.selectedSection[0];

                } else {
                    $scope.selectedFile = $scope.selectedSection[pos + 1];

                }
                break;
            }
        }
    };

    $scope.prevtImg = function () {
        for (var i in $scope.selectedSection) {
            var pos = parseInt(i);
            if ($scope.selectedSection[pos].id == $scope.selectedFile.id) {
                if (pos == 0) {
                    $scope.selectedFile = $scope.selectedSection[$scope.selectedSection.length - 1];

                } else {
                    $scope.selectedFile = $scope.selectedSection[pos - 1];

                }

                break;
            }
        }
    };

    $scope.showModalAyudaEnvolvente = function () {
        var modal = angular.element('#modalAyudaEnvolvente');
        modal.modal('show');
    };
    $scope.showModalAyudaDetalles = function () {
        var modal = angular.element('#modalAyudaDetalles');
        modal.modal('show');
    };
    $scope.showModalAyudaProblemas = function () {
        var modal = angular.element('#modalAyudaProblemas');
        modal.modal('show');
    };
    $scope.showModalAyudaArquitectura = function () {
        var modal = angular.element('#modalAyudaArquitectura');
        modal.modal('show');
    };
    $scope.showModalAyudaEscantillon = function () {
        var modal = angular.element('#modalAyudaEscantillon');
        modal.modal('show');
    };
    $scope.showModalAyudaEstructura = function () {
        var modal = angular.element('#modalAyudaEstructura');
        modal.modal('show');
    };
    $scope.showModalAyudaEspecialidad = function () {
        var modal = angular.element('#modalAyudaEspecialidad');
        modal.modal('show');
    };

    $scope.myDz = null;
    $scope.dzMethodsEnv = {};


    $scope.downloadPhotos = function () {
        var ids = [];
        var filerray = angular.element(".chk-photos");
        for (var i = 0; i < filerray.length; i++) {
            if (filerray[i].checked) {
                //console.log("checked", filerray[i].getAttribute("file-id"));
                ids.push( parseInt(filerray[i].getAttribute("file-id")));
            }
        }
        if (ids.length > 0) {
            var strIds = ids.join("-");
            downFiles(strIds);
        }
         
    }

    $scope.downloadBluePrints = function () {
        var ids = [];
        var filerray = angular.element(".chk-files");
        for (var i = 0; i < filerray.length; i++) {
            if (filerray[i].checked) {
                //console.log("checked", filerray[i].getAttribute("file-id"));
                ids.push(parseInt(filerray[i].getAttribute("file-id")));
            }
        }
        if (ids.length > 0) {
            var strIds = ids.join("-");
            downFiles(strIds);
        }

    }

    function downFiles(ids) {
        $http({
            method: 'GET',
            url: '/api/DisenioPasivo/downloadfiles',
            params: {
                "ids": ids
            },
            responseType: 'arraybuffer'
        }).then(function (response) {

            var blob = new Blob([response.data], { type: "application/zip" });


            var url = URL.createObjectURL(blob);

            var a = document.createElement("a");
            document.body.appendChild(a);
            a.style = "display: none";

            a.href = url
            a.download = "paso4.zip";
            a.click();
        });
    }

});
