﻿//let consumos = null;
let energeticos = null;
//let divisionId = null;
let energeticoActual = null;
let apiBaseUrl = '';

let anio = new Date().getFullYear();

// Obtener la URL base de la API al cargar
$(document).ready(function() {
   
});

const monthNames = ["Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio",
    "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"
];

$(document).ready(function () {
    let servicioId = localStorage.getItem("servicioId");
    let divisionId = localStorage.getItem("divisionId");
    
    $.get('/Consumo/GetConfigurationValue', function(response) {
        apiBaseUrl = response._apiConfiguration.apiGestionaEnergia;
         // Cargar valor inicial del textarea
        function cargarObservacionInicial() {
            $.ajax({
                url: `${apiBaseUrl}/api/divisiones/inexistencia-eyv/${divisionId}`,
                type: 'GET',
                headers: {
                    'Authorization': 'Bearer ' + localStorage.getItem('token')
                },
                success: function(response) {
                    $("#txtObservacion").val(response.observacion || '');
                },
                error: function(error) {
                    console.error('Error al cargar la observación:', error);
                }
            });
        }
         // Asegurarse de que el elemento exista antes de cargar la observación
        if ($("#txtObservacion").length) {
            cargarObservacionInicial();
        } else {
            // Si el elemento no existe, esperar a que se cree
            const observer = new MutationObserver(function(mutations) {
                if ($("#txtObservacion").length) {
                    cargarObservacionInicial();
                    observer.disconnect();
                }
            });
            observer.observe(document.body, { childList: true, subtree: true });
        }
    });

   

   
    
    getServicio(servicioId);
    getComprasByDivision(null, anio).then();
    getEnergeticosByDivisionId(divisionId);
    $('#btnEnableCompras').on('click', function () {
        toogleServicioCompras();
    });
    Eventos();
});

async function getServicio(Id) {
    let response;
    try {
        response = await $.get({
            url: apiServicios + "/" + Id
        });

        if (!response.compraActiva) {
            $("#btnEnableCompras").removeClass("btn-danger").removeClass("btn-success").addClass("btn-success").text("Habilitar Compras");
            $('#compraInfo').removeAttr("hidden");
        }
        if (response.compraActiva) {
            $("#btnEnableCompras").removeClass("btn-success").removeClass("btn-danger").addClass("btn-danger").text("Deshabilitar Compras");
            $('#compraInfo').attr("hidden","hidden");
        }
    } catch (e) {

    }
}

function toogleServicioCompras(Id) {
    let servicioId = localStorage.getItem("servicioId");
    let divisionId = localStorage.getItem("divisionId");
    $.get({
        url: apiServicios + "/toogle/" + servicioId + "/" + divisionId
    }, function () {
        location.reload();
    }).fail(function () {
        alert("fail");
    });
}

async function getEnergeticosByDivisionId(divisionId) {
    let response;
    try {
        $("#btnModalCompras").attr("disabled", "disabled");

        response = await $.get({
            url: apiEnergeticos + "/GetEnergeticosActivos/" + divisionId,
        });

        energeticos = response.result;
        console.log(energeticos);

        $("#btnModalCompras").removeAttr("disabled");

    } catch (e) {
        $('#frmComprasAlert').html("Error al obtener datos de los energeticos de la unidad seleccionada.")
            .removeClass("alert-success")
            .addClass("alert-danger")
            .show();
    }
}

async function getExtensionesParaArchivos() {
    let response;

    try {

        response = await $.get({
            url: apiArchivoAdjunto + "/GetExtPermitidasFactura/"
        });

        let exts;

        $.each(response, function (index, item) {

            exts += item.extension;

        });
        //$("#facturas").attr("accept");

    } catch (e) {

    }
}

/**
 * Obtiene las compras segun la division guardandola en la variable "consumos".
 * La cual contiene una lista de "subMenu" la cual entrega los datos especificos para 
 * desplegar el submenu para consumos (energeticos) y para boletas (años) que corresponde a la division.
 * La siguiente lista corresponde a las "compras" que entrega toda la informacion necesaria para desplegarlas en ambas tablas
 * Archivo: consumo.js
 */
