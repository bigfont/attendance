﻿(function () {

    /* use strict */

    var app = angular.module('attendance', ['ui.bootstrap']);

    //#region TODO Set this as a app wide constant or service somewhere
    var apiBaseUrl = "http://attendance1-api.azurewebsites.net/api";
    if (window.location.href.indexOf('localhost') >= 0) {
        var apiBaseUrl = 'http://localhost/attendance.webapi/api'
    }
    //#endregion

    app.controller('VisitCtrl', ['$scope', '$http', function ($scope, $http) {

        var visitApiUrl = apiBaseUrl + "/visit";

        // child controls will populate these 
        // do we need to instantiate them here?
        $scope.persons = {};
        $scope.events = {};
        $scope.selectedEvent = {};
        $scope.selectedPersons = {};
        $scope.visitDateTime = {};

        $scope.saveVisits = function () {

            var visits,
                selectedEventId,
                selectedDate,
                selectedDateAtNoon,                
                iso8601Date,
                selectedDateTime;

            visits = [];
            selectedEventId = $scope.selectedEvent.Id;

            selectedDateAtNoon = new Date($scope.visitDateTime); 
            selectedDateAtNoon.setHours(0, 0, 0, 0); // midnight on the morning of the selected date
            selectedDateAtNoonInIso8601Format = selectedDateAtNoon.toJSON();

            console.log(selectedDateAtNoonInIso8601Format);

            // iterate persons and add selected ones
            angular.forEach($scope.persons, function (value, key) {
                if (value.Selected) {
                    var visit = {
                        PersonId: value.Id,
                        EventId: selectedEventId,
                        DateTime: selectedDateAtNoonInIso8601Format
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

    }]);

    app.controller('DatepickerCtrl', ['$scope', function ($scope) {

        // set dt as today's date
        $scope.today = function () {
            $scope.$parent.visitDateTime = new Date();
        };
        $scope.today();
        $scope.format = 'dd-MMMM-yyyy';

    }]);

    app.controller('PersonCtrl', ['$scope', '$http', function ($scope, $http) {

        var personApiUrl = apiBaseUrl + "/person";

        function initNewPerson() {
            $scope.newPerson = { First: '', Last: '', Id: null };
        }
        initNewPerson();

        $scope.postPerson = function () {
            var newPerson = {
                FirstName: $scope.newPerson.First,
                LastName: $scope.newPerson.Last,
            };

            $http.post(personApiUrl, newPerson)
                .success(function (data, status, headers, config) {
                    data.Selected = true;
                    $scope.$parent.persons.push(data);
                    initNewPerson();
                })
                .error(function (data, status, headers, config) { });
        };

        $scope.putPerson = function (person) {
            $http.put(personApiUrl, person)
                .success(function (data, status, headers, config) {
                    person.edit = false;
                })
                .error(function (data, status, headers, config) {
                    console.log('error');
                });
        }

        $scope.deletePerson = function (person) {
            $http.delete(personApiUrl + '/' + person.Id)
                .success(function (data, status, headers, config) {
                    var index = $scope.$parent.persons.indexOf(person);
                    $scope.$parent.persons.splice(index, 1);
                })
                .error(function (data, status, headers, config) {
                    console.log('error');
                });
        }


        $http.get(personApiUrl)
            .success(function (data, status, headers, config) {

                angular.forEach(data, function (value, key) {
                    value.Selected = false;
                });

                $scope.$parent.persons = data;
            })
            .error(function (data, status, headers, config) { });
    }]);

    app.controller('EventCtrl', ['$scope', '$http', function ($scope, $http) {

        var eventApiUrl = apiBaseUrl + "/event";

        function setSelectedEvent(index) {
            $scope.$parent.selectedEvent = $scope.$parent.events[index];
        }

        function initNewEvent() {
            $scope.newEvent = { Name: '' };
        }
        initNewEvent();

        $scope.postEvent = function () {
            var newEvent = {
                Name: $scope.newEvent.Name
            };

            $http.post(eventApiUrl, newEvent)
                .success(function (data, status, headers, config) {
                    $scope.$parent.events.push(data);
                    setSelectedEvent($scope.events.length - 1);
                    initNewEvent();
                })
                .error(function (data, status, headers, config) { });
        };

        $scope.putEvent = function (event) {
            $http.put(eventApiUrl, event)
                .success(function (data, status, headers, config) {
                    event.edit = false;
                })
                .error(function (data, status, headers, config) {
                    console.log('error');
                });
        }

        $scope.deleteEvent = function (event) {
            $http.delete(eventApiUrl + '/' + event.Id)
                .success(function (data, status, headers, config) {
                    var index = $scope.$parent.events.indexOf(event);
                    $scope.$parent.events.splice(index, 1);
                })
                .error(function (data, status, headers, config) {
                    console.log('error');
                });
        }

        $http.get(eventApiUrl)
            .success(function (data, status, headers, config) {
                $scope.$parent.events = data;
                setSelectedEvent(0);
                console.log('success');
            })
            .error(function (data, status, headers, config) {
                console.log('error');
            });
    }]);

    app.controller('StatisticsCtrl', ['$scope', '$http', function ($scope, $http) {

        var statisticsApiUrl = apiBaseUrl + "/statistics";

        $scope.visits = {};

        $scope.monthNames = [
            'January',
            'February',
            'March',
            'April',
            'May',
            'June',
            'July',
            'August',
            'September',
            'October',
            'November',
            'December'
        ];

        // get the total number of visits for each event over its lifetime
        function getVisitsSinceInception() {

            $http.get(statisticsApiUrl + '/visits/inception')
                .success(function (data, status, headers, config) {
                    $scope.visits.all = data;
                    console.log('success');
                })
                .error(function (data, status, headers, config) {
                    console.log('error');
                });

        }

        function getVisitsByMonth() {
            $http.get(statisticsApiUrl + '/visits/month')
                .success(function (data, status, headers, config) {
                    $scope.visits.month = data;
                    console.log('success');
                })
                .error(function (data, status, headers, config) {
                    console.log('error');
                });
        }

        function getVisitsComprehensive() {
            $http.get(statisticsApiUrl + '/visits/comprehensive')
                .success(function (data, status, headers, config) {
                    $scope.visits.comprehensive = data;
                    console.log('success');
                })
                .error(function (data, status, headers, config) {
                    console.log('error');
                });
        }

        // TODO: $on and $emit only work if the emiting controller is a child of the listening controller
        // so, we'll need to do this through a shared service instead
        // see also http://stackoverflow.com/questions/11252780/whats-the-correct-way-to-communicate-between-controllers-in-angularjs
        $scope.$on("updateStatistics", function (event, args) {

            window.alert('hello');
            getVisitsSinceInception();
            getVisitsByMonth();
            getVisitsComprehensive();

        });

        // TODO: consider making these one network call not two
        getVisitsSinceInception();
        getVisitsByMonth();
        getVisitsComprehensive();

    }]);

}());
