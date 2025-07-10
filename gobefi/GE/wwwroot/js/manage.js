$(document).ready(function () {
    $("#Rut").rut({ formatOn: 'keyup', validateOn: 'keyup' })
        .on('rutInvalido', function () {
            $(this).next().addClass("d-block").html("Rut invalido");
        })
        .on('rutValido', function (e, rut) {
            if (rut > 1000000) {
                $(this).next().removeClass("d-block");
            }
        });


    //alert($("#RegionId").val());
});

$("#RegionId").change(function () {
    $("#ProvinciaId").html("<option value=''>-- SELECCIONE --</option>");
    $("#ComunaId").html("<option value=''>-- SELECCIONE --</option>");

    if ($(this).val() != "") {
        loadSelectData($("#ProvinciaId"), apiProvincia + "/getByRegionId/" + $(this).val(), "selected");
    } else {
        loadSelectData($("#ProvinciaId"), apiProvincia, "selected");
    }

    
});

$("#ProvinciaId").change(function () {
    $("#ComunaId").html("<option value=''>-- SELECCIONE --</option>");

    if ($(this).val() !== "") {
        loadSelectData($("#ComunaId"), apiComuna + "/getByProvinciaId/" + $(this).val(), "selected");
    } else {
        loadSelectData($("#ProvinciaId"), apiComuna, "selected");
    }

    
});