async function getComprasByDivision(energeticoId, filtro) {
    //alert(energeticoId)

    if (energeticoId == undefined || energeticoId ==null ) {
        energeticoId = $(".nav-subDetalle.active").attr("data-id");
    }


    try {
        $("#loader").show();

        consumos = await $.get({
            url: apiCompras + '/byDivision/' + localStorage.getItem('divisionId') + "/" + filtro,
        });

        if (consumos.compras.length == 0) {
            let selectFiltro = document.getElementById("selFiltro");

            if ($('#selFiltro option').length > 0) {
                $('#selFiltro').empty();
            };
            
            var years = consumos.subMenu.aniosSubMenu;

            years.sort(function numberDes(a, b) {
                return b - a;
            });

            $.each(years, function (key, year) {
               
                let optionFiltro = document.createElement("option");
                optionFiltro.text = year;
                optionFiltro.value = year;
                selectFiltro.appendChild(optionFiltro);
            });

            $("#loader").hide();
            $("#consumos-contenedor").html("<div class='alert alert-info' role='alert'>" +
                "<h4 class='alert-heading'>No hay compras!</h4>" +
                "<p>Ingrese nueva compra con el boton \"Agregar Compra\"</p>" +
                "<hr>" +
                "<p class='mb-0'>Valide que la unidad tenga Energéticos activados</p>" +
                "</div>");
        } else {
            CargarSubMenuConsumos(energeticoId);
            CargarTablaSegunEnergetico($(".nav-subDetalle.active").attr("data-id"));
        }

    } catch (e) {
        if (e.status == 401) {
            $("#consumos-contenedor").html("<div class='alert alert-danger'>" +
                "   <strong>Sin autorizacion!</strong> Ud no se encuentra autorizado para visualizar las compras de la unidad." +
                "</div>");
        } else {
            $("#consumos-contenedor").html("<div class='alert alert-danger'>" +
                "   <strong>Error!</strong> Ha ocurrido un problema. Favor contacte a su administrador." +
                "<a data-toggle='collapse' href='#collapseMasDetalle' role='button' aria-expanded='false' aria-controls='collapseMasDetalle'>" +
                "<br>Ver mas detalles" +
                "</a>" +
                "<div class='collapse' id='collapseMasDetalle'>" +
                "<div class='card card-body'>" +
                e.responseText +
                "</div>" +
                "</div>" +
                "</div>");
        }
    }
    finally {
        $("#loader").hide();
    }
}

function CargarGrafico() {

    google.charts.load('current', { 'packages': ['corechart'] });
    google.charts.setOnLoadCallback(drawChart);

    function drawChart() {

        var data = google.visualization.arrayToDataTable([
            ['Task', 'Hours per Day'],
            ['Electricidad', 11],
            ['Gas Natural', 5],
            ['Diesel', 8]
        ]);

        var options = {
            title: 'Consumo [kWh]'
        };

        var chart = new google.visualization.PieChart(document.getElementById('piechart'));

        chart.draw(data, options);
    }
}

function CargarEnergeticosAlDDL() {
    $('#disclaimer-sin-medidor').hide();
    $('#energeticoId').empty();

    $('#energeticoId').append($('<option>', {
        value: '',
        text: $('#selectVal').val()
    }));

    let data = energeticos;
    $.each(data, function (key, item) {
        $('#energeticoId').append($('<option>', {
            value: item.id,
            text: item.nombre
        }));
    });
}

