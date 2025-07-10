(function () {
    'use strict';

    var certificadosApp = angular.module('certificadosApp', []);

    certificadosApp.controller('certificadosController', function ($scope, $http) {

      
        $http({
            method: 'GET',
            url: '/api/certificados/notasbyuser'
        }).then(function (response) {
            $scope.notas = response.data.notas;
        });

        $scope.downloadPdf = function (nombre,nota,dia,mes,anio,serie,codigo)
        {

            var doc = new jsPDF({
                orientation: 'landscape'

            });
            var img = new Image()
            img.src = '/images/diplomav2.png'
            doc.addImage(img, "JPEG", 0, 0, 300, 220);
            doc.setFontType("bold");
           
            doc.setFontSize(38);
            doc.text(nombre, 75, 92);
            doc.setFontSize(15);
            doc.text(nota.toString(), 131, 101);

            doc.setFontType("bold");
            doc.setFontSize(13);
            doc.text(`N° Serie: ${serie}`, 40, 183);

            doc.setFontSize(13);
            doc.text(`Código de validación: ${codigo}`, 95, 183);

            doc.setFontSize(13);
            doc.setTextColor(105, 110, 107);
            doc.setFontType("normal");
            doc.text(`Santiago, ${dia} de ${mes} del ${anio}`, 192, 183);

            



            doc.save('certificado.pdf')
        }

        $scope.certificadosSelected = null;
        $http({
            method: 'GET',
            url: '/api/certificados'
        }).then(function (response) {
            $scope.certificados = response.data.certificados

        });

    });


})();
