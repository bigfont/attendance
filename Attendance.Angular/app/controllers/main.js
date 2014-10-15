angular.module('attendance', ['ui.bootstrap']);

angular.module('attendance').controller('DatepickerCtrl', function ($scope) {

    // set dt as today's date
    $scope.today = function () {
        $scope.dt = new Date();
    };
    $scope.today();
    
    $scope.format = 'dd-MMMM-yyyy';
});