function Eventos() {

    // Variable para almacenar el temporizador
    let typingTimer;
    // Tiempo de espera después de que el usuario deja de escribir (500ms)
    const doneTypingInterval = 500;

    $("#txtObservacion").on('input', function() {
        clearTimeout(typingTimer);
        const $textarea = $(this);
        const observacion = $textarea.val();

        typingTimer = setTimeout(() => {
            $('#save-indicator').removeClass('d-none');
            const divisionId = localStorage.getItem("divisionId");
            $.ajax({
                url: `${apiBaseUrl}/api/divisiones/inexistencia-eyv/${divisionId}`,
                type: 'PUT',
                headers: {
                    'Authorization': 'Bearer ' + localStorage.getItem('token'),
                    'Content-Type': 'application/json'
                },
                data: JSON.stringify({ observacion: observacion }),
                success: function(response) {
                    if (response.success) {
                        mostrarToast('Éxito', 'Observación actualizada correctamente', 'success');
                    } else if (response.message) {
                        mostrarToast('Aviso', response.message, 'info');
                    } else {
                        // Mostrar aviso genérico si no hay confirmación explícita
                        mostrarToast('Información', 'Los cambios se han guardado', 'info');
                    }
                    $('#save-indicator').addClass('d-none');
                },
                error: function(error) {
                    // Solo mostrar error si es un error real (status >= 400)
                    if (error.status >= 400) {
                        mostrarToast('Error', 'Error al actualizar: ' + (error.responseJSON?.message || error.statusText), 'danger');
                    }
                    $('#save-indicator').addClass('d-none');
                }
            });
        }, doneTypingInterval);
    });

    $("#selFiltro").on('change', function () {
        $("#consumos-contenedor").html("");
        $('#consumos-navegacion .nav-detalle-data').empty();
        var anio = $("#selFiltro").val();
        getComprasByDivision(null, anio).then();
    })

    $('body').on('click', "a[id-compra]", function () {
        CargarEnergeticosAlDDL();
    });

    $("#btnModalCompras").click(function () {
        CargarEnergeticosAlDDL();
    });

// Función reutilizable para cargar observación
function recargarObservacion(idDivision) {
    if (!idDivision) {
        $("#txtObservacion").val(''); // Limpiar si no hay ID válido
        return;
    }
    $.ajax({
        url: `${apiBaseUrl}/api/divisiones/inexistencia-eyv/${idDivision}`,
        type: 'GET',
        headers: { 'Authorization': 'Bearer ' + localStorage.getItem('token') },
        success: function (response) {
            $("#txtObservacion").val(response.observacion || '');
        },
        error: function (error) {
            console.error('Error al recargar la observación:', error); // LOG 3
            $("#txtObservacion").val(''); // Limpiar en caso de error
        }
    });
}

$("#bh-divisiones").change(function () {
    $(".nav-detalle-data").empty();
    $("#contenedor-consumos").empty();
    $("#contenedor-boletas").empty();
    let nuevaDivisionId = $(this).val(); // Capturar nuevo valor

    // Actualizar la variable global 'divisionId' si otras partes del código dependen de ella
    // (Basado en el código original que hacía 'divisionId = $(this).val();')
    divisionId = nuevaDivisionId;

    // Llamar a las funciones existentes, pasando la nueva ID si es necesario/posible
    getEnergeticosByDivisionId(nuevaDivisionId).then(); // Pasar nueva ID
    getComprasByDivision(null, anio).then(); // Esta parece usar localStorage, mantener como está por ahora
    let servicioId = localStorage.getItem("servicioId");
    getServicio(servicioId); // Esta parece no depender directamente de la división
    recargarObservacion(nuevaDivisionId); // <-- LLAMADA AÑADIDA con la nueva ID
});


    $("body").on("change", "#tabla-segun-energetico tfoot tr th #selectAnioFooter", function (event) {

        // let energeticoId = $('#contenedor-detalle ul li a.active').attr('data-id');
        let comprasSegunEnergetico = [];

        let energeticoActualId = $('#consumos-navegacion ul li a.active').attr('data-id');
        let tieneNumCliente = consumos.subMenu.energeticoSubMenu.find(e => e.id == energeticoActualId).tieneNumCliente;

        FiltrarComprasSegunEnergeticoActivo(energeticoActualId, comprasSegunEnergetico);

        MostrarPieDeTabla($("option:selected", $(this)).val(), comprasSegunEnergetico, tieneNumCliente);
    });
    // $("body").on("change", "#tabla-segun-energetico tfoot tr th #selectUnidadMedida", function (event){
    //     MostrarPieDeTabla($("#selectAnioFooter").val(), $("option:selected", $(this)).val());
    // });

    // Marca las compras con el mismo id-compra para que resalten al momento de poner el mouse encima 
    $('body').on('mouseover', "a[id-compra]", function () {
        if ($(this).children().children().hasClass("progress-bar-custom")) {
            let mismaCompra = $(this).attr("id-compra");

            $.each($("a[id-compra=" + mismaCompra + "]"), function (key, item) {
                $(item).children().children().addClass("hover");
            });
        }
    });

    // Desmarca las compras con el mismo id-compra para que no resalten al momento de quitar el mouse de encima
    $('body').on('mouseout', "#tabla-segun-energetico tbody tr td a", function () {
        if ($(this).children().children().hasClass("progress-bar-custom")) {
            let mismaCompra = $(this).attr("id-compra");

            $.each($("a[id-compra=" + mismaCompra + "]"), function (key, item) {
                $(item).children().children().removeClass("hover");
            });
        }
    });

    // Cambio de opcion en la listas del contenedor-detalle para el submenu de "Consumos" o "Boletas"
    $("body").on('click', ".nav-subDetalle", function () {
        $('.nav-subDetalle').removeClass('active');

        var $clicked = $(this);
        $('.nav-subDetalle').each(function () {
            var $menu = $(this);
            if (!$menu.is($clicked)) {
                $($menu.attr('data-item')).hide();
            }
        });

        $($clicked.attr('data-item')).show();
        $(this).addClass('active');

        if ($('input[type=radio][name=options]:checked').val() == "consumos") {
            CargarTablaSegunEnergetico($(this).attr("data-id"));
        } else if ($('input[type=radio][name=options]:checked').val() == "boletas") {
            CargarTablaBoletas();
        }

    });

    // Cambio de opcion entre switch de "Consumos" y "Boletas"
    $("body").on('change', "input[type=radio][name=options]", function () {
        //$('#contenedor-detalle li').remove();
        $("#consumos-contenedor").empty();

        if (this.id == 'chkConsumos') {
            CargarSubMenuConsumos();

            $('#chkConsumos').attr('checked', 'checked').parent().addClass('active').removeClass('btn-light').addClass('btn-primary');
            $('#chkBoletas').parent().removeClass('active').removeClass('btn-primary').addClass('btn-light');

            CargarTablaSegunEnergetico($(".nav-subDetalle.active").attr("data-id"));

            //$("#contenedor-consumos").show();
        }
        else if (this.id == 'chkBoletas') {
            CargarSubMenuBoletas();

            $('#chkConsumos').parent().removeClass('active').removeClass('btn-primary').addClass('btn-light');
            $('#chkBoletas').attr('checked', 'checked').parent().addClass('active').removeClass('btn-light').addClass('btn-primary');

            CargarTablaBoletas();

            //$("#contenedor-boletas").show();
        }


    });
}

