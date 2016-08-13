

var app = angular.module('myApp', []);
app.controller('myCtrl', function ($scope) {

    $scope.numOfEvents = 1;
    var resultsDB = [{
        country: 'berlin',
        typeOfEvent: 'preform',
        startDate: '15/02/16',
        endDate: '18/02/16',
        price: '256$'
    },
{
    country: 'brazil',
    typeOfEvent: 'soccer',
    startDate: '03/03/16',
    endDate: '07/03/16',
    price: '375$'
}];


    $scope.results = resultsDB;
    $scope.isResults = false;
    $scope.searchValues = {
        startDate: $scope.startDate,
        endDate: $scope.endDate,
        EventType: $scope.EventType
    };

    $scope.search = function () {
        $scope.isResults = true;

        var res = $http.post('/savecompany_json', dataObj);
        res.success(function (data, status, headers, config) {
            $scope.results = data;
        });
        res.error(function (data, status, headers, config) {
            alert("failure message: " + JSON.stringify({ data: data }));
        });
    }

    $scope.AddEventField = function () {
        $scope.numOfEvents = $scope.numOfEvents + 1;
    }
});

