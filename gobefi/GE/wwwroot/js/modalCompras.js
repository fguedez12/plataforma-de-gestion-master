/**
 * PAGINA DONDE SE OBTIENE EJEMPLO
 * https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-2.1
 * */


//let energeticos = null;
let medidores = null;
//let division = null;
//let numClientes = null;
//let divisionId = null;
let parametrosDeMedicion = null;



$(document).ready(function () {
    
    divisionId = localStorage.getItem("divisionId");

    getParametroMedicion();
});


function getParametroMedicion() {
    $.ajax({
        type: 'GET',
        url: apiParametroMedicion,
        success: function (data) {

            parametrosDeMedicion = data;

        }, error: function (err) {
            console.log(err);

            $('#frmComprasAlert').html("Error al obtener datos de los parametros de medicion para electricidad")
                .removeClass("alert-success")
                .addClass("alert-danger")
                .show();
        }
    });
}

async function GetNumerosCliente() {
    let response;

    try {
        $("#numeroClienteId").attr("disabled", "disabled");

        response = await $.get({
            url: apiNumClientes + "/byDivisionId/" + divisionId + "/byEnergeticoId/" + $("select#energeticoId").val(),
        });

        llenarSelect($("#numeroClienteId"), response);

        $("#numeroClienteId").removeAttr("disabled");

    } catch (e) {

    }   
}

function getMedidores(numeroClienteId) {
    $.ajax({
        type: 'GET',
        url: apiMedidores + "/ParaCompra/ByNumClienteId/" + numeroClienteId + "/ByDivisionId/" + localStorage.getItem("divisionId"),
        async: false,
        success: function (data) {
            medidores = data;

        }, error: function (err) {
            console.log(err);

            $('#frmComprasAlert').html("Error al obtener datos de los medidores")
                        .removeClass( "alert-success" )
                        .addClass( "alert-danger" )
                        .show();
        }
    });
}

/*
 * Muestra los numeros de clientes de la division 
 * */
function getMedidoresByNumCliente(numeroClienteId) {
    if (numeroClienteId){
        //let tieneNumCliente = consumos.subMenu.energeticoSubMenu.find(i => i.id == $("#energeticoId").val()).tieneNumCliente;
        let tieneNumCliente = energeticos.find(i => i.id == $("#energeticoId").val()).tieneNumCliente;
        if (!tieneNumCliente){
            return false;
        }
    }
    

    $('#medidorId').empty();
    getMedidores(numeroClienteId);
    //medidores = GetMedidores(numeroClienteId).then();

    //se agrega el valor "-- seleccionar --"
    $('#medidorId').append($('<option>', {
        value: '',
        text: $('#selectVal').val()
    }));


    $("#listaMedidores").remove();
    let fieldSet = document.createElement("fieldset");
    fieldSet.id = "listaMedidores";
    $("label[for=numeroClienteId]").parent().next().after(fieldSet);

    //let dataMedidores = division.medidores;
    let dataMedidores = medidores;
    let options = "";

    let indexMedidorCompra = 0;
    $.each(dataMedidores, function (key, item) {

        //Medidor corresponde al numero de cliente?
        if (item.numeroClienteId != numeroClienteId) {
            return true;
        }

        CrearCamposMedidores(indexMedidorCompra, item, energeticos);

        // options += "<option value=" + item.id + ">" + item.numero + "</option>";
        ++indexMedidorCompra;
    });

    // $(".select-medidores").append(options);
}

/**
 * Crea un fieldset en el formulario de compras, donde agrega el cuerpo para agregar un medidor 
 * y generar fila para el modelo compraMedidor
 */
