angular.module('attendance', ['ui.bootstrap']);

angular.module('attendance').controller('DatepickerCtrl', function ($scope) {

    // set dt as today's date
    $scope.today = function () {
        $scope.dt = new Date();
    };
    $scope.today();

    // clear dt
    $scope.clear = function () {
        $scope.dt = null;
    };    
    
    $scope.format = 'dd-MMMM-yyyy';
});