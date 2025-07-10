//Accion de modificar
$("body").on('click', "#medidores a[data-target='#ModalMedidor']", function (e) {

    //console.log("abriendo modal para modificar medidor");
    let medidorId = $(this).parent().parent().attr('class'); //obtiene el id de la fila (row class)

    $('#hiddenId').val(medidorId);

    //$('#ModalMedidor #ModalLongTitle').html('Editar Medidor');
    
    $("#ModalMedidor #btnAgregarMedidor").html('Editar Medidor');
    $("#divFrmMedidor h5").html("<strong>Editar Medidor</strong>");
    //$("#msjeModalMedidor").hide();
    $("#tituloModalMedidor").parent().remove();
    $("#divTblMedidores").remove();
    $("hr").remove();
    $("#NumeroClienteId").val("");
    $("#tbNumeroMedidor").val("");
    $("input[name='_inteligente']").attr('checked', false);
    $("input[name='_compartido']").attr('checked', false);

    //BuscarMedidor(medidorId).then();

    ObtenerDatosParaAbrirModal(medidorId).then();
});

$(document).ready(function () {
    //accion de agregar
    $("#medidores button[data-target='#ModalMedidor']").click(function () {

        $("#tituloModalMedidor").parent().remove();
        $("#divTblMedidores").remove();
        $("hr").remove();
        $("#divFrmMedidor h5").html("<strong>Agregar Medidor</strong>");

        //$("#msjeModalMedidor").hide();
        console.log($('#hiddenId').val());
        console.log($('#Id').val());
        $('#hiddenId').val(0);
        //$('#Id').val(0);
        $("#divFrmMedidor #Id").val(0);
          
        $("#frmMedidor")[0].reset();
        $("#frmMedidor span").html('');
        $("#frmMedidor input[type=checkbox]").removeAttr("checked");

        $("#ModalMedidor #btnAgregarMedidor").html('Agregar Medidor');

        ObtenerDatosParaAbrirModal(undefined).then();
    });

    //$("#tbNumeroMedidor").focusout(function () {
    //    ExisteMedidor();
    //});

    //$("#NumeroClienteId").change(function () {
    //    ExisteMedidor();
    //});

    //$("#btnAgregarMedidor").click(function (e) {
    //    alert();
    //    e.preventDefault();

    //    GuardarMedidor().then();
    //});

});

//$("body").on('click', "#ModalNumCliente #btnAgregarMedidor", function (e) {

//    e.preventDefault();
//    GuardarMedidor().then();
//});

async function ObtenerDatosParaAbrirModal(medidorId) {

    let numClientesResponse;
    //let elementos = $("#NumeroClienteId, #tbNumeroMedidor, #btnAgregarMedidor, #frmMedidor input[type='checkbox']");
    let elementos = $("select#NumeroClienteId, input#tbNumeroMedidor, input#btnAgregarMedidor, #frmMedidor input[type='checkbox']");
    $("#NumeroClienteId").html("");
    try {

        elementos.attr("disabled", "disabled");
        $("#NumeroClienteId").append(new Option("-- SELECCIONE --", ""));

        numClientesResponse = await $.post({
            url: "/miunidad/ListClientes"
        });

        //console.log(numClientesResponse);
        
        $.each(numClientesResponse, function (index, numCliente) {
            $("#NumeroClienteId").append(new Option(numCliente.numero, numCliente.id));
        });

        if (medidorId !== undefined) {
            await BuscarMedidor(medidorId);
        }
        
        elementos.removeAttr("disabled");
    } catch (e) {
        console.log("Error: " + e);
    }
}


async function BuscarMedidor(medidorId) {
    let data;
    try {

        data = await $.get({
            url: "/miunidad/GetMedidor",
            data: { id: medidorId },
            contentType: 'application/json; charset=utf-8',
            datatype: 'json'
        });

        //console.log('medidor',data);
        $("#frmMedidor #Id").val(data.id);
        $("input#tbNumeroMedidor").val(data.numero);
        $("select#NumeroClienteId").val(data.numeroCliente.id);
        $("input#tbFases").val(data.numFases);
        $("input#tbMedicion").val("falta");
        $("input#tbFlujoMedicion").val(data.flujoMedicion);

        $("input[name='_inteligente']").attr('checked', data._inteligente);
        $("input[name='_compartido']").attr('checked', data._compartido);
        $("input[name='_factura']").attr('checked', data._factura);

    } catch (e) {
        console.log(e);
    }
}