function CrearCamposMedidores(index, item, energeticos){
    let attrMedidorId = document.createAttribute("medidorId");
    attrMedidorId.value = item.id;


    let rowMedidor = document.createElement("div");
    rowMedidor.id = "medidor" + index;
    
    
    //fila
    let rowDiv = document.createElement("div");        
    rowDiv.className = "form-group row";

    //label numero de medidor
    let lblNumeroMedidor = document.createElement("label");
    lblNumeroMedidor.className = "col-sm-3 col-form-label";
    lblNumeroMedidor.setAttribute("for", "medidorId" + item.id);
    let ltNumMedidor = document.createTextNode("Número de Medidor:");
    lblNumeroMedidor.appendChild(ltNumMedidor);

    let divSelectNumMedidor = document.createElement("div");
    divSelectNumMedidor.className = "col-sm-3";

    // select de medidores
    let selectNumMedidor = document.createElement("select");
    selectNumMedidor.className = "form-control custom-select select-medidores";
    selectNumMedidor.id = "medidorId" + index;
    let option = document.createElement("option");
    option.text = item.numero; // $('#selectVal').val();
    option.value = item.id // "";
    selectNumMedidor.add(option);
    selectNumMedidor.name = "medidorId";
    selectNumMedidor.disabled = true;
    selectNumMedidor.setAttributeNode(attrMedidorId);

    divSelectNumMedidor.appendChild(selectNumMedidor);


    let lblTipoMedidor;
    let divSelectTipoMedidor;
    if ($('#energeticoId').val() == "3"){ // Electricidad
        //label tipo de medidor
        lblTipoMedidor = document.createElement("label");
        lblTipoMedidor.className = "col-sm-2 col-form-label";
        lblTipoMedidor.setAttribute("for","parametroMedicionId" + index);
        let ltTipoMedidor = document.createTextNode("Tipo Medidor:");
        lblTipoMedidor.appendChild(ltTipoMedidor);

        divSelectTipoMedidor = document.createElement("div");
        divSelectTipoMedidor.className = "col-sm-4";

        // select de tipo medidor
        let selectTipoMedidor = document.createElement("select");
        selectTipoMedidor.className = "form-control custom-select mostrar-unidad-medida";
        selectTipoMedidor.id = "parametroMedicionId" + index;

        selectTipoMedidor.setAttribute("medidorId", item.id)
        //selectTipoMedidor.setAttributeNode(attrMedidorId);

        option = document.createElement("option");
        option.text = $('#selectVal').val();
        option.value = "";
        option.setAttribute("data-item", "");
        selectTipoMedidor.add(option);
        selectTipoMedidor.name = "parametroMedicionId";

        //$.each(consumos.parametrosMedicion, function(i, parametroMedicion){
        $.each(parametrosDeMedicion, function (i, parametroMedicion) {
            let newOption = document.createElement("option");
            newOption.text = parametroMedicion.nombre;
            newOption.value = parametroMedicion.id;
            newOption.setAttribute("data-item", parametroMedicion.abrvUnidadMedida);
            selectTipoMedidor.add(newOption);
        });

        

        divSelectTipoMedidor.appendChild(selectTipoMedidor);
        selectTipoMedidor.options[1].selected = true;

        // rowDiv.appendChild(lblTipoMedidor);
        // rowDiv.appendChild(divSelectTipoMedidor);
        
    } else {
        // if ($("#unidadMedidaId").length == 0){
        //     let numeroClienteField = document.querySelector("label[for='numeroClienteId'").parentNode;

        //     createSelectElement(numeroClienteField, energeticos.find(e => e.id == $('#energeticoId').val()).unidadesMedida, "unidadMedidaId");
        // }

        // // Unidad de Medida
        lblTipoMedidor = document.createElement("label");
        lblTipoMedidor.className = "col-sm-2 col-form-label";
        lblTipoMedidor.setAttribute("for","unidadMedidaId" + index);
        let tnUnidadMedida = document.createTextNode("Unidad de Medida:");
        
        lblTipoMedidor.appendChild(tnUnidadMedida);

        divSelectTipoMedidor = document.createElement("div");
        divSelectTipoMedidor.className = "col-sm-4";

        // select de unidades de medida
        let selectUMedida = document.createElement("select");
        selectUMedida.className = "form-control custom-select mostrar-unidad-medida";
        selectUMedida.id = "unidadMedidaId" + index;
        //selectUMedida.setAttributeNode(attrMedidorId);
        selectUMedida.setAttribute("medidorId", item.id)

        selectUMedida.name = "unidadMedidaId";

        $.each(energeticos.find(e => e.id == $('#energeticoId').val()).unidadesMedida, function(i, uMedida){
            let newOption = document.createElement("option");
            newOption.text = uMedida.nombre;
            newOption.value = uMedida.id;
            newOption.setAttribute("data-item", uMedida.abreviacion);
            selectUMedida.add(newOption);
        }); 
        
        divSelectTipoMedidor.appendChild(selectUMedida);
    }

    rowDiv.appendChild(lblNumeroMedidor);
    rowDiv.appendChild(divSelectNumMedidor);
    rowDiv.appendChild(lblTipoMedidor);
    rowDiv.appendChild(divSelectTipoMedidor);

    let row2Div = document.createElement("div");        
    row2Div.className = "form-group row";

    //label numero de medidor
    let lblCantidad = document.createElement("label");
    lblCantidad.className = "col-sm-3 col-form-label";
    lblCantidad.setAttribute("for", "consumo" + index);
    let ltCantidad = document.createTextNode("Consumo");
    lblCantidad.appendChild(ltCantidad);

    let divInputTextCantidad = document.createElement("div");
    divInputTextCantidad.className = "col-3";

    let inputTextcantidad = document.createElement("input");
    inputTextcantidad.type = "text";
    inputTextcantidad.className = "form-control";
    inputTextcantidad.id = "consumo" + index;
    inputTextcantidad.name = "consumo";
    //inputTextcantidad.setAttributeNode(attrMedidorId);
    inputTextcantidad.setAttribute("medidorId", item.id)
    inputTextcantidad.setAttribute("ownmedidorid", item.divisionId)
    inputTextcantidad.setAttribute("autocomplete","off");
    divInputTextCantidad.appendChild(inputTextcantidad);

    //label unidad medida
    let lblUnidadMedida = document.createElement("label");
    lblUnidadMedida.className = "col-4 col-form-label";
    lblUnidadMedida.setAttribute("for", "UnidadMedida" + item.id);
    let ltUnidadMedida = document.createTextNode("");
    lblUnidadMedida.appendChild(ltUnidadMedida);

    let hrElement = document.createElement("hr");

    row2Div.appendChild(lblCantidad);
    row2Div.appendChild(divInputTextCantidad);
    row2Div.appendChild(lblUnidadMedida);
    
    rowMedidor.appendChild(rowDiv);
    rowMedidor.appendChild(row2Div);
    
    
    let fsMedidores = document.getElementById("listaMedidores");
    fsMedidores.appendChild(rowMedidor);
    fsMedidores.appendChild(hrElement);

    $( "select[id^='parametroMedicionId']" ).trigger("change").attr('disabled', 'disabled');
}

function createSelectElement(divAnterior, opciones, stringId){
    var myDiv = divAnterior;

    // Unidad de Medida
    titulo = document.createElement("label");
    titulo.className = "col-sm-4 col-form-label";
    titulo.setAttribute("for", stringId);
    let textTitulo = document.createTextNode("Unidad de Medida:");
    
    titulo.appendChild(textTitulo);

    div = document.createElement("div");
    div.className = "form-group row";

    div.appendChild(titulo);

    //Create and append select list
    var selectList = document.createElement("select");
    
    selectList.id = stringId;
    selectList.className = "form-control custom-select mostrar-unidad-medida";
    option = document.createElement("option");
    option.text = $('#selectVal').val();
    option.value = "";
    option.setAttribute("data-item", "");
    selectList.add(option);
    selectList.name = stringId;

    divSelect = document.createElement("div");
    divSelect.className = "col-sm-6";

    divSelect.appendChild(selectList);
    div.appendChild(divSelect);

    myDiv.after(div);

    //Create and append the options
    $.each(opciones, function(i, item){
        let option = document.createElement("option");
        option.text = item.nombre;
        option.value = item.id;
        option.setAttribute("data-item", item.abreviacion);
        selectList.add(option);
    }); 
}


