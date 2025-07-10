function DescargarReporte(e, servicioId, reporteId) {
    e.preventDefault();
    window.location = apiReportes + '/' + servicioId + '/' + reporteId;

}

function getReportes(servicioId) {

    $("#tblReportes tbody").empty();
    $.ajax({
        type: 'GET',
        url: apiReportes + '/' + servicioId,
        //async: true,
        beforeSend: function () {
            // $("#loader").show();
        },
        success: function (data) {
            let fila = "";
            let datas1 = data.filter(item => item.seccion === 1)
                .sort((a, b) => a.order - b.order);
            let datas2 = data.filter(item => item.seccion === 2)
                .sort((a, b) => a.order - b.order);
            let datas3 = data.filter(item => item.seccion === 3)
                .sort((a, b) => a.order - b.order);

            $.each(datas1, function (key, reporte) {
                let generado = "#' onClick='DescargarReporte(event," + servicioId + "," + reporte.id + ")'";
                let target = "";
                if (reporte.rutaDondeObtenerArchivo) {
                    generado = reporte.rutaDondeObtenerArchivo + reporte.tipoArchivo.extension;
                    target = "target = '_blank'";
                }

                let columnas = "<td class='w-75 small'>" + reporte.nombre + "</td>";
                columnas += "<td class='text-center'>" + reporte.tipoArchivo.nombreCorto + "</td>";
                columnas += "<td class='text-center'><a href='" + generado + "' class='btn btn-primary btn-sm' " + target + " ><i class='fa fa-download' aria-hidden='true'></i> Descargar</a></td>";

                fila = "<tr id='" + reporte.id + "'>" + columnas + "</tr>";

                $("#tblReportes tbody").append(fila);
            });

            $.each(datas2, function (key, reporte) {
                let generado = "#' onClick='DescargarReporte(event," + servicioId + "," + reporte.id + ")'";
                let target = "";
                if (reporte.rutaDondeObtenerArchivo) {
                    generado = reporte.rutaDondeObtenerArchivo + reporte.tipoArchivo.extension;
                    target = "target = '_blank'";
                }

                let columnas = "<td class='w-75 small'>" + reporte.nombre + "</td>";
                columnas += "<td class='text-center'>" + reporte.tipoArchivo.nombreCorto + "</td>";
                columnas += "<td class='text-center'><a href='" + generado + "' class='btn btn-primary btn-sm' " + target + " ><i class='fa fa-download' aria-hidden='true'></i> Descargar</a></td>";

                fila = "<tr id='" + reporte.id + "'>" + columnas + "</tr>";

                $("#tblReportess2 tbody").append(fila);
            });
            $.each(datas3, function (key, reporte) {
                let generado = "#' onClick='DescargarReporte(event," + servicioId + "," + reporte.id + ")'";
                let target = "";
                if (reporte.rutaDondeObtenerArchivo) {
                    generado = reporte.rutaDondeObtenerArchivo + reporte.tipoArchivo.extension;
                    target = "target = '_blank'";
                }

                let columnas = "<td class='w-75 small'>" + reporte.nombre + "</td>";
                columnas += "<td class='text-center'>" + reporte.tipoArchivo.nombreCorto + "</td>";
                columnas += "<td class='text-center'><a href='" + generado + "' class='btn btn-primary btn-sm' " + target + " ><i class='fa fa-download' aria-hidden='true'></i> Descargar</a></td>";

                fila = "<tr id='" + reporte.id + "'>" + columnas + "</tr>";

                $("#tblReportess3 tbody").append(fila);
            });


        },
        error: function (result) {
            var content = "<div class='alert alert-danger'>" +
                "   <strong>Error!</strong> Ha ocurrido un problema. Favor contacte a su administrador."
            "</div>";
            $("#tblReportes").parent().append(content);

        },
        complete: function () {
            // $("#loader").hide();
        }
    });
}

function getNombreServicio() {
    $(".subtitulo").empty();
    $.ajax({
        type: 'GET',
        url: apiServicios + '/' + localStorage.getItem('servicioId'),
        beforeSend: function () {
            // $("#loader").show();
        },
        success: function (data) {
            $(".subtitulo").text(data.nombre);
        },
        error: function (result) {
            var content = "<div class='alert alert-danger'>" +
                "   <strong>Error!</strong> Ha ocurrido un problema. Favor contacte a su administrador."
            "</div>";
            $("#divisiones").append();

        },
        complete: function () {
            // $("#loader").hide();
        }
    });
}