async function GuardarMedidor() {
    
    
    let response;
    try {
        

        $("#btnAgregarMedidor").button("loading");
        //$("#msjeModalMedidor").hide();
        //$("#msjeModalMedidor").removeClass("alert-success").removeClass("alert-danger");
        //$("#msjeModalMedidor ul").remove();
        //$("#msjeModalMedidor p").empty();

        response = await $.post({
            url: $("#frmMedidor").attr("action"),
            data: $("#frmMedidor").serialize()
        });
        //.done(function (response) {
        //    //$("#msjeModalMedidor").addClass("alert-success").show().children("p").html("El nuevo medidor se ha creado satisfactoriamente.");
        //}
        //).fail(function (xhr, status, error) {
        //    //console.log(xhr.responseText);
           
        //    //$("#msjeModalMedidor").show().children("p").html(xhr.responseText);
        //});
        
        $("#divTblMedidores").html(response);

        //let numMedidor = $("#tbNumeroMedidor").val();

        let msgResult = "El nuevo medidor se ha creado satisfactoriamente.";

        if ($("#divFrmMedidor #Id").val() != 0) {
            msgResult = "Medidor actualizado satisfactoriamente.";
        } else {
            $("#frmMedidor")[0].reset();
        }

        //$("#msjeModalMedidor p").html(msgResult);
        //$("#msjeModalMedidor").addClass("alert-success").show();

        $("#msjeModalMedidor").html('<div class="alert alert-success fade show"><button type="button" class="close close-alert" data-dismiss="alert" aria-hidden="true">×</button>' + msgResult  + '</div>');
        $("#msjeModalMedidor .alert").delay(200).addClass("in").fadeOut(10000);

        //GetAll();
        BuscarMedidoresAsociados();

    } catch (e) {
        
        
        let ulItem = $("<ul>");
        

        $.each(JSON.parse(e.responseText), function (node, text) {
            
            if (node == "NumeroClienteId"){ // la api retorna "el valor no es valido" por lo cual se cambia por la siguiente glosa
                text = "Numero de cliente es obligatorio";
            }

            let liItem = $("<li>").append(text);

            ulItem.append(liItem);

            
        });

        //$("#msjeModalMedidor").append(ulItem);

        //$("#msjeModalMedidor").addClass("alert-danger").show();

        $("#msjeModalMedidor").html("<div class=\"alert alert-danger fade show\"><button type=\"button\" class=\"close close-alert\" data-dismiss=\"alert\" aria-hidden=\"true\">×</button>" + ulItem.html() + "</div>");
        $("#msjeModalMedidor .alert").delay(200).addClass("in").fadeOut(10000);


    } finally {
        $("#btnAgregarMedidor").button("reset");
        //$("#ModalMedidor").modal("hide");
    }

    return response;

}

function ExisteMedidor() {
    let numMedidor = $("#tbNumeroMedidor").val();
    let numCliente = $("#NumeroClienteId").val();

    if (!numMedidor || !numCliente) {
        return
    }

    //let medidor = FindMedidor(numMedidor, numCliente);

    //console.log(medidor);
}

// queda para segunda etapa buscar medidor existente y que no permita ingresar el mismo medidor para el mismo numero de cliente, mismo energetico, mismo numero de medidor
async function FindMedidor(numMedidor, numClienteId) {
    let response;
    try {


        response = await $.post({
            url: apiMedidores + "/CheckExistMedidor",
            data: JSON.stringify({
                divisionId: localStorage.getItem("divisionId"),
                energeticoId: "5",
                numeroClienteId: numClienteId,
                numeroMedidor: numMedidor
            })
        });

        return response;
    } catch (e) {
        alert(e.responseText);
    }
}