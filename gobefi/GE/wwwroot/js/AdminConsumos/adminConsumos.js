$(document).ready(function () {
    loadSelectDataAsync($("#RegionId"), apiRegion + "/getregiones", "", "", true);
    loadSelectDataAsync($("#InstitucionId"), apiInstituciones, "", "", true, true).then(function (){

        if ($("#InstitucionId option").length > 1){
            let value = $("#InstitucionId").find("option").eq(1).val();
            $("#InstitucionId").val(value).change();
            //CargarTabla();
        }
    });
    loadSelectDataAsync($("#EstadoId"), apiEstadoValidacion + "/getestados", "", "", true);
});

$("#frmBuscarCompras").submit(function (e) {
    e.preventDefault();

    CargarTabla();
});

$("#frmBuscarId").submit(function (e) {
    e.preventDefault();

    CargarTabla();
});

//Defino los controles como variables globales
var $InstitucionId = $('#InstitucionId');
var $ServicioId = $('#ServicioId');
var $RegionId = $('#RegionId');
var $EdificioId = $('#EdificioId');
var $UnidadId = $('#UnidadId');
var $EnergeticoId = $('#EnergeticoId');
var $NumClienteId = $('#NumClienteId');
var $NumMedidorId = $('#NumMedidorId');
var $NombreId = $("#NombreId");
var $UnidadPmgId = $("#UnidadPmgId");
var $EstadoId = $('#EstadoId');
var $CompraId = $("#CompraId");

async function CargarTabla2() {

   
    let compraId = $("#CompraIdBuscar").val();
    

    let hasFilters = compraId.length;
    if (hasFilters <= 0) {
        $("#MsjeSinFiltros").show();
        $("#divTblValidarConsumos").html("");
        return;
    }

    $("#divTblValidarConsumos").html("");
    $('#cover-spin').show();
    //$("#loader").show();
    $("#MsjeSinFiltros").hide();

    let responseCompras;
    try {
        responseCompras = await $.get({
            url: apiCompras + "/getComprasParaValidar",
            data: {
                compraId: compraId,
            }
        });

        let tbl = $("<table>", {
            id: "tblValidarConsumos",
            style: "font-size: 0.7em",
            class: "table table-striped"
        });

        let trHead = $("<tr>");
        trHead.append($("<th>").html("Id"));
        trHead.append($("<th>").html("Inicio de Lectura"));
        trHead.append($("<th>").html("Unidad"));
        trHead.append($("<th>").html("Energético"));
        trHead.append($("<th>").html("N° de Cliente"));
        trHead.append($("<th>").html("Revisado Por"));
        trHead.append($("<th>", { stryle: "text-align: center;" }).html("Estado"));

        let thead = $("<thead>").append(trHead);
        let tbody = $("<tbody>");

        tbl.append(thead);


        $.each(responseCompras, function (i, compra) {
            let newRow = $("<tr>");
            newRow.append($("<td>").text(compra.id));
            newRow.append($("<td>").text(compra.inicioDeLectura));
            newRow.append($("<td>").text(compra.unidad));
            newRow.append($("<td>").text(compra.energetico));
            newRow.append($("<td>").text(compra.numCliente));
            newRow.append($("<td>").text(compra.revisadoPor));

            let colorButton;
            switch (compra.estadoId) {
                case "ok":
                    colorButton = "success";
                    break;
                case "sin_revision":
                    colorButton = "secondary";
                    break;
                case "observado":
                    colorButton = "danger";
                    break;
            }

            newRow.append($("<td>", { style: "text-align: center;" }).append($("<button/>", {
                text: compra.estado,
                click: function () { ModalMostrarCompra($(this).attr("compraId")); },
                compraId: compra.id,
                class: "btn btn-" + colorButton + " btn-sm",
                style: "width: 100%; font-size: 12px;"
            }).attr("data-toggle", "modal").attr("data-target", "#ModalCompras")));

            tbody.append(newRow);
        });

        tbl.append(tbody);
        $("#divTblValidarConsumos").html(tbl);

        configurarDataTable($("#tblValidarConsumos"), false, false, "");

    } catch (e) {
        console.log(e);
    } finally {
        $('#cover-spin').hide();
        //$("#loader").hide();
    }
}


