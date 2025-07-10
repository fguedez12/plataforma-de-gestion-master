(function () {
    'use strict';
    //var url = "https://localhost:7016";
    //var url = "http://api-gestionaenergia.minenergia.qa";
    var url = "http://api-gestionaenergia.minenergia.cl";
    angular.module("inmuebleApp")
        .constant("APIURL", url);
    angular.module("medicionInteligenteApp")
        .constant("APIURL", url);
   
})()

