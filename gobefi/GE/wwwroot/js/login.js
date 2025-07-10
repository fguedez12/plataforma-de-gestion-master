angular.module("loginApp", [])
    .controller("loginController", function ($scope, $rootScope, $http, $window) {
        $scope.btnText = 'Ingresar';
        $scope.loading = false;
        $scope.errorLogin = false;

        $http({
            method: 'GET',
            url: `/settings?sectionName=ApiConfiguration&paramName=apiGestionaEnergia`
            
        }).then(function (response) {
            //console.log("ReadDataFromAppSettings", response);
            $rootScope.APIURL = response.data;
            
        })

        $scope.loginApi = function () {
            $scope.loading = true;
            $scope.btnText = '...Cargando';
            //console.log("click");
            $http({
                method: 'POST',
                url: `${$rootScope.APIURL}/api/account/login`,
                data: JSON.stringify($scope.userCredentials)
            }).then(function (response) {
               
                $window.localStorage["token"] = response.data.token;
                console.log(response);
                $("#login-form").submit();
                //$scope.btnText = 'Ingresar';
                //$scope.loading = false;
            }, error => {
                $scope.btnText = 'Ingresar';
                $scope.loading = false;
                $scope.errorLogin = true;
            })
        }
    })