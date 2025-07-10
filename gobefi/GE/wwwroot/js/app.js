var app = angular.module("myapp", [
    "disenioApp",
    'adminAjustesApp',
    'adminCertificadosApp',
    'edificioApp',
    'inmuebleApp',
    'adminMedidoresApp',
    'unidadApp',
    'certificadosApp',
    'common',
    'modalSexoApp',
    'miUnidadApp',
    'adminUnidadApp',
    'reporteConsumoApp',
    'medicionInteligenteApp',
    'adminDocumentosApp',
    'adminAlcanceApp',
    'adminPapelApp',
    'adminAguaApp',
    'adminResiduosApp',
    'adminMisServiciosApp',
    'ui.router',
    'pagesModule',
    'componentsModule',
    'adminAgregadaApp',
    'adminColaboradoresApp',
    'actualizacionUnidadApp',
    'adminDiagnosticoApp',
    'sistemasApp',
    'planGestionApp',
    'implementacionSeguimientoApp',
    'localytics.directives'
    ])
    .controller("myappController", function () {

    });

app.factory('BearerAuthInterceptor', function ($window, $q) {
    return {
        request: function (config) {
            config.headers = config.headers || {};
            if ($window.localStorage.getItem('token')) {
                // may also use sessionStorage
                config.headers.Authorization = 'Bearer ' + $window.localStorage.getItem('token');
            } else {
                window.location.href = '/Account/Login';
            }
            return config || $q.when(config);
        },
        //response: function (response) {
        //    if (response.status === 401) {
        //        window.location.href = '/Account/Login';
        //    }
        //    return response || $q.when(response);
        //}
    };
});

app.factory('ErrorInterceptor', function ($q) {
    return {
        'responseError': function (rejection) {
            if (rejection.status === 401) {
                window.location.href = '/Account/Login';
            }
            return $q.reject(rejection);
        }
    };
});

// Register the previously created AuthInterceptor.
app.config(function ($httpProvider) {
    $httpProvider.interceptors.push('BearerAuthInterceptor');
    $httpProvider.interceptors.push('ErrorInterceptor');
});
// Función auxiliar para mostrar notificaciones toast (movida desde consumos.js)
function mostrarToast(titulo, mensaje, tipo) {
    // Crear o reutilizar el contenedor de toasts
    let toastContainer = $('#toast-container');
    if (!toastContainer.length) {
        toastContainer = $('<div id="toast-container" style="position: fixed; top: 20px; right: 20px; z-index: 1050;"></div>');
        $('body').append(toastContainer);
    }

    // Mapear tipos a clases CSS
    const typeClasses = {
        'success': 'bg-success text-white',
        'danger': 'bg-danger text-white',
        'warning': 'bg-warning text-dark',
        'info': 'bg-info text-white'
    };

    const toast = $('<div class="toast-alert" style="min-width: 250px; margin-bottom: 10px; opacity: 0; transition: opacity 0.5s;">')
        .append($('<div class="toast-header ' + (typeClasses[tipo] || 'bg-primary text-white') + '" style="padding: 0.5rem 1rem; border-radius: 0.25rem 0.25rem 0 0;">')
            .append('<strong class="mr-auto">' + titulo + '</strong>')
            .append('<button type="button" class="ml-2 mb-1 close" style="cursor: pointer; background: transparent; border: none; font-size: 1.5rem;">&times;</button>'))
        .append($('<div class="toast-body" style="padding: 1rem; background: white; border-radius: 0 0 0.25rem 0.25rem; box-shadow: 0 0.25rem 0.75rem rgba(0,0,0,0.1);">').text(mensaje));

    toastContainer.append(toast);
    
    // Mostrar con animación
    setTimeout(() => toast.css('opacity', 1), 10);
    
    // Configurar cierre
    toast.find('.close').click(function() {
        toast.css('opacity', 0);
        setTimeout(() => toast.remove(), 500);
    });
    
    // Cierre automático después de 3 segundos
    setTimeout(() => {
        toast.css('opacity', 0);
        setTimeout(() => toast.remove(), 500);
    }, 3000);
}