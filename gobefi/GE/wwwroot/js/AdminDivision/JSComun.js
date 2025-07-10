async function ObtenerNuevoEdificio(urlApi, edificioId) {
    let response;

    try {

        response = await $.get({
            url: urlApi + "/getForSelect/" + edificioId,
            cache: false
        });

        let edificioInfo = await $.get({
            url: urlApi + "/" + edificioId,
            cache: false
        });

        if (edificioInfo.comunaId == $("#SelEdiComunaId").val()) {
            $("#SelEdiEdificioId").append(new Option(response.nombre, response.id));
            MySort("SelEdiEdificioId");
            $("select#SelEdiEdificioId").val(edificioId);
            $("input#EdificioId").val(edificioId);
        } else {
            $("#SelEdiProvinciaId").val("").attr("disabled", "disabled");
            $("#SelEdiEdificioId").val("").attr("disabled", "disabled");
            $("#SelEdiComunaId").val("").attr("disabled", "disabled");
            $("#SelEdiRegionId").val("")
        }

        return response;
    } catch (e) {
        alert("Error al obtener datos del edificio recientemente agregado");
    }
}

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

function fillVehiculos(lstServiciosCtrl, lstVehiculosId) {
    var listVehiculos = $("#" + lstVehiculosId);
    listVehiculos.empty();

    var selectedServicio = lstServiciosCtrl.options[lstServiciosCtrl.selectedIndex].value;
    //console.log(selectedServicio);
    if (selectedServicio != null && selectedServicio != '') {
        $.getJSON("/AdminDivision/GetVehiculosJson", { servicioId: selectedServicio }, function (vehiculos) {
            //console.log(vehiculos);
            if (vehiculos != null && !jQuery.isEmptyObject(vehiculos)) {
                 //console.log(vehiculos);
                $.each(vehiculos, function (index, vehiculo) {
                    listVehiculos.append($('<option/>',
                        {
                            value: vehiculo.id,
                            text: vehiculo.patente
                        }));
                });
            }
            return;
        });
    }

}