// Metodo que limpia el formulario
function LimpiarFormulario() {
    $("label[for='factura']").children().html("Seleccione un Archivo...");
    $("#listaMedidores").remove();
    let energetico = $("#energeticoId").val();
    $("#frmCompras")[0].reset();
    $("#energeticoId").val(energetico);
}


function ObtenerDatosFormulario(obj){
    //let elements = Array.from($('#frmCompras')[0].elements).filter(e => !e.getAttribute("name") && e.value !== "");
    let elements = Array.from($('#frmCompras')[0].elements).filter(e => !e.classList.contains("d-none"));
    for( var i = 0; i < elements.length; ++i ) {
        let element = elements[i];
        let name = element.id;
        let value = element.value;

        if (name === "listaMedidores"){
            let medidores = [];
            let childs = {};
            let newElements = element.elements;
            let medidorAnterior;
            
            for (let j = 0; j < newElements.length; ++j) {
                let newElement = newElements[j];
                let medidorActual = newElement.getAttribute("medidorId");

                if ($("input[medidorId='" + medidorActual + "'").val() == "") {
                    continue;
                }

                if (medidorActual !== medidorAnterior && !objetoEstaVacio(childs)) {
                    medidores.push(childs);
                    childs = {};
                }
                
                if (newElement !== undefined){

                    let newName = newElement.name;
                    let newValue = newElement.value;
    
                    if( newName && newElement.value) {
                        childs[newName] = newValue;

                        medidorAnterior = medidorActual;
                    }
                }
            }

            if (childs.consumo){
                medidores.push(childs);
            }

            value = medidores;
        }

        if (name === "factura"){
            name = undefined;
        }

        if (value === "" || (Array.isArray(value) && value.length == 0)){
            value = null;
        }

        if(name) {

            if (name === 'createdAt') {
                obj[name] = moment(value, 'YYYY-MM-DD HH:mm');
            } else {
                obj[name] = value;
            }
        }
    }
    obj["divisionId"] = divisionId;

    if ($("#unidadMedidaId").val() !== undefined){
        obj["unidadMedidaId"] = $("#unidadMedidaId").val();
    } else {
        obj["unidadMedidaId"] = null;
    }
}



/**
 * Agrega una nueva compra
 * */
function addNuevaCompra() {

    //if ($("#frmCompras .invalid-feedback").length > 0)
    //    return;

    let obj = {};
    ObtenerDatosFormulario(obj);
    obj.sinMedidor = $("#sinMedidor").prop("checked");

    // subir factura al servidor
    subirArchivo(obj);
    
    // guardar compra en base de datos
    // console.log( JSON.stringify(obj));
    $.ajax({
        type: 'POST',
        accepts: 'application/json',
        url: apiCompras,
        contentType: 'application/json',
        data: JSON.stringify(obj),
        beforeSend: function () {

            $('button[type=submit]').addClass('disabled');
            $("#frmComprasAlert").hide();
        },
        error: function (jqXHR, textStatus, errorThrown) {
            MostrarErroresCompras(jqXHR, textStatus, errorThrown);
        },
        success: function (result) {

            // consumo.js/ 
            // todo: redirigir a la pestaña del energetico que se agrego la compra
            getComprasByDivision(result.energeticoId,anio);

            //$("a[data-target='#ModalCompras']").hide();
            $("#ModalCompras").modal('toggle');

            //LimpiarFormulario();
        },
        complete: function (data) {
            $('button[type=submit]').removeClass('disabled');
        }
        
    });

    $('#factura').val('');
}

function MostrarErroresCompras(jqXHR, textStatus, errorThrown) {
    console.log('Error: ' + errorThrown + " | " + textStatus + " | " + jqXHR.responseText);
    $("#loader").hide();

    if (IsJsonString(jqXHR.responseText)) {
        $(".is-invalid").removeClass("is-invalid");
        $(".invalid-feedback").remove();

        let jsonError = jQuery.parseJSON(jqXHR.responseText);

        $.each(jsonError, function (nodo, text) {
            if (nodo.indexOf('.') > 0)
                nodo = nodo.split('.')[0].toLowerCase();

            if (nodo[0] === nodo[0].toUpperCase())
                nodo = nodo.charAt(0).toLowerCase() + nodo.slice(1);

            if (nodo == "facturaId") {
                nodo="factura"
            }

            if ($('#' + nodo).attr("type") == "date" || $('#' + nodo).hasClass("flatpickr-input")) {
                $('#' + nodo).addClass('is-invalid').next().after("<div class='invalid-feedback d-block'>" + text + "</div>");
            } else if (nodo == "listaMedidores") {
                $('#' + nodo).before("<div class='invalid-feedback d-block'>" + text + "</div>");
                $("input[id^='consumo']").addClass('is-invalid');
            }
            else {
                $('#' + nodo).addClass('is-invalid').after("<div class='invalid-feedback d-block'>" + text + "</div>");
            }


        });

        //ya que numero cliente y medidores se muestran solo si se escoje algun energetico especifico se realiza aparte la validacion
        if ($('#numeroClienteId').val() == "") {
            $('#numeroClienteId').addClass('is-invalid');
        }
        if ($('#medidorId').val() == "") {
            $('#medidorId').addClass('is-invalid');
        }
    }
    //else {
    //    if (jqXHR.status >= 300) {
    //        $('#frmComprasAlert').html(jqXHR.responseText)
    //            .removeClass("alert-success")
    //            .addClass("alert-danger")
    //            .show();
    //    }
    //}
}