async function CargarTabla() {

    let institucionId = parseInt($("#InstitucionId").val()) || "";
    let servicioId = parseInt($("#ServicioId").val()) || "";
    let regionId = parseInt($("#RegionId").val()) || "";
    let edificioId = parseInt($("#EdificioId").val()) || "";
    let divisionId = $("#UnidadId").val();
    let energeticoId = $("#EnergeticoId").val();
    let numClienteId = $("#NumClienteId").val();
    let numMedidorId = $("#NumMedidorId").val();
    let compraId = $("#CompraId").val();
    let unidadPmgIdVal = $UnidadPmgId.val();
    let estadoId = $EstadoId.val();

    let hasFilters = parseInt(institucionId) || 0  + parseInt(servicioId) || 0 + parseInt(regionId) || 0 + parseInt(edificioId) || 0 + compraId.length;
    if (hasFilters <= 0) {
        $("#MsjeSinFiltros").show();
        $("#divTblValidarConsumos").html("");
        return;
    }

    $("#divTblValidarConsumos").html("");
    $('#cover-spin').show();
    //$("#loader").show();
    $("#MsjeSinFiltros").hide();

    let responseCompras;
    try {
        responseCompras = await $.get({
            url: apiCompras + "/getComprasParaValidar",
            data: {
                institucionId: institucionId,
                servicioId: servicioId,
                regionId: regionId,
                edificioId: edificioId,
                divisionId: divisionId,
                energeticoId: energeticoId,
                numClienteId: numClienteId,
                numMedidorId: numMedidorId,
                compraId: compraId,
                unidadPmgId: unidadPmgIdVal == 'on' ? true : false,
                estadoId: estadoId
            }
        });

        let tbl = $("<table>", {
            id: "tblValidarConsumos",
            style: "font-size: 0.7em",
            class: "table table-striped"
        });

        let trHead = $("<tr>");
        trHead.append($("<th>").html("Id"));
        trHead.append($("<th>").html("Inicio de Lectura"));
        trHead.append($("<th>").html("Unidad"));
        trHead.append($("<th>").html("Energético"));
        trHead.append($("<th>").html("N° de Cliente"));
        trHead.append($("<th>").html("Revisado Por"));
        trHead.append($("<th>", { stryle: "text-align: center;" }).html("Estado"));

        let thead = $("<thead>").append(trHead);
        let tbody = $("<tbody>");

        tbl.append(thead);


        $.each(responseCompras, function (i, compra) {
            let newRow = $("<tr>");
            newRow.append($("<td>").text(compra.id));
            newRow.append($("<td>").text(compra.inicioDeLectura));
            newRow.append($("<td>").text(compra.unidad));
            newRow.append($("<td>").text(compra.energetico));
            newRow.append($("<td>").text(compra.numCliente));
            newRow.append($("<td>").text(compra.revisadoPor));

            let colorButton;
            switch (compra.estadoId)
            {
                case "ok":
                    colorButton = "success";
                    break;
                case "sin_revision":
                    colorButton = "secondary";
                    break;
                case "observado":
                    colorButton = "danger";
                    break;
            }

            newRow.append($("<td>", { style: "text-align: center;" }).append($("<button/>", {
                text: compra.estado,
                click: function () { ModalMostrarCompra($(this).attr("compraId")); },
                compraId: compra.id,
                class: "btn btn-" + colorButton + " btn-sm",
                style: "width: 100%; font-size: 12px;"
            }).attr("data-toggle", "modal").attr("data-target", "#ModalCompras") ));

            tbody.append(newRow);
        });

        tbl.append(tbody);
        $("#divTblValidarConsumos").html(tbl);

        configurarDataTable($("#tblValidarConsumos"), false, false, "");

    } catch (e) {
        console.log(e);
    } finally {
        $('#cover-spin').hide();
        //$("#loader").hide();
    }
}

function SetearFecha(campoId, dato) {
    let fecha = dato.split(' ')[0];
    $("input[id='" + campoId + "']").val(fecha).flatpickr({
        "allowInput": true,
        "maxDate": new Date(),
        "minDate": "1900-01-01",
        "locale": "es",
        "dateFormat": "d-m-Y",
        //"mode": "single",
        "defaultDate": fecha
    });
}

