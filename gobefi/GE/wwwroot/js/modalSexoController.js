var app = angular.module('modalSexoApp', []);
app.controller("modalController", function ($scope, $http) {

    $scope.updateUser = {};

    $scope.updateSexo = function () {
        var userId = '@UserManager.GetUserId(User)'
        $http({
            method: 'PUT',
            url: "/actualizaSexo?userId=" + userId + "&sexoId=" + $scope.updateUser.sexoId,
            data: JSON.stringify($scope.updateUser)
        }).then(function (response) {
            angular.element('#modalSexo').modal('hide');
            $scope.updateUser = {};
            $scope.modalSexoForm.$setPristine().$setUntouched;
        });
    }

});