function IsJsonString(str) {
    try {
        JSON.parse(str);
    } catch (e) {
        return false;
    }
    return true;
}

function subirArchivo(obj){
    var formData = new FormData();
    formData.append('archivo', $('#factura')[0].files[0]);

    var _url = apiArchivoAdjunto + "/division/" + divisionId;  //.replace("{divisionId}", divisionId); // "https://localhost:5001/api/compras/" + divisionId + "/archivoAdjunto";
    // https://localhost:5001/api/archivoAdjunto/division/
    $.ajax({
        url: _url,
        type: 'POST',
        data: formData,
        processData: false,  // tell jQuery not to process the data
        contentType: false,  // tell jQuery not to set contentType
        async: false,
        success: function (result) {
            obj["facturaId"] = result.newId;
        },
        error: function (jqXHR, textStatus, errorThrown) {
            MostrarErroresCompras(jqXHR, textStatus, errorThrown);
        },
        complete: function (jqXHR, status) {
        }
    });
}

function ActualizarArchivo(facturaId) {

    let obj = {};
    ObtenerDatosFormulario(obj);
    obj.sinMedidor = $("#sinMedidor").prop("checked");
    obj["facturaId"] = $("a[factura-id]").attr('factura-id');

    var formData = new FormData();
    formData.append('archivo', $('#factura')[0].files[0]);
    formData.append('id', facturaId);


    var _url = apiArchivoAdjunto + "/" + facturaId + "/division/" + divisionId;  //.replace("{divisionId}", divisionId); // "https://localhost:5001/api/compras/" + divisionId + "/archivoAdjunto";
    

    $.ajax({
        url: _url,
        type: 'PUT',
        data: formData,
        processData: false,  // tell jQuery not to process the data
        contentType: false,  // tell jQuery not to set contentType
        async: false,
        success: function () {
            console.log("archivo actualizado");


            $.ajax({
                url: apiCompras + '/' + $('#id').val(),
                type: 'PUT',
                accepts: 'application/json',
                contentType: 'application/json',
                data: JSON.stringify(obj),
                success: function (result) {
                    //getDivision();

                    $('#frmComprasAlert').html("Compra actualizada satisfactoriamente").show();

                    // consumo.js/ 
                    // redirigir a la pestaña del energetico que se agrego la compra
                    var anio = $("#selFiltro").val();
                    getComprasByDivision(null,anio);
                    CargarTablaSegunEnergetico($(".nav-subDetalle.active").attr("data-id"));


                    if ($("#btnAccion").attr("type") == "submit") {
                        $("#ModalCompras").modal('toggle');

                    }

                },
                error: function (jqXHR, textStatus, errorThrown) {
                    MostrarErroresCompras(jqXHR, textStatus, errorThrown);
                },
            });

        },
        error: function (jqXHR, textStatus, errorThrown) {
            MostrarErroresCompras(jqXHR, textStatus, errorThrown);
        },
        complete: function (jqXHR, status) {
        }
    });
}

/**
 * COMO EJEMPLO
 * @param {any} id
 */
function deleteItem(id) {
    $.ajax({
        url: apiCompras + '/' + id,
        type: 'DELETE',
        success: function (result) {
            //getDivision();
        }
    });
}

/**
 * EVENTOS
 */


//$('#frmCompras').on('submit', function () {
//$("body").on('submit', "#frmCompras", function () {
function ActualizarCompra() {
    ActualizarArchivo($("a[factura-id]").attr('factura-id'));
    $('#factura').val('');
    return false;
};

