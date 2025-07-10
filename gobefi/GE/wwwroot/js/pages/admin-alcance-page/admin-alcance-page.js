var app = angular.module('adminAlcanceApp', ['ui.router', 'cp.ngConfirm', 'ServicesModule'])
    .controller("adminAlcanceController", function ($rootScope, $state, $http) {
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
        $rootScope.servicioEvId = localStorage.getItem('servicioEvId');
        if (!$rootScope.servicioEvId) {
            window.location.href = '/AdminMisServicios';
        }
        $state.go("adminAlcance@home");

    }).controller("homeAlcanceController", function ($scope, $rootScope, $http, $ngConfirm, $serviciosModule) {

        if (!$rootScope.lectura) {
            window.location.href = `/AdminMisServicios`
        }

        $http({
            method: 'GET',
            url: `/settings?sectionName=ApiConfiguration&paramName=apiGestionaEnergia`

        }).then(function (response) {
            $rootScope.APIURL = response.data;
            $serviciosModule.getDiagnostico($rootScope.servicioEvId).then(resp => {
                console.log(resp.data);
                $scope.etapaSEV = resp.data.etapaSEV;
                // Generar años disponibles incluyendo "Sin información"
                $scope.aniosDisponibles = ["0"].concat(generarAniosDisponibles($scope.etapaSEV, $rootScope.servicioEvId));
                if (resp.data.revisionDiagnosticoAmbiental) {
                    if (!$rootScope.isAdmin) {
                        $rootScope.isConsulta = true
                    }
                }
            });
            LoadData();
        });
        $scope.nombreServicio = localStorage.getItem('servicioEvNombre');
        $scope.divisiones = [];
        $scope.justificacionValidada = false;
        $scope.searchText = '';
        $scope.estadoCarga = 'Incompleto';
        $scope.revisionRed = 'No validado';
        $scope.estapaSEV = 1;

        $scope.chageInicioGestion = function (division) {

            //console.log(division);
            $http({
                method: 'POST',
                url: `${$rootScope.APIURL}/api/divisiones/set-inicio-gestion`,
                data: JSON.stringify(division)
            }).then(function (response) {
                checkJustificacion();
            });
        }


        $scope.chageRestoItems = function (division) {

            //console.log(division);
            $http({
                method: 'POST',
                url: `${$rootScope.APIURL}/api/divisiones/set-resto-items`,
                data: JSON.stringify(division)
            }).then(function (response) {
                checkJustificacion();
            });
        }

        $scope.saveJustificacion = function () {
            //console.log($scope.servicio);
            $scope.servicio.id = $rootScope.servicioEvId
            $http({
                method: 'POST',
                url: `${$rootScope.APIURL}/api/servicios/set-justificacion`,
                data: JSON.stringify($scope.servicio)
            }).then(function (response) {
                checkJustificacion();
                $ngConfirm({
                    title: 'Justificacion',
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
            });
        }

        $scope.searchUnidad = function () {
            if ($scope.searchText.length > 2 || $scope.searchText.length == 0) {
                LoadData();
            }
        }

        // Función para generar los años según la etapa SEV
        // Función para generar los años según la etapa SEV o por excepción de servicioId
        function generarAniosDisponibles(etapa, servicioId) {
            // Excepciones por servicioId
            if (servicioId == 569 || servicioId == 376) {
                // Generar años de 2023 a 2028
                let aniosExcepcion = [];
                for (let anio = 2023; anio <= 2028; anio++) {
                    aniosExcepcion.push(anio.toString());
                }
                return aniosExcepcion;
            }

            // Lógica estándar por etapaSEV
            switch (etapa) {
                case 1:
                    return ['2025', '2026', '2027'];
                case 2:
                    return ['2024', '2025', '2026'];
                case 3:
                    return ['2023', '2024', '2025'];
                default:
                    // Comportamiento por defecto si etapaSEV no es 1, 2 o 3 (puedes ajustarlo)
                    console.warn("Etapa SEV no reconocida:", etapa, ". Usando años por defecto.");
                    return ['2025', '2026', '2027']; // O un array vacío: []
            }
        }

        function checkJustificacion() {
            // Obtener el año base según la etapa actual
            const anioBase = $scope.aniosDisponibles && $scope.aniosDisponibles.length > 0 ? $scope.aniosDisponibles[0] : null;
            const anioBaseStr = anioBase ? anioBase.toString() : null;

            var result = $scope.divisiones.filter(function (a) {
                // La justificación se considera válida si:
                // 1. Los años son iguales al año base, o
                // 2. El valor es "0" (Sin información)
                return !anioBaseStr ||
                    ((a.anioInicioGestionEnergetica != anioBaseStr && a.anioInicioGestionEnergetica != "0") ||
                        (a.anioInicioRestoItems != anioBaseStr && a.anioInicioRestoItems != "0"));
            })

            var resultCarga = $scope.divisiones.filter(function (a) {
                // Considerar "0" como un valor válido
                return (a.anioInicioGestionEnergetica === "" || a.anioInicioGestionEnergetica === null) ||
                    (a.anioInicioRestoItems === "" || a.anioInicioRestoItems === null);
            })

            if (result.length > 0) {
                $scope.justificacionValidada = false;
                $scope.estadoCarga = 'Incompleto';
            } else {
                $scope.justificacionValidada = true;
                $scope.estadoCarga = 'Completo';
            }

            if (resultCarga.length > 0) {
                $scope.estadoCarga = 'Incompleto';
            } else {
                $scope.estadoCarga = 'Completo';
            }
        }

        function LoadData() {
            $http({
                method: 'GET',
                url: `${$rootScope.APIURL}/api/ajustes/`
            }).then(function (res) {
                //console.log(res.data)
                $scope.alcanceActivo = res.data.activeAlcanceModule;
                if (!$scope.alcanceActivo && !$rootScope.isAdmin) {
                    $rootScope.escritura = false;
                } else {
                    $rootScope.escritura = true;
                }
                console.log($rootScope.escritura);
            });
            $scope.loadingTable = true;
            $http({
                method: 'GET',
                url: `${$rootScope.APIURL}/api/divisiones/by-servicio-id?servicioId=${$rootScope.servicioEvId}&searchText=${$scope.searchText}`
            }).then(function (response) {
                $scope.loadingTable = false;
                if (response.data.ok) {
                    $scope.divisiones = response.data.divisiones;
                    $scope.servicio = response.data.servicio;

                    console.log("Años disponibles:", $scope.aniosDisponibles); // Log 1: Ver años disponibles

                    $scope.divisiones.forEach(division => {
                        console.log(`Verificando división ID ${division.id}: anioInicioGestionEnergetica=${division.anioInicioGestionEnergetica}, anioInicioRestoItems=${division.anioInicioRestoItems}`); // Log 2: Ver valores actuales

                        // Verificar y corregir anioInicioGestionEnergetica
                        // Permitir el valor "0" para "Sin información" y validar otros valores contra aniosDisponibles
                        if (division.anioInicioGestionEnergetica &&
                            division.anioInicioGestionEnergetica !== "0" &&
                            !$scope.aniosDisponibles.includes(division.anioInicioGestionEnergetica.toString())) {
                            console.log(`Valor inválido encontrado en anioInicioGestionEnergetica (${division.anioInicioGestionEnergetica}) para división ${division.id}. Cambiando a null.`);
                            division.anioInicioGestionEnergetica = null;
                        }

                        // Verificar y corregir anioInicioRestoItems
                        // Permitir el valor "0" para "Sin información" y validar otros valores contra aniosDisponibles
                        if (division.anioInicioRestoItems &&
                            division.anioInicioRestoItems !== "0" &&
                            !$scope.aniosDisponibles.includes(division.anioInicioRestoItems.toString())) {
                            console.log(`Valor inválido encontrado en anioInicioRestoItems (${division.anioInicioRestoItems}) para división ${division.id}. Cambiando a null.`);
                            division.anioInicioRestoItems = null;
                        }
                    });

                    //console.log($scope.divisiones);
                    if (response.data.servicio.revisionRed) {
                        $scope.revisionRed = 'Validado';
                    }
                    if (response.data.servicio.comentarioRed) {
                        $scope.servicio.comentarioRed = response.data.servicio.comentarioRed;
                    }
                }
                checkJustificacion();
            })
        };
    });