angular.module("pagesModule", ["ui.router"])
    .config(function ($stateProvider, $urlRouterProvider) {
        //$urlRouterProvider.otherwise("/");
        $stateProvider
            .state("adminDiagnostico@home", {
                url: "/admin-diagnostico-home",
                templateUrl: "/js/pages/admin-diagnostico-page/templates/home.html",
                controller: "homeDiagnosticoController",
                controllerAs: "homeCtrl"
            })
            .state("actualizacionUnidad@home", {
                url: "/actualizacion-unidad",
                templateUrl: "/js/pages/actualizacion-unidad/templates/home.html",
                controller: "homeActualizacionUnidadController",
                controllerAs: "homeCtrl"
            })
            .state("adminColaboradores@home", {
                url: "/admin-colaboradores-home",
                templateUrl: "/js/pages/admin-colaboradores-page/templates/home.html",
                controller: "homeColaboradoresController",
                controllerAs: "homeCtrl"
            })
            .state("adminAgregada@home", {
                url: "/admin-agregada-home",
                templateUrl: "/js/pages/admin-agregada-page/templates/home.html",
                controller: "homeAgregadaController",
                controllerAs: "homeCtrl"
            })
            .state("adminResiduos@home", {
                url: "/admin-residuos-home",
                templateUrl: "/js/pages/admin-residuos-page/templates/home.html",
                controller: "homeResiduosController",
                controllerAs: "homeCtrl"
            })
            .state("adminAgua@home", {
                url: "/admin-agua-home",
                templateUrl: "/js/pages/admin-agua-page/templates/home.html",
                controller: "homeAguaController",
                controllerAs: "homeCtrl"
            })
            .state("adminPapel@home", {
                url: "/admin-papel-home",
                templateUrl: "/js/pages/admin-papel-page/templates/home.html",
                controller: "homePapelController",
                controllerAs: "homeCtrl"
            })
            .state("adminAlcance@home", {
                url: "/admin-alcance-home",
                templateUrl: "/js/pages/admin-alcance-page/templates/home.html",
                controller: "homeAlcanceController",
                controllerAs: "homeCtrl"
            })
            .state("adminDocumentos@selunidad", {
                url: "/admin-documentos-selunidad",
                templateUrl: "/js/pages/admin-documentos-page/templates/selunidad.html",
                controller: "selunidadDocumentosController",
                controllerAs: "selunidadCtrl"
            })
            .state("adminDocumentos@home", {
                url: "/admin-documentos-home",
                templateUrl: "/js/pages/admin-documentos-page/templates/home.html",
                controller: "homeDocumentosController",
                controllerAs: "homeCtrl"
            })
            .state("adminUnidad@home", {
                url: "/admin-unidad-home",
                templateUrl: "/js/pages/admin-unidad-page/templates/home.html",
                controller: "homeController",
                controllerAs: "homeCtrl"
            })
            .state("adminUnidad@buscarDireccion", {
                url: "/admin-unidad-buscar-direccion",
                templateUrl: "/js/pages/admin-unidad-page/templates/buscar-direccion.html",
                controller: "buscarDireccionController",
                controllerAs: "buscarDireccionCtrl"
            })
            .state("adminUnidad@crearUnidad", {
                url: "/admin-unidad-crear-unidad",
                templateUrl: "/js/pages/admin-unidad-page/templates/crear-unidad.html",
                controller: "crearUnidadController",
                controllerAs: "crearUnidadMiUnidadCtrl"
            })
            .state("adminUnidad@asociarInmueble", {
                url: "/admin-unidad-asociar-inmueble",
                templateUrl: "/js/pages/admin-unidad-page/templates/asociar-inmueble.html",
                controller: "asociarInmuebleController",
                controllerAs: "asociarInmuebleCtrl"
            })
            .state("adminUnidad@editarUnidad", {
                url: "/admin-unidad-editar-unidad",
                templateUrl: "/js/pages/admin-unidad-page/templates/editar-unidad.html",
                controller: "editarUnidadController",
                controllerAs: "editarUnidadCtrl"
            })
            .state("miUnidad@home", {
                url: "/mi-unidad-home",
                templateUrl: "/js/pages/mi-unidad-page/templates/home.html",
                controller: "homeMiUnidadController",
                controllerAs: "homeMiUnidadCtrl"
            })
            .state("reporteConsumo@home", {
                url: "/reporte-consumo-home",
                templateUrl: "/js/pages/reporte-consumo-page/templates/home.html",
                controller: "homeReporteConsumoController",
                controllerAs: "homeReporteConsumoCtrl"
            })
            .state("medicionInteligente@home", {
                url: "/medicion-inteligente-home",
                templateUrl: "/js/pages/medicion-inteligente/templates/home.html",
                controller: "homeMedicionInteligenteController",
                controllerAs: "homeMedicionInteligenteCtrl"
            })
            .state("sistemas@home", {
                url: "/sistemas-home",
                templateUrl: "/js/pages/sistemas-page/templates/home.html",
                controller: "homeSistemasController",
                controllerAs: "homeSistemasCtrl"
            })
            .state("planGestion@home", {
                url: "/plan-gestion-home",
                templateUrl: "/js/pages/plan-gestion-page/templates/home.html",
                controller: "homePlanGestionController",
                controllerAs: "homePlanGestionCtrl"
            })
            .state("mantenedor@Accion", {
                url: "/accion",
                templateUrl: "/js/pages/plan-gestion-page/templates/accion.html",
                controller: "accionController",
                controllerAs: "accionControllerCtrl"
            })
            .state("mantenedor2@Accion", {
                url: "/accion2",
                templateUrl: "/js/pages/plan-gestion-page/templates/accion2.html",
                controller: "accion2Controller",
                controllerAs: "accion2ControllerCtrl"
            })
            .state("mantenedor3@Accion", {
                url: "/accion3",
                templateUrl: "/js/pages/plan-gestion-page/templates/accion3.html",
                controller: "accion3Controller",
                controllerAs: "accion3ControllerCtrl"
            })
            .state("mantenedor4@Accion", {
                url: "/accion4",
                templateUrl: "/js/pages/plan-gestion-page/templates/accion4.html",
                controller: "accion4Controller",
                controllerAs: "accion4ControllerCtrl"
            })
            .state("implementacionSeguimiento@home", {
                url: "/implementacion-seguimiento-home",
                templateUrl: "/js/pages/implementacion-seguimiento-page/templates/home.html",
                controller: "homeImplementacionSeguimientoController",
                controllerAs: "homeImplementacionSeguimientoCtrl"
            })

    })