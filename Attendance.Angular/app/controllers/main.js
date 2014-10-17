﻿(function () {

    /* use strict */

    var app = angular.module('attendance', ['ui.bootstrap']);

    //#region TODO Set this as a app wide constant or service somewhere
    var apiBaseUrl = "http://attendance1-api.azurewebsites.net/api";
    if (window.location.href.indexOf('localhost') >= 0) {
        var apiBaseUrl = 'http://localhost/attendance.webapi/api'
    }
    //#endregion

    app.controller('VisitCtrl', function ($scope, $http) {

        var visitApiUrl = apiBaseUrl + "/visit";

        // child controls will populate these 
        // do we need to instantiate them here?
        $scope.persons = {};
        $scope.events = {};
        $scope.selectedEvent = {};
        $scope.visitDateTime = {};

        $scope.saveVisits = function () {

            var visits,
                selectedEventId,
                date,
                jsTimestampe,
                iso8601Date,
                selectedDateTime;

            visits = [];
            selectedEventId = $scope.selectedEvent.Id;                        

            date = new Date($scope.visitDateTime);
            jsTimestamp = date.getTime();
            iso8601Date = date.toJSON();
            selectedDateTime = iso8601Date;

            // iterate persons and add selected ones
            angular.forEach($scope.persons, function (value, key) {
                if (value.Selected) {
                    var visit = {
                        PersonId: value.Id,
                        EventId: selectedEventId,
                        DateTime: selectedDateTime
                    };
                    visits.push(visit);
                }
            });

            console.table(visits);

            $http.post(visitApiUrl, visits)
                .success(function (data, status, headers, config) {
                    console.log('success');
                })
                .error(function (data, status, headers, config) {
                    console.table(data);
                });

        };

    });

    app.controller('DatepickerCtrl', function ($scope) {

        // set dt as today's date
        $scope.today = function () {
            $scope.$parent.visitDateTime = new Date();
        };
        $scope.today();
        $scope.format = 'dd-MMMM-yyyy';

    });

    app.controller('PersonCtrl', function ($scope, $http) {        

        var personApiUrl = apiBaseUrl + "/person";

        function initNewPerson() {
            $scope.newPerson = { First: '', Last: '', Id: null };
        }
        initNewPerson();

        $scope.addPerson = function () {
            var newPerson = {
                FirstName: $scope.newPerson.First,
                LastName: $scope.newPerson.Last,
            };

            $http.post(personApiUrl, newPerson)
                .success(function (data, status, headers, config) {
                    data.Selected = true;
                    $scope.persons.push(data);
                    initNewPerson();
                })
                .error(function (data, status, headers, config) { });
        };

        $http.get(personApiUrl)
            .success(function (data, status, headers, config) {

                angular.forEach(data, function (value, key) {
                    value.Selected = false;
                });

                $scope.$parent.persons = data;
            })
            .error(function (data, status, headers, config) { });
    });

    app.controller('EventCtrl', function ($scope, $http) {

        var eventApiUrl = apiBaseUrl + "/event";

        $http.get(eventApiUrl)
            .success(function (data, status, headers, config) {
                $scope.$parent.events = data;
                $scope.$parent.selectedEvent = $scope.$parent.events[0];
                console.log('success');
            })
            .error(function (data, status, headers, config) {
                console.log('error');
            });

        //$scope.$parent.events = [
        //    { Id: 1, Name: "Conjuring Club", Selected: true },
        //    { Id: 2, Name: "Internet of Things", Selected: false }
        //];

    });

}());
