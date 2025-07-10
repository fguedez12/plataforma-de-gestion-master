var servicesModule = angular.module('ServicesModule', []);

servicesModule.factory('$institucionModule', function ($http, $rootScope) {
    return {
        getList: function () {
            return  $http({
                method: 'GET',
                url: `${$rootScope.APIURL}/api/institucion`,
            })
        }
        
    }
});

servicesModule.factory('$serviciosModule', function ($http, $rootScope) {
    return {
        getByInstitucionId: function (id) {

            return $http({
                method: 'GET',
                url: `${$rootScope.APIURL}/api/servicios/getlist-by-institucionid/${id}`,
                headers: { 'me-validation': 'Energia2023!!' }
            })
        },
        setDiagnosticoAmbiental: function (id) {
            dataObj = [
                {
                    "op": "replace",
                    "path": "/RevisionDiagnosticoAmbiental",
                    "value":true
                },
                {
                    "op": "replace",
                    "path": "/CompraActiva",
                    "value": false
                }

            ]
            return $http({
                method: 'PATCH',
                url: `${$rootScope.APIURL}/api/servicios/${id}`,
                data: dataObj
            })
        },
        getDiagnostico: function (id) {
            return $http({
                method: 'GET',
                url: `${$rootScope.APIURL}/api/servicios/get-diagnostico/${id}`
            })
        },
        setColaboradoresModAlcance: function (id,numero,modifica) {
            dataObj = [
                {
                    "op": "replace",
                    "path": "/ColaboradoresModAlcance",
                    "value": numero
                },
                {
                    "op": "replace",
                    "path": "/ModificacioAlcance",
                    "value": modifica
                }
            ]
            return $http({
                method: 'PATCH',
                url: `${$rootScope.APIURL}/api/servicios/${id}`,
                data: dataObj
            })
        }
    }
});


servicesModule.factory('$divisionModule', function ($http, $rootScope) {
    return {
        patchDivision: function (id, data) {
            dataobj = [
                {
                    "op": "replace",
                    "path": "/NroRol",
                    "value": data.nroRol
                },
                {
                    "op": "replace",
                    "path": "/SinRol",
                    "value": data.sinRol
                },
                {
                    "op": "replace",
                    "path": "/JustificaRol",
                    "value": data.justificaRol
                }
                ,
                {
                    "op": "replace",
                    "path": "/Funcionarios",
                    "value": data.funcionarios
                },
                {
                    "op": "replace",
                    "path": "/NroOtrosColaboradores",
                    "value": data.nroOtrosColaboradores
                }
                ,
                {
                    "op": "replace",
                    "path": "/DisponeVehiculo",
                    "value": data.disponeVehiculo
                },
                {
                    "op": "replace",
                    "path": "/VehiculosIds",
                    "value": data.vehiculosIds.join(',')
                },
                {
                    "op": "replace",
                    "path": "/AccesoFacturaAgua",
                    "value": data.accesoFacturaAgua
                },
                {
                    "op": "replace",
                    "path": "/InstitucionResponsableAguaId",
                    "value": data.institucionResponsableAguaId
                }
                ,
                {
                    "op": "replace",
                    "path": "/ServicioResponsableAguaId",
                    "value": data.servicioResponsableAguaId
                },
                {
                    "op": "replace",
                    "path": "/ServicioResponsableAguaId",
                    "value": data.servicioResponsableAguaId
                },
                {
                    "op": "replace",
                    "path": "/OrganizacionResponsableAgua",
                    "value": data.organizacionResponsableAgua
                },
                {
                    "op": "replace",
                    "path": "/ComparteMedidorAgua",
                    "value": data.comparteMedidorAgua
                },
                {
                    "op": "replace",
                    "path": "/DisponeCalefaccion",
                    "value": data.disponeCalefaccion
                },
                {
                    "op": "replace",
                    "path": "/AireAcondicionadoElectricidad",
                    "value": data.aireAcondicionadoElectricidad
                },
                {
                    "op": "replace",
                    "path": "/CalefaccionGas",
                    "value": data.calefaccionGas
                },
                {
                    "op": "replace",
                    "path": "/GestionBienes",
                    "value": data.gestionBienes
                }
            ]

            return $http({
                method: 'PATCH',
                url: `${$rootScope.APIURL}/api/divisiones/${id}`,
                data: dataobj
            })
        },
        patchDivisionUsaBidon: function(id,value) {
            dataobj = [
                {
                    "op": "replace",
                    "path": "/UsaBidon",
                    "value": value
                }
            ];

            return $http({
                method: 'PATCH',
                url: `${$rootScope.APIURL}/api/divisiones/${id}`,
                data: dataobj
            })
        }
    }
});
servicesModule.factory('$viajesModule', function ($http, $rootScope) {
    return {
        getPaisList: function () {
            return $http({
                method: 'GET',
                url: `${$rootScope.APIURL}/api/v2/viajes/paises`,
            })
        },
        getAeropuertoByPaisId: function (paisId) {
            return $http({
                method: 'GET',
                url: `${$rootScope.APIURL}/api/v2/viajes/aeropuerto/${paisId}`,
            })
        }

    }
});