function CargarSubMenuConsumos(energeticoIdActual) {
    $('#consumos-navegacion .nav-detalle-data').empty();

    var total = consumos.subMenu.energeticoSubMenu.length - 1;

    $.each(consumos.subMenu.energeticoSubMenu, function (key, item) {

        let activo = '';
        let lastItem = '';

        if ((energeticoIdActual != undefined && energeticoIdActual == item.id) || (energeticoIdActual == undefined && key == 0)) {
            activo = ' active';
        }



        if (total === key) {
            lastItem = ' mr-auto';
        }

        let link = "<a class='nav-link nav-subDetalle" + activo + "' data-item='#energetico" + item.id + "' data-id='" + item.id + "'>" + item.nombre + "</a>";
        let li = "<li class='nav-item" + lastItem + "' style='cursor:pointer;'>" + link + "</li>";


        $('#consumos-navegacion .nav-detalle-data').append(li);
    });

    CargaRadioLista();

}

function CargarSubMenuBoletas() {
    $('#consumos-navegacion .nav-detalle-data').empty();

    var aniosDeCompras = consumos.subMenu.aniosSubMenu.sort(function numberDes(a, b) {
        return b - a;
    });
    var anio = $("#selFiltro").val();
    aniosDeCompras = [anio];
    var total = aniosDeCompras.length - 1;
    total = 1;
    $.each(aniosDeCompras, function (key, item) {

        let activo = '';
        let lastItem = '';

        if (key === 0) {
            activo = ' active';
        }

        if (total === key) {
            lastItem = ' mr-auto';
        }


        let link = "<a class='nav-link nav-subDetalle" + activo + "' data-item='#" + item + "'>" + item + "</a>";
        let li = "<li class='nav-item" + lastItem + "' style='cursor:pointer;'>" + link + "</li>";


        $('#consumos-navegacion .nav-detalle-data').append(li);
    })


    CargaRadioLista();

}

function CargaRadioLista() {
    var inputRadioConsumos = "<input type='radio' name='options' id='chkConsumos' checked='checked' value='consumos'> Consumos";
    var labelBtnConsumos = "<label class='btn btn-primary mr-2 active'>" + inputRadioConsumos + "</label>";

    var inputRadioBoletas = "<input type='radio' name='options' id='chkBoletas' value='boletas'> Boletas";
    var labelBtnBoletas = "<label class='btn btn-light'>" + inputRadioBoletas + "</label>";

    var divChks = "<div class='btn-group btn-group-toggle' data-toggle='buttons'>" + labelBtnConsumos + labelBtnBoletas + "</div>";


    var liChk = "<li>" + divChks + "</li>";
    $('#consumos-navegacion .nav-detalle-data').append(liChk);
}


/**
 * Archivo: consumo.js
 * Recarga la tabla de compras segun el energetico a traves de los datos que se encuentran en la variable "consumos" que es seteada al momento de obtener
 * los datos de la api compras, para recargar con las nuevas compras, utilizar funcion "getComprasByDivision()" antes de ejecutar esta
 */
function CargarTablaSegunEnergetico(energeticoActualId) {
    //$('#contenedor-consumos div').remove();
    let contenedor = $("#consumos-contenedor");
    contenedor.empty();

    //let energeticoActualId = $('#consumos-navegacion ul li a.active').attr('data-id');
    let comprasSegunEnergetico = [];

    let tieneNumCliente = consumos.subMenu.energeticoSubMenu.find(e => e.id == energeticoActualId).tieneNumCliente;

    FiltrarComprasSegunEnergeticoActivo(energeticoActualId, comprasSegunEnergetico);
    MostrarCabecera(energeticoActualId, contenedor);
    MostrarCuerpoAñosMedidores(comprasSegunEnergetico, tieneNumCliente, contenedor);

    if (tieneNumCliente) {
        $.each(comprasSegunEnergetico, function (i, compra) {
            if (compra.sinMedidor  || compra.numeroCliente=="") {
                SinNumCliente(compra);
            } else {
                MostrarCompra(compra);
            }
            
        });
    } else {
        $.each(comprasSegunEnergetico, function (i, compra) {
            SinNumCliente(compra);
        });

    }

    MostrarPieDeTabla(new Date().getFullYear(), comprasSegunEnergetico, tieneNumCliente, contenedor);
}

function FiltrarComprasSegunEnergeticoActivo(energeticoActualId, comprasSegunEnergetico) {
    $.each(consumos.compras, function (key, item) {
        if (item.energeticoId == energeticoActualId) {
            if (item.sinMedidor) {
                item.numeroCliente="SinMedidor"
            }
            comprasSegunEnergetico.push(item);
        }
    });
}

function MostrarCabecera(energeticoId, contenedor) {
    //Encabezado de la tabla
    let trHead = "<tr><th>Año</th><th style='width: 90px;'>Número de Cliente</th>";

    $.each(monthNames, function (i, mes) {
        trHead += "<th>" + mes.substring(0, 3) + "</th>";
    });

    trHead += "</tr>";

    let thead = "<thead style='background-color: #BCE3FD; color: #000000;'>" + trHead + "</thead>";

    // pintar la tabla en pantalla
    let tabla = "<table id='tabla-segun-energetico' class='table table-responsive-xl table-borderless' style='table-layout: fixed; word - wrap: break-word;'>" + thead + "</table>";

    let divContenedor = "<div id='energetico" + energeticoId + "'>" + tabla + " </div>"

    contenedor.append(divContenedor);
}