async function ModalActualizarParaCompra(idCompra) {
    let response;
    try {

        response = await $.get({
            url: apiCompras + "/" + idCompra
        });

        $("select[id='energeticoId']").val(response["energeticoId"]);
        $("select[id='energeticoId']").change();
        $("#frmCompras input#id").attr("owner-id", response["createdByDivisionId"] );


        await GetNumerosCliente();

        $("select[id='numeroClienteId']").val(response["numeroClienteId"]);
        if (!response.sinMedidor) {
            $("select[id='numeroClienteId']").change();
        } else {
            $("#sinMedidor").prop("checked", response.sinMedidor);
            $("#sinMedidor").change();
        }
        
        let fechaInicio = response["inicioLectura"].split(' ')[0];
        $("input[id='inicioLectura']").val(fechaInicio).flatpickr({
            "allowInput": true,
            "maxDate": new Date(),
            "minDate": "1900-01-01",
            "locale": "es",
            "dateFormat": "d-m-Y",
            //"mode": "single",
            "defaultDate": fechaInicio
        });

        let fechaFin = response["finLectura"].split(' ')[0];
        $("input[id='finLectura']").val(fechaFin).flatpickr({
            "allowInput": true,
            "maxDate": new Date(),
            "minDate": "1900-01-01",
            "locale": "es",
            "dateFormat": "d-m-Y",
            //"mode": "single",
            "defaultDate": fechaFin
        });

        let fechaCompra = response["fechaCompra"].split(' ')[0]
        $("input[id='fechaCompra']").val(fechaCompra).flatpickr({
            "allowInput": true,
            "maxDate": new Date(),
            "minDate": "1900-01-01",
            "locale": "es",
            "dateFormat": "d-m-Y",
            //"mode": "single",
            "defaultDate": fechaCompra
        });



        $.each(response["listaMedidores"], function (item, valor) {
            //$("select[id='medidorId" + item + "']").val(valor["medidorId"]);
            //$("select[name='medidorId'] option[value=" + valor["medidorId"] + "]").parent().val(valor["medidorId"]);
            $("input[medidorId='" + valor["medidorId"] + "']").val(valor["consumo"]);
            
            $("select[medidorId='" + valor["medidorId"] + "'][id^='unidadMedidaId']").val(valor["unidadMedidaId"]);
            $("select[medidorId='" + valor["medidorId"] + "']").change();
        });

        $("input[id='consumoCompra']").val(response["consumo"]);
        $("span[id='textUnidadMedida']").html(response["textUnidadMedida"]);

        $("select[id='unidadMedidaId']").val(response["unidadMedidaId"]);
        $("select[id='unidadMedidaId']").change();

        $("input[id='costo']").val(response["costo"]);

        $("input[id='factura']").next().html("<small>Seleccione un Archivo...</small>");
        $("label[for='factura']").next().children("a").remove();
        var urlFactura = apiArchivoAdjunto + "/getByFacturaId/";
        $("label[for='factura']").next().append("<a href='" + urlFactura + response["facturaId"] + "' factura-id='" + response["facturaId"] + "'><i class='fa fa-file-pdf-o' aria-hidden='true'></i> ver factura</a>")

        $("textarea[id='observacion']").val(response["observacion"]);
        $("input[id='createdAt']").val(new Date(response["createdAt"]).toLocaleString("es-CL"));

        $("input[id='ultimaActualizacion']").val(new Date(response["updatedAt"]).toLocaleString("es-CL"));
        $("input[id='modificadoPor']").val(response["modifiedBy"]);
        $("input[id='Estado']").val(response["estadoValidacion"]);

        $("textarea[id='observacionRevision']").val(response["observacionRevision"]);
        let obsval = response["observacionRevision"]

        if (obsval == null) {
            $("textarea[id='observacionRevision']").parent().parent().attr("hidden", "hidden");
        }

        if (response["estadoValidacion"] == "Ok") {
            $("#lblEstado").html("validada");
            $("button[id='btnAccion']").removeAttr("form").removeAttr("type").attr("type", "button").attr("data-toggle", "modal").attr("data-target", "#modalConfirm");
        } else if (response["estadoValidacion"] == "Observado") {
            $("#lblEstado").html("observada");
            $("button[id='btnAccion']").removeAttr("form").removeAttr("type").attr("type", "button").attr("data-toggle", "modal").attr("data-target", "#modalConfirm");

        } else {
            $("button[id='btnAccion']").removeAttr("type").removeAttr("data-toggle").removeAttr("data-target").attr("form", "frmCompras").attr("type", "submit");
        }

    } catch (e) {
        $('#frmComprasAlert').html("Error al levantar el modal de compras \r\n" + e)
                        .removeClass( "alert-success" )
                        .addClass( "alert-danger" )
                        .show();
    }
}

