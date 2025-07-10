var app = angular.module('implementacionSeguimientoApp', ['ui.router', 'cp.ngConfirm', 'ServicesModule'])
    .controller("implementacionSeguimientoController", function ($rootScope, $state) {
        var userId = angular.element("#input-id").val();
        var isAdmin = angular.element("#input-is-admin").val();
        const isConsulta = angular.element("#input-is-consulta").val();
        const lectura = angular.element("#input-lectura").val();
        const escritura = angular.element("#input-escritura").val();
        $rootScope.userId = userId;
        $rootScope.isAdmin = isAdmin == 'False' ? false : true;
        $rootScope.isConsulta = isConsulta == 'False' ? false : true;
        $rootScope.lectura = lectura == 'False' ? false : true;
        $rootScope.escritura = escritura == 'False' ? false : true;
        $rootScope.selectedServicioId = localStorage.getItem('servicioEvId');
        $rootScope.selectedServicioNombre = localStorage.getItem('servicioEvNombre');
        $state.go("implementacionSeguimiento@home");
    })
    .controller("homeImplementacionSeguimientoController", function ($scope, $rootScope, $http, $ngConfirm, $planGestionModule, $state) {

        if (!$rootScope.lectura) {
            window.location.href = `/AdminMisServicios`
        }

        // Inicialización de variables
        $scope.filtros = {
            dimensionId: null,
            objetivoId: null,
            anio: null
        };
        
        $scope.dimensiones = [];
        $scope.objetivos = [];
        $scope.aniosDisponibles = [];
        $scope.brechas = [];
        $scope.acciones = [];
        $scope.loading = false;
        $scope.loadingObjetivos = false;
        $scope.escritura = $rootScope.escritura;

        // Obtener configuración de API
        $http({
            method: 'GET',
            url: `/settings?sectionName=ApiConfiguration&paramName=apiGestionaEnergia`
        }).then(function (response) {
            $rootScope.APIURL = response.data;
            init();
        });

        // Inicialización del controlador
        function init() {
            // Validar que hay un servicio seleccionado
            if (!$rootScope.selectedServicioId) {
                window.mostrarToast('Error', 'No hay servicio seleccionado. Redirigiendo...', 'warning');
                setTimeout(() => {
                    window.location.href = '/AdminMisServicios';
                }, 2000);
                return;
            }
            
            cargarDimensiones();
            cargarAniosDisponibles();
            cargarObjetivos();
        }

        // Función para mapear AccionToEditDTO a AccionToSaveDTO
        function mapAccionToSave(accionToEdit) {
            return {
                dimensionBrechaId: accionToEdit.dimensionBrechaId,
                objetivoId: accionToEdit.objetivoId,
                medidaId: accionToEdit.medidaId,
                otraMedida: accionToEdit.otraMedida,
                medidaDescripcion: accionToEdit.medidaDescripcion,
                cobertura: accionToEdit.cobertura || '',
                nivelMedida: accionToEdit.nivelMedida || '',
                gestorRespnsableId: accionToEdit.gestorRespnsableId,
                responsableNombre: accionToEdit.responsableNombre,
                responsableEmail: accionToEdit.responsableEmail,
                adjunto: accionToEdit.adjunto,
                adjuntoNombre: accionToEdit.adjuntoNombre,
                otroServicio: accionToEdit.otroServicio || '',
                presupuestoIngenieria: accionToEdit.presupuestoIngenieria,
                presupuestoIngenieriaPedido: accionToEdit.presupuestoIngenieriaPedido,
                presupuestoImplementacion: accionToEdit.presupuestoImplementacion,
                presupuestoImplementacionPedido: accionToEdit.presupuestoImplementacionPedido,
                piloto: accionToEdit.piloto || false,
                itemPresupuestario: accionToEdit.itemPresupuestario,
                subtitulo: accionToEdit.subtitulo,
                asignacionPresupuestaria: accionToEdit.asignacionPresupuestaria,
                costoAsociado: accionToEdit.costoAsociado,
                estadoAvance: accionToEdit.estadoAvance,
                observaciones: accionToEdit.observaciones,
                unidades: accionToEdit.unidades || [],
                servicios: accionToEdit.servicios || [],
                userId: $rootScope.userId
            };
        }

        // Cargar dimensiones
        function cargarDimensiones() {
            $planGestionModule.getDimensiones().then(function(response) {
                $scope.dimensiones = response.data;
            }).catch(function(error) {
                console.error('Error al cargar dimensiones:', error);
                window.mostrarToast('Error', 'Error al cargar dimensiones', 'danger');
            });
        }

        // Cargar años disponibles
        function cargarAniosDisponibles() {
            $planGestionModule.getAniosDisponibles().then(function(response) {
                $scope.aniosDisponibles = response.data;
            }).catch(function(error) {
                console.error('Error al cargar años:', error);
                window.mostrarToast('Error', 'Error al cargar años disponibles', 'danger');
            });
        }

        // Cargar objetivos por servicio y filtrar por dimensión en el frontend
        function cargarObjetivos(dimensionId = null) {
            const servicioId = $rootScope.selectedServicioId;
            if (!servicioId) {
                console.error('No hay servicio seleccionado');
                return;
            }

            $scope.loadingObjetivos = true;
            
            // Usar el endpoint que funciona: /api/v2/plangestion/objetivos/{servicioId}
            $planGestionModule.getObjetivos(servicioId).then(function(response) {
                let objetivos = response.data;
                
                // Filtrar por dimensión en el frontend si se especifica
                if (dimensionId) {
                    objetivos = objetivos.filter(objetivo => objetivo.dimensionBrechaId == dimensionId);
                }
                
                // Aplicar truncamiento para opciones largas y agregar tooltips
                objetivos = objetivos.map(objetivo => ({
                    ...objetivo,
                    tituloOriginal: objetivo.titulo,
                    tituloTruncado: objetivo.titulo.length > 50 ? objetivo.titulo.substring(0, 50) + '...' : objetivo.titulo,
                    descripcionOriginal: objetivo.descripcion,
                    descripcionTruncada: objetivo.descripcion.length > 100 ? objetivo.descripcion.substring(0, 100) + '...' : objetivo.descripcion
                }));
                
                $scope.objetivos = objetivos;
                $scope.loadingObjetivos = false;
            }).catch(function(error) {
                console.error('Error al cargar objetivos:', error);
                window.mostrarToast('Error', 'Error al cargar objetivos', 'danger');
                $scope.loadingObjetivos = false;
            });
        }

        // Manejar cambio de dimensión
        $scope.onDimensionChange = function() {
            $scope.filtros.objetivoId = null;
            $scope.brechas = [];
            $scope.acciones = [];
            
            // Cargar objetivos filtrados por servicio y dimensión seleccionada
            cargarObjetivos($scope.filtros.dimensionId);
        };

        // Manejar cambio de objetivo
        $scope.onObjetivoChange = function() {
            if ($scope.filtros.objetivoId) {
                cargarBrechas();
                cargarAcciones();
            } else {
                $scope.brechas = [];
                $scope.acciones = [];
            }
        };

        // Cargar brechas del objetivo seleccionado
        function cargarBrechas() {
            if (!$scope.filtros.objetivoId) return;
            
            $scope.loading = true;
            $http({
                method: 'GET',
                url: `${$rootScope.APIURL}/api/implementacion-seguimiento/brechas/${$scope.filtros.objetivoId}`
            }).then(function(response) {
                $scope.brechas = response.data;
                $scope.loading = false;
            }).catch(function(error) {
                console.error('Error al cargar brechas:', error);
                window.mostrarToast('Error', 'Error al cargar brechas', 'danger');
                $scope.loading = false;
            });
        }

        // Cargar acciones con filtros
        function cargarAcciones() {
            if (!$scope.filtros.objetivoId) return;
            
            $scope.loading = true;
            let params = new URLSearchParams();
            params.append('objetivoId', $scope.filtros.objetivoId);
            params.append('includeTareas', 'true'); // Agregar parámetro para incluir tareas
            
            if ($scope.filtros.anio) {
                params.append('anio', $scope.filtros.anio);
            }

            $http({
                method: 'GET',
                url: `${$rootScope.APIURL}/api/implementacion-seguimiento/acciones?${params.toString()}`
            }).then(function(response) {
                // Inicializar tareas como array vacío si no están presentes
                $scope.acciones = response.data.map(accion => ({
                    ...accion,
                    tareas: accion.tareas || [],
                    mostrarTareas: false // Inicializar estado del acordeón
                }));
                
                console.log('Acciones cargadas:', $scope.acciones.length);
                console.log('Tareas por acción:', $scope.acciones.map(a => ({ id: a.id, tareas: a.tareas.length })));
                $scope.loading = false;
            }).catch(function(error) {
                console.error('Error al cargar acciones:', error);
                window.mostrarToast('Error', 'Error al cargar acciones', 'danger');
                $scope.loading = false;
                // En caso de error, inicializar como array vacío para evitar errores en el template
                $scope.acciones = [];
            });
        }

        // Aplicar filtros
        $scope.aplicarFiltros = function() {
            if ($scope.filtros.objetivoId) {
                cargarBrechas();
                cargarAcciones();
            }
        };

        // Limpiar filtros
        $scope.limpiarFiltros = function() {
            $scope.filtros = {
                dimensionId: null,
                objetivoId: null,
                anio: null
            };
            $scope.brechas = [];
            $scope.acciones = [];
            // Cargar objetivos sin filtro de dimensión pero con servicio
            cargarObjetivos();
        };

        // Obtener estado badge class
        $scope.getEstadoBadgeClass = function(estado) {
            // Normalizar el estado (trim y lowercase para comparación)
            const estadoNormalizado = (estado || '').toString().trim().toLowerCase();
            
            switch(estadoNormalizado) {
                case 'completada':
                case 'completado':
                case 'implementado':
                    return 'badge-completada';
                case 'en progreso':
                case 'en proceso':
                    return 'badge-en-proceso';
                case 'no completada':
                case 'no implementado':
                case 'pendiente':
                case '':
                case 'null':
                case 'undefined':
                    return 'badge-no-completada';
                case 'cancelado':
                    return 'badge-cancelado';
                default:
                    // Para estados desconocidos, también usar 'no completada' como fallback
                    return 'badge-no-completada';
            }
        };

        // Abrir modal de actualización de acción
        $scope.abrirModalAccion = function(accion) {
            // Mostrar loading
            $scope.loading = true;
            
            // Obtener la acción completa desde el backend
            $http({
                method: 'GET',
                url: `${$rootScope.APIURL}/api/implementacion-seguimiento/accion/${accion.id}`
            }).then(function(response) {
                $scope.accionSeleccionada = angular.copy(response.data);
                
                // Inicializar estadoAvance si está vacío, null o undefined
                if (!$scope.accionSeleccionada.estadoAvance ||
                    $scope.accionSeleccionada.estadoAvance.trim() === '' ||
                    $scope.accionSeleccionada.estadoAvance === 'null' ||
                    $scope.accionSeleccionada.estadoAvance === 'undefined') {
                    $scope.accionSeleccionada.estadoAvance = 'No completada';
                }
                
                $scope.loading = false;
                $("#modal-actualizar-accion").modal("show");
            }).catch(function(error) {
                console.error('Error al obtener datos completos de la acción:', error);
                $scope.loading = false;
                window.mostrarToast('Error', 'Error al cargar los datos de la acción', 'danger');
            });
        };

        // Abrir modal de actualización de tarea
        $scope.abrirModalTarea = function(tarea) {
            $scope.tareaSeleccionada = angular.copy(tarea);
            
            // Inicializar estadoAvance si está vacío, null o undefined
            if (!$scope.tareaSeleccionada.estadoAvance ||
                $scope.tareaSeleccionada.estadoAvance.trim() === '' ||
                $scope.tareaSeleccionada.estadoAvance === 'null' ||
                $scope.tareaSeleccionada.estadoAvance === 'undefined') {
                $scope.tareaSeleccionada.estadoAvance = 'No implementado';
            }
            
            $("#modal-actualizar-tarea").modal("show");
        };

        // Cerrar modal de acción
        $scope.cerrarModalAccion = function() {
            $scope.accionSeleccionada = null;
            $("#modal-actualizar-accion").modal("hide");
        };

        // Cerrar modal de tarea
        $scope.cerrarModalTarea = function() {
            $scope.tareaSeleccionada = null;
            $("#modal-actualizar-tarea").modal("hide");
        };

        // Guardar actualización de acción
        $scope.guardarAccion = function() {
            if (!$scope.accionSeleccionada) return;

            // Validar que se haya seleccionado un estado válido
            if (!$scope.accionSeleccionada.estadoAvance || $scope.accionSeleccionada.estadoAvance === '') {
                window.mostrarToast('Validación', 'Debe seleccionar un estado de avance', 'warning');
                return;
            }

            // Mapear AccionToEditDTO a AccionToSaveDTO usando la función helper
            var accionToSave = mapAccionToSave($scope.accionSeleccionada);

            $http({
                method: 'PUT',
                url: `${$rootScope.APIURL}/api/implementacion-seguimiento/accion/${$scope.accionSeleccionada.id}`,
                data: JSON.stringify(accionToSave),
                headers: {
                    'Content-Type': 'application/json'
                }
            }).then(function(response) {
                window.mostrarToast('Éxito', 'Acción actualizada correctamente', 'success');
                $scope.cerrarModalAccion();
                cargarAcciones(); // Recargar datos
            }).catch(function(error) {
                console.error('Error al actualizar acción:', error);
                window.mostrarToast('Error', 'Error al actualizar la acción', 'danger');
            });
        };

        // Guardar actualización de tarea
        $scope.guardarTarea = function() {
            if (!$scope.tareaSeleccionada) return;

            // Validar que se haya seleccionado un estado válido
            if (!$scope.tareaSeleccionada.estadoAvance || $scope.tareaSeleccionada.estadoAvance === '') {
                window.mostrarToast('Validación', 'Debe seleccionar un estado de avance', 'warning');
                return;
            }

            // Validar que la descripción de tarea ejecutada es requerida
            if (!$scope.tareaSeleccionada.descripcionTareaEjecutada ||
                $scope.tareaSeleccionada.descripcionTareaEjecutada.trim() === '') {
                window.mostrarToast('Validación', 'La descripción de tarea ejecutada es requerida', 'warning');
                return;
            }

            $http({
                method: 'PUT',
                url: `${$rootScope.APIURL}/api/implementacion-seguimiento/tarea/${$scope.tareaSeleccionada.id}`,
                data: JSON.stringify($scope.tareaSeleccionada)
            }).then(function(response) {
                window.mostrarToast('Éxito', 'Tarea actualizada correctamente', 'success');
                $scope.cerrarModalTarea();
                cargarAcciones(); // Recargar datos
            }).catch(function(error) {
                console.error('Error al actualizar tarea:', error);
                window.mostrarToast('Error', 'Error al actualizar la tarea', 'danger');
            });
        };

        // Toggle accordion para tareas
        $scope.toggleTareas = function(accion) {
            accion.mostrarTareas = !accion.mostrarTareas;
        };

        // Función para calcular fechas automáticas de acciones
        $scope.calcularFechasAccion = function(accion) {
            // Agregar validación al inicio para manejar cuando accion es undefined
            if (!accion) {
                return {
                    fechaInicio: 'No definida',
                    fechaFin: 'No definida'
                };
            }
            
            if (!accion.tareas || accion.tareas.length === 0) {
                return {
                    fechaInicio: accion.fechaInicioManual || accion.fechaInicio || 'No definida',
                    fechaFin: accion.fechaFinManual || accion.fechaFin || 'No definida'
                };
            }

            // Filtrar tareas con fechas válidas
            let fechasInicio = accion.tareas
                .filter(t => t && t.fechaInicio)
                .map(t => new Date(t.fechaInicio))
                .filter(f => !isNaN(f));
                
            let fechasFin = accion.tareas
                .filter(t => t && t.fechaFin)
                .map(t => new Date(t.fechaFin))
                .filter(f => !isNaN(f));

            return {
                fechaInicio: fechasInicio.length > 0 ?
                    new Date(Math.min(...fechasInicio)).toLocaleDateString('es-CL') :
                    (accion.fechaInicioManual || accion.fechaInicio || 'No definida'),
                fechaFin: fechasFin.length > 0 ?
                    new Date(Math.max(...fechasFin)).toLocaleDateString('es-CL') :
                    (accion.fechaFinManual || accion.fechaFin || 'No definida')
            };
        };

        // Función para truncar texto largo
        $scope.truncateText = function(text, maxLength) {
            if (!text) return '';
            if (text.length <= maxLength) return text;
            return text.substring(0, maxLength) + '...';
        };
    });