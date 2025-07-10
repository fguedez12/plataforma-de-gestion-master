(function () {
    'use strict' ;
     
    var unidadApp = angular.module('unidadApp', ['ngMap', 'common', 'ngAnimate']);

    unidadApp.controller('unidadController', function ($scope, $http, $rootScope, $timeout) {

        const MaxPagination = 10; 

        $scope.pageNumber = 1;
        $scope.startPage = 1;

        $scope.showSt0 = true;
        $scope.showSt1 = false;
        $scope.showSt2 = false;
        $scope.showSt3 = false;
        $scope.showSt4 = false;
        $scope.showDel = false;
        $scope.disabledServicio = true;
        $scope.requiredUnidad = true;
        $scope.data = {};

        loadData();

        $scope.$on('addinmueble', function () {
            window.location.replace("/AdminInmueble");
        });

        $scope.$on('address-changed', function () {
            $http({
                method: 'POST',
                url: '/api/inmueble/getbyaddress',
                data: JSON.stringify($rootScope.direccion)
            }).then(function (response) {
                $rootScope.inmuebleSearched = true;
                $scope.inmuebleList = response.data.inmuebles;
            });
        });

        $scope.selectInstitucion = function () {
            $http({
                method: 'GET',
                url: `/api/servicios/getByInstitucionId/${$scope.unidad.institucionId}`

            }).then(function (response) {

                $scope.data.serivicios = response.data;
                $scope.disabledServicio = false;
            });
        };

        $scope.saveUnidad = function () {
            $scope.unidad.inmuebleId= $scope.inmueble.id;
            $http({
                method: 'POST',
                url: '/api/unidad',
                data: JSON.stringify($scope.unidad)
            }).then(function (response) {

                $scope.unidad.unidadId = response.data.unidades[0].id;

                        $scope.showSt2 = false;
                        $timeout(function () {
                            $scope.showSt3 = true;
                        }, 300);
                });
        };

        $scope.backto1 = function () {

            $scope.showSt2 = false;
            $timeout(function () {
                $scope.showSt1 = true;
            }, 300);

        }

        $scope.goToStep1 = function () {
            $scope.showSt0 = false;
            $timeout(function () {
                $scope.showSt1 = true;
            }, 300);
        }

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
                angular.element('#modalFiltroUnidades').modal('show');
            }, function (error) {
                ErrorMsg(error);
            });
        };

        $scope.addInmueble = function (edificio) {
            if (!edificio.checked) {
                edificio.checked = false;
            }
            edificio.checked = !edificio.checked;

            var ui = { inmuebleId: edificio.id, unidadId: $scope.unidad.unidadId }

            if (edificio.checked) {
                $http({
                    method: 'POST',
                    url: '/api/inmueble/addunidad',
                    data: JSON.stringify(ui)
                }).then(function () {
                    
                    edificio.unidadesInmuebles.push(ui);
                });
            } else {
                $http({
                    method: 'POST',
                    url: '/api/inmueble/remunidad',
                    data: JSON.stringify({ inmuebleId: edificio.id, unidadId: $scope.unidad.unidadId })
                }).then(function () {
                    var removeIndex = edificio.unidadesInmuebles.map(function (item) { return item.inmuebleId; }).indexOf(edificio.id);
                    edificio.unidadesInmuebles.splice(removeIndex, 1);
                });
            }

           
        };

        $scope.addPiso = function (piso) {
            if (!piso.checked) {
                piso.checked = false;
            }
            piso.checked = !piso.checked;
            var up = { pisoId: piso.id, unidadId: $scope.unidad.unidadId };
            if (piso.checked) {
                $http({
                    method: 'POST',
                    url: '/api/piso/addunidad',
                    data: JSON.stringify(up)
                }).then(function () {
                    piso.unidadesPisos.push(up);
                });
            } else {
                $http({
                    method: 'POST',
                    url: '/api/piso/remunidad',
                    data: JSON.stringify({ pisoId: piso.id, unidadId: $scope.unidad.unidadId })
                }).then(function () {
                    var removeIndex = piso.unidadesPisos.map(function (item) { return item.pisoId; }).indexOf(piso.id);
                    piso.unidadesPisos.splice(removeIndex, 1);
                });
            }


        };

        $scope.addArea = function (area) {
            if (!area.checked) {
                area.checked = false;
            }
            area.checked = !area.checked;

            var ap = { areaId: area.id, unidadId: $scope.unidad.unidadId };

            if (area.checked) {
                $http({
                    method: 'POST',
                    url: '/api/area/addunidad',
                    data: JSON.stringify(ap)
                }).then(function () {
                    area.unidadesAreas.push(ap)
                });
            } else {
                $http({
                    method: 'POST',
                    url: '/api/area/remunidad',
                    data: JSON.stringify(ap)
                }).then(function () {
                    var removeIndex = area.unidadesAreas.map(function (item) { return item.areaId; }).indexOf(area.id);
                    area.unidadesAreas.splice(removeIndex, 1);
                });
            }


        };

        //$scope.saveInmueble = function () {
        //    $scope.inmueble.direccion = $rootScope.direccion;
        //    if (!$scope.editInmueble) {
        //        $http({
        //            method: 'POST',
        //            url: '/api/inmueble',
        //            data: JSON.stringify($scope.inmueble)
        //        }).then(function (response) {
        //            alert("Ok");
        //        });
        //    }

        //    $scope.showSt2 = false;
        //    $scope.showSt3 = true;

        //};

        //$scope.saveEdificio = function (parent) {
        //    if (parent == 0) {
        //        $scope.edificio.direccion = $rootScope.direccion;
        //    }

        //    $scope.edificio.parentId = parent;
        //    $http({
        //        method: 'POST',
        //        url: '/api/inmueble',
        //        data: JSON.stringify($scope.edificio)
        //    }).then(function (response) {
        //        $scope.inmueble.children.push(response.data.inmuebles[0]);
        //        $scope.edificio = {};
        //        $scope.edificioForm.$setPristine();
        //        $scope.edificioForm.$setUntouched();
        //        angular.element("#modalEdificio").modal('hide');

        //    });

        //    $scope.showSt2 = false;
        //    $scope.showSt3 = true;

        //};

        $scope.$on('selInmueble', function (evt, inm) {
            $scope.inmueble = inm;
            $http({
                method: 'GET',
                url: `/api/inmueble/getUnidades/${inm.id}`
            }).then(function (response) {
                $scope.data.unidades = response.data.unidades;
                $scope.showSt1 = false;
                $timeout(function () {
                    $scope.showSt2 = true;
                }, 700)
            });

        });

        $scope.loadUnidad = function () {
            console.log("Unidad: " + $scope.unidad.unidadId);
            resteChecked();

            if ($scope.inmueble.tipoInmueble == 1) {
                $scope.inmueble.children.forEach(function (edificio) {
                    edificio.unidadesInmuebles.forEach(function (ui) {
                        if (ui.unidadId == $scope.unidad.unidadId) {
                            edificio.checked = true;
                        }

                        edificio.pisos.forEach(function (piso) {
                            piso.unidadesPisos.forEach(function (up) {
                                if (up.unidadId == $scope.unidad.unidadId) {
                                    piso.checked = true;
                                }
                            })

                            piso.areas.forEach(function (area) {
                                area.unidadesAreas.forEach(function (ua) {
                                    if (ua.unidadId == $scope.unidad.unidadId) {
                                        area.checked = true;
                                    }
                                });


                            });
                        })
                    });
                });
            } else {
                      

                $scope.inmueble.pisos.forEach(function (piso) {
                    piso.unidadesPisos.forEach(function (up) {
                        if (up.unidadId == $scope.unidad.unidadId) {
                            piso.checked = true;
                        }
                    })

                    piso.areas.forEach(function (area) {
                        area.unidadesAreas.forEach(function (ua) {
                            if (ua.unidadId == $scope.unidad.unidadId) {
                                area.checked = true;
                            }
                        });


                    });
                });
            }
            

            console.log($scope.inmueble);
            $scope.showSt2 = false;
            $timeout(function () {
                $scope.showSt3 = true;
            }, 700)
        };

        function resteChecked() {

            if ($scope.inmueble.tipoInmueble == 1) {
                $scope.inmueble.children.forEach(function (edificio) {
                    edificio.unidadesInmuebles.forEach(function (ui) {
                        edificio.checked = false;

                        edificio.pisos.forEach(function (piso) {
                            piso.unidadesPisos.forEach(function (up) {
                                piso.checked = false;
                            })
                            piso.areas.forEach(function (area) {
                                area.unidadesAreas.forEach(function (ua) {
                                    area.checked = false;
                                });
                            });
                        })
                    });
                });
            } else {

 
                       

                $scope.inmueble.pisos.forEach(function (piso) {
                    piso.unidadesPisos.forEach(function (up) {
                        piso.checked = false;
                    })
                    piso.areas.forEach(function (area) {
                        area.unidadesAreas.forEach(function (ua) {
                            area.checked = false;
                        });
                    });
                })



            }  
        }

        $scope.backto2 = function () {

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
            $scope.inmuebleCopy = angular.copy($scope.inmueble);
            if ($scope.inmueble.tipoInmueble == 1) {
                $scope.inmuebleCopy.children = $scope.inmuebleCopy.children.filter(function (edificio) {
                    return edificio.checked;
                });
                $scope.inmuebleCopy.children.forEach(function (edificio) {
                    edificio.pisos = edificio.pisos.filter(function (piso) {
                        return piso.checked;
                    });
                    edificio.pisos.forEach(function (piso) {
                        piso.areas = piso.areas.filter(function (area) {
                            return area.checked;
                        });
                    })
                })
            } else {

                

                $scope.inmuebleCopy.pisos = $scope.inmueble.pisos.filter(function (piso) {
                        return piso.checked;
                });

                $scope.inmuebleCopy.pisos.forEach(function (piso) {
                    piso.areas = piso.areas.filter(function (area) {
                        return area.checked;
                    });
                });
            }
            

            $scope.showSt3 = false;
            $timeout(function () {
                $scope.showSt4 = true;
            }, 300);
        };

        $scope.cancelModalFiltro = function () {
            $scope.filtro.direccion = null;
            $scope.filtro.regionId = null;
            $scope.filtro.comunaId = null;
            angular.element('#modalFiltroUnidades').modal('hide');
        };

        function loadData() {
            $scope.filtro = {};
            $scope.filtro.unidad = "";
           
            getUnidadPagin(1);
            $http({
            method: 'GET',
            url: '/api/instituciones'

            }).then(function (response) {
                $scope.data.instituciones = response.data;
            });
        };

        function getUnidadPagin(page) {

            $http({
                method: 'GET',
                url: `/api/unidad/filter?page=${page}&unidad=${$scope.filtro.unidad}`

            }).then(function (response) {
                $scope.unidadList = response.data;
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

        $scope.nextPage = function () {

            if ($scope.pageNumber < $scope.pages) {
                $scope.pageNumber = $scope.pageNumber + 1;
                getUnidadPagin($scope.pageNumber);
            }


        };

        $scope.prevPage = function () {
            if ($scope.pageNumber > 1) {
                $scope.pageNumber = $scope.pageNumber - 1;
                getUnidadPagin($scope.pageNumber);
            }

        };

        $scope.getPage = function (page) {
            getUnidadPagin(page);

        };

        $scope.backto0 = function () {
            $scope.showDel = false;
            $scope.showSt1 = false;
            $scope.showSt2 = false;
            $timeout(function () {
                $scope.showSt0 = true;
            }, 300);
            loadData();
        }

        $scope.filtrarUnidad = function () {
            if ($scope.filtro.unidad == null) {
                $scope.filtro.unidad = "";
            }
           
            getUnidadPagin($scope.pageNumber);
            angular.element('#modalFiltroUnidades').modal('hide');
        };

        $scope.showEditUnidad = function (unidad) {
           
            $scope.unidadToEdit = unidad;
            angular.element('#modalEditUnidad').modal('show');
        };

        $scope.cancelModalEditUnidad = function () {
            $scope.unidadToEdit = {};
            angular.element('#modalEditUnidad').modal('hide');
        };

        $scope.UpdateUnidad = function () {
            $http({
                method: 'PUT',
                url: `/api/unidad/${$scope.unidadToEdit.id}`,
                data: JSON.stringify($scope.unidadToEdit)

            }).then(function (response) {
                $scope.cancelModalEditUnidad();
                loadData();
            }, function error(response) { ErrorMsg(response); });
        };

        $scope.deleteUnidad = function (unidad) {
            $scope.showSt0 = false;
            $scope.showDel = true;
            $scope.unidadToDelete = unidad;
        };

        $scope.confirmDeleteUnidad = function (id) {
            //console.log(id);
            $http({
                method: 'DELETE',
                url: `/api/unidad/${id}`

            }).then(function (response) {
                $scope.backto0();
            }, function error(response) { ErrorMsg(response); });


        };

    });


})();