$(document).ready(function () {
    $("#btnGuardarCambios").click(function () {
        $("#frmCompras").submit();
        $("#ModalCompras, #modalConfirm").modal('toggle');

    });

    $("body").on('click', "#btnEliminarCompra", function () {
        eliminar=confirm("¿Deseas eliminar este registro?");
        if (eliminar){
            $.ajax({
                url: apiCompras + "/" + $(this).attr("id-compra"),
                type: 'DELETE',
                success: function(result) {
                    //getDivision();

                    // consumo.js/ 
                    getComprasByDivision(null,anio);

                    if ($('input[type=radio][name=options]:checked').val() == "consumos"){
                        CargarTablaSegunEnergetico($(".nav-subDetalle.active").attr("data-id"));
                    } else if ($('input[type=radio][name=options]:checked').val() == "boletas"){
                        CargarTablaBoletas();
                    }

                    //$("a[data-target='#ModalCompras']").hide();
                    $("#ModalCompras").modal('toggle');
                },
                error: function(error) {
                    alert("eliminado incompleta");
                    console.log(error);
                    $('#frmComprasAlert').html("Error al eliminar la compra")
                        .removeClass( "alert-success" )
                        .addClass( "alert-danger" )
                        .show();
                }
            });
        }
    });

    //Accion de modificar
    $("body").on('click', "a[data-target='#ModalCompras']", function () {
        //let idCompra = $(this).children().children().attr("id-compra");
        let idCompra = $(this).attr("id-compra");

        $('.modal-title').html("Actualizar Compra");
        $("button[form='frmCompras']").html("<i class='fa fa-pencil-square-o' aria-hidden='true'></i> Actualizar Compra");
        $("#frmCompras").removeAttr("onsubmit").attr("onsubmit", "ActualizarCompra()");
        $("#frmCompras input#id").val(idCompra);
        $('#frmComprasAlert').hide();
        $("#btnEliminarCompra").removeClass("d-none");

        $("#camposDeEdicion").empty();
        crearCampoTexto($("#camposDeEdicion"), "ultimaActualizacion", "Ultima actualización:",true);
        crearCampoTexto($("#camposDeEdicion"), "modificadoPor", "Modificado por:", true);
        crearCampoTexto($("#camposDeEdicion"), "Estado", "Estado:", true);
        crearCampoTextoArea($("#camposDeEdicion"), "observacionRevision", "Observación Revisión:", true);
        

        //$("#btnEliminarCompra").remove();
        //var btnEliminar = "<button type='button' class='btn btn-danger' id='btnEliminarCompra' id-compra='" + idCompra + "'><i class='fa fa-trash-o' aria-hidden='true'></i> Eliminar</button>";
        $("#btnEliminarCompra").attr("id-compra", idCompra);
        //$(".modal-footer").append(btnEliminar);

        ModalActualizarParaCompra(idCompra).then();

    });


    

    $('body').on('change', "#finLectura", function () {

        let fechaSeleccionada = new Date($(this).val());

        //if (fechaSeleccionada > new Date()) {
        //    $("#finLectura").addClass("is-invalid").next().after("<div class='invalid-feedback d-block'>No puede ser mayor a la fecha actual.</div>")
        //} else {
        //    $("#finLectura").removeClass("is-invalid").next().next().remove();
        //}
    });

    $('body').on( 'change', ".mostrar-unidad-medida", function() {
        let thisChage = this;

        $.each($("label[for*='UnidadMedida']"), function(key, label){
            label.innerText =  $(thisChage).find('option:selected').attr("data-item");
        });

        
        $.each($("select[id*=unidadMedidaId]"), function(key, select){
            select.value = $(thisChage).find('option:selected').val();
        });

    });

    $("#sinMedidor").change(function () {
        console.log(this.checked);
        $("#camposDinamicos").empty();
        if (this.checked) {
            compraSinMedidor();
        } else {
            compraConMedididor();
        }
    });

    $("#energeticoId").on('change', function () {
        
        let tieneNumCliente = false;
        if ($(this).val()) {
            if ($(this).val() == "3" || $(this).val() == "2") {
                tieneNumCliente = true;
            } else {
                tieneNumCliente = energeticos.find(i => i.id == $(this).val()).tieneNumCliente;
                if (tieneNumCliente && $(this).val() == "1") {
                    $("#div-sin-medidor").removeAttr("hidden");
                } else {
                   
                }
            }

        } else {
            $("#div-sin-medidor").attr("hidden", true);
        }
        $("#camposDinamicos").empty();

        if (tieneNumCliente) {

            compraConMedididor();
             
            
        } else {
            compraSinMedidor();
        }

    });

    function compraConMedididor() {
        $('#div-costo-total').show();
        $('#disclaimer-sin-medidor').hide();
        $("#consumoCompra").parent().parent().parent().remove();
        $('#frmCompras fieldset').empty();

        crearSeparador($("#camposDinamicos"));
        crearCampoFecha($("#camposDinamicos"), "inicioLectura", "Inicio de lectura:");
        crearCampoFecha($("#camposDinamicos"), "finLectura", "Fin de lectura:");
        crearCampoSelect($("#camposDinamicos"), "numeroClienteId", "Número de cliente:");
        crearSeparador($("#camposDinamicos"));

        let creandoCompra = $("#frmCompras").attr("onsubmit") != "ActualizarCompra()";
        //getNumeroClientes(async);
        GetNumerosCliente().then();
    }

    function compraSinMedidor() {
        crearSeparador($("#camposDinamicos"));
        crearCampoFecha($("#camposDinamicos"), "fechaCompra", "Fecha de compra:");
        crearSeparador($("#camposDinamicos"));
        // $('#fechaCompra').attr('disabled', false);
        // $('#inicioLectura, #finLectura, #numeroClienteId').attr('disabled', true);
        // $('#inicioLectura, #finLectura, #numeroClienteId').parent().parent().remove();
        // $('#fechaCompra').parent().parent().removeClass('d-none');
        // $('#fechaCompra').removeClass('d-none');
        // $("#frmCompras #listaMedidores").remove();

        //cambio para ingesar solo los datos necesarios para las compras sin medidor HL:2022-09-12
        CrearCampoConsumoCompra();
            //$('#div-costo-total').hide();
            //$('#disclaimer-sin-medidor').show();
    }

    $('body').on('change', "input[type='date']", function () {
        $(this).removeClass("is-invalid").next().next().remove();
    });

    $('body').on('change', "#numeroClienteId", function () {

        console.log(divisionId);

        $(this).removeClass("is-invalid").next().remove();

        $('#frmCompras fieldset').empty();

        getMedidoresByNumCliente($(this).val());

        let energeticoId = $("#energeticoId").val();

        let isAdmin = $("#hfIsAdmin").val().toLowerCase();

       

        if ($(this).val() != "" && isAdmin=='false') {
            validaNumeroByMedidor(divisionId, energeticoId, $(this).val());
            validaOwner(divisionId);
        }

        $('#medidorId').prop('disabled', false);

    });

    function validaOwner(divisionId)
    {
        let ownerId = $("#frmCompras input#id").attr("owner-id");
        let compraId = $("#frmCompras input#id").val();
        if (compraId == 0) {
            $("#frmCompras [ownmedidorid]").attr("disabled", "disabled");
            $("#frmCompras [ownmedidorid=" + divisionId + "]").removeAttr("disabled");
        } else {
            if (divisionId != ownerId) {
                $("#frmCompras select").attr("disabled", "disabled");
                $("#frmCompras input").attr("disabled", "disabled");
                $("#frmCompras textarea").attr("disabled", "disabled");
                $("#frmCompras [ownmedidorid=" + divisionId + "]").removeAttr("disabled");
                $("#btnEliminarCompra").attr("hidden", "hidden");
            } else {
                $("#frmCompras select").removeAttr("disabled");
                $("#frmCompras input").removeAttr("disabled");
                $("#frmCompras textarea").removeAttr("disabled");
                $("#frmCompras [ownmedidorid]").attr("disabled", "disabled");
                $("#frmCompras [ownmedidorid=" + divisionId + "]").removeAttr("disabled");
                $("#frmCompras select").attr("disabled", "disabled");
                $("#frmCompras .select-medidores").attr("disabled", "disabled");
                $("#frmCompras .mostrar-unidad-medida").attr("disabled", "disabled");
                $("#frmCompras #createdAt").attr("disabled", "disabled");
                $("#frmCompras #ultimaActualizacion").attr("disabled", "disabled");
                $("#frmCompras #modificadoPor").attr("disabled", "disabled");
                $("#frmCompras #Estado").attr("disabled", "disabled");
                $("#btnEliminarCompra").removeAttr("hidden");
            }
        }

        $("#frmCompras #observacionRevision").attr("disabled", "disabled");
       
    }

    $('#ModalCompras').on('hidden.bs.modal', function () {
        $("#frmCompras select").removeAttr("disabled");
        $("#frmCompras input").removeAttr("disabled");
        $("#frmCompras textarea").removeAttr("disabled");
        $("#btnEliminarCompra").removeAttr("hidden");
        $(".is-invalid").removeClass("is-invalid");
        $(".invalid-feedback").remove();
    })

    async function validaNumeroByMedidor(divisionId, energeticoId, numeroCliente) {

        response = await $.get({
            url: apiNumClientes + "/validaNumeroByMedidor/" + divisionId + "/byEnergetico/" + energeticoId + "/byNumeroCliente/" + numeroCliente
        });

        console.log(response);

        if (response==false) {

            $('#frmComprasAlert').html("Numero de cliente con medidor compartido - solo lectura")
                .removeClass("alert-success")
                .addClass("alert-warning")
                .show();
            $("#btnAccion").hide();
            $("#btnEliminarCompra").hide();
        } else {

            $('#frmComprasAlert')
                .hide();
            $("#btnAccion").show();
            $("#btnEliminarCompra").show();
            $("#btnEliminarCompra").show();
        }
    };

    $('.modal').on('hidden.bs.modal', function () {
        $("#btnAccion").show();
    });

    $('.custom-file-input').on('change', function () {

        let fileName = $(this).val().split('\\').pop();

        if (String(fileName).length > 40)
            fileName = String(fileName).substring(0, 25).trim() + '.....' + String(fileName).substring(fileName.length - 15, fileName.length).trim();

        $(this).parent().children("label").children().html(fileName);
    });

    $('input').change(function () {
        $(this).removeClass('is-invalid');
        $(this).closest('.form-group').find('label.error-message').remove();
        $(this).parent().parent().find(".invalid-feedback").remove();
    });

    $('select').change(function () {
        $(this).removeClass('is-invalid');
        $(this).closest('.form-group').find('label.error-message').remove();
    });

    //$('#btnModalCompras').click(function () {
    $("body").on('click', "#btnModalCompras", function () {

        $("#camposDinamicos").empty();

        crearSeparador($("#camposDinamicos"));
        crearCampoFecha($("#camposDinamicos"), "fechaCompra", "Fecha de compra:");
        crearSeparador($("#camposDinamicos"));

        $("#camposDeEdicion").empty();

        $("button[id='btnAccion']").attr("form", "frmCompras").attr("type", "submit").removeAttr("type").removeAttr("data-toggle").removeAttr("data-target");
        $("label[for='factura']").next().children("a").remove();
        $('.modal-title').html("Agregar Compra");
        $("button[form='frmCompras']").html("Agregar Compra");
        $('#frmComprasAlert').hide();
        $("#frmCompras input#id").val("0");
        $("#frmCompras")[0].reset();
        $("#frmCompras").attr("onsubmit", "addNuevaCompra()");
        $("label.custom-file-label[for='factura'").children().text("Seleccione un Archivo...");
        $("#btnEliminarCompra").addClass("d-none");
        $(".is-invalid").removeClass("is-invalid");
        $(".invalid-feedback").remove();

        

        //if ($('#energeticoId > option').length == 2) {
        //    let energeticoId = $('#energeticoId > option')[1].value;
        //    $('#energeticoId').val(energeticoId).change().attr("disabled", "disabled");
        //    //MostrarCamposSegunEnergetico(energeticoId);
        //} else {
        //    $('#frmCompras fieldset').empty();
        //    $('#frmCompras')[0].reset();
        //    $('#fechaCompra, #inicioLectura, #finLectura, #numeroClienteId').attr('disabled', true);
        //    $("#frmComprasAlert").hide();
        //}
    });

});