async function ModalMostrarCompra(idCompra) {
    let response;
    try {
        $("#ModalLongTitle").html("Revisar Compra");
        $(".modal-footer #btnAcciones").empty();
        $("#consumoCompra").parent().parent().parent().remove();
        $('#frmCompras fieldset').empty();
        $("#camposDeEdicion").empty();

        response = await $.get({
            url: apiCompras + "/ParaValidar/" + idCompra
        });

        let opt = $("<option>").val(response["energeticoId"]).attr("selected", "selected").html(response["energetico"].nombre);
        $("select[id='energeticoId']").append(opt);
        $("select[id='energeticoId']").attr("disabled", "disabled");

        $("#camposDinamicos").empty();

        if (response["tieneConsumos"]) {
            let energeticos = [ response["energetico"] ];

            crearSeparador($("#camposDinamicos"));
            crearCampoFecha($("#camposDinamicos"), "inicioLectura", "Inicio de lectura:");
            SetearFecha("inicioLectura", response["inicioLectura"]);
            $("#inicioLectura").attr("disabled", "disabled");

            crearCampoFecha($("#camposDinamicos"), "finLectura", "Fin de lectura:");
            SetearFecha("finLectura", response["finLectura"]);
            $("#finLectura").attr("disabled", "disabled");

            crearCampoSelect($("#camposDinamicos"), "numeroClienteId", "Número de cliente:");
            crearSeparador($("#camposDinamicos"));

            let opt = $("<option>").html(response["numeroCliente"]).val(response["numeroClienteId"]).attr("selected", "selected");
            $("#numeroClienteId").append(opt).attr("disabled", "disabled");
            SetConsumos(response["id"], energeticos).then(function () {
                $.each(response["listaMedidores"], function (item, valor) {
                    //$("select[id='medidorId" + item + "']").val(valor["medidorId"]);
                    //$("select[name='medidorId'] option[value=" + valor["medidorId"] + "]").parent().val(valor["medidorId"]);
                    $("input[medidorId='" + valor["medidorId"] + "']").val(valor["consumo"]).attr("disabled", "disabled");

                    $("select[medidorId='" + valor["medidorId"] + "'][id^='unidadMedidaId']").append("<option>" + valor["unidadMedida"] + "</option>").attr("disabled", "disabled");
                    $("select[medidorId='" + valor["medidorId"] + "']").change();

                    // checkear por que en algunos casos el dato no se llena siendo necesario este if 22/07/2019
                    if ($("label[for^='UnidadMedida" + valor["medidorId"] + "']").html() == "undefined") {
                        $("label[for^='UnidadMedida" + valor["medidorId"] + "']").text(valor["abrv"]);
                    }
                    
                });

                $("input[name='consumo']").attr("disabled", "disabled");
            });

        } else {
            crearSeparador($("#camposDinamicos"));
            crearCampoFecha($("#camposDinamicos"), "fechaCompra", "Fecha de compra:");
            SetearFecha("fechaCompra", response["fechaCompra"])
            $("#fechaCompra").attr("disabled", "disabled");
            crearSeparador($("#camposDinamicos"));

            //CrearCampoConsumoCompra();
            campoConsumo();

            $("#consumoCompra").val(response["consumo"]).attr("disabled", "disabled");

            let opt = $("<option>", { selected: "selected", }).html(response["textUnidadMedida"]);
            $("#unidadMedidaId").append(opt).attr("disabled", "disabled");
        }

        $("input[id='costo']").val(response["costo"]).attr("disabled", "disabled");

        $("input[id='factura']").attr("disabled", "disabled").next().html("<small>Seleccione un Archivo...</small>");
        $("label[for='factura']").next().children("a").remove();
        let urlFactura = apiArchivoAdjunto + "/getByFacturaId/";
        $("label[for='factura']").next().append("<a href='" + urlFactura + response["facturaId"] + "' factura-id='" + response["facturaId"] + "'><i class='fa fa-file-pdf-o' aria-hidden='true'></i> ver factura</a>")

        $("textarea[id='observacion']").val(response["observacion"]).attr("disabled", "disabled");
        $("input[id='createdAt']").val(new Date(response["createdAt"]).toLocaleString("es-CL"));


        crearCampoTexto($("#camposDeEdicion"), "ultimaActualizacion", "Ultima actualización:", true);
        crearCampoTexto($("#camposDeEdicion"), "modificadoPor", "Modificado por:", true);
        crearCampoTexto($("#camposDeEdicion"), "revisadoPor", "Revisado por:", true);
        crearCampoTexto($("#camposDeEdicion"), "revisadoEn", "Revisado en:", true);
        crearCampoTexto($("#camposDeEdicion"), "estado", "Estado:", true);
        crearCampoTextoArea($("#camposDeEdicion"), "observacionVal", "Observación revisión:", false);

        $("input[id='ultimaActualizacion']").val(new Date(response["updatedAt"]).toLocaleString("es-CL"));
        $("input[id='modificadoPor']").val(response["modifiedBy"]);
        $("input[id='revisadoPor']").val(response["revisadoPor"]);
        if (response["reviewedAt"]) {
            $("input[id='revisadoEn']").val(new Date(response["reviewedAt"]).toLocaleString("es-CL"));
        } else {
            $("input[id='revisadoEn']").val("");
        }
       
        $("input[id='estado']").val(response["estadoValidacion"]);

        $("textarea[id='observacionVal']").val(response["observacionRevision"]);

        let btnObservar = $("<button>", {
            class: "btn btn-danger mr-2",
            compraId: response["id"],
            accion: "o",
            click: function () { accionEstado($(this).attr("compraId"), $(this).attr("accion")) }
        }).html("Observar");

        let btnValidar = $("<button>", {
            class: "btn btn-success",
            compraId: response["id"],
            accion: "v",
            click: function () { accionEstado($(this).attr("compraId"), $(this).attr("accion")) }

        }).html("Validar");

        $(".modal-footer #btnAcciones").append(btnObservar).append(btnValidar);

    } catch (e) {
        $('#frmComprasAlert').html("Error al levantar el modal de compras \r\n" + e)
            .removeClass("alert-success")
            .addClass("alert-danger")
            .show();
    }
}


