
var app = angular.module('medicionInteligenteApp', ['ui.router', 'cp.ngConfirm'])
    .controller("medicionInteligenteController", function ($rootScope, $state) {
        var userId = angular.element("#input-id").val();
        $rootScope.userId = userId;
        var isConsulta = angular.element("#input-consulta").val();
        $rootScope.isConsulta = isConsulta;
        $state.go("medicionInteligente@home");
    })
    .controller("homeMedicionInteligenteController", function ($scope, $rootScope, $http, $timeout, $state, $ngConfirm) {

        $scope.desde = new Date(Date.now());
        $scope.hasta = new Date(Date.now());
        $scope.chkFrecuencia = "15m";
        $scope.desdePot = new Date(Date.now());
        $scope.hastaPot = new Date(Date.now());
        $scope.chkFrecuenciaPot = "15m";
       

        $http({
            method: 'GET',
            url: `/settings?sectionName=ApiConfiguration&paramName=apiGestionaEnergia`

        }).then(function (response) {
            //console.log("ReadDataFromAppSettings", response);
            $rootScope.APIURL = response.data;
            getApiData();
        })
       
        $scope.showMedidores = function () {
            openModal();
        };

        $scope.showMedidoresPot = function () {
            openModalPot();
        };

        $scope.toogleAllMedidores = function () {
            if ($scope.checkAll) {
                setOffMedidores();
            } else {
                setOnMedidores();
            }
            //console.log("toogle");
        }

        $scope.toogleAllMedidoresPot = function () {
            if ($scope.checkAllPot) {
                setOffMedidoresPot();
            } else {
                setOnMedidoresPot();
            }
            //console.log("toogle");
        }

        $scope.toogleMedidor = function (medidor) {
            if (medidor.checked) {
                medidor.checked = false;
            } else {
                medidor.checked = true;
            }
        }

        function setOffMedidores() {
            for (var i = 0; i < $scope.medidores.length; i++) {
                $scope.medidores[i].checked = false;
            }
        }

        function setOffMedidoresPot() {
            for (var i = 0; i < $scope.medidoresPot.length; i++) {
                $scope.medidoresPot[i].checked = false;
            }
        }

        function setOnMedidores() {
            for (var i = 0; i < $scope.medidores.length; i++) {
                $scope.medidores[i].checked = true;
            }
        }
        function setOnMedidoresPot() {
            for (var i = 0; i < $scope.medidoresPot.length; i++) {
                $scope.medidoresPot[i].checked = true;
            }
        }

        $scope.setActiveDay = function () {
            $scope.activeYear = '';
            $scope.activeMonth = '';
            $scope.activeWeek = '';
            $scope.activeDay = 'active';
            getApiDataDay();
        }

        $scope.setActiveDayPot = function () {
            console.log("hola");
            $scope.activeYearPot = '';
            $scope.activeMonthPot = '';
            $scope.activeWeekPot = '';
            $scope.activeDayPot = 'active';
            getApiDataDayPot();
        }


        $scope.setActiveWeek = function () {
            $scope.activeYear = '';
            $scope.activeMonth = '';
            $scope.activeWeek = 'active';
            $scope.activeDay = '';
            getApiDataWeek();
        }

        $scope.setActiveWeekPot = function () {
            $scope.activeYearPot = '';
            $scope.activeMonthPot = '';
            $scope.activeWeekPot = 'active';
            $scope.activeDayPot = '';
            getApiDataWeekPot();
        }

        $scope.setActiveMonth = function () {
            $scope.activeYear = '';
            $scope.activeMonth = 'active';
            $scope.activeWeek = '';
            $scope.activeDay = '';
            getApiDataMonth();
        }

        $scope.setActiveMonthPot = function () {
            $scope.activeYearPot = '';
            $scope.activeMonthPot = 'active';
            $scope.activeWeekPot = '';
            $scope.activeDayPot = '';
            getApiDataMonthPot();
        }

        $scope.setActiveYear = function () {
            $scope.activeYear = 'active';
            $scope.activeMonth = '';
            $scope.activeWeek = '';
            $scope.activeDay = '';
            getApiDataYear();
        }

        $scope.setActiveYearPot = function () {
            $scope.activeYearPot = 'active';
            $scope.activeMonthPot = '';
            $scope.activeWeekPot = '';
            $scope.activeDayPot = '';
            getApiDataYearPot();
        }

        function unsetActive() {
            $scope.activeYear = '';
            $scope.activeMonth = '';
            $scope.activeWeek = '';
            $scope.activeDay = '';
        }
        function unsetActivePot() {
            $scope.activeYearPot = '';
            $scope.activeMonthPot = '';
            $scope.activeWeekPot = '';
            $scope.activeDayPot = '';
        }

        $scope.consultaAvanzada = function () {
            $("#modalMedidores").modal("hide");
            unsetActive();
            var unidadId = localStorage.getItem("divisionId");
            $scope.loading = true;
            var data = {};
            data.id = unidadId;
            data.desde = $scope.desde;
            data.hasta = $scope.hasta;
            data.frecuencia = $scope.chkFrecuencia;
            data.medidores = setMedidoresArray($scope.medidores);
            $http({
                method: 'POST',
                url: `${$rootScope.APIURL}/api/medicioninteligente/consultaavanzada`,
                data: JSON.stringify(data)
            }).then(function (response) {
                $scope.apiData = response.data;
                $scope.loading = false;
                chart.options.scales.x.title.text = setXlabel($scope.chkFrecuencia);
                chart.data = $scope.apiData;
                chart.update();
               
            });


            //console.log(post);
        };

        $scope.consultaAvanzadaPot = function () {
            $("#modalMedidoresPot").modal("hide");
            unsetActivePot();
            var unidadId = localStorage.getItem("divisionId");
            $scope.loadingPot = true;
            var data = {};
            data.id = unidadId;
            data.desde = $scope.desdePot;
            data.hasta = $scope.hastaPot;
            data.frecuencia = $scope.chkFrecuenciaPot;
            data.medidores = setMedidoresArray($scope.medidoresPot);
            $http({
                method: 'POST',
                url: `${$rootScope.APIURL}/api/medicioninteligente/consultaavanzadapot`,
                data: JSON.stringify(data)
            }).then(function (response) {
                $scope.apiDataPot = response.data;
                $scope.loadingPot = false;
                chartPot.options.scales.x.title.text = setXlabel($scope.chkFrecuenciaPot);
                chartPot.data = $scope.apiDataPot;
                chartPot.update();
            });


            //console.log(post);
        };

        function setXlabel(frecuency) {
            var label = "";
            switch (frecuency) {
                case "15m":
                    label = "Hora";
                    break;
                case "1h":
                    label = "Hora";
                    break;
                case "1d":
                    label = "Día";
                    break;
                case "1M":
                    label = "Mes";
                default:
            }

            return label;
        }

        function openModal() {

            $("#modalMedidores").modal("show");
        }

        function openModalPot() {

            $("#modalMedidoresPot").modal("show");
        }

        function getApiData() {
            var unidadId = localStorage.getItem("divisionId");
            $scope.loading = true;
            $http.get(`${$rootScope.APIURL}/api/medicioninteligente/mediciones/${unidadId}`)
                .then(function (result) {
                    $scope.activeYear = 'active';
                    //console.log(result.data);
                    $scope.apiData = result.data;
                    $scope.loading = false;
                    InitiChart();
                });

            $http.get(`${$rootScope.APIURL}/api/medicioninteligente/medicionespot/${unidadId}`)
                .then(function (result) {
                    $scope.activeYearPot = 'active';
                    //console.log(result.data);
                    $scope.apiDataPot = result.data;
                    $scope.loadingPot = false;
                    InitiChartPot();
                });
           
            $http.get(`${$rootScope.APIURL}/api/medicioninteligente/medidores/${unidadId}`)
                .then(function (result) {
                    //console.log(result.data);
                    $scope.medidores = angular.copy(result.data);
                    $scope.medidoresPot = angular.copy(result.data);
                    $scope.checkAll = true;
                    $scope.checkAllPot = true;
                    setMedidoresChecked($scope.medidores);
                    setMedidoresChecked($scope.medidoresPot);
                });

        }

        function setMedidoresChecked(medidores) {
           
            for (var i = 0; i < medidores.length; i++) {
                medidores[i].checked = true;
            }
        }

        function setMedidoresArray(medidores) {

            var medidoresId = [];

            for (var i = 0; i < medidores.length; i++) {
                if (medidores[i].checked) {
                    medidoresId.push(medidores[i].id);
                }
            }

            return medidoresId;
        }

        function getApiDataWeek() {
            var unidadId = localStorage.getItem("divisionId");
            $scope.loading = true;
            $http.get(`${$rootScope.APIURL}/api/medicioninteligente/mediciones/semana/${unidadId}`)
                .then(function (result) {
                    //console.log(result.data);
                    $scope.apiData = result.data;
                    $scope.loading = false;
                    chart.options.scales.x.title.text = "Dia";
                    chart.data = $scope.apiData;
                    chart.update();
                });

        }

        function getApiDataWeekPot() {
            var unidadId = localStorage.getItem("divisionId");
            $scope.loadingPot = true;
            $http.get(`${$rootScope.APIURL}/api/medicioninteligente/medicionespot/semana/${unidadId}`)
                .then(function (result) {
                    //console.log(result.data);
                    $scope.apiDataPot = result.data;
                    $scope.loadingPot = false;
                    chartPot.options.scales.x.title.text = "Dia";
                    chartPot.data = $scope.apiData;
                    chartPot.update();
                });

        }

        function getApiDataDay() {
            var unidadId = localStorage.getItem("divisionId");
            $scope.loading = true;
            $http.get(`${$rootScope.APIURL}/api/medicioninteligente/mediciones/dia/${unidadId}`)
                .then(function (result) {
                    //console.log(result.data);
                    $scope.apiData = result.data;
                    $scope.loading = false;
                    chart.options.scales.x.title.text = "Hora";
                    chart.data = $scope.apiData;
                    chart.update();
                });

        }

        function getApiDataDayPot() {
            var unidadId = localStorage.getItem("divisionId");
            $scope.loadingPot = true;
            $http.get(`${$rootScope.APIURL}/api/medicioninteligente/medicionespot/dia/${unidadId}`)
                .then(function (result) {
                    //console.log(result.data);
                    $scope.apiDataPot = result.data;
                    $scope.loadingPot = false;
                    chartPot.options.scales.x.title.text = "Hora";
                    chartPot.data = $scope.apiDataPot;
                    chartPot.update();
                });

        }

        function getApiDataMonth() {
            var unidadId = localStorage.getItem("divisionId");
            $scope.loading = true;
            $http.get(`${$rootScope.APIURL}/api/medicioninteligente/mediciones/mes/${unidadId}`)
                .then(function (result) {
                    //console.log(result.data);
                    $scope.xAxisLabel = "Dia";
                    $scope.apiData = result.data;
                    $scope.loading = false;
                    chart.options.scales.x.title.text = "Dia";
                    chart.data = $scope.apiData;
                    chart.update();
                });

        }

        function getApiDataMonthPot() {
            var unidadId = localStorage.getItem("divisionId");
            $scope.loadingPot = true;
            $http.get(`${$rootScope.APIURL}/api/medicioninteligente/medicionespot/mes/${unidadId}`)
                .then(function (result) {
                    //console.log(result.data);
                    $scope.xAxisLabel = "Dia";
                    $scope.apiDataPot = result.data;
                    $scope.loadingPot = false;
                    chartPot.options.scales.x.title.text = "Dia";
                    chartPot.data = $scope.apiDataPot;
                    chartPot.update();
                });

        }

        function getApiDataYear() {
            var unidadId = localStorage.getItem("divisionId");
            $scope.loading = true;
            $http.get(`${$rootScope.APIURL}/api/medicioninteligente/mediciones/${unidadId}`)
                .then(function (result) {
                    $scope.apiData = result.data;
                    $scope.loading = false;
                    chart.options.scales.x.title.text = "Mes";
                    chart.data = $scope.apiData;
                    chart.update();
                });
        }

        function getApiDataYearPot() {
            var unidadId = localStorage.getItem("divisionId");
            $scope.loadingPot = true;
            $http.get(`${$rootScope.APIURL}/api/medicioninteligente/medicionespot/${unidadId}`)
                .then(function (result) {
                    $scope.apiDataPot = result.data;
                    $scope.loadingPot = false;
                    chartPot.options.scales.x.title.text = "Mes";
                    chartPot.data = $scope.apiDataPot;
                    chartPot.update();
                });
        }

        var chart;

        function InitiChart() {
            const ctx = document.getElementById('myChart').getContext('2d');
            chart = new Chart(ctx, {
                type: 'bar',
                data: $scope.apiData,
                options: {
                    plugins: {
                        tooltip: {
                            callbacks: {
                                label: function (context) {
                                    let label = context.dataset.label || '';

                                    if (label) {
                                        label += ': ';
                                    }
                                    if (context.parsed.y !== null) {
                                        label += new Intl.NumberFormat('es-CL', { style: 'decimal' }).format(context.parsed.y) + ' kWh';
                                    }
                                    return label;
                                }
                            }
                        },
                        title: {
                            display: true,
                            text: 'Energía'
                        },
                    },
                    responsive: true,
                    scales: {
                        x: {
                            stacked: true,
                            title: {
                                display: true,
                                text: "Mes"                                         
                            }
                        },
                        y: {
                            stacked: true,
                            title: {
                                display: true,
                                text: 'kWh'
                            }
                        }
                    }
                }
            });
        }

        var chartPot;

        function InitiChartPot() {
            const ctx = document.getElementById('myChartPot').getContext('2d');
            chartPot = new Chart(ctx, {
                type: 'line',
                data: $scope.apiDataPot,
                options: {
                    plugins: {
                        tooltip: {
                            callbacks: {
                                label: function (context) {
                                    let label = context.dataset.label || '';

                                    if (label) {
                                        label += ': ';
                                    }
                                    if (context.parsed.y !== null) {
                                        label += new Intl.NumberFormat('es-CL', { style: 'decimal' }).format(context.parsed.y) + ' kW';
                                    }
                                    return label;
                                }
                            }
                        },
                        title: {
                            display: true,
                            text: 'Potencia Máxima [kW]'
                        },
                    },
                    responsive: true,
                    scales: {
                        x: {
                            stacked: true,
                            title: {
                                display: true,
                                text: "Mes"
                            }
                        },
                        y: {
                            stacked: true,
                            title: {
                                display: true,
                                text: 'kW'
                            }
                        }
                    }
                }
            });
        }

    })