function crearSeparador(dentroDe) {
    let hrElement = document.createElement("hr");
    dentroDe.append(hrElement);
}

function crearCampoTextoArea(dentroDe, nombreCampo, nombre, disabled = false) {
    let mainDiv = document.createElement("div");
    mainDiv.className = "form-group row";

    let lblName = document.createElement("label");
    lblName.className = "col-sm-4 col-form-label";
    lblName.setAttribute("for", nombreCampo);
    lblName.innerText = nombre;



    let divCajaTexto = document.createElement("div");
    divCajaTexto.className = "col-sm-6";

    let cajaTexto = document.createElement("textarea");
    cajaTexto.className = "form-control";
    cajaTexto.rows = 3;
    cajaTexto.id = nombreCampo;
    if (disabled) {
        cajaTexto.setAttribute("disabled", disabled);
    } else {
        cajaTexto.removeAttribute("disabled");
    }
 


    divCajaTexto.appendChild(cajaTexto);

    mainDiv.appendChild(lblName);
    mainDiv.appendChild(divCajaTexto);

    dentroDe.append(mainDiv);

}

function crearCampoTexto(dentroDe, nombreCampo, nombre, disabled = false) {
    let mainDiv = document.createElement("div");
    mainDiv.className = "form-group row";

    let lblName = document.createElement("label");
    lblName.className = "col-sm-4 col-form-label";
    lblName.setAttribute("for", nombreCampo);
    lblName.innerText = nombre;

    

    let divCajaTexto = document.createElement("div");
    divCajaTexto.className = "col-sm-6";

    let cajaTexto = document.createElement("input");
    cajaTexto.className = "form-control";
    cajaTexto.id = nombreCampo;
    cajaTexto.type = "text";
    cajaTexto.setAttribute("disabled", disabled);
    

    divCajaTexto.appendChild(cajaTexto);

    mainDiv.appendChild(lblName);
    mainDiv.appendChild(divCajaTexto);

    dentroDe.append(mainDiv);

}

