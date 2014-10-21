(function () {

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
        $scope.selectedPersons = {};
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
                    $scope.$parent.persons.push(data);
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
                    var index = $scope.events.indexOf(event);
                    $scope.events.splice(index, 1);
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
    });

    app.controller('StatisticsCtrl', function ($scope, $http) {

        var statisticsApiUrl = apiBaseUrl + "/statistics";

        $scope.visits = {};

        function getAllVisits() {

            $http.get(statisticsApiUrl + '/visits/all')
                .success(function (data, status, headers, config) {
                    $scope.visits.all = data;
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
            getAllVisits();

        });

        getAllVisits();

    });

}());
