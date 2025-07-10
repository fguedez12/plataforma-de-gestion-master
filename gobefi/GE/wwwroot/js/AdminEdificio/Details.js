$(document).ready(function () {
    RegionProvinciaComuna($("input#ComunaId").val()).then();

});


async function RegionProvinciaComuna(comunaId) {
    let comunaResponse;
    let regionResponse;
    let provinciaResponse;

    try {

        comunaResponse = await $.get({
            url: apiComuna + "/" + comunaId

        });

        regionResponse = await $.get({
            url: apiRegion + "/getregiones"
        });

        CargarDDL($("select#RegionId"), comunaResponse.regionId, regionResponse);

        provinciaResponse = await $.get({
            url: apiProvincia + "/getByRegionId/" + comunaResponse.regionId
        });

        CargarDDL($("select#ProvinciaId"), comunaResponse.provinciaId, provinciaResponse);

        comunaResponseData = await $.get({
            url: apiComuna + "/getByProvinciaId/" + comunaResponse.provinciaId
        });

        CargarDDL($("select#ComunaId"), comunaResponse.id, comunaResponseData);

    } catch (e) {
        alert("error" + e);
    }

}

function CargarDDL(selectItem, valorSeleccionado, datos) {
    let options = "";

    $.each(datos, function (index, item) {
        options += "<option " + (item["id"] == valorSeleccionado ? 'selected' : '') + " value='" + item.id + "'" + "title='" + item.nombre + "'" + ">" + item.nombre + "</option>";
    });

    selectItem.append(options);
}