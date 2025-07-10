angular.module("componentsModule", [])
    .component("filtroUnidades", {
        templateUrl: "/js/components/filtro-unidades/filtro-unidades.html",
        controller: filtroUnidadesController,
        bindings: {
            title: "@",
            userId: "@",
            nuevo: '<',
            volver: '<',
            onInstitucionChange: '&',
            onServicioChange: '&',
            onRegionChange: '&',
            onNewClick: '&',
            onBackClick: '&'
        }
    })
    .component("selectInstitucion", {
        templateUrl: "/js/components/select-institucion/select-institucion.html",
        controller: selectInstitucionController,
        bindings: {
            userId: "@",
            institucionId: "@",
            onInstitucionChange: '&',
        }
    })
    .component("selectServicio", {
        templateUrl: "/js/components/select-servicio/select-servicio.html",
        controller: selectServicioController,
        bindings: {
            userId: "@",
            servicioId: "@",
            servicios: "<",
            onServicioChange: '&',
        }
    })
    .component("selectUnidad", {
        templateUrl: "/js/components/select-unidad/select-unidad.html",
        controller: selectUnidadController,
        bindings: {
            userId: "@",
            unidadId: "@",
            unidades: "<",
            onUnidadChange: '&',
        }
    })
    .component('unidadesTable', {
        templateUrl: './js/components/unidades-table/unidades-table.html',
        controller: unidadesTableController,
        bindings: {
            userId: '@',
            admin:'<',
            unidades: '<',
            loading: '<',
            activo: '<',
            acciones: '<',
            onDelete: "&",
            onEdit: "&",
            onView:"&"
        }
    })
    .component("findLocation", {
        templateUrl: "/js/components/find-location/find-location.html",
        controller: findLocationController,
        bindings: {
            onChangeLocation: "&"
        }
    })
    .component("inmuebleSearch", {
        templateUrl: "/js/components/inmueble-search/inmueble-search.html",
        controller: inmuebleSearchController,
        bindings: {
            inmuebleListSearch: '<',
            inmuebleSearched: '<',
            inmuebleSelected: '&',
            loading:"<"

        }
    })
    .component("unidadForm", {
        templateUrl: "/js/components/unidad-form/unidad-form.html",
        controller: unidadFormController,
        bindings: {
            admin:"<",
            inmueble: "<",
            onBack: "&",
            userId: "<",
            onSave: "&",
            unidad: "<",
            editMode: "<",
            institucionesResponsables:"<"
        }
    })
    .component("inmuebleTop", {
        templateUrl: "/js/components/inmueble-top/inmueble-top.html",
        controller: inmuebleTopController,
        bindings: {
            inmueble: "<"
        }
    })
    .component("inmuebleDetalle", {
        templateUrl: "/js/components/inmueble-detalle/inmueble-detalle.html",
        controller: inmuebleDetalleController,
        bindings: {
            admin: "<",
            editMode:"<",
            inmueble: "<",
            onBack: "&",
            onAddPiso: "&",
            onAddArea: "&",
            onAddEdificio : "&",
            onSave: "&"
        }
    })
    ;