/**
 * Funcion que muestra los años y medidores de las compras segun el energetico
 * esta funcion considera la fecha de inicio y la fecha de fin de la lectura para mostrar las compras,
 * cada celda que corresponda a los meses tiene una clase con la siguiente nomenclatura: "celda + año + mes + numero medidor"
 * Creado - 11/10/2018
 */
function MostrarCuerpoAñosMedidores(comprasSegunEnergetico, tieneNumCliente, contenedor) {

    //´Cuerpo o datos de la division en la tabla 
    let trBody = "";
    let tbody = "<tbody>"


    // obtengo y guardo distintos años en el array anio
    let anios = [];
    if (tieneNumCliente) {
        $.each(comprasSegunEnergetico, function (key, compra) {

            if (compra.sinMedidor) {
                if (!anios.includes(compra.anioCompra)) {
                    anios.push(compra.anioCompra);
                }

            } else {

                if (!anios.includes(compra.anioFechaInicio)) {
                    anios.push(compra.anioFechaInicio);
                }

                if (!anios.includes(compra.anioFechaFin)) {
                    anios.push(compra.anioFechaFin);
                }
            }

            
        });
    } else {
        $.each(comprasSegunEnergetico, function (key, compra) {
            if (!anios.includes(compra.anioCompra)) {
                anios.push(compra.anioCompra);
            }
        });
    }

    //ordenar de mayor a menor
    anios.sort(function (a, b) { return b - a });

    var anioActual = 0;
    var numeroClienteActual = 0;

    $.each(anios, function (key, anio) {

        let comprasPorAño = [];
        let comprasPorAñoSinMedidor = [];

        if (tieneNumCliente) {
            comprasPorAño = comprasSegunEnergetico.filter(a => a.anioFechaInicio === anio || a.anioFechaFin === anio);
            comprasPorAñoSinMedidor = comprasSegunEnergetico.filter(a => a.anioCompra === anio && a.sinMedidor);
            if (comprasPorAñoSinMedidor.length > 0) {
                comprasPorAño = comprasPorAño.concat(comprasPorAñoSinMedidor);
            }
        } else {
            comprasPorAño = comprasSegunEnergetico.filter(a => a.anioCompra === anio);
        }


        // obtengo y guardo segun el año los medidores en array medidores
        let numeroClientes = [];

        $.each(comprasPorAño, function (key, compra) {
            if (!numeroClientes.includes(compra.numeroCliente)) {
                numeroClientes.push(compra.numeroCliente);
            }

            if (!numeroClientes.includes("SinMedidor") && compra.sinMedidor)
            {
                numeroClientes.push("SinMedidor");
            }
        });

        // se recorre los diferentes medidores para comenzar a pintar las compras
        $.each(numeroClientes, function (j, numeroCliente) {

            if (anio !== '') {
                anioActual = anio;
            }

            if (numeroCliente !== '') {
                numeroClienteActual = numeroCliente;
            }

            if (anio == '' && numeroCliente == '') {
                return true;
            }

            if (!tieneNumCliente) {
                numeroCliente = "";
            }

            trBody = "<tr>" +
                "<td>" + anio + "</td>" +
                "<td>" + numeroCliente + "</td>";

            // Cada columna corresponde a un mes, y las compras son marcadas segun fecha de inicio y fecha de fin para los energeticos que tienen medidores
            // si el energetico no tiene medidor la compra se marca segun la fecha de compra

            //pintar las celdas vacias
            for (let i = 0; i < 12; i++) {
                let numCliente = numeroCliente.replace(/ /g, "").replace(/-/g, "").replace(/\./g, ''); //quita todos los espacios, puntos y guiones de la cadena

                trBody += "<td class='pr-0 pl-0 celda" + anioActual + i.toString().padStart(2, "0") + numCliente + " mes" + i + "'></td>";
            }

            trBody += "</tr>";
            tbody += trBody;

            if (anio == anioActual) {
                anio = '';
            }

            if (numeroCliente == numeroClienteActual) {
                numeroCliente = '';
            }
        });
    });


    tbody += "</tbody>";
    contenedor.children().children().append(tbody);
    //$('#consumos-contenedor div table').append(tbody);
}

