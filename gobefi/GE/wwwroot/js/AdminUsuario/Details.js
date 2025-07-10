$(document).ready(function () {

    CargarProvinciaComuna($("#RegionId").val()).then();

    loadSelectDataAsync($("#InstitucionesId"), apiInstituciones + "/getAsociadosByUserId/" + $("input#Id").val(), "", "").then(
        loadSelectData($("#ServiciosId"), apiServicios + "/getAsociadosByUserId/" + $("input#Id").val(), "", "",false)
    );

});

async function CargarProvinciaComuna(regionId) {
    let response;
    try {

        response = await $.get({
            url: apiProvincia + "/getByRegionId/" + regionId
        });

        let options = "";

        $.each(response, function (index, item) {
            options += "<option " + (item["id"] == $("input#ProvinciaId").val() ? 'selected' : '') + " value='" + item.id + "'" + "title='" + item.nombre + "'" + ">" + item.nombre + "</option>";
        });

        $("select#ProvinciaId").append(options);

        response = await $.get({
            url: apiComuna + "/getByProvinciaId/" + $("input#ProvinciaId").val()
        });

        options = "";

        $.each(response, function (index, item) {
            options += "<option " + (item["id"] == $("input#ComunaId").val() ? 'selected' : '') + " value='" + item.id + "'" + "title='" + item.nombre + "'" + ">" + item.nombre + "</option>";
        });

        $("select#ComunaId").append(options);

    } catch (e) {
        alert("error " + e);
    }

}