async function accionEstado(compraId, accion) {
    
    let response;
    try {

        response = await $.post({
            url: apiCompras + "/" + compraId + "/accion/" + accion,
            data: {obs : $("#observacionVal").val()}
        });

        let btnColor;
        let btnTexto;

        switch (accion) {
            case "o":
                btnColor = "btn-danger";
                btnTexto = "Observado";
                break;
            case "v":
                btnColor = "btn-success"
                btnTexto = "Ok";
                break;
            default: break;
        }

        let btn = $("#tblValidarConsumos button[compraId='" + compraId + "']");

        if (btn.hasClass("btn-secondary")) {
            btn.toggleClass("btn-secondary " + btnColor);
        }

        if (btn.hasClass("btn-danger")) {
            btn.toggleClass("btn-danger " + btnColor);
        }

        if (btn.hasClass("btn-success")) {
            btn.toggleClass("btn-success " + btnColor);
        }

        btn.html(btnTexto);
        btn.parent().prev().html(response);

        $('#ModalCompras').modal('toggle');


    } catch (e) {

    }

}

async function getMedidoresByCompraId(compraId) {
    let response;
    try {

        response = await $.get({
            url: apiMedidores + "/ByCompraId/" + compraId,
        })


        return response;
    } catch (e) {
        $('#frmComprasAlert').html("Error al obtener datos de los medidores")
            .removeClass("alert-success")
            .addClass("alert-danger")
            .show();
    }

}

async function SetConsumos(compraId, energeticos) {

    $("#listaMedidores").remove();
    let fieldSet = $("<fieldset>", { id: "listaMedidores" });// document.createElement("fieldset");
    //fieldSet.id = "listaMedidores";
    $("label[for=numeroClienteId]").parent().next().after(fieldSet);

    //let dataMedidores = division.medidores;
    let dataMedidores = await getMedidoresByCompraId(compraId); //response; // medidores;

    let indexMedidorCompra = 0;
    $.each(dataMedidores, function (key, item) {

        CrearCamposMedidores(indexMedidorCompra, item, energeticos);

        ++indexMedidorCompra;
    });
  
}

