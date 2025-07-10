var apiCompras = ''; 
var apiDivision = '';
var apiArchivoAdjunto = '';
var apiEnergeticos = '';
var apiNumClientes = '';
var apiMedidores = '';
var apiMenu = '';
var apiReportes = '';
var apiRegion = '';
var apiProvincia = '';
var apiComuna = '';
var apiEdificio = '';
var apiServicios = '';
var apiInstituciones = '';
var apiUnidadesDeMedidas = '';
var apiParametroMedicion = '';

$(document).ready(function () {
    apiConfiguration();
});

function apiConfiguration() {
    $.ajax({
        type: "GET",
        url: "/Consumo/GetConfigurationValue",
        async: false,
        //data: {
        //        sectionName = "MySection",
        //        paramName = "MyParameter"
        //}
    }).done(function (data) {
        apiCompras = data._apiConfiguration.apiCompras;
        apiDivision = data._apiConfiguration.apiDivision;
        apiArchivoAdjunto = data._apiConfiguration.apiArchivoAdjunto;
        apiEnergeticos = data._apiConfiguration.apiEnergeticos;
        apiEstadoValidacion = data._apiConfiguration.apiEstadoValidacion;
        apiNumClientes = data._apiConfiguration.apiNumClientes;
        apiMedidores = data._apiConfiguration.apiMedidores;
        apiMenu = data._apiConfiguration.apiMenu;
        apiReportes = data._apiConfiguration.apiReportes;
        apiRegion = data._apiConfiguration.apiRegion;
        apiProvincia = data._apiConfiguration.apiProvincia;
        apiComuna = data._apiConfiguration.apiComuna;
        apiEdificio = data._apiConfiguration.apiEdificio;
        apiServicios = data._apiConfiguration.apiServicios;
        apiInstituciones = data._apiConfiguration.apiInstituciones;
        apiUnidadesDeMedidas = data._apiConfiguration.apiUnidadesDeMedidas;
        apiParametroMedicion = data._apiConfiguration.apiParametroMedicion;
    });
}