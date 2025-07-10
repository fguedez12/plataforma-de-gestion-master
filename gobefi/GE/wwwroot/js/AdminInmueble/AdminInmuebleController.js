(function () {
    'use strict';

    var inmuebleApp = angular.module('inmuebleApp', ['ngMap', 'common', 'ngAnimate', 'cp.ngConfirm']);

    inmuebleApp.controller('inmuebleController', function ($scope, $http, $rootScope, $timeout, $ngConfirm, $q) {

     

        const COMPLEJOTEXT = "Complejo";
        const EDIFICIOTEXT = "Edificio";
        const MaxPagination = 10; 
        $scope.pageNumber = 1;
        $scope.startPage = 1;
        $scope.inmuebleText = COMPLEJOTEXT;
        $scope.inmueble = {};
        $scope.edificio = {};
        $scope.inmueble.tipoInmueble = 1;
        $scope.edificio.tipoInmueble = 2;
        $scope.inmuebleList = null;
        $scope.showSt0 = true;
        $scope.showSt1 = false;
        $scope.showSt2 = false;
        $scope.showSt3 = false;
        $scope.showDel = false;
        $scope.tipoEdificio = false;
        $scope.tipoComplejo = true;
        $scope.inmueble.tipoAdministracionId = "1";
        $scope.edificio.tipoAdministracionId = "1";
        $scope.disabledServicio = true;
        $scope.disabledServicioEdificio = true;
        $scope.disabledInstitucion = false;
        $scope.disabledInstitucionEdificio = false;
        $scope.requiredIS = true;
        $scope.requiredISEdificio = true;
        $scope.editModeInmueble = false;
        $scope.pisosIguales = false;
        $scope.piso = {};
        $rootScope.bajoPisos = [];
        $rootScope.sobrePisos = [];
        $scope.data = {};
        $rootScope.inmuebleSearched = false;
        $scope.modalPisoTitulo = "Agregar Piso";
        $scope.modalPisoTitulomodalAreaTitulo = "Agregar Área";
        $scope.modalEdificioTitle = "Agregar Edificio";
        $scope.editArea = false;
        $scope.editEdificio = false;
        $scope.minValidatorAnio = 0;

        $http({
            method: 'GET',
            url: `/settings?sectionName=ApiConfiguration&paramName=apiGestionaEnergia`

        }).then(function (response) {
            //console.log("ReadDataFromAppSettings", response);
            $rootScope.APIURL = response.data;
            loadData();

        })

   

        $scope.$on('addinmueble', function () {
            let inmueble = {};
            $scope.editModeInmueble = false;
            inmueble.calle = $rootScope.direccion.calle;
            inmueble.numero = $rootScope.direccion.numero;
            inmueble.comuna = $rootScope.direccion.comuna;
            inmueble.region = $rootScope.direccion.region;
            console.log(inmueble);
            $scope.inmueble = inmueble;
            $scope.inmueble.tipoAdministracionId = "1";
            $scope.inmueble.tipoInmueble = 1;
            $scope.showSt1 = false;
            $timeout(function () {
                $scope.showSt2 = true;
            }, 700)
        });


        $scope.deleteEdificio = function (inmuebleId, inmueble) {
            $http({
                method: 'DELETE',
                url: `${$rootScope.APIURL}/api/divisiones/delete/${inmuebleId}`,
            }).then(function (response) {
                inmueble.children = inmueble.children.filter(el => el.id != inmuebleId);
                $scope.cancelModalEdificio();
            });

        };

        $scope.deleteArea = function (areaId) {
            //console.log(piso);
            $http({
                method: 'POST',
                url: `${$rootScope.APIURL}/api/areas/delete/${areaId}`,
            }).then(function (response) {
                if (!$scope.piso.areas) {
                    $scope.piso.areas = [];
                }
                $scope.piso.areas = $scope.piso.areas.filter(el => el.id != areaId);
                $scope.hideModalAreas();
            });
        };

        $scope.deletePiso = function (pisoId) {

            if ($scope.inmueble.tipoInmueble == 1) {
                $scope.edificio = $scope.inmueble.children.find(x => x.id == $scope.edificioId);
            } else {
                $scope.edificio = $scope.inmueble;
            }

            $http({
                method: 'POST',
                url: `${$rootScope.APIURL}/api/pisos/delete/${pisoId}`,
            }).then(function (response) {
                if (!$scope.edificio.pisos) {
                    $scope.edificio.pisos = {};
                }
                $scope.edificio.pisos = $scope.edificio.pisos.filter(el => el.id != pisoId);
                $scope.hideModalPisos();
            });
        }

        $scope.changeRegion = function () {
            if ($scope.filtro.regionId === null) {
                $scope.data.comunas = [];
            } else {
                $http({
                    method: 'GET',
                    url: `/api/comunas/V2/byregionid/${$scope.filtro.regionId}`
                }).then(function (response) {
                    $scope.data.comunas = response.data;
                }, function (error) {
                    ErrorMsg(error);
                });
            }
        };

        $scope.showModalFiltro = function () {
            $http({
                method: 'GET',
                url: '/api/regiones/V2'
            }).then(function (response) {
                if ($scope.filtro.direccion == "") {
                    $scope.filtro.direccion = null;
                }
                if ($scope.filtro.regionId == "") {
                    $scope.filtro.regionId = null;
                }
                if ($scope.filtro.comunaId == "") {
                    $scope.filtro.comunaId = null;
                }
               
                $scope.data.regiones = response.data;
                angular.element('#modalFiltroInmueble').modal('show');
            }, function (error) {
                    ErrorMsg(error);
            });

            
        };

        $scope.cancelModalFiltro = function () {
            $scope.filtro.direccion = null;
            $scope.filtro.regionId = null;
            $scope.filtro.comunaId = null;
            angular.element('#modalFiltroInmueble').modal('hide');
        };

        $scope.filtrarInmueble = function () {
            if ($scope.filtro.direccion == null) {
                $scope.filtro.direccion = "";
            }
            if ($scope.filtro.regionId == null) {
                $scope.filtro.regionId = "";
            }
            if ($scope.filtro.comunaId == null) {
                $scope.filtro.comunaId = "";
            }
            getInmueblePagin($scope.pageNumber);
            angular.element('#modalFiltroInmueble').modal('hide');
        };

     

        $scope.goToStep1 = function () {
            $rootScope.adress = "";
            angular.element('#input-places').val('');
            $rootScope.direccion = {};
            $rootScope.inmuebleListSearch = [];
            $rootScope.inmuebleSearched = false;
            $scope.showSt0 = false;
            $scope.showSt1 = true;
            
        }

        $scope.getPage = function (page) {
            getInmueblePagin(page);
            
        };

        $scope.nextPage = function () {
           
            if ($scope.pageNumber < $scope.pages) {
                $scope.pageNumber = $scope.pageNumber + 1;
                getInmueblePagin($scope.pageNumber);
            }
           

        };

        $scope.prevPage = function () {
            if ($scope.pageNumber > 1) {
                $scope.pageNumber = $scope.pageNumber - 1;
                getInmueblePagin($scope.pageNumber);
            }

        };

        //getByInstitucionId / { institucionId }

        $scope.selectTipoComplejo = function () {
            $scope.minValidatorAnio = 0;
            $scope.inmuebleText = COMPLEJOTEXT;
            $scope.tipoEdificio = false;
            $scope.tipoComplejo = true;
            $scope.inmueble.tipoInmueble = 1;
        }
        $scope.selectTipoEdificio = function () {
            $scope.minValidatorAnio = 1800;
            $scope.inmuebleText = EDIFICIOTEXT;
            $scope.tipoEdificio = true;
            $scope.tipoComplejo = false;
            $scope.inmueble.tipoInmueble = 2;
        }

        $scope.selectInstitucion = function () {
            if ($scope.inmueble.administracionMinisterioId == null) {
                $scope.inmueble.administracionServicioId = null;
                $scope.disabledServicio = true;
            } else {
                $http({
                    method: 'GET',
                    url: `/api/servicios/getByInstitucionId/${$scope.inmueble.administracionMinisterioId}`

                }).then(function (response) {

                    $scope.data.serivicios = response.data;
                    $scope.disabledServicio = false;
                });

            }
        }

        $scope.selectInstitucionEdificio = function () {
            if ($scope.edificio.administracionMinisterioId == null) {
                $scope.edificio.administracionServicioId = null;
                $scope.disabledServicioEdificio = true;
            } else {
                $http({
                    method: 'GET',
                    url: `/api/servicios/getByInstitucionId/${$scope.edificio.administracionMinisterioId}`

                }).then(function (response) {

                    $scope.data.serivicios = response.data;
                    $scope.disabledServicioEdificio = false;
                });

            }
        }

        $scope.miInstitucion = function () {
            $scope.inmueble.administracionMinisterioId = null;
            $scope.inmueble.administracionServicioId = null;
            $scope.disabledInstitucion = false;
            $scope.disabledServicio = true;
            $scope.requiredIS = true;
            loadData();
        }
        $scope.miInstitucionEdificio = function () {
            $scope.edificio.administracionMinisterioId = null;
            $scope.edificio.administracionServicioId = null;
            $scope.disabledInstitucionEdificio = false;
            $scope.disabledServicioEdificio = true;
            $scope.requiredISEdificio = true;
            loadData();
        }

        $scope.otrainstitucion = function () {
            $scope.inmueble.administracionMinisterioId = null;
            $scope.inmueble.administracionServicioId = null;
            $scope.disabledInstitucion = false;
            $scope.disabledServicio = true;
            $scope.requiredIS = true;
            $http({
                method: 'GET',
                url: '/api/instituciones'

            }).then(function (response) {
                $scope.data.instituciones = response.data;
                $scope.inmueble.institucionId = null;
                $scope.inmueble.servicioId = null;
                $scope.disabledServicio = true;
            });
        }

        

        $scope.otrainstitucionEdificio = function () {
            $scope.edificio.administracionMinisterioId = null;
            $scope.edificio.administracionServicioId = null;
            $scope.disabledInstitucionEdificio = false;
            $scope.disabledServicioEdificio = true;
            $scope.requiredISEdificio = true;
            getInstituciones().then(resp => {
                $scope.edificio.institucionId = null;
                $scope.edificio.servicioId = null;
                $scope.disabledServicioEdificio = true;
            })
        }


        function getInstituciones() {
            var defered = $q.defer();
            var promise = defered.promise;
            $http({
                method: 'GET',
                url: '/api/instituciones'

            }).then(function (response) {
                $scope.data.instituciones = response.data;
                defered.resolve(true);
            });

            return promise;
        }

        $scope.privadoSelected = function () {
            $scope.inmueble.administracionMinisterioId = null;
            $scope.inmueble.administracionServicioId = null;
            $scope.disabledInstitucion = true;
            $scope.disabledServicio = true;
            $scope.requiredIS = false;
        }

        $scope.privadoSelectedEdificio = function () {
            $scope.edificio.administracionMinisterioId = null;
            $scope.edificio.administracionServicioId = null;
            $scope.disabledInstitucionEdificio = true;
            $scope.disabledServicioEdificio = true;
            $scope.requiredISEdificio = false;
        }

        $scope.backto1 = function () {
            
            $scope.showSt2 = false;
            $timeout(function () {
                $scope.showSt1 = true;
            }, 300);
          
        }

        $scope.backto0 = function () {
            $scope.showDel = false;
            $scope.showSt1 = false;
            $scope.showSt2 = false;
            $timeout(function () {
                $scope.showSt0 = true;
            }, 300);
            loadData();
        }

        $scope.saveInmueble = function () {
            //console.log($rootScope.direccion);
            $scope.inmueble.direccion = $rootScope.direccion;
            if (!$scope.editModeInmueble) {
                $http({
                    method: 'POST',
                    url: '/api/inmueble',
                    data: JSON.stringify($scope.inmueble)
                }).then(function (response) {
                    $scope.inmueble = response.data.inmuebles[0];
                    $scope.editModeInmueble = true;
                    //alert("Ok");
                });
            } else {
                $http({
                    method: 'PUT',
                    url: '/api/inmueble',
                    data: JSON.stringify($scope.inmueble)
                }).then(function (response) {
                    $scope.editModeInmu6eble = false;
                    //$scope.inmueble = response.data.inmuebles[0];
                    //alert("Ok");
                });
            }

            $scope.showSt2 = false;
            $scope.showSt3 = true;
            
        };

        $scope.saveEdificio = function (parent) {
            if (parent == 0) {
                $scope.edificio.direccion = $rootScope.direccion;
            }

            $scope.edificio.parentId = parent;
            $scope.edificio.tipoInmueble = 2;
            $http({
                method: 'POST',
                url: '/api/inmueble',
                data: JSON.stringify($scope.edificio)
            }).then(function (response) {
                $scope.inmueble.children.push(response.data.inmuebles[0]);
                $scope.edificio = {};
                $scope.edificio.tipoAdministracionId = "1";
                $scope.edificioForm.$setPristine();
                $scope.edificioForm.$setUntouched();
                angular.element("#modalEdificio").modal('hide');
                
            });

            $scope.showSt2 = false;
            $scope.showSt3 = true;

        };

        $scope.editEdificioForm = function () {
            console.log($scope.edificio);

            $http({
                method: 'PUT',
                url: `/api/inmuebles/V2/${$scope.edificio.id}`,
                data: JSON.stringify($scope.edificio)
            }).then(function (response) {
                $scope.edificio = {};
                $scope.edificioForm.$setPristine();
                $scope.edificioForm.$setUntouched();
                angular.element("#modalEdificio").modal('hide');

            });

            $scope.showSt2 = false;
            $scope.showSt3 = true;

        };

        $scope.loadInmueble = function (id) {
            $http({
                method: 'GET',
                url: `/api/inmuebles/V2/${id}`

            }).then(function (response) {
                $scope.inmueble = response.data;
            });
        }

        $scope.editImueble = function (id) {
            $scope.editModeInmueble = true;
            $http({
                method: 'GET',
                url: `/api/inmuebles/V2/${id}`

            }).then(function (response) {
                $scope.showSt0 = false;
                $rootScope.$broadcast('selInmueble', response.data);
            });
        }

        $scope.viewImueble = function (id) {
            $scope.editModeInmueble = true;
            $http({
                method: 'GET',
                url: `/api/inmuebles/V2/${id}`

            }).then(function (response) {
                $scope.showSt0 = false;
                getInstituciones();
                $rootScope.$broadcast('selInmueble', response.data);
            });

        }

        $scope.$on('selInmueble', function (evt, inm) {
            //console.log("Aca");
            $scope.editModeInmueble = true;
            $scope.inmueble = inm;
            if (inm.tipoInmueble == 1) {
                $scope.tipoComplejo = true;
                $scope.tipoEdificio = false;
                $scope.inmuebleText = COMPLEJOTEXT;
                $scope.minValidatorAnio = 0;
            } else {
                $scope.tipoComplejo = false;
                $scope.tipoEdificio = true;
                $scope.inmuebleText = EDIFICIOTEXT;
                $scope.minValidatorAnio = 1800;
            }


            if (inm.tipoAdministracionId == "3") {
                $scope.privadoSelected();
            }
            if (inm.administracionMinisterioId != null) {
                $scope.selectInstitucion();
            }
            $scope.showSt1 = false;
            $timeout(function () {
                $scope.showSt2 = true;
            }, 700)
        })

        $scope.showModalEdificio = function (inmueble) {
            //console.log(inmueble);
            $scope.editEdificio = false;
            $scope.modalEdificioTitle = 'Nuevo Edificio';   
            $scope.edificio.tipoUsoId = inmueble.tipoUsoId;
            angular.element("#modalEdificio").modal('show');
        }

        $scope.showModalEditEdificio = function (edificio) {
            $scope.modalEdificioTitle = "Editar Edificio";
            $scope.editEdificio = true;
            $scope.edificio = edificio;
            angular.element("#modalEdificio").modal('show');
        }

        $scope.cancelModalEdificio = function () {
            $scope.edificio = {};
            $scope.edificio.tipoAdministracionId = "1";
            $scope.edificioForm.$setPristine();
            $scope.edificioForm.$setUntouched();
            angular.element("#modalEdificio").modal('hide');
            $scope.editModeInmueble = false;


        }

        $scope.getTipoNivelPiso = function () {
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
        };

        $scope.countPisos = function () {
            if ($scope.piso.tipoNivelId == 1 && $rootScope.bajoPisos.length == 0) {
                $scope.pisosIguales = true;
            }
            if ($scope.piso.tipoNivelId == 2 && $rootScope.sobrePisos.length == 0) {
                $scope.pisosIguales = true;
            }
        };

        $scope.showModalPisos = function (edificio) {
            $scope.modalPisoTitulo = "Crear Piso";
            $scope.piso.tipoUsoId = edificio.tipoUsoId;
            $rootScope.bajoPisos = edificio.pisos.filter(function (piso) {
                return piso.tipoNivelId == 1;
            });
            console.log($scope.data.numeroPiso);
            $rootScope.sobrePisos = edificio.pisos.filter(function (piso) {
                return piso.tipoNivelId == 2;
            });

            $scope.edificioId = edificio.id;
            angular.element('#modalPiso').modal('show');

        };

        $scope.showModalEditPisos = function (edificio, piso) {
            console.log(piso);
            $scope.modalPisoTitulo = "Editar Piso"
            $scope.editPiso = true;
            $rootScope.bajoPisos = edificio.pisos.filter(function (piso) {
                return piso.tipoNivelId == 1;
            });
            $rootScope.sobrePisos = edificio.pisos.filter(function (piso) {
                return piso.tipoNivelId == 2;
            });
            $scope.piso = piso;

            $scope.edificioId = edificio.id;
            console.log($scope.inmueble);
            if ($scope.inmueble.tipoInmueble == 1) {
                $scope.edificio = $scope.inmueble.children.find(x => x.id == edificio.id);
            }
            console.log($scope.edificio);

            angular.element('#modalPiso').modal('show');

        };

        $scope.hideModalPisos = function () {
            $scope.editPiso = false;
            $scope.piso = {};
            $scope.tipoNivel = {};
            $scope.pisosForm.$setUntouched();
            $scope.pisosForm.$setPristine();
            angular.element('#modalPiso').modal('hide');

        };

        $scope.countPisos = function () {
            $scope.pisosIguales = false;
            if ($scope.piso.tipoNivelId == 1 && $rootScope.bajoPisos.length == 0) {
                $scope.pisosIguales = true;
            }
            if ($scope.piso.tipoNivelId == 2 && $rootScope.sobrePisos.length == 0) {
                $scope.pisosIguales = true;
            }
        };
        $scope.checkPisosIguales = function () {
            if (!$scope.piso.pisosIguales) {
                $scope.porPiso = 'por piso'
            } else {
                $scope.porPiso = '';
            }
        };

        $scope.getNumeroPiso = function () {
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
        };

        $scope.chekPiso = function () {
            if ($scope.piso.numeroPisoId == undefined) {
                $scope.pisosForm.npiso.$setValidity("check", true);
                return;
            }
            let pisosList = $scope.piso.tipoNivelId == 2 ? $rootScope.sobrePisos : $rootScope.bajoPisos;

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


        $scope.submitPisoForm = function () {

            const ENTRE_PISO_NOMBRE = "Entre Piso";
            $scope.piso.inmuebleId = $scope.edificioId;
            $http({
                method: 'POST',
                url: '/api/Piso',
                data: JSON.stringify($scope.piso)
            })
                .then(function (response) {
                    $scope.piso = {};
                    $scope.pisosIguales = false;
                    $scope.pisosForm.$setUntouched();
                    $scope.pisosForm.$setPristine();
                    $scope.loadInmueble($scope.inmueble.id);
                    //if ($scope.inmueble.tipoInmueble == 1) {
                    //    $scope.inmueble.children.forEach(function (edificio) {
                    //        if (edificio.id == $scope.edificioId) {
                    //            edificio.pisos = response.data.pisoPasoUnoList;
                    //        }
                    //    });
                    //} else {
                    //    $scope.inmueble.pisos = response.data.pisoPasoUnoList;
                    //}
                    
                    $scope.hideModalPisos();

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
                    $scope.hideModalPisos();
                }
        };

        $scope.editPisoForm = function () {
            //console.log($scope.piso);
            $http({
                method: 'PUT',
                url: `/api/Piso/${$scope.piso.id}`,
                data: JSON.stringify($scope.piso)
            })
                .then(function (response) {
                    
                    

                    $scope.hideModalPisos();

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
                    $scope.hideModalPisos();
                }
        };

        $scope.submitAreasForm = function () {
            $scope.area.pisoId = $scope.pisoId;
            $http({
                method: 'POST',
                url: '/api/Area',
                data: JSON.stringify($scope.area)
            })
                .then(function (response) {
                    $scope.area = {};
                    $scope.areasForm.$setUntouched();
                    $scope.areasForm.$setPristine();
                    if ($scope.inmueble.tipoInmueble == 1) {
                        $scope.inmueble.children.forEach(function (edificio) {
                            edificio.pisos.forEach(function (piso) {
                                if (piso.id == $scope.pisoId) {
                                    piso.areas = response.data.list;
                                }
                            })
                        })
                    } else {

                        $scope.inmueble.pisos.forEach(function (piso) {
                            if (piso.id == $scope.pisoId) {
                                piso.areas = response.data.list;
                            }
                        });

                    }
                    $scope.hideModalAreas();

                }), function () {
                    $scope.hideModalAreas();
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
        };

        $scope.submitEditAreasForm = function () {

            $scope.area.pisoId = $scope.pisoId;

            $http({
                method: 'PUT',
                url: `/api/Area/${$scope.area.id}`,
              
                data: JSON.stringify($scope.area)
            })
                .then(function (response) {
                    $scope.area = {};
                    $scope.areasForm.$setUntouched();
                    $scope.areasForm.$setPristine();
                    if ($scope.inmueble.tipoInmueble == 1) {
                        $scope.inmueble.children.forEach(function (edificio) {
                            edificio.pisos.forEach(function (piso) {
                                if (piso.id == $scope.pisoId) {
                                    piso.areas = response.data.list;
                                }
                            })
                        })
                    } else {

                        $scope.inmueble.pisos.forEach(function (piso) {
                            if (piso.id == $scope.pisoId) {
                                piso.areas = response.data.list;
                            }
                        });

                    }

                    $scope.hideModalAreas();

                }), function () {
                    $scope.hideModalAreas();
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
        };

        $scope.showModalArea = function (piso) {
            $scope.piso = piso;
            console.log($scope.piso);
            $scope.area = {};
            $scope.editArea = false;
            $scope.modalAreaTitulo = "Crear Área";
            $scope.pisoId = piso.id;
            $scope.area.tipoUsoId = piso.tipoUsoId
            angular.element('#modalArea').modal('show');

        };

        $scope.showModalEditArea = function (piso, area) {
            $scope.editArea = true;
            $scope.modalAreaTitulo = "Editar Área";
            $scope.pisoId = piso.id;
            $scope.area = area;
            $scope.piso = piso;
            angular.element('#modalArea').modal('show');

        };

        $scope.hideModalAreas = function () {
            $scope.area = {};
            $scope.areasForm.$setUntouched();
            $scope.areasForm.$setPristine();
            angular.element('#modalArea').modal('hide');

        };


        $scope.backto2 = function () {
            $scope.loadInmueble($scope.inmueble.id);
            $scope.editModeInmueble = true;
            $scope.showSt3 = false;
            $timeout(function () {
                $scope.showSt2 = true;
            }, 300);

        }

        $scope.backto3 = function () {

            $scope.showSt4 = false;
            $timeout(function () {
                $scope.showSt3 = true;
            }, 300);

        }

        $scope.goToStep4 = function () {
            $scope.showSt3 = false;
            $timeout(function () {
                $scope.showSt4 = true;
            }, 300);
        };

        $scope.eliminarInmueble = function (inmueble) {
            $scope.showSt0 = false;
            $scope.showDel = true;
            $scope.inmuebleToDel = inmueble;
        }

        $scope.confirmDelete = function (id) {
            console.log(id);
            $http({
                method: 'DELETE',
                url: `/api/inmuebles/V2/${id}`

            }).then(function (response) {
                $scope.backto0();
            }, function error(response) { ErrorMsg(response); });
           

        }

        function getInmueblePagin(page) {

            $http({
                method: 'GET',
                url: `/api/inmuebles/V2/filter?page=${page}&direccion=${$scope.filtro.direccion}&regionid=${$scope.filtro.regionId}&comunaId=${$scope.filtro.comunaId}`

            }).then(function (response) {
                $scope.inmuebleList = response.data;
                $scope.pages = parseInt(response.headers('Pages'));
                $scope.TotalRecords = parseInt(response.headers('TotalRecords'));
                $scope.pageNumber = page;
                overPaged($scope.pages, MaxPagination, page);
            }, function error(response) { ErrorMsg(response); });
        }

        function overPaged(pages, maxPages, pageNumber) {

            if (pages > maxPages) {

                $scope.showPages = maxPages;
                $scope.overPagesUp = true;

                if (pageNumber + 2 >= pages) {
                    $scope.overPagesUp = false;
                    $scope.startPage = pages + 1 - maxPages;
                } else {
                    if (pageNumber + 2 > maxPages) {
                        $scope.startPage = pageNumber - 2;
                    }
                }

                if (pageNumber - 2 > 1) {
                    $scope.overPagesDown = true;
                } else {
                    $scope.overPagesDown = false;
                    $scope.startPage = 1;
                }



            } else {
                $scope.showPages = pages;
                $scope.overPagesUp = false;
                $scope.overPagesDown = false;
            }

        }

        function loadData() {
            $scope.filtro = {};
            $scope.filtro.direccion = "";
            $scope.filtro.regionId = "";
            $scope.filtro.comunaId = "";
            getInmueblePagin(1);

            $http({
                method: 'GET',
                url: `${$rootScope.APIURL}/api/tipouso/list`
               
            }).then(function (response) {
                $scope.tipousos = response.data;
            });
            $http({
                method: 'GET',
                url: `/api/inmueble/userdata`

            }).then(function (response) {
                $scope.data = response.data;
                $scope.getTipoNivelPiso();
                $scope.getNumeroPiso();
            });

        }

        function ErrorMsg(response) {
            angular.element('.modal').modal('hide');
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

    });


})();
