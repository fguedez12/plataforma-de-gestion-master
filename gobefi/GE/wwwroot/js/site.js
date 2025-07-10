// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function (e) {

});

/**
 * 
 * @param {Object} element Elemento DOM al que se agregaran los datos 
 * @param {string} url Url el cual va a buscar los datos
 * @param {string} value Valor que verifica si corresponde o no dejar seleccionado el campo
 * @param {string} field Nombre del Campo al cual se realiza la comparacion para verificar si se encuentra seleccionado 
 * @param {boolean} keepEnable Indica si el elemento se debe habilitar despues de obtener la data
 */
function loadSelectData(element, url, value = null, field = null, keepEnable = true, titulo = "-- SELECCIONE --", withTitle = false) {
    if (field === null) {
        field = 'id';
    }

    $.ajax({
        url: url,
        type: "GET",
        beforeSend: function () {
            element.html("<option value='optionCargando'>Cargando...</option>");
            //element.attr('disabled', 'disabled');
            element.attr("readonly", true);
        },
        success: function (data) {
            let options = "";

            if ('result' in data) {
                $.each(data.result, function (index, item) {
                    options += "<option " + (item[field] === value ? 'selected' : '') + " value='" + item.id + "' " + (withTitle ? item.nombre : "") + ">" + item.nombre + "</option>";
                });
            } else {
                $.each(data, function (index, item) {
                    options += "<option " + (item[field] === value ? 'selected' : '') + " value='" + item.id + "'" + (withTitle ? "title='" + item.nombre + "'" : "") + ">" + item.nombre + "</option>";
                });
            }

            
            if (element.attr("multiple") != "multiple") {
                element.html("<option value=''>" + titulo + "</option>");
            } 

            element.children("option[value='optionCargando']").remove();

            element.append(options);
        },
        error: function (data) {
            console.log("Nope!");
        },
        complete: function () {
            if (keepEnable) {
                element.removeAttr('disabled');
                element.removeAttr("readonly");
            }


            MySort(element.attr("id"));
        }
    });
}

async function loadSelectDataAsync(element, url, value, field, keepEnable, withTitle, fieldName) {
    let data;
    try {
        element.html("<option value='optionCargando'>Cargando...</option>");
        //element.attr('disabled', 'disabled');
        element.attr("readonly", true);

        data = await $.get({
            url: url,
        });

        let options = "";

        if (typeof fieldName =="undefined") {

            fieldName = "nombre";
        }


        if ('result' in data) {

            $.each(data.result, function (index, item) {
                options += "<option " + (item[field] == value ? 'selected' : '') + " value='" + item.id + "' " + (withTitle ? item[fieldName] : "") + ">" + item[fieldName] + "</option>";
            });
        } else {
            $.each(data, function (index, item) {
                options += "<option " + (item[field] == value ? 'selected' : '') + " value='" + item.id + "'" + (withTitle ? "title='" + item[fieldName] + "'" : "") + ">" + item[fieldName] + "</option>";
            });
        }

        element.children("option[value='optionCargando']").remove();

        if (element.attr("multiple") != "multiple") {
            element.html("<option value=''>-- SELECCIONE --</option>");
        }

        element.append(options);

        if (keepEnable) {
            element.removeAttr('disabled', 'disabled');
            element.removeAttr("readonly");
        }

    } catch (e) {
        throw e;
    } finally {
        MySort(element.attr("id"));
        element.val(value);
    }
}

/**
 * Agrega items seleccionado en el segundo elemento y lo agrega al primero (asociar items)
 * metodo contraparte es "removeSelectData"
 * @param {any} firstElement
 * @param {any} secondElement
 */
function addSelectData(firstElement, secondElement) {
    secondElement.find("option:selected").each(function () {
        firstElement.append($(this).clone());
    });
    secondElement.find("option:selected").remove();

    MySort(firstElement.attr("id"));
    MySort(secondElement.attr("id"));
}

