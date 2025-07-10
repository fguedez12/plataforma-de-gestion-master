var app = angular.module('adminUnidadApp', ['ui.router','cp.ngConfirm'])
    .controller("adminUnidadController", function ($rootScope, $state,$http) {
        var userId = angular.element("#input-id").val();
        var isAdmin = angular.element("#input-is-admin").val();
        const isConsulta = angular.element("#input-is-consulta").val();
        $rootScope.userId = userId;
        $rootScope.isAdmin = isAdmin == 'False' ? false : true;
        $rootScope.isConsulta = isConsulta == 'False' ? false : true;
        $http({
            method: 'GET',
            url: `/settings?sectionName=ApiConfiguration&paramName=apiGestionaEnergia`

        }).then(function (response) {
            console.log("ReadDataFromAppSettings", response);
            $rootScope.APIURL = response.data;
        })
        $state.go("adminUnidad@home");
    })
    .controller("homeController", function ($scope, $rootScope, $http, $timeout, $state,$ngConfirm) {
        $scope.unidades = [];
        $scope.loading = false;
        $scope.admin = $rootScope.isAdmin;
        $scope.institucionId = '';
        $scope.regionId = '';
        $scope.servicioId = '';
        $scope.data = {};
        $scope.loading = true;

        $http.get(`/api/unidad/getasociadosbyuser/${$rootScope.userId}`)
            .then(function (result) {
                $scope.loading = false;
                $scope.unidades = result.data;
                //console.log("$scope.unidades", $scope.unidades);
                intitDataTable($timeout);
            });


        $scope.changeInstitucion = function (institucionId) {
            $scope.institucionId = institucionId;
            $scope.getFilter();
        };
        $scope.changeServicio = function (servicioId) {
            $scope.servicioId = servicioId;
            $scope.getFilter();
        };
        $scope.changeRegion = function (regionId) {
            $scope.regionId = regionId;
            $scope.getFilter();
        };
        $scope.newClick = function () {
            
            $state.go("adminUnidad@buscarDireccion");
        };
        $scope.backClick = function () {
            console.log("backClick");
            window.location.href = '/Admin';
        };

        $scope.getFilter = function () {
            $scope.loading = true;
            $http({
                method: 'GET',
                url: `/api/unidad/getbyfilter?userId=${$scope.userId}&institucionId=${$scope.institucionId}&servicioId=${$scope.servicioId}&regionId=${$scope.regionId}`,
            }).then(function (response) {
                $scope.loading = false;
                $scope.unidades = response.data;
                intitDataTable($timeout);
            });
        };

        $scope.eliminar = function (unidad) {

            $ngConfirm({
                title: 'Eliminar unidad',
                content: '<p>¿Esta seguro de eliminar la unidad?<p>',
                buttons: {
                    Ok: {
                        text: "Ok",
                        btnClass: "btn btn-danger",
                        action: function () {
                            $http({
                                method: 'DELETE',
                                url: `/api/unidad/${unidad.id}`

                            }).then(function () {
                                $rootScope.unidad = {};
                                $state.go('adminUnidad@home', null, { reload: true });
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

        $scope.editar = function (unidad) {
            $rootScope.unidad = unidad;
            //console.log("$rootScope.unidad", $rootScope.unidad)
            $state.go('adminUnidad@editarUnidad', null, { reload: true });
        }

        function intitDataTable($timeout) {
            $('#unidadesTable').DataTable().destroy();
            $timeout(function () {
                //console.log(vm.unidades)
                $('#unidadesTable').DataTable({
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
            }, 200);
        }
    })
    .controller("buscarDireccionController", function ($scope,$rootScope,$state, $http) {
       
        $scope.inmuebleListSearch = [];
        $scope.inmuebleSearched = false;
        $scope.loading = false;

        $scope.backToHome = function () {
            $state.go('adminUnidad@home', null, { reload : true });
        };

        $scope.locationChange = function (direccion) {
            //console.log(direccion);
            $scope.loading = true;
            $http({
                method: 'POST',
                url: '/api/inmueble/getbyaddress',
                data: JSON.stringify(direccion)
            }).then(function (response) {
                $scope.loading = false;
                $scope.inmuebleSearched = true;
                $scope.inmuebleListSearch = response.data.inmuebles;
            });
        };

        $scope.inmuebleSelected = function (inmueble) {
            $rootScope.inmueble = inmueble;
            
            $state.go('adminUnidad@crearUnidad', null, { reload: true });
            console.log(inmueble);
        }
        
    })
    .controller("crearUnidadController", function ($scope, $rootScope, $state, $http) {

        $http.get(`${$rootScope.APIURL}/api/institucion`).then(resp => {
            //console.log(resp.data);
            $scope.institucionesResponsables = resp.data;
        });

        if ($rootScope.inmueble == null) {
            $state.go('adminUnidad@home', null, { reload: true });
        }

        $scope.unidad = {};
        console.log($scope.unidad);

        $scope.inmuebleSelected = $rootScope.inmueble;

        $scope.back = function () {
            $rootScope.unidad = {};
            $state.go('adminUnidad@buscarDireccion', null, { reload: true });
        }

        $scope.saveUnidad = function (unidad) {
            //console.log(unidad);
            //unidad.inmuebleId = $rootScope.inmueble.id;
            $rootScope.unidad = unidad;
            $rootScope.unidad.inmuebles = [];
            if ($rootScope.inmueble.tipoInmueble == 1) {
                $rootScope.unidad.inmuebles.push({ id: $rootScope.inmueble.id, edificios: [], pisos: [] });
            } else {
                $rootScope.unidad.inmuebles.push({ id: 0, edificios: [], pisos: [] });
                $rootScope.unidad.inmuebles[0].edificios.push({ id: $rootScope.inmueble.id, pisos: [] });
            }
            
            $state.go('adminUnidad@asociarInmueble', null, { reload: true });
        }
    })
    .controller("asociarInmuebleController", function ($rootScope, $state, $scope, $http, $ngConfirm, $q) {
        $scope.admin = $rootScope.isAdmin;
        if ($rootScope.unidad == null) {
            $state.go('adminUnidad@home', null, { reload: true });
        }

        $scope.inmuebleSelected = $rootScope.inmueble;



        checkLevel($scope.inmuebleSelected);

        console.log($scope.inmuebleSelected);

        function getUnidadById(id) {
            var defered = $q.defer();
            var promise = defered.promise;
            $http({
                method: 'GET',
                url: `/api/unidad/${id}`,
            }).then(function (response) {
                console.log(response.data.servicio.nombre);
                defered.resolve(response.data.servicio.nombre);
               
            });
            return promise;
        }

        function checkLevel(inmueble) {
            if (inmueble) {
                if (inmueble.tipoInmueble == 1) {
                    if (inmueble.children.length > 0) {
                        inmueble.blockedByUse = false;
                        angular.forEach(inmueble.children, function (child, key) {
                            if (child.pisos.length > 0) {
                                child.blockedByUse = false;
                                angular.forEach(child.pisos, function (piso, key) {
                                    if (piso.areas.length > 0) {
                                        piso.blockedByUse = false;

                                        //Ver si hay unidades en las areas
                                        angular.forEach(piso.areas, function (area, key) {
                                            if (area.unidadesAreas.length > 0) {
                                                var mismaUnidad = area.unidadesAreas.find(x => x.unidadId == $rootScope.unidad.id);
                                                if (!mismaUnidad) {
                                                    area.blockedByUse = true;
                                                    var blockUnidadId = area.unidadesAreas[0].unidadId;
                                                    getUnidadById(blockUnidadId).then(resp => {
                                                        area.blockedByUseName = resp;
                                                    });
                                                } else {
                                                    area.blockedByUse = false;
                                                }

                                            } else {
                                                area.blockedByUse = false;
                                            }
                                        })

                                    } else {
                                        if (piso.unidadesPisos.length > 0) {
                                            var mismaUnidad = piso.unidadesPisos.find(x => x.unidadId == $rootScope.unidad.id);
                                            if (!mismaUnidad) {
                                                piso.blockedByUse = true;
                                                var blockUnidadId = piso.unidadesPisos[0].unidadId;
                                                getUnidadById(blockUnidadId).then(resp => {
                                                    piso.blockedByUseName = resp;
                                                });
                                            } else {
                                                piso.blockedByUse = false;
                                            }

                                        } else {
                                            piso.blockedByUse = false;
                                        }

                                    }

                                });


                            } else {
                                if (child.unidadesInmuebles.length > 0) {
                                    var mismaUnidad = child.unidadesInmuebles.find(x => x.unidadId == $rootScope.unidad.id);
                                    if (!mismaUnidad) {
                                        child.blockedByUse = true;
                                    } else {
                                        child.blockedByUse = false;
                                    }

                                } else {
                                    child.blockedByUse = false;
                                }

                            }

                        });
                    } else {
                        if (inmueble.unidadesInmuebles.length > 0) {
                            var mismaUnidad = child.unidadesInmuebles.find(x => x.unidadId == $rootScope.unidad.id);
                           
                            if (!mismaUnidad) {
                                inmueble.blockedByUse = true;
                                var blockUnidadId = child.unidadesInmuebles[0].unidadId;
                                getUnidadById(blockUnidadId).then(resp => {
                                    inmueble.blockedByUseName = resp;
                                });
                            } else {
                                inmueble.blockedByUse = false;
                            }

                        } else {
                            inmueble.blockedByUse = false;
                        }
                    }
                } else {
                    if (inmueble.pisos.length > 0) {
                        inmueble.blockedByUse = false;
                        angular.forEach(inmueble.pisos, function (piso, key) {
                            if (piso.areas.length > 0) {
                                piso.blockedByUse = false;

                                //Ver si hay unidades en las areas
                                angular.forEach(piso.areas, function (area, key) {
                                    if (area.unidadesAreas.length > 0) {
                                        var mismaUnidad = area.unidadesAreas.find(x => x.unidadId == $rootScope.unidad.id);
                                        if (!mismaUnidad) {
                                            area.blockedByUse = true;
                                        } else {
                                            area.blockedByUse = false;
                                        }

                                    } else {
                                        area.blockedByUse = false;
                                    }
                                })

                            } else {
                                if (piso.unidadesPisos.length > 0) {
                                    var mismaUnidad = piso.unidadesPisos.find(x => x.unidadId == $rootScope.unidad.id);
                                    if (!mismaUnidad) {
                                        piso.blockedByUse = true;
                                    } else {
                                        piso.blockedByUse = false;
                                    }

                                } else {
                                    piso.blockedByUse = false;
                                }

                            }

                        });


                    } else {
                        if (inmueble.unidadesInmuebles.length > 0) {
                            var mismaUnidad = inmueble.unidadesInmuebles.find(x => x.unidadId == $rootScope.unidad.id);
                            if (!mismaUnidad) {
                                inmueble.blockedByUse = true;
                            } else {
                                inmueble.blockedByUse = false;
                            }

                        } else {
                            inmueble.blockedByUse = false;
                        }

                    }

                }
            }
           
        }

        $scope.back = function () {
            if ($rootScope.editMode) {
                $state.go('adminUnidad@editarUnidad', null, { reload: true });
            } else {
                $state.go('adminUnidad@crearUnidad', null, { reload: true });
            }
            
        }

        $scope.addEdificio = function (edificio) {
            if (!edificio.checked) {
                edificio.checked = false
            }

            edificio.checked = !edificio.checked;

            if (edificio.checked) {
                $rootScope.unidad.inmuebles[0].edificios.push({ id: edificio.id , pisos: []});
            } else {
                
                for (var i = 0; i < $rootScope.unidad.inmuebles[0].edificios.length; i++) {
                    if ($rootScope.unidad.inmuebles[0].edificios[i].id == edificio.id) {
                        $rootScope.unidad.inmuebles[0].edificios.splice(i, 1);
                        i--;
                    }
                }

                edificio.checked = false;

                edificio.pisos.forEach(function (piso, index, array) {
                    piso.checked = false;
                    piso.areas.forEach(function (area, index, array) {
                        area.checked = false;
                    })
                })

            }

        }

        $scope.addPiso = function (piso,edificio) {
            if (!piso.checked) {
                piso.checked = false;
            }
            piso.checked = !piso.checked;
            if (piso.checked) {
                var edificioUnidad = false;
                if (edificio) {
                    for (var i = 0; i < $rootScope.unidad.inmuebles[0].edificios.length; i++) {
                        if ($rootScope.unidad.inmuebles[0].edificios[i].id == edificio.id) {
                            $rootScope.unidad.inmuebles[0].edificios[i].pisos.push({ id: piso.id, areas: [] });
                            edificioUnidad = true;
                        }
                    }
                } else {
                    $rootScope.unidad.inmuebles[0].edificios[0].pisos.push({ id: piso.id, areas: [] });
                    edificioUnidad = true;
                };
                
                if (!edificioUnidad) {
                    var newEdificio = { id: edificio.id, pisos: [] };
                    newEdificio.pisos.push({ id: piso.id, areas: [] });
                    $rootScope.unidad.inmuebles[0].edificios.push(newEdificio);
                    edificio.checked = true;
                }
                piso.checked = true;
            } else {
                if (edificio) {
                    for (var i = 0; i < $rootScope.unidad.inmuebles[0].edificios.length; i++) {
                        if ($rootScope.unidad.inmuebles[0].edificios[i].id = edificio.id) {
                            for (var j = 0; j < $rootScope.unidad.inmuebles[0].edificios[i].pisos.length; j++) {
                                if ($rootScope.unidad.inmuebles[0].edificios[i].pisos[j].id == piso.id) {
                                    $rootScope.unidad.inmuebles[0].edificios[i].pisos.splice(j, 1);
                                    j--;
                                }
                            }
                        }
                    }
                } else {
                    for (var j = 0; j < $rootScope.unidad.inmuebles[0].edificios[0].pisos.length; j++) {
                        if ($rootScope.unidad.inmuebles[0].edificios[0].pisos[j].id == piso.id) {
                            $rootScope.unidad.inmuebles[0].edificios[0].pisos.splice(j, 1);
                            j--;
                        }
                    }

                }
                

                piso.checked = false;

                piso.areas.forEach(function (area, index, array) {
                    area.checked = false;
                })
               
            }
        };

        $scope.addArea = function (area, piso,edificio) {
            if (!area.checked) {
                area.checked = false;
            }
            area.checked = !area.checked;
            if (area.checked) {
                var edificioUnidad = false;
                for (var i = 0; i < $rootScope.unidad.inmuebles[0].edificios.length; i++) {
                    if (edificio) {
                        if ($rootScope.unidad.inmuebles[0].edificios[i].id == edificio.id) {
                            edificioUnidad = true;
                            var pisoUnidad = false;
                            for (var j = 0; j < $rootScope.unidad.inmuebles[0].edificios[i].pisos.length; j++) {
                                if ($rootScope.unidad.inmuebles[0].edificios[i].pisos[j].id == piso.id) {
                                    pisoUnidad = true;
                                    $rootScope.unidad.inmuebles[0].edificios[i].pisos[j].areas.push({ id: area.id });
                                    area.checked = true;
                                   
                                }
                            }
                            if (!pisoUnidad) {
                                var newPiso = { id: piso.id, areas: [] };
                                var newPiso = { id: piso.id, areas: [] };
                                newPiso.areas.push({ id: area.id });
                                $rootScope.unidad.inmuebles[0].edificios[i].pisos.push(newPiso);
                            }
                        }
                    }  
                }
                if (!edificioUnidad) {
                    if (edificio) {
                        var newEdificio = { id: edificio.id, pisos: [] };
                        var newPiso = { id: piso.id, areas: [] };
                        newPiso.areas.push({ id: area.id });
                        newEdificio.pisos.push(newPiso);
                        $rootScope.unidad.inmuebles[0].edificios.push(newEdificio);
                        edificio.checked = true;
                    } else {
                        var pisoExist = false;
                        for (var i = 0; i < $rootScope.unidad.inmuebles[0].edificios[0].pisos.length; i++) {
                            if ($rootScope.unidad.inmuebles[0].edificios[0].pisos[i].id == piso.id) {
                                $rootScope.unidad.inmuebles[0].edificios[0].pisos[i].areas.push({ id: area.id });
                                pisoExist = true;
                            }
                        };
                        if (!pisoExist) {
                            var newPiso = { id: piso.id, areas: [] };
                            newPiso.areas.push({ id: area.id });
                            $rootScope.unidad.inmuebles[0].edificios[0].pisos.push(newPiso);
                        }
                    }
                   
                };
                
                piso.checked = true;
                area.checked = true;
            } else {
                if (edificio) {
                    for (var i = 0; i < $rootScope.unidad.inmuebles[0].edificios.length; i++) {
                        if ($rootScope.unidad.inmuebles[0].edificios[i].id = edificio.id) {
                            for (var j = 0; j < $rootScope.unidad.inmuebles[0].edificios[i].pisos.length; j++) {
                                if ($rootScope.unidad.inmuebles[0].edificios[i].pisos[j].id == piso.id) {
                                    for (var k = 0; k < $rootScope.unidad.inmuebles[0].edificios[i].pisos[j].areas.length; k++) {
                                        if ($rootScope.unidad.inmuebles[0].edificios[i].pisos[j].areas[k].id == area.id) {
                                            $rootScope.unidad.inmuebles[0].edificios[i].pisos[j].areas.splice(k, 1);
                                            k--;
                                            area.checked = false;
                                        }
                                    }
                                }
                            }
                        }
                    }
                } else {
                    for (var j = 0; j < $rootScope.unidad.inmuebles[0].edificios[0].pisos.length; j++) {
                        if ($rootScope.unidad.inmuebles[0].edificios[0].pisos[j].id == piso.id) {
                            for (var k = 0; k < $rootScope.unidad.inmuebles[0].edificios[0].pisos[j].areas.length; k++) {
                                if ($rootScope.unidad.inmuebles[0].edificios[0].pisos[j].areas[k].id == area.id) {
                                    $rootScope.unidad.inmuebles[0].edificios[0].pisos[j].areas.splice(k, 1);
                                    k--;
                                    area.checked = false;
                                }
                            }
                        }
                    }

                }
            }
        }

        $scope.saveUnidad = function () {
            console.log($rootScope.unidad);
            if ($rootScope.editMode) {
                $http({
                    method: 'PUT',
                    url: `/api/unidad/${$rootScope.unidad.id}`,
                    data: JSON.stringify($rootScope.unidad)
                }).then(function (response) {
                    $ngConfirm({
                        title: 'Unidad Actualizada correctamente',
                        content: '',
                        buttons: {
                            Ok: {
                                text: "Ok",
                                btnClass: "btn btn-primary",
                                action: function () {
                                    $rootScope.unidad = {};
                                    $state.go('adminUnidad@home', null, { reload: true });
                                }
                            }

                        }
                    });

                }, function error(response) {
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
            } else {
                $http({
                    method: 'POST',
                    url: '/api/unidad',
                    data: JSON.stringify($rootScope.unidad)
                }).then(function (response) {
                    $ngConfirm({
                        title: 'Unidad Creada correctamente',
                        content: '',
                        buttons: {
                            Ok: {
                                text: "Ok",
                                btnClass: "btn btn-primary",
                                action: function () {
                                    $rootScope.unidad = {};
                                    $state.go('adminUnidad@home', null, { reload: true });
                                }
                            }

                        }
                    });

                }, function error(response) {
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
            }
           

        }
    })
    .controller("editarUnidadController", function ($scope, $rootScope, $state,$http) {
        //console.log("RootScope.unidad",$rootScope.unidad);
        $scope.admin = $rootScope.isAdmin;
        $scope.userId = $rootScope.userId;
        if ($rootScope.unidad == null) {
            $state.go('adminUnidad@home', null, { reload: true });
        }
        $rootScope.unidad.inmuebles = [];
        $scope.loadingTop = true;
        $rootScope.editMode = true;
        $http({
            method: 'GET',
            url: `/api/unidad/${$rootScope.unidad.id}`
        }).then(function (response) {
            //console.log("inmuebleUnidad",response.data);
            $scope.loadingTop = false;
            $scope.inmuebleUnidad = response.data;
            for (var i = 0; i < $scope.inmuebleUnidad.inmuebles.length; i++) {
                if ($scope.inmuebleUnidad.inmuebles[i].tipoInmueble == 1) {
                    $scope.inmuebleUnidad.complejo = $scope.inmuebleUnidad.inmuebles[i];
                    $scope.inmuebleUnidad.inmuebles.splice(i, 1);
                }
            }

            var inmueleId;
            if ($scope.inmuebleUnidad.complejo) {
                inmueleId = $scope.inmuebleUnidad.complejo.id;
            } else {
                inmueleId = $scope.inmuebleUnidad.inmuebles[0].id;
            }

            $http({
                method: 'GET',
                url: `/api/inmuebles/V2/${inmueleId}`
            }).then(function (response) {
                $scope.loadingTop = false;
                $rootScope.inmueble = response.data;
                //console.log("inmueble", $rootScope.inmueble)
                setInmuebleUnidad($rootScope.inmueble, $scope.inmuebleUnidad);
            });
        });
        $scope.back = function () {
            $rootScope.editMode = false;
            $state.go('adminUnidad@home', null, { reload: true });
        }
        $scope.saveUnidad = function (unidad) {
            $state.go('adminUnidad@asociarInmueble', null, { reload: true });
        }

        function setInmuebleUnidad(inmueble, inmuebleUnidad) {
            if (inmueble.tipoInmueble == 1) {
                $rootScope.unidad.inmuebles.push({ id: inmueble.id, edificios: [] });
                for (var i = 0; i < inmueble.children.length; i++) {
                    for (var j = 0; j < inmuebleUnidad.inmuebles.length; j++) {
                        if (inmueble.children[i].id == inmuebleUnidad.inmuebles[j].id) {
                            inmueble.children[i].checked = true;
                            $rootScope.unidad.inmuebles[0].edificios.push({ id: inmueble.children[i].id , pisos: [] });
                        }
                    }

                    for (var k = 0; k < inmueble.children[i].pisos.length; k++) {
                        for (var l = 0; l < inmuebleUnidad.pisos.length; l++) {
                            if (inmueble.children[i].pisos[k].id == inmuebleUnidad.pisos[l].id) {
                                inmueble.children[i].pisos[k].checked = true;
                                $rootScope.unidad.inmuebles[0].edificios[i].pisos.push({ id: inmueble.children[i].pisos[k].id, areas: [] });
                            }
                        }

                        for (var m = 0; m < inmueble.children[i].pisos[k].areas.length; m++) {
                            for (var n = 0; n < inmuebleUnidad.areas.length; n++) {
                                if (inmueble.children[i].pisos[k].areas[m].id == inmuebleUnidad.areas[n].id) {
                                    inmueble.children[i].pisos[k].areas[m].checked = true;
                                    let piso = $rootScope.unidad.inmuebles[0].edificios[i].pisos.find(piso => piso.id == inmueble.children[i].pisos[k].id);
                                    piso.areas.push({ id: inmueble.children[i].pisos[k].areas[m].id});
                                }
                            }
                        }

                    }
                }
            } else {
                $rootScope.unidad.inmuebles.push({ id: 0, edificios: [] });
                $rootScope.unidad.inmuebles[0].edificios.push({ id: inmueble.id, pisos: [] });
                for (var k = 0; k < inmueble.pisos.length; k++) {
                    for (var l = 0; l < inmuebleUnidad.pisos.length; l++) {
                        if (inmueble.pisos[k].id == inmuebleUnidad.pisos[l].id) {
                            inmueble.pisos[k].checked = true;
                            $rootScope.unidad.inmuebles[0].edificios[0].pisos.push({ id: inmueble.pisos[k].id, areas: [] });
                        }
                    }

                    for (var m = 0; m < inmueble.pisos[k].areas.length; m++) {
                        for (var n = 0; n < inmuebleUnidad.areas.length; n++) {
                            if (inmueble.pisos[k].areas[m].id == inmuebleUnidad.areas[n].id) {
                                inmueble.pisos[k].areas[m].checked = true;
                                $rootScope.unidad.inmuebles[0].edificios[0].pisos[k].areas.push({ id: inmueble.pisos[k].areas[m].id});
                            }
                        }
                    }

                }
            }
        }
    })
    ;