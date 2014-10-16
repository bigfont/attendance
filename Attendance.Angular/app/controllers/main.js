﻿var app = angular.module('attendance', ['ui.bootstrap']);

app.controller('DatepickerCtrl', function ($scope) {

    // set dt as today's date
    $scope.today = function () {
        $scope.dt = new Date();
    };
    $scope.today();

    $scope.format = 'dd-MMMM-yyyy';
});

app.controller('PersonCtrl', function ($scope, $http) {

    var personApiUrl = "http://attendance1-api.azurewebsites.net/api/person";

    function initNewPerson() {
        $scope.newPerson = { First: '', Last: '' };
    }
    initNewPerson();

    $scope.add = function () {
        var newPerson = {
            FirstName: $scope.newPerson.First,
            LastName: $scope.newPerson.Last
        };

        $http.post(personApiUrl, newPerson)
            .success(function (data, status, headers, config) { })
            .error(function (data, status, headers, config) { });

        // refresh ui
        $scope.persons.push(newPerson);
        initNewPerson();
    };

    $scope.persons = [
        { FirstName: "Noah" },
        { FirstName: "Marley" },
        { FirstName: "Anjali" },
        { FirstName: "Jaysen" },
    ];

    $http.get(personApiUrl)
        .success(function (data, status, headers, config) {
            $scope.persons = data;
        })
        .error(function (data, status, headers, config) { });
});

app.controller('EventCtrl', function ($scope) {

    $scope.events = [
        { Name: "Conjuring Club" },
        { Name: "Internet of Things" }
    ];

});
