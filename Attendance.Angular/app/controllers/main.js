var app = angular.module('attendance', ['ui.bootstrap']);

app.controller('VisitCtrl', function ($scope, $http) {

    $scope.persons = {}; // PersonCtrl will access this too.

    $scope.saveVisits = function () {

        console.log('saving');
        console.table($scope.persons); // .table works in firefox 34
        console.table($scope.events);

        var visits = [];

        angular.forEach($scope.persons, function (value, key) {
            if (value.Selected)
            {
                var visit = {
                    PersonId: value.Id,
                    EventId: ''
                };
                visits.push();
            }
        });



    };

});

app.controller('DatepickerCtrl', function ($scope) {

    // set dt as today's date
    $scope.today = function () {
        $scope.dt = new Date();
    };
    $scope.today();

    $scope.format = 'dd-MMMM-yyyy';
});

app.controller('PersonCtrl', function ($scope, $http) {

    //#region TODO Set this as a app wide constant or service somewhere
    var apiBaseUrl = "http://attendance1-api.azurewebsites.net/api";
    if (window.location.href.indexOf('localhost') >= 0) {
        var apiBaseUrl = 'http://localhost/attendance.webapi/api'
    }
    //#endregion

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

app.controller('EventCtrl', function ($scope) {

    $scope.$parent.events = [
        { Name: "Conjuring Club", Selected: true },
        { Name: "Internet of Things", Selected: false }
    ];

    $scope.$parent.selectedEvent = $scope.$parent.events[0];

});