function campoConsumo() {
    $("label[for='consumoCompra']").parent().remove();

    let rowDiv = $("<div>", {
        class: "form-group row"
    });

    rowDiv.append($("<label>", {
        class: "col-sm-4 col-form-label"
    }).html("Consumo:"));

    let inputConsumo = $("<input />", {
        type: "text",
        class: "form-control",
        id: "consumoCompra"
    });
    let selectUnidadMedida = $("<select>", {
        class: "form-control custom-select",
        id: "unidadMedidaId"
    });

    let selectDiv = $("<div>", { class: "input-group-prepend" });
    selectDiv.append(selectUnidadMedida);

    let inputsDiv = $("<div>", { class: "col-sm-6" });

    let inputs = $("<div>", { class: "input-group" }).append(inputConsumo).append(selectDiv);

    inputsDiv.append(inputs);

    rowDiv.append(inputsDiv);

    $("label[for=costo]").parent().after(rowDiv);
}

//function ResetSelect(select) {
//    select.val("").attr("disabled", "disabled").change();
//}

$("#InstitucionId").change(function () {

    if ($(this).val() == "") {
        $("#ServicioId").val("").attr("disabled", "disabled").change();
        $CompraId.val("");
        return;
    }
    loadSelectDataAsync($("#ServicioId"), apiServicios + "/getByInstitucionId/" + $(this).val(), "", "", true, "").then(function () {
        if ($("#ServicioId option").length > 1) {
            let value = $("#ServicioId").find("option").eq(1).val();
            $("#ServicioId").val(value).change();
        }

        $UnidadId.val("").attr('disabled', 'disabled');//.change();
        $EstadoId.val("");
        //CargarTabla();

    });
    
});


$ServicioId.change(function () {
    if ($(this).val() == "") {
        $("#RegionId").val("").change();
        $("#UnidadId").val("").attr("disabled", "disabled").change();
        $NombreId.val("").attr("disabled", "disabled").change();
        $CompraId.val("");
        return;
    }

    let regionId = parseInt($("#RegionId").val());
    if (!$.isNumeric(regionId)){
        regionId = "0";
    }
    let pmg = $('#UnidadPmgId').prop('checked');
    loadSelectDataAsync($("#UnidadId"), apiDivision + "/GetByServicioId/" + $(this).val() + "/ByRegionId/" + regionId + "/byPmg/" + pmg, "", "", true, "", "direccion");
    $EstadoId.val('');
    loadSelectDataAsync($NombreId, apiDivision + "/GetByServicioId/" + $(this).val() + "/ByRegionId/" + regionId + "/byPmg/" + pmg, "", "", true, "")
        .then(function () {
            CargarTabla();
        });
    $EnergeticoId.val('').attr('disabled', 'disabled').change();
});

$UnidadId.change(function () {

    if ($(this).val() == "" || $(this).val() == null) {
        $EnergeticoId.val("").attr("disabled", "disabled").change();
        $CompraId.val("");
        return;
    }

    $NombreId.val($UnidadId.val());

    //ResetSelect($NumClienteId);
    loadSelectDataAsync($("#EnergeticoId"), apiEnergeticos + "/GetEnergeticosActivos/" + $(this).val(), "", "", true)
        .then(function () {
            $EstadoId.val('');
            CargarTabla();
        });
    $NumClienteId.val('').attr('disabled', 'disabled').change();
});

$EnergeticoId.change(function () {
    if ($(this).val() == "") {
        $NumClienteId.val("").attr("disabled", "disabled").change();
        $CompraId.val("");
        return;
    }

    let unidadId = parseInt($UnidadId.val());
    let edificioId = parseInt($EdificioId.val());
    if (!$.isNumeric(unidadId) && !$.isNumeric(edificioId)) {
        return;
    }

    if ($.isNumeric(unidadId)) {
        loadSelectDataAsync($("#NumClienteId"), apiNumClientes + "/byDivisionId/" + unidadId + "/byEnergeticoId/" + $(this).val(), "", "", true)
            .then(function () {
                $EstadoId.val('');
                CargarTabla();
            });
    } else {
        loadSelectDataAsync($NumClienteId, apiNumClientes + "/byEdificioId/" + edificioId + "/byEnergeticoId/" + $(this).val(), "", "", true)
            .then(function () {
                $EstadoId.val('');
                CargarTabla();
            });
    }

    $NumMedidorId.val('').attr('disabled', 'disabled').change();

});