servicesModule.factory('$sistemasModule', function ($http, $rootScope) {
    return {
        getData: function (unidadId) {
            return $http({
                method: 'GET',
                url: `${$rootScope.APIURL}/api/v2/sistemas/getData/${unidadId}`,
            })
        },
        getEnergeticosByEquipoId: function (equipoId) {
            return $http({
                method: 'GET',
                url: `${$rootScope.APIURL}/api/v2/sistemas/energetico-by-equipoid/${equipoId}`,
            })
        },
        getColectores: function (tipo) {
            return $http({
                method: 'GET',
                url: `${$rootScope.APIURL}/api/v2/sistemas/colectores?tipo=${tipo}`,
            })
        },
        saveSistemas: function (divisionId,dataSistemas) {
            return $http({
                method: 'PUT',
                url: `${$rootScope.APIURL}/api/v2/sistemas/${divisionId}`,
                data: JSON.stringify(dataSistemas),
            })
        }
    }
});

servicesModule.factory('$planGestionModule', function ($http, $rootScope) {
    return {
        getData: function (servicioId) {
            return $http({
                method: 'GET',
                url: `${$rootScope.APIURL}/api/v2/plangestion/${servicioId}`,
            })
        },
        saveObs: function (item, servicioId) {
            return $http({
                method: 'POST',
                url: `${$rootScope.APIURL}/api/v2/plangestion/dimension-servicio/${servicioId}`,
                data: JSON.stringify(item),
            })
        },
        getPgaData: function (servicioId) {
            return $http({
                method: 'GET',
                url: `${$rootScope.APIURL}/api/v2/plangestion/${servicioId}/pga`,
            })
        },
        savePgaInfo: function (item, servicioId) {
            return $http({
                method: 'PUT',
                url: `${$rootScope.APIURL}/api/v2/plangestion/pga-info/${servicioId}`,
                data: JSON.stringify(item),
            })
        },
        getUnidades: function (servicioId, searchText) {
            return $http({
                method: 'GET',
                url: `${$rootScope.APIURL}/api/v2/plangestion/unidades-by-servicioId/${servicioId}?searchText=${searchText}`,
            })
        },
        saveBrecha: function (servicioId,brecha, unidades) {
            let item = brecha;
            //console.log(brecha)
            //console.log(unidades)
            item.unidades = unidades;
            return $http({
                method: 'POST',
                url: `${$rootScope.APIURL}/api/v2/plangestion/save-brecha/${servicioId}`,
                data: JSON.stringify(item),
            })
        },
        getbrechas: function (servicioId) {
            return $http({
                method: 'GET',
                url: `${$rootScope.APIURL}/api/v2/plangestion/brechas/${servicioId}`,
            })
        },
        getbrechabyId: function (id) {
            return $http({
                method: 'GET',
                url: `${$rootScope.APIURL}/api/v2/plangestion/brecha-by-id/${id}`,
            })
        },
        editBrecha: function (id, brecha, unidades) {
            let item = brecha;
            //console.log(brecha)
            //console.log(unidades)
            item.unidades = unidades;
            return $http({
                method: 'PUT',
                url: `${$rootScope.APIURL}/api/v2/plangestion/edit-brecha/${id}`,
                data: JSON.stringify(item),
            })
        },
        deleteBrecha: function (id) {
            return $http({
                method: 'DELETE',
                url: `${$rootScope.APIURL}/api/v2/plangestion/brecha/${id}`
            })
        },
        saveObjetivo: function (objetivo, brechas) {
            let item = objetivo;
            //console.log(brecha)
            //console.log(unidades)
            item.brechasSelected = brechas;
            return $http({
                method: 'POST',
                url: `${$rootScope.APIURL}/api/v2/plangestion/save-objetivo`,
                data: JSON.stringify(item),
            })
        },
        getObjetivos: function (servicioId) {
            return $http({
                method: 'GET',
                url: `${$rootScope.APIURL}/api/v2/plangestion/objetivos/${servicioId}`,
            })
        },
        getObjetivoById: function (id) {
            return $http({
                method: 'GET',
                url: `${$rootScope.APIURL}/api/v2/plangestion/objetivo-by-id/${id}`,
            })
        },
        editObjetivo: function (id, objetivo, brechas) {
            let item = objetivo;
            //console.log(brecha)
            //console.log(unidades)
            item.brechasSelected = brechas;
            return $http({
                method: 'PUT',
                url: `${$rootScope.APIURL}/api/v2/plangestion/edit-objetivo/${id}`,
                data: JSON.stringify(item),
            })
        },
        deleteObjetivo: function (id) {
            return $http({
                method: 'DELETE',
                url: `${$rootScope.APIURL}/api/v2/plangestion/objetivo/${id}`
            })
        },
        getMedidas: function (dimId) {
            return $http({
                method: 'GET',
                url: `${$rootScope.APIURL}/api/v2/plangestion/medidas/${dimId}`,
            })
        },
        getUnidadesByObjetivoId: function (objetivoId) {
            return $http({
                method: 'GET',
                url: `${$rootScope.APIURL}/api/v2/plangestion/unidades-by-objetivoId/${objetivoId}`,
            })
        },
        getUsuariosByServicioId: function (servicioId) {
            return $http({
                method: 'GET',
                url: `${$rootScope.APIURL}/api/v2/plangestion/usuarios-by-servicioId?servicioId=${servicioId}`,
            })
        },
        getOtrosServicios: function (servicioId) {
            return $http({
                method: 'GET',
                url: `${$rootScope.APIURL}/api/v2/plangestion/otros-servicios/${servicioId}`,
            })
        },
        saveAccion: function (accion) {
            let item = accion;
            item.unidades = accion.unidadesDeAccionSelect;
            item.servicios = accion.serviciosSelected;
            return $http({
                method: 'POST',
                url: `${$rootScope.APIURL}/api/v2/plangestion/accion`,
                data: JSON.stringify(item),
            })
        },
        getAcciones: function (servicioId) {
            return $http({
                method: 'GET',
                url: `${$rootScope.APIURL}/api/v2/plangestion/acciones/${servicioId}`,
            })
        },
        getAccionById: function (id) {
            return $http({
                method: 'GET',
                url: `${$rootScope.APIURL}/api/v2/plangestion/accion-by-id/${id}`,
            })
        },
        editAccion: function (accion) {
            let item = accion;
            item.unidades = accion.unidadesDeAccionSelect;
            item.servicios = accion.serviciosSelected;
            return $http({
                method: 'PUT',
                url: `${$rootScope.APIURL}/api/v2/plangestion/edit-accion/${accion.id}`,
                data: JSON.stringify(item),
            })
        },
        deleteAccion: function (id) {
            return $http({
                method: 'DELETE',
                url: `${$rootScope.APIURL}/api/v2/plangestion/accion/${id}`
            })
        },
        saveIndicador: function (indicador) {
            let item = indicador;
            return $http({
                method: 'POST',
                url: `${$rootScope.APIURL}/api/v2/plangestion/save-indicador`,
                data: JSON.stringify(item),
            })
        },
        getindicadores: function (servicioId) {
            return $http({
                method: 'GET',
                url: `${$rootScope.APIURL}/api/v2/plangestion/indicadores/${servicioId}`,
            })
        },
        getIndicadorById: function (id) {
            return $http({
                method: 'GET',
                url: `${$rootScope.APIURL}/api/v2/plangestion/indicador-by-id/${id}`,
            })
        },
        editIndicador: function (id, objetivo) {
            let item = objetivo;
            return $http({
                method: 'PUT',
                url: `${$rootScope.APIURL}/api/v2/plangestion/edit-indicador/${id}`,
                data: JSON.stringify(item),
            })
        },
        deleteIndicador: function (id) {
            return $http({
                method: 'DELETE',
                url: `${$rootScope.APIURL}/api/v2/plangestion/indicador/${id}`
            })
        },
        savePrograma: function (programa, servicioId) {
            let item = programa;
            programa.servicioId = servicioId;
            return $http({
                method: 'POST',
                url: `${$rootScope.APIURL}/api/v2/plangestion/programa`,
                data: JSON.stringify(item),
            })
        },
        getProgramas: function (servicioId) {
            return $http({
                method: 'GET',
                url: `${$rootScope.APIURL}/api/v2/plangestion/programas/${servicioId}`,
            })
        },
        getProgramaById: function (id) {
            return $http({
                method: 'GET',
                url: `${$rootScope.APIURL}/api/v2/plangestion/programa-by-id/${id}`,
            })
        },
        editPrograma: function (id, objetivo) {
            let item = objetivo;
            return $http({
                method: 'PUT',
                url: `${$rootScope.APIURL}/api/v2/plangestion/edit-programa/${id}`,
                data: JSON.stringify(item),
            })
        },
        deletePrograma: function (id) {
            return $http({
                method: 'DELETE',
                url: `${$rootScope.APIURL}/api/v2/plangestion/programa/${id}`
            })
        },
        // Funciones CRUD para Tareas - siguiendo el patrón de indicadores
        saveTarea: function (tarea) {
            let item = tarea;
            return $http({
                method: 'POST',
                url: `${$rootScope.APIURL}/api/v2/plangestion/save-tarea`,
                data: JSON.stringify(item),
            })
        },
        getTareas: function (servicioId) {
            return $http({
                method: 'GET',
                url: `${$rootScope.APIURL}/api/v2/plangestion/tareas/${servicioId}`,
            })
        },
        getTareaById: function (id) {
            return $http({
                method: 'GET',
                url: `${$rootScope.APIURL}/api/v2/plangestion/tarea-by-id/${id}`,
            })
        },
        editTarea: function (id, tarea) {
            let item = tarea;
            return $http({
                method: 'PUT',
                url: `${$rootScope.APIURL}/api/v2/plangestion/edit-tarea/${id}`,
                data: JSON.stringify(item),
            })
        },
        deleteTarea: function (id) {
            return $http({
                method: 'DELETE',
                url: `${$rootScope.APIURL}/api/v2/plangestion/tarea/${id}`
            })
        },
        // Funciones para Filtros (Tarea #4)
        getFiltros: function () {
            return $http({
                method: 'GET',
                url: `${$rootScope.APIURL}/api/v2/plan-gestion/filtros`,
            })
        },
        getDimensiones: function () {
            return $http({
                method: 'GET',
                url: `${$rootScope.APIURL}/api/v2/plan-gestion/filtros/dimensiones`,
            })
        },
        getObjetivosByDimension: function (dimensionId) {
            const url = dimensionId
                ? `${$rootScope.APIURL}/api/v2/plan-gestion/filtros/objetivos?dimensionId=${dimensionId}`
                : `${$rootScope.APIURL}/api/v2/plan-gestion/filtros/objetivos`;
            return $http({
                method: 'GET',
                url: url,
            })
        },
        getObjetivosByServicioDimension: function (servicioId, dimensionId) {
            let params = new URLSearchParams();
            params.append('servicioId', servicioId);
            
            if (dimensionId) {
                params.append('dimensionId', dimensionId);
            }
            
            return $http({
                method: 'GET',
                url: `${$rootScope.APIURL}/api/v2/plan-gestion/filtros/objetivos-por-servicio?${params.toString()}`,
            })
        },
        getAniosDisponibles: function () {
            return $http({
                method: 'GET',
                url: `${$rootScope.APIURL}/api/v2/plan-gestion/filtros/anios`,
            })
        },
        // Funciones para Lista de Acciones y Tareas con filtros
        getAccionesTareasFiltradas: function (servicioId, filtros) {
            let params = new URLSearchParams();
            params.append('servicioId', servicioId);
            
            if (filtros.dimensionId) {
                params.append('dimensionId', filtros.dimensionId);
            }
            if (filtros.objetivoId) {
                params.append('objetivoId', filtros.objetivoId);
            }
            if (filtros.anio) {
                params.append('anio', filtros.anio);
            }
            if (filtros.tipo) {
                params.append('tipo', filtros.tipo); // 'acciones' o 'tareas'
            }
            
            return $http({
                method: 'GET',
                url: `${$rootScope.APIURL}/api/v2/plan-gestion/acciones-tareas?${params.toString()}`,
            })
        }
    }
});