/**
 * Elimina items seleccionados en el primer elemento y los agrega en el segundo (desasociar items)
 * metodo contraparte es "addSelectData"
 * @param {any} firstElement
 * @param {any} secondElement
 */
function removeSelectData(firstElement, secondElement) {

    firstElement.find("option:selected").each(function () {
        secondElement.append($(this).clone());
    });
    firstElement.find("option:selected").remove();

    MySort(firstElement.attr("id"));
    MySort(secondElement.attr("id"));

}

function MySort(selectId) {
    let soptions = $('#' + selectId + ' option').sort(function (a, b) {
        let alphabet = '*!@_.()#^&%-=+01234567989aáAÁbBcCdDeéEÉfFgGhHiíIÍjJkKlLmMnNñÑoóOÓpPqQrRsStTuúUÚvVwWxXyYzZ';

        let index_a = alphabet.indexOf(a.text[0]),
            index_b = alphabet.indexOf(b.text[0]);

        if (index_a === index_b) {
            // same first character, sort regular
            if (a.text < b.text) {
                return -1;
            } else if (a.text > b.text) {
                return 1;
            }
            return 0;
        } else {
            return index_a - index_b;
        }
    });

    $('select#' + selectId).html(soptions);

}

function MostrarErrores(e) {

    $(".is-invalid").removeClass("is-invalid");
    $(".invalid-feedback").remove();

    $.each(e.responseJSON, function (nodo, text) {
        if (nodo.indexOf('.') > 0)
            nodo = nodo.split('.')[0].toLowerCase();

        //if (nodo[0] === nodo[0].toUpperCase())
        //    nodo = nodo.charAt(0).toLowerCase() + nodo.slice(1);

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

/**
 * retorna dd-MM-yyyy
 * @param {any} dateToFormat
 */
function GetFormattedDate(dateToFormat) {
    dateToFormat = new Date(dateToFormat);
    let month = dateToFormat.getMonth() + 1;
    let day = dateToFormat.getDate();
    let year = dateToFormat.getFullYear();

    if (year < 2000) {
        return "";
    }


    return day.toString().padStart(2, "0") + "-" + month.toString().padStart(2, "0") + "-" + year;
}

function configurarDataTable(element, buscar = true, cambioCantidadFilas = true, informacion = "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros") {
    element.css("width", "100%").DataTable({
        "searching": buscar,
        "lengthChange": cambioCantidadFilas,
        "language": {
            "sProcessing": "Procesando...",
            "sLengthMenu": "Mostrar _MENU_ registros",
            "sZeroRecords": "No se encontraron resultados",
            "sEmptyTable": "Ningún dato disponible en esta tabla",
            "sInfo": informacion,
            "sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
            "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
            "sInfoPostFix": "",
            "sSearch": "Buscar:",
            "sUrl": "",
            "sInfoThousands": ".",
            "sLoadingRecords": "Cargando...",
            "oPaginate": {
                "sFirst": "Primero",
                "sLast": "Último",
                "sNext": "Siguiente",
                "sPrevious": "Anterior"
            }
        }
    });
}

// Loading button plugin (removed from BS4): Utilizado de la siguiente forma
// $("#itemId").button("loading") -> boton cargando
// $("#itemId").button("reset") -> boton normal
(function ($) {
    $.fn.button = function (action) {
        if (action === 'loading' && this.data('loading-text')) {
            this.data('original-text', this.html()).html(this.data('loading-text')).prop('disabled', true);
        }
        if (action === 'reset' && this.data('original-text')) {
            this.html(this.data('original-text')).prop('disabled', false);
        }
    };
}(jQuery));	


/**
 * Valida si un objeto tipo "{}" tiene datos o no 
 * @param {any} obj tipo {}
 */
function objetoEstaVacio(obj) {
    for (var key in obj) {
        if (obj.hasOwnProperty(key))
            return false;
    }
    return true;
}