$NumClienteId.change(function () {
    if ($(this).val() == "") {
        $("#NumMedidorId").val("").attr("disabled", "disabled").change();
        $CompraId.val("");
        return;
    }

    loadSelectDataAsync($("#NumMedidorId"), apiMedidores + "/ByNumeroCliente/" + $(this).val(), "", "", true).
        then(function () {
            $EstadoId.val('');
            CargarTabla();
        });
});

$NumMedidorId.change(function () {

    if ($(this).val() == "") {
        $CompraId.val("");
        return;
    }
    $EstadoId.val('');
    CargarTabla();

})

$RegionId.change(function () {
    if ($(this).val() == "") {
        $("#EdificioId").val("").attr("disabled", "disabled").change();
        $CompraId.val("");
        return;
    }
    $NombreId.val('').attr("disabled", "disabled").change();
    loadSelectDataAsync($("#EdificioId"), apiEdificio + "/getByRegionId/" + $(this).val(), "", "", true)
        .then(function () {
            $EstadoId.val('');
            CargarTabla();
        });


    //loadSelectDataAsync($("#UnidadId"), apiDivision + "/GetByServicioId/" + $("#ServicioId").val() + "/ByRegionId/" + $(this).val(), "", "", true);
});

$EdificioId.change(function (){
    if ($(this).val() == "") {
        $("#EnergeticoId").val("").attr("disabled", "disabled").change();
        $NombreId.val('').attr("disabled", "disabled").change();
        $UnidadId.val('').attr("disabled", "disabled").change();
        $CompraId.val("");
        return;
    }
    $NumClienteId.val('').attr("disabled", "disabled").change();
    loadSelectDataAsync($("#EnergeticoId"), apiEnergeticos + "/GetByEdificioId/" + $(this).val(), "", "", true)
    loadSelectDataAsync($NombreId, apiDivision + "/getByEdificioId/" + $(this).val(), "", "", true, "");
    loadSelectDataAsync($("#UnidadId"), apiDivision + "/getByEdificioId/" + $(this).val(), "", "", true, "", "direccion").then(function () {
        $EstadoId.val('');
        CargarTabla();
    });
    loadSelectDataAsync($NombreId, apiDivision + "/getByEdificioId/" + $(this).val(), "", "", true, "")
    .then(function () {
        CargarTabla();
    });

});

$NombreId.change(function () {
    $UnidadId.val($NombreId.val());
   
    if ($(this).val() == "") {
        $("#EnergeticoId").val("").attr("disabled", "disabled").change();
        $CompraId.val("");
        return;
    }

    //ResetSelect($NumClienteId);
    loadSelectDataAsync($("#EnergeticoId"), apiEnergeticos + "/GetEnergeticosActivos/" + $(this).val(), "", "", true)
        .then(function () {
            $EstadoId.val('');
            CargarTabla();
        });
    $NumClienteId.val('').attr('disabled', 'disabled').change();
});

$UnidadPmgId.change(function () {
    if ($(this).val() == 'on') {
        $(this).val('off')
    } else {
        $(this).val('on')
    }
    let regionId = parseInt($("#RegionId").val());
    if (!$.isNumeric(regionId)) {
        regionId = "0";
    }
    let pmg = $(this).prop('checked');
    let servicioId = $ServicioId.val();
    if (!$.isNumeric(servicioId)) {

        servicioId = 0;
    }
    loadSelectDataAsync($NombreId, apiDivision + "/GetByServicioId/" + servicioId + "/ByRegionId/" + regionId + "/byPmg/" + pmg, "", "", true, "");
    loadSelectDataAsync($("#UnidadId"), apiDivision + "/GetByServicioId/" + servicioId + "/ByRegionId/" + regionId + "/byPmg/" + pmg, "", "", true, "", "direccion")
        .then(function () {
            $EstadoId.val('');
            CargarTabla();
        })
});

$EstadoId.change(function () {

    if ($(this).val() == "") {
        return;
    }
    CargarTabla();
});