function filtroUnidadesController($http) {
    var vm = this;
    vm.institucionId = '';
    vm.servicioId = '';
    vm.regionId = '';

    vm.$onInit = function () {
        $http.get(`/api/instituciones/getinstituciones/${vm.userId}`)
            .then(function (result) {
                vm.instituciones = result.data;
            });

        $http.get(`/api/regiones/getregiones`)
            .then(function (result) {
                vm.regiones = result.data;
            })

    }

    vm.changeInstitucion = function () {
        if (vm.institucionId == '') {
            vm.servicios = [];
        } else {
            $http.get(`/api/servicios/getserviciosbyuserandinstitucion/${vm.userId}/${vm.institucionId}`)
                .then(function (result) {
                    vm.servicios = result.data;
                    console.log("Change");

                })
        }

        vm.onInstitucionChange({ institucionId: vm.institucionId });
    };

    vm.changeServicio = function () {

        vm.onServicioChange({ servicioId: vm.servicioId });

    };

    vm.changeRegion = function () {

        vm.onRegionChange({ regionId: vm.regionId });


    };
    vm.clickNew = function () {
        vm.onNewClick();
    };
    vm.clickBack = function () {
        vm.onBackClick();
    };
};
function selectInstitucionController($http) {
    var vm = this;
    vm.institucionId = '';
    vm.$onInit = function () {

        $http({
            method: 'GET',
            url: `/settings?sectionName=ApiConfiguration&paramName=apiGestionaEnergia`

        }).then(function (response) {
            vm.APIURL = response.data;
            $http.get(`${vm.APIURL}/api/Institucion/getAsociadosByUserId/${vm.userId}`)
                .then(function (result) {
                    vm.instituciones = result.data;
                });
        });
    };

    vm.changeInstitucion = function () {
        vm.onInstitucionChange({ institucionId: vm.institucionId });
    }
};
function selectServicioController($http) {
    var vm = this;

    vm.changeServicio = function () {
        vm.onServicioChange({servicioId:vm.servicioId})
    }

};
function selectUnidadController($http) {
    var vm = this;

    vm.changeUnidad = function () {
        vm.onUnidadChange({ unidadId: vm.unidadId })
    }

};
function unidadesTableController($http, $timeout) {
    var vm = this;
    vm.$onInit = function () {

    };
    vm.deleteUnidad = function (unidad) {
        
        vm.onDelete({ unidad: unidad });
    };

    vm.editUnidad = function (unidad) {
        vm.onEdit({ unidad: unidad });
    }

    vm.viewUnidad = function (unidad) {
        vm.onView({ unidad: unidad });
    }
};
async function getSelectData($http, id, url) {
    const response = await $http({
        method: 'GET',
        url: url + id
    });
    return response.data;
};
async function SetObj(arr, nombre) {
    const itemList = await Promise.all(arr.map(item => Promise.resolve(item)))
    const filterdList = itemList.filter(item => item.nombre == nombre);
    if (filterdList.length > 0) {
        return filterdList[0];
    }
}
function findLocationController($http, NgMap, $timeout) {
    var vm = this;
    vm.adress = "";
    vm.types = "['address']";
    var lat = -33.444287;
    var lng = -70.6565585;
    vm.initialPos = [lat, lng]
    vm.direccion = {};
    vm.data = {};
    vm.$onInit = function () {
        $http({
            method: 'GET',
            url: '/api/regiones/getregiones'
        }).then(function (response) {
            vm.data.regiones = response.data;
        });

    };

    vm.getpos = function (event) {
        vm.initialPos = [event.latLng.lat(), event.latLng.lng()];
        vm.direccion.latitud = event.latLng.lat();
        vm.direccion.longitud = event.latLng.lng();
    };

    vm.changeLocation = async function () {

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
                    console.log(comuna);
                    if (comuna == "Cabo de Hornos") {
                        comuna = "Cabo de Hornos (Ex Navarino)";
                        console.log(comuna);
                    }
                }


            }
        }

        //En caso de usar ng-model
        vm.direccion.adress = vm.adress;
        vm.direccion.direccionCompleta = `${calle} ${numero}, ${comuna}`
        vm.direccion.calle = calle;
        vm.direccion.numero = numero;
        vm.direccion.comunaId = null;
        vm.direccion.region = region;
        vm.direccion.provincia = provincia;
        vm.direccion.comuna = comuna;
        vm.direccion.latitud = vm.place.geometry.location.lat();
        vm.direccion.longitud = vm.place.geometry.location.lng();

        //en caso de usar controles mvc
        $calle.val(calle);
        $numero.val(numero);
        $lat.val(vm.place.geometry.location.lat());
        $lng.val(vm.place.geometry.location.lng());

        //vm.direccion.regionId = await changeRegion($timeout, region);
        var regionObj = await SetObj(vm.data.regiones, region);
        if (!regionObj) {
            vm.direccion.regionId = "";
            vm.direccion.region = "";
        }
        vm.direccion.regionId = regionObj.id;
        vm.direccion.region = regionObj.nombre;

        vm.data.provincias = await getSelectData($http, vm.direccion.regionId, '/api/provincia/getByRegionId/');

        var provinciaObj = await SetObj(vm.data.provincias, provincia);
        if (!provinciaObj) {
            vm.direccion.provinciaId = "";
            vm.direccion.provincia = "";
        }

        vm.direccion.provinciaId = provinciaObj.id;
        vm.direccion.provincia = provinciaObj.nombre;

        vm.data.comunas = await getSelectData($http, vm.direccion.provinciaId, '/api/comuna/getByProvinciaId/');

        var comunaObj = await SetObj(vm.data.comunas, comuna);
        if (!comunaObj) {
            vm.direccion.comunaId = "";
            vm.direccion.comuna = "";
        }

        vm.direccion.comunaId = comunaObj.id;
        vm.direccion.comuna = comunaObj.nombre;

        vm.map.setCenter(vm.place.geometry.location);
        vm.initialPos = [vm.place.geometry.location.lat(), vm.place.geometry.location.lng()]

        vm.onChangeLocation({ direccion: vm.direccion });

    }
    NgMap.getMap({ id: 'map1' }).then(function (map) {
        vm.map = map;
    });


};
function inmuebleSearchController() {
    var vm = this;
    vm.inmuebleSearched = false;
    vm.selInmueble = function (inmueble) {
        vm.inmuebleSelected({ inmueble: inmueble });
    }

    vm.newInmueble = function () {
        console.log("Nuevo inmueble");
        window.location.href = "/AdminInmueble";
    }

}
function unidadFormController($http) {
    var vm = this;
    vm.disabledServicio = true;
    vm.disabledServicioResponsable = true;
    vm.nameTaken = false;
    //console.log(vm.unidad);
    vm.$onInit = function () {
       

        //console.log(vm.unidad);
        if (vm.editMode) {
            vm.disabledServicioResponsable = false;
            if (vm.unidad.accesoFactura == 2) {
                $http.get(`/api/servicios/getByInstitucionIdAsync/${vm.unidad.institucionResponsableId}`)
                    .then(function (result) {
                        vm.seriviciosResponsables = result.data;
                        //console.log(vm.institucionesResponsables);
                        vm.loadingServicioResponsable = false;
                        vm.disabledServicioResponsable = false;
                    })
            }
            
        } else {
            vm.disabledServicioResponsable = true;
        }

        vm.loadingInstituciones = true;
        $http.get(`/api/instituciones/getinstituciones/${vm.userId}`)
            .then(function (result) {
                vm.instituciones = result.data;
                vm.loadingInstituciones = false;
            });

        if (vm.unidad) {
            $http.get(`/api/servicios/getserviciosbyuserandinstitucion/${vm.userId}/${vm.unidad.institucionId}`)
                .then(function (result) {
                    vm.servicios = result.data;
                    vm.loadingServicio = false;
                    vm.disabledServicio = false;
                })
        }
    };

    vm.changeInstitucion = function () {
        vm.loadingServicio = true;
        $http.get(`/api/servicios/getserviciosbyuserandinstitucion/${vm.userId}/${vm.unidad.institucionId}`)
            .then(function (result) {
                vm.servicios = result.data;
                vm.loadingServicio = false;
                vm.disabledServicio = false;
            })
    }

    vm.changeInstitucionResponsable = function () {
        vm.loadingServicioResponsable = true;
        $http.get(`/api/servicios/getByInstitucionIdAsync/${vm.unidad.institucionResponsableId}`)
            .then(function (result) {
                vm.seriviciosResponsables = result.data;
                console.log(vm.institucionesResponsables);
                vm.loadingServicioResponsable = false;
                vm.disabledServicioResponsable = false;
            })
    }

    vm.saveUnidad = function () {
        if (!vm.editMode) {
            $http.get(`/api/unidad/${vm.unidad.nombre}/${vm.unidad.servicioId}`)
                .then(function (result) {
                    console.log(result.data);
                    if (result.data) {
                        vm.nameTaken = true;
                    } else {
                        vm.nameTaken = false;
                        vm.onSave({ unidad: vm.unidad });
                    }

                })
        } else {
            vm.onSave({ unidad: vm.unidad });
        }
        
    }

    vm.back = function () {
        vm.onBack();
    }
};
function inmuebleTopController() {
    var vm = this;

}
function inmuebleDetalleController() {
    var vm = this;
    vm.back = function () {

        vm.onBack();
    };

    vm.addEdificio = function (edificio) {
        vm.onAddEdificio({edificio: edificio })
    };

    vm.addPiso = function (piso,edificio) {
        vm.onAddPiso({ piso: piso, edificio : edificio })
    };

    vm.addArea = function (area,piso,edificio) {
        vm.onAddArea({ area: area, piso:piso, edificio : edificio })
    };
    vm.saveUnidad = function () {
        vm.onSave();
    }
}