function MostrarCompra(compra) {

    let diaFinLectura = new Date(compra.finLectura).getDate();
    let mesFinLectura = new Date(compra.finLectura).getMonth();
    let anioFinLectura = new Date(compra.finLectura).getFullYear();

    let diaInicioLectura = new Date(compra.inicioLectura).getDate();
    let mesInicioLectura = new Date(compra.inicioLectura).getMonth();
    let anioInicioLectura = new Date(compra.inicioLectura).getFullYear();
    let numeroCliente = compra.numeroCliente.replace(/ /g, "").replace(/-/g, "").replace(/\./g, ''); //quita todos los espacios, puntos y guiones de la cadena

    let tooltip = "Fecha Inicio: " + diaInicioLectura + " de " + monthNames[mesInicioLectura] + " del " + anioInicioLectura + "\r\n" +
        "Fecha Fin: " + diaFinLectura + " de " + monthNames[mesFinLectura] + " del " + anioFinLectura + "\r\n" +
        "Consumo: " + compra.consumo + " " + compra.abrev;


    let anchoBarra = compra.ancho;
    let ml = compra.marginIzq;  // 1,97 * 30 [dias] = [59,1 px] aprox
    let firstMonthYear = compra.cantidadPorMes.find(c => c.mes == (mesInicioLectura + 1) && c.anio == anioInicioLectura);

    $.each(compra.cantidadPorMes, function (j, cantidad) {

        let hddn = "<input type='hidden' name='AbrevUniMedidaConNumCliente' value='" + cantidad.cantidadEnkWh + "'>";
        $(".celda" + cantidad.anio + (cantidad.mes - 1).toString().padStart(2, "0") + numeroCliente).append(hddn);
        if (j > 0 && !cantidad.continuaBarra) {
            $(".celda" + cantidad.anio + (cantidad.mes - 1).toString().padStart(2, "0") + numeroCliente).append("<div class='progress' style='background-color: white;display:none;'></div>");
        }

    });

    if (compra.distintoAnio) {
        anchoBarra = 50 * firstMonthYear.llenado;
        for (var resto = firstMonthYear.mes - 1; resto < 11; resto++) {
            anchoBarra = anchoBarra + 50;
        }
    }

    let progressColor = colorCompra(compra);


    let pbInitialClass = "progress-bar progress-bar-custom";
    let divProgressBar = "<div class='" + pbInitialClass + "' role='progressbar' style='width: 100%; background-color: " + progressColor + ";' aria-valuenow='100' aria-valuemin='0' aria-valuemax='100'>"
        + compra.consumo + "</div>";

    let attrModal = "data-toggle='modal' data-target='#ModalCompras'";

    let countProgress = $(".celda" + firstMonthYear.anio + (firstMonthYear.mes - 1).toString().padStart(2, "0") + numeroCliente).find("a > .progress").length;

    let margin = "";

    let marginval = 0;

    if (compra.compraSolapadaLv == 1) {
        marginval = countProgress * -16 + (compra.compraSolapadaLv - 1) * 16
    } else {
        if (countProgress == 0) {

            marginval = 16 * (compra.compraSolapadaLv - 1)
        } else {
            marginval = 0;
        }

    }

    margin = "margin-top:" + marginval + "px;"

    //console.log(compra);

    let divProgress = "<div class='progress' style='width:" + anchoBarra + "px;margin-left:" + ml + "px;" + margin + "'>" + divProgressBar + "</div>";

    let aItem = "<a href='' " + attrModal + " id-compra='" + compra.id + "' title='" + tooltip + "'>" + divProgress + "</a>";

    $(".celda" + firstMonthYear.anio + (firstMonthYear.mes - 1).toString().padStart(2, "0") + numeroCliente).append(aItem);

    //para insertar el resto de la compra en el año y mes siguiente en caso que la compra corresponda a distintos años
    let lastMonthYear = compra.cantidadPorMes.find(c => c.mes == (mesFinLectura + 1) && c.anio == anioFinLectura);
    if (compra.distintoAnio) {
        let comprasUltimoAnio = compra.cantidadPorMes.filter(a => a.anio == anioFinLectura);
        let nuevoAnchoBarra = 0;
        for (var resto = 0; resto < lastMonthYear.mes; resto++) {
            nuevoAnchoBarra = nuevoAnchoBarra + (comprasUltimoAnio[resto].llenado * 59);
        }
        divProgressBar = "<div class='" + pbInitialClass + "' role='progressbar' style='width: 100%; background-color: " + progressColor + ";' aria-valuenow='100' aria-valuemin='0' aria-valuemax='100'>"
            + compra.consumo +
            "</div>";

        let countProgressDa = $(".celda" + lastMonthYear.anio + "00" + numeroCliente).find("a > .progress").length;

        let marginDa = ""

        let marginvalDa = 0;

        if (compra.compraSolapadaLv == 1) {
            marginvalDa = countProgressDa * -16 + (compra.compraSolapadaLv - 1) * 16
        } else {

            if (countProgressDa == 0) {

                marginvalDa = 16 * (compra.compraSolapadaLv - 1)
            } else {
                marginvalDa = 0;
            }
        }



        marginDa = "margin-top:" + marginvalDa + "px;"


        //console.log(compra.id);
        //console.log(countProgressDa);



        let divProgress = "<div class='progress' style='width:" + nuevoAnchoBarra + "px;" + marginDa + "'>" + divProgressBar + "</div>";

        //console.log(divProgress);

        let aItem = "<a href='' " + attrModal + " id-compra='" + compra.id + "' title='" + tooltip + "'>" + divProgress + "</a>";

        $(".celda" + lastMonthYear.anio + "00" + numeroCliente).append(aItem);
        //console.log($(".celda" + lastMonthYear.anio + "00" + numeroCliente));

    }


}


