$(document).ready(function () {
    RegionProvinciaComuna($("input#ComunaId").val()).then();
    CargarEdificiosEnDDL($("input#ComunaId").val()).then();
    CargarInstitucionesEnDDL().then(); 
    CargarServicioEnDDL().then();
});


async function CargarEdificiosEnDDL(comunaId) {
    let edificioResponse;
    try {
        edificioResponse = await $.get({
            url: apiEdificio + "/getByComunaId/" + comunaId
        });

        CargarDDL($("#EdificioId"), $("input#EdificioId").val(), edificioResponse);
    } catch (e) {
        alert("error - " + e);
    }
}

async function CargarInstitucionesEnDDL() {
    let institucionesResponse;

    try {

        institucionesResponse = await $.get({
            url: apiInstituciones
        });

        CargarDDL($("#InstitucionId"), $("input#InstitucionId").val(), institucionesResponse);

    } catch (e) {

    }
}

async function CargarServicioEnDDL() {
    let servicioResponse;

    try {

        servicioResponse = await $.get({
            url: apiServicios + "/getByInstitucionId/" + $("input#InstitucionId").val()
        });

        CargarDDL($("#ServicioId"), $("input#ServicioId").val(), servicioResponse);

    } catch (e) {

    }
}

async function CargarTipoDeUsoEnDDL() {
    let servicioResponse;

    try {

        servicioResponse = await $.get({
            url: apitipo + "/getByInstitucionId/" + $("input#InstitucionId").val()
        });

        CargarDDL($("#ServicioId"), $("input#ServicioId").val(), servicioResponse);

    } catch (e) {

    }
}