function crearCampoSelect(dentroDe, nombreCampo, nombre) {
    let mainDiv = document.createElement("div");
    mainDiv.className = "form-group row";

    let lblName = document.createElement("label");
    lblName.className = "col-sm-4 col-form-label";
    lblName.setAttribute("for", nombreCampo);
    lblName.innerText = nombre;

    mainDiv.appendChild(lblName);

    let divSelect = document.createElement("div");
    divSelect.className = "col-sm-6";

    let select = document.createElement("select");
    select.className = "form-control custom-select";
    select.id = nombreCampo;


    var option = document.createElement("option");
    option.text = $('#selectVal').val();
    option.value = "0";
    select.add(option);

    divSelect.appendChild(select);

    mainDiv.appendChild(divSelect);

    dentroDe.append(mainDiv);
}

function llenarSelect(selectElement, opciones) {

    for (var i = 0; i < opciones.length; i++) {
        if (selectElement.children("option[value='" + opciones[i].id + "']").length > 0) {
            return;
        }

        var option = document.createElement("option");
        option.text = opciones[i].numero;
        option.value = opciones[i].id;
        selectElement.append(option);
    }
}

function crearCampoFecha(dentroDe, nombreCampo, nombre, defaultValue) {

    let mainDiv = document.createElement("div");
    mainDiv.className = "form-group row";

    let lblName = document.createElement("label");
    lblName.className = "col-sm-4 col-form-label";
    lblName.setAttribute("for", nombreCampo);
    lblName.innerText = nombre;

    mainDiv.appendChild(lblName);

    let divTextBox = document.createElement("div");
    divTextBox.className = "col-sm-4 input-group mb-3";

    let inputDate = document.createElement("input");
    inputDate.type = "date";
    inputDate.className = "form-control";
    inputDate.id = nombreCampo;
    inputDate.placeholder = "dd-mm-aaaa";
    inputDate.setAttribute("autocomplete", "off")

    

    let divSpan = document.createElement("div");
    divSpan.className = "input-group-append";

    let spanIcon = document.createElement("span");
    spanIcon.className = "input-group-text";

    let icon = document.createElement("i");
    icon.className = "fa fa-calendar";
    icon.setAttribute("aria-hidden", "true");

    divSpan.appendChild(spanIcon);

    spanIcon.appendChild(icon);

    divTextBox.appendChild(inputDate);
    divTextBox.appendChild(divSpan);

    mainDiv.appendChild(divTextBox);

    dentroDe.append(mainDiv);

    const optional_config_calendario = {
        "allowInput": true,
        "maxDate": new Date(),
        "minDate": "1900-01-01",
        "locale": "es",
        "dateFormat": "d-m-Y"//,
        //"mode": "single",
        //"defaultDate": defaultValue //['01-01-2015']
    }

    $("#" + nombreCampo).flatpickr(optional_config_calendario);
}

/**
 * Crea el div con label y caja de texto del ingreso de consumo de la compra para los que tienen y no tienen medidores
 * al detectar que el energetico tiene mas de 1 unidad de medida asociada, se crea un drop down list y se muestra
 * al detectar que tiene solo 1 unidad de medida asociada al energetico, se agrega un span fijo con la abreviacion
 */
function CrearCampoConsumoCompra(){

    //if ($("#consumoCompra").is(":visible")){
        $("label[for='consumoCompra']").parent().remove();
    //}
 
    let divInputConsumoCompra = document.createElement("div");
    divInputConsumoCompra.className = "form-group row";
 
    let lblConsumoCompra = document.createElement("label");
    lblConsumoCompra.className = "col-sm-4 col-form-label";
    lblConsumoCompra.setAttribute("for","consumoCompra");
    let ltnConsumoCompra = document.createTextNode("Consumo: (Opcional)");
    lblConsumoCompra.appendChild(ltnConsumoCompra);

    divInputConsumoCompra.appendChild(lblConsumoCompra);

    let divColumnGroup = document.createElement("div");
    divColumnGroup.className = "col-sm-6";

    let divInputGroup = document.createElement("div");
    divInputGroup.className = "input-group";

    let divInputPrepend = document.createElement("div");
    divInputPrepend.className = "input-group-prepend";

    let inputTextConsumoCompra = document.createElement("input");
    inputTextConsumoCompra.type = "text";
    inputTextConsumoCompra.className = "form-control";
    inputTextConsumoCompra.id = "consumoCompra"
    inputTextConsumoCompra.setAttribute("autocomplete", "off");

  

    let spanUnidadMedida = document.createElement("span");

    if (!$("#energeticoId").val()){
        return false;
    }

    if (energeticos.find(e => e.id == $("#energeticoId").val()).unidadesMedida.length > 1){

        // select de unidades de medida
        let selectUnidadesMedida = document.createElement("select");
        selectUnidadesMedida.className = "form-control custom-select";
        selectUnidadesMedida.id = "unidadMedidaId";
        selectUnidadesMedida.name = "unidadMedidaId";

        $.each(energeticos.find(e => e.id == $('#energeticoId').val()).unidadesMedida, function(i, itemUniMedida){
            let newOption = document.createElement("option");
            newOption.text = itemUniMedida.nombre;
            newOption.value = itemUniMedida.id;
            newOption.setAttribute("data-item", itemUniMedida.abreviacion);
            selectUnidadesMedida.add(newOption);
        });

        divInputPrepend.appendChild(selectUnidadesMedida);
    }
    else {
        let uMedida = energeticos.find(e => e.id == $("#energeticoId").val()).unidadesMedida[0].abreviacion;

        spanUnidadMedida.className = "input-group-text";
        spanUnidadMedida.id = "textUnidadMedida";

        let textUnidadMedida = document.createTextNode(uMedida);
        spanUnidadMedida.appendChild(textUnidadMedida);
    
        divInputPrepend.appendChild(spanUnidadMedida);
    }

    divInputGroup.appendChild(inputTextConsumoCompra);
    divInputGroup.appendChild(divInputPrepend);

    divColumnGroup.appendChild(divInputGroup);

    divInputConsumoCompra.appendChild(divColumnGroup);

    $("label[for=costo]").parent().after(divInputConsumoCompra);
}