function colorCompra(compra) {
    let progressColor = "red";

    if (compra.estado === "Ok") {
        progressColor = "green";
    } else if (compra.estado === "Sin Revision") {
        progressColor = "DimGray";
    }

    return progressColor;
}

function SinNumCliente(compra) {
    //ingresar compras con fecha inicio y fecha de fin en las celdas ya creadas
    //$.each(comprasSegunEnergetico, function(i, compra){
    let diaFechaCompra = new Date(compra.fechaCompra).getDate();
    let mesFechaCompra = new Date(compra.fechaCompra).getMonth();
    let anioFechaCompra = new Date(compra.fechaCompra).getFullYear();

    let tooltip = "Fecha Compra: " + diaFechaCompra + " de " + monthNames[mesFechaCompra] + " del " + anioFechaCompra + "\r\n" +
        "Cantidad: " + compra.consumo + " " + compra.abrev;

    var hddn = "<input type='hidden' name='AbrevUniMedidaSinNumCliente' value='" + compra.consumoEnkWh + "'>";
    if (compra.sinMedidor) {
        $(".celda" + anioFechaCompra + mesFechaCompra.toString().padStart(2, "0")+"SinMedidor").append(hddn);
    } else {
        $(".celda" + anioFechaCompra + mesFechaCompra.toString().padStart(2, "0")).append(hddn);
    }
   

    let progressColor = colorCompra(compra);

    //Cambio para mostar el id en las sin consumo

    let consumo;
    if (compra.consumo > 0) {
        consumo = compra.consumo
    } else {
        if (compra.energeticoId == 7) {
            consumo = `${compra.costo}$`
        } else {
            consumo = `Id:${compra.id}`    
        }
        
    }

    let pbInitialClass = "progress-bar progress-bar-custom";
    let divProgressBar = "<div class='" + pbInitialClass + "' role='progressbar' style='width: 100%; background-color: " + progressColor + "' aria-valuenow='100' aria-valuemin='0' aria-valuemax='100'>"
        + consumo + "</div>";

    var divProgress = "<div class='progress'>" + divProgressBar + "</div>";

    let attrModal = "data-toggle='modal' data-target='#ModalCompras'";

    var aItem = "<a href='' " + attrModal + " id-compra='" + compra.id + "' title='" + tooltip + "'>" + divProgress + "</a>";


    if (compra.sinMedidor) {
        $(".celda" + anioFechaCompra + mesFechaCompra.toString().padStart(2, "0") + "SinMedidor").append(aItem);
    } else {
        $(".celda" + anioFechaCompra + mesFechaCompra.toString().padStart(2, "0")).append(aItem);
    }

    //});
}

function MostrarPieDeTabla(anioParametro, compras, tieneNumCliente, contenedor = $("#consumos-contenedor")) {
    // Pie de la tabla con los totalizadores correspondientes
    $("#tabla-segun-energetico tfoot").remove();


    let selectAnio = document.createElement("select");
    selectAnio.id = "selectAnioFooter";

    let selectFiltro = document.getElementById("selFiltro");

    var years = consumos.subMenu.aniosSubMenu;

    //if (!years.includes(new Date().getFullYear())) {
    //    years.push(new Date().getFullYear());
    //}

    years.sort(function numberDes(a, b) {
        return b - a;
    });

    if ($('#selFiltro option').length == 0) {
        $.each(years, function (key, year) {
            let optionFiltro = document.createElement("option");
            optionFiltro.text = year;
            optionFiltro.value = year;
            selectFiltro.appendChild(optionFiltro);
        });
    };

    //$.each(years, function (key, year) {
    //    let optionAnio = document.createElement("option");
    //    optionAnio.text = year;
    //    optionAnio.value = year;
      
    //    selectAnio.appendChild(optionAnio);

    //});

    var yearAc = $('#selFiltro').val();

    let optionAnio = document.createElement("option");
    optionAnio.text = yearAc;
    optionAnio.value = yearAc;
    selectAnio.appendChild(optionAnio);

    let trFoot = "<tr><th scope='row' colspan='2' class='pl-0'>Total mensual en kWh Año: " + selectAnio.outerHTML + "</th>";

    let anio = yearAc;
    //if (years.length > 0) {
    //    anio = years[0];
    //}

    //let anio = new Date().getFullYear();
    //if (anioParametro !== undefined && years.includes(anioParametro)) {
    //    anio = anioParametro;
    //}


    for (var mes = 0; mes < 12; mes++) {
        let sum = 0;
        let buscarEnFecha = anio.toString() + mes.toString().padStart(2, "0");

        var numConsumos = $("td[class*=celda" + buscarEnFecha + "] input[type='hidden']").length;

        $.each($("td[class*=celda" + buscarEnFecha + "] input[type='hidden']"), function (key, item) {
            sum = Number(sum) + Number(item.value.replace(',', '.'));

        });

        let esAzul = true;

        if (tieneNumCliente) {
            esAzul = EsAzul(anio, mes, compras);
        }

        let color = esAzul ? "#0069d9" : "#C0C0C0";

        if (sum > 0) {
            trFoot += "<td class='px-0' style='background-color: " + color + "; color: #fff;text-align:center;'>" + sum.toFixed(2) + "</td>";
        } else if (numConsumos > 0) {
            trFoot += "<td class='px-0' style='background-color: " + color + "; color: #fff;text-align:center;'>" + sum.toFixed(2) + "</td>";
        }
        else {
            trFoot += "<td class='px-0' style='color: black;text-align:center;'>0</td>";
        }
    }

    trFoot += "</tr>";
    let tfoot = "<tfoot style='background-color: #BCE3FD; color: #000000;'>" + trFoot + "</tfoot>";

    contenedor.children().children().append(tfoot);

    $("#selectAnioFooter").val(anio);
}

