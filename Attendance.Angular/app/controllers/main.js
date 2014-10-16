var app = angular.module('attendance', ['ui.bootstrap']);

app.controller('DatepickerCtrl', function ($scope) {

    // set dt as today's date
    $scope.today = function () {
        $scope.dt = new Date();
    };
    $scope.today();
    
    $scope.format = 'dd-MMMM-yyyy';
});

app.controller('PersonCtrl', function ($scope, $http) {

    function initNewPerson()
    {
        $scope.newPerson = { First: '', Last: '' };
    }
    initNewPerson();

    $scope.add = function () {

        // create new person
        var newPerson = {
            FirstName: $scope.newPerson.First, 
            LastName: $scope.newPerson.Last 
        };

        // post new person
        var postUrl = 'http://attendance1-api.azurewebsites.net/api/person';
        var postData = JSON.stringify(newPerson);
        $http({ url: postUrl, data: postData, method:'POST' })
            .success(function () {
                window.alert("success");
            })
            .error(function (data, status, headers, config) {               
                window.alert("error");
            });

        // refresh to ui
        $scope.persons.push(newPerson);
        initNewPerson();
    };

    $scope.persons = [
        { FirstName: "Noah" },
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
