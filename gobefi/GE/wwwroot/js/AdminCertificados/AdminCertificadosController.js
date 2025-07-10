
(function () {
    'use strict';

    var certificadosApp = angular.module('adminCertificadosApp', []);

    certificadosApp.controller('adminCertificadosController', function ($scope, $http, $interval, $sce) {

       
        $scope.showNextPage = false;
        $scope.title = "Certificados";
        $scope.viewResul = false;
        $scope.cargaResul = '';
        $scope.cargaOk = null;
        $scope.btnDisabled = true;
        $scope.filtroNombre = "";
        $scope.filtroCorreo = "";
        $scope.disableServicios = true;
        $scope.exportText = "Exportar datos";
        $scope.exportLoading = false;
        $scope.zipText = "Exportar Certificados";
        $scope.zipLoading = false;
        $scope.progressStyle = '0%';
        $scope.countNotas = 0;
        $scope.showProgress = false;
        $scope.cargarText = "Cargar archivo";
        $scope.cargarDisabled = false;

        loadData(1);

        $scope.goToPage = function(page){
            loadData(page);
        };

        $scope.nextPage = function () {
            if ($scope.pageIndex < $scope.stopPageIndex){
                loadData($scope.pageIndex + 1);
            }
            
        };
        $scope.prevPage = function () {
            if ($scope.pageIndex > $scope.startPageIndex) {
                loadData($scope.pageIndex - 1);
            }
            
        };

        function loadData(page) {
            $scope.loadingTable = true;
            var filtroMinisterio = ""
            if ($scope.filtroMinisterio) {
                filtroMinisterio = $scope.filtroMinisterio
            }
            var filtroServicio = ""
            if ($scope.filtroServicio) {
                filtroServicio = $scope.filtroServicio
            }
            $http({
                method: 'GET',
                url: `/api/certificados/notas?page=${page}&filtronombre=${$scope.filtroNombre}&filtroCorreo=${$scope.filtroCorreo}&filtroMinisterio=${filtroMinisterio}&filtroServicio=${filtroServicio}`
            }).then(function (response) {
                $scope.notas = response.data.notasPorPagina.notas;
                $scope.pageIndex = response.data.notasPorPagina.pageIndex;
                $scope.startPageIndex = response.data.notasPorPagina.startPageIndex;
                $scope.stopPageIndex = response.data.notasPorPagina.stopPageIndex;
                $scope.ministeriosList = response.data.notasPorPagina.ministerios;
                $scope.serviciosList = response.data.notasPorPagina.servicios;
                $scope.loadingTable = false;
            });
        };


        $scope.changeMinisterio = function () {
            if ($scope.filtroMinisterio != null) {
                $scope.filteredServicios = $scope.serviciosList.filter(function (f) {
                    return f.ministerioId == $scope.filtroMinisterio
                });

                $scope.disableServicios = false;
            } else {
                $scope.disableServicios = true;
            }

        };

        $scope.showFliters = function () {
            var modal = angular.element("#modalFiltros");
            modal.modal('show');
        };

        $scope.filtrar = function () {
            loadData(1);

            var modal = angular.element("#modalFiltros");
            modal.modal('hide');
        }

        

        $scope.certificadosSelected = null;
        $http({
            method: 'GET',
            url: '/api/certificados'
        }).then(function (response) {
            $scope.certificados = response.data.certificados

        });

        $scope.downloadPdf = function (nombre, nota, dia, mes, anio, serie, codigo) {

            var doc = new jsPDF({
                orientation: 'landscape'

            });
            var img = new Image()
            img.src = '/images/diploma.png'
            doc.addImage(img, "JPEG", 0, 0, 300, 220);
            doc.setFontType("bold");

            doc.setFontSize(44);
            doc.text("CERTIFICADO", 103, 57);
            doc.setFontSize(15);

            doc.setFontSize(15);
            doc.setTextColor(105, 110, 107);
            doc.setFontType("normal");
            doc.text("El Ministerio de Energía otorga el siguiente certificado a:", 87, 75);

            doc.setFontType("bold");
            doc.setTextColor(0, 0, 0);
            doc.setFontSize(38);
            doc.text(nombre, 75, 90);

            doc.setFontSize(15);
            doc.setTextColor(105, 110, 107);
            doc.setFontType("normal");
            doc.text(`Por haber aprobado con calificación ${nota.toString()} de 10 el curso de Gestor Energético para el sector`, 40, 101);

            doc.setFontSize(15);
            doc.setTextColor(105, 110, 107);
            doc.setFontType("normal");
            doc.text("Público del programa Gestiona Energía.", 40, 108);

            doc.setFontSize(15);
            doc.setTextColor(105, 110, 107);
            doc.setFontType("normal");
            doc.text("Agradecemos la disponibilidad y apoyo de su parte en nuestro objetivo conjunto de potenciar", 40, 120);

            doc.setFontSize(15);
            doc.setTextColor(105, 110, 107);
            doc.setFontType("normal");
            doc.text("El buen uso de la Energía.", 40, 128);

            doc.setFontType("bold");
            doc.setFontSize(13);
            doc.text(`N° Serie: ${serie}`, 40, 192);

            doc.setFontSize(13);
            doc.text(`Código de validación: ${codigo}`, 95, 192);

            doc.setFontSize(13);
            doc.setTextColor(105, 110, 107);
            doc.setFontType("normal");
            doc.text(`Santiago, ${dia} de ${mes} del ${anio}`, 192, 192);

            doc.save('certificado.pdf')
        }

        $scope.dowloadZip = function () {
            $scope.zipText = "Cargando...";
            var filtroMinisterio = "";

            if ($scope.filtroMinisterio) {
                filtroMinisterio = $scope.filtroMinisterio
            }
            var filtroServicio = ""
            if ($scope.filtroServicio) {
                filtroServicio = $scope.filtroServicio
            }
            $http({
                method: 'GET',
                url: `/api/certificados/notas?page=0&filtronombre=${$scope.filtroNombre}&filtroCorreo=${$scope.filtroCorreo}&filtroMinisterio=${filtroMinisterio}&filtroMinisterio=${filtroServicio}`
            }).then(function (response) {
                $scope.notasTotal = response.data.notasPorPagina.notas;
                $scope.countNotas = response.data.notasPorPagina.notas.length;
                angular.element('#modalZip').modal('show');
                
            });
        }

        $scope.cancelDownload = function () {
            $scope.zipText = "Exportar Certificados";
            angular.element('#modalZip').modal('hide');
        }


        $scope.processZip = function processZip() {


            $scope.showProgress = true;


            
            $scope.zipLoading = false;

            angular.element('#modalZip').modal('hide');
            var q = 1;
            const monthNames = ["Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio",
                "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"
            ];
            var zip = new JSZip();

            var interval = $interval(increaseQ, 50);

            function increaseQ() {
                var i = q - 1;
                $scope.someThing = q;
                var percent = (q / $scope.countNotas) * 100;
                $scope.progressStyle = percent.toString() + '%';

                var doc = new jsPDF({
                    orientation: 'landscape'

                });

                var img = new Image()
                img.src = '/images/diplomav2.png'
                doc.addImage(img, "JPEG", 0, 0, 300, 220);
                doc.setFontType("bold");

                doc.setFontSize(38);
                doc.text($scope.notasTotal[i].nombreUsuario, 75, 92);
                doc.setFontSize(15);
                doc.text($scope.notasTotal[i].notaFinal.toString(), 130, 101);

                doc.setFontType("bold");
                doc.setFontSize(13);
                doc.text(`N° Serie: ${$scope.notasTotal[i].numeroSerie}`, 40, 183);

                doc.setFontSize(13);
                doc.text(`Código de validación: ${$scope.notasTotal[i].codigo}`, 95, 183);

                var fecha = new Date($scope.notasTotal[i].fechaEntrega);

                doc.setFontSize(13);
                doc.setTextColor(105, 110, 107);
                doc.setFontType("normal");
                doc.text(`Santiago, ${fecha.getDate()} de ${monthNames[fecha.getMonth()]} del ${fecha.getFullYear()}`, 192, 183);

                var filename = $scope.notasTotal[i].nombreUsuario.replace(" ", "").replace(".", "");
                var servicioId = $scope.notasTotal[i].servicioId;

                zip.file(servicioId+'_'+filename + i + ".pdf", doc.output('blob'));

                if (q == $scope.countNotas) {
                    $interval.cancel(interval);
                    $scope.showProgress = false;
                    $scope.zipText = "Exportar Certificados";
                    $scope.zipLoading = false;
                    zip.generateAsync({ type: "blob" })
                        .then(function (content) {
                            saveAs(content, "Certificados.zip");
                        });
                    

                }

                else q++;
            }
        }

        $scope.cargar = function () {
            $scope.cargarText = "Cargando..";
            $scope.cargarDisabled = true;
            var file = document.forms['certificadoForm']['file'].files[0];
            var backendUrl = `/api/certificados/cargar/${$scope.certificadosSelected}`
            var fd = new FormData();

            fd.append('myFile', file, 'carga.xlsx');

            $http.post(backendUrl, fd, {
                // this cancels AngularJS normal serialization of request
                transformRequest: angular.identity,
                // this lets browser set `Content-Type: multipart/form-data` 
                // header and proper data boundary
                headers: { 'Content-Type': undefined }
            })

                .then(function (response) {
                    $scope.viewResul = true;
                    $scope.cargaResul = "Proceso finalizado, registros cargados " + response.data.countOk+"<br/>resumen de errores:<br/><ul><li>Errores de Id: " + response.data.countId + "</li><li>Errores de Email: " + response.data.countEmail + "</li><li>Errores de Nota: " + response.data.countNota + "</li><li>Errores de Numero de Serie: " + response.data.countSerie + "</li><li>Errores de Fecha: " + response.data.countFecha + "</li><li>Errores de Duración: " + response.data.countDuracion +"</li></ul>";
                    $scope.trustedHtml = $sce.trustAsHtml($scope.cargaResul);
                    $scope.cargaOk = true;
                    $scope.cargarText = "Cargar archivo";
                    $scope.cargarDisabled = false;
                    loadData(1);

                })

        };

        var mystyle = {
            headers: true,
            columns: [
                { columnid: 'usuarioId', title: 'UsuarioId' },
                { columnid: 'nombres', title: 'Nombres' },
                { columnid: 'apellidos', title: 'Apellidos' },
                { columnid: 'email', title: 'Email' },
                { columnid: 'tipoGestor', title: 'Tipo de Gestor' },
                { columnid: 'numeroSerie', title: 'N° de Serie' },
                { columnid: 'nombreCertificado', title: 'Tipo de certificado' },
                { columnid: 'duracion', title: 'Duración' },    
                { columnid: 'fechaEntrega', title: 'Fecha de entrega' },  
                { columnid: 'notaFinal', title: 'Nota' },  


                
            ],
        };

        $scope.exportData = function () {
            $scope.exportText = "Cargando...";
            $scope.exportLoading = true;
            var filtroMinisterio = "";

            if ($scope.filtroMinisterio) {
                filtroMinisterio = $scope.filtroMinisterio
            }
            var filtroServicio = ""
            if ($scope.filtroServicio) {
                filtroServicio = $scope.filtroServicio
            }

            $http({
                method: 'GET',
                url: `/api/certificados/notas?page=0&filtronombre=${$scope.filtroNombre}&filtroCorreo=${$scope.filtroCorreo}&filtroMinisterio=${filtroMinisterio}&filtroMinisterio=${filtroServicio}`
            }).then(function (response) {
                $scope.notasTotal = response.data.notasPorPagina.notas;
                
                alasql('SELECT * INTO XLS("reporte_certificados.xls",?) FROM ?', [mystyle, $scope.notasTotal]);
                $scope.exportText = "Exportar datos";
                $scope.exportLoading = false;
            });

           

        };

    });


})();