function EsAzul(anio, mes, compras) {
    let comprasSegunAnio = [];

    $.each(compras, function (i, compra) {
        if (compra.anioCompra == anio) {
            comprasSegunAnio.push(compra);
        } else if (compra.anioFechaInicio == anio) {
            comprasSegunAnio.push(compra);
        } else if (compra.anioFechaFin == anio) {
            comprasSegunAnio.push(compra);
        }
    });


    let cantidadesPorMes = $.map(comprasSegunAnio, function (compra) { return compra.cantidadPorMes });

    let porMes = cantidadesPorMes.filter(c => (c.mes - 1) == mes);

    if (mes === 0) {
        let cantidadesPorAnioAnterior = $.map(compras.filter(c => c.anioCompra == anio - 1), function (v) { return v.cantidadPorMes });
        let cantidadEnero = cantidadesPorAnioAnterior.filter(c => (c.mes - 1) == mes && c.anio == anio);

        if (cantidadEnero.length > 0) {
            porMes.push(cantidadEnero[0]);
        }

    }


    let sumPorcentaje = 0;
    $.each(porMes, function (key, e) {
        sumPorcentaje = sumPorcentaje + e.llenado;

    });

    return sumPorcentaje > 0.92;

}



function CargarTablaBoletas() {
    // $("#contenedor-boletas").empty();
    let contenedor = $("#consumos-contenedor");
    contenedor.empty();

    CargaCabeceraTablaBoletas(contenedor);
    CargaCuerpoTablaBoletas(contenedor);
}

function CargaCabeceraTablaBoletas(contenedor) {
    $('#contenedor-boletas table').remove();

    //Encabezado de la tabla
    let trHead = "<tr>";

    trHead += "<th style='width: 8%;'>Id</th>";
    trHead += "<th style='width: 14%;'>Numero de Clientes</th>";
    //trHead += "<th style='width: 12%;'>Medidor</th>";
    trHead += "<th style='width: 20%;'>Energetico</th>";
    trHead += "<th style='width: 20%;'>Fecha Inicio Lectura/Compra</th>";
    trHead += "<th>Cantidad</th>";
    trHead += "<th>Estado</th>";

    trHead += "</tr>";

    let thead = "<thead style='background-color: #BCE3FD; color: #000000;'>" + trHead + "</thead>";

    // pintar la tabla en pantalla
    let tabla = "<table id='tabla-segun-anio' class='table table-responsive-xl table-borderless' style='table-layout: fixed; word - wrap: break-word;'>" + thead + "</table>";

    //$('#contenedor-boletas').append(tabla);
    contenedor.append(tabla);

}

function CargaCuerpoTablaBoletas(contenedor) {
   
    let subMenuActivo = $('.nav-subDetalle.active').attr('data-item').replace('#', '');
    let tbody = "<tbody>";
    let trBody = '';

    $.each(consumos.compras, function (i, compra) {
        
        let dateToShow = "";

        if (compra.inicioLectura == "0001-01-01T00:00:00") {
            dateToShow = GetFormattedDate(compra.fechaCompra);
        } else {
            dateToShow = GetFormattedDate(compra.inicioLectura);
        }

        //if (subMenuActivo != dateToShow.substr(6)) {
        //    return;
        //}
        //console.log(compra.consumo);

        trBody += "<tr>";

        let attrModal = "data-toggle='modal' data-target='#ModalCompras'";

        let tdBody = "<td><a href='#' id-compra='" + compra.id + "' " + attrModal + ">" + compra.id + "</a></td>";
        tdBody += "<td>" + compra.numeroCliente + "</td>";
        //tdBody += "<td>" + compra.numeroMedidor + "</td>";
        var energeticoId = compra.energeticoId;
        tdBody += "<td>" + energeticos.find(e => e.id == energeticoId).nombre + "</td>";
        tdBody += "<td>" + dateToShow + "</td>";
        tdBody += "<td>" + compra.consumo + " " + compra.abrev + "</td>";
        tdBody += "<td>" + compra.estado + "</td>";


        trBody += tdBody + "</tr>";
    });


    tbody += trBody + "</tbody>";
    //$('#contenedor-boletas table').append(tbody).excelTableFilter();
    contenedor.children().append(tbody).excelTableFilter();
}

