var app = angular.module('attendance', ['ui.bootstrap']);

app.controller('DatepickerCtrl', function ($scope) {

    // set dt as today's date
    $scope.today = function () {
        $scope.dt = new Date();
    };
    $scope.today();
    
    $scope.format = 'dd-MMMM-yyyy';
});

app.controller('PersonCtrl', function ($scope) {

    $scope.persons = [
        { FirstName:"Noah" },
        { FirstName: "Marley" },
        { FirstName: "Anjali" },
        { FirstName: "Jaysen" },
    ];

});

app.controller('EventCtrl', function ($scope) {

    $scope.events = [
        { Name: "Conjuring Club" },
        { Name: "Internet of Things" }
    ];

});