

var app = angular.module('myApp', ['ngMaterial', 'ngMessages'])
app.config(function ($mdThemingProvider) {
    var customBlueMap = $mdThemingProvider.extendPalette('blue-grey', {
        'contrastDefaultColor': 'light',
        'contrastDarkColors': ['50'],
        '50': 'ffffff'
    });
    $mdThemingProvider.definePalette('customBlue', customBlueMap);
    $mdThemingProvider.theme('default')
      .primaryPalette('customBlue', {
          'default': '700',
          'hue-1': '50'
      })
      .accentPalette('pink');
    $mdThemingProvider.theme('input', 'default')
          .primaryPalette('grey')
});

var SavedPathCtrl = app.controller('SavedPathCtrl', function ($scope, $http, $mdDialog, $mdMedia) {

    // Varible def
    $scope.savedPathsResult;
    $scope.noResults;

    $scope.init = function () {
        var res = $http.post('/Home/recieveLocations', JSON.stringify($scope.searchValues));
        res.success(function (data, status, headers, config) {
            $scope.savedPathsResult = data;
            if (data === "") {
                $scope.noResults = true;
            }
            else {
                $scope.noResults = false;
            }
        });
        res.error(function (data, status, headers, config) {
            alert("failure message: " + JSON.stringify({ data: data }));
        });
    };
});


var LoginCtrl = app.controller('LoginCtrl', function ($scope, $http, $mdDialog, $mdMedia) {
    $scope.userNameToLogin;
    $scope.PasswordToLogin;
    $scope.status = '  ';
    $scope.showCustom = function (event) {
            $mdDialog.show({
                clickOutsideToClose: true,
                scope: $scope,
                preserveScope: true,
                template: '<md-dialog aria-label="Login"  ng-cloak> ' +
                            '<form> <md-toolbar> <div class="md-toolbar-tools"> ' +
                            '<h2>Login</h2> <span flex></span> ' +
                            '<md-button class="md-icon-button" ng-click="closeDialog()"> ' +
                            'close </md-button> </div> </md-toolbar> ' +
                            '<md-dialog-content> <div class="md-dialog-content" layout="column"> ' +
                            ' <md-input-container> <label>User Name </label><input ng-model="userNameToLogin"></input> </md-input-container>' +
                            '<md-input-container> <label>Password </label> <input type="password"  ng-model="PasswordToLogin"> </md-input-container>' +
                            '<md-button class="md-primary md-raised" ng-click="tryToLogin()" flex="100" flex-gt-md="auto"> Submit </md-button> ' +
                            '</div> </md-dialog-content> </form> </md-dialog> ',
                controller: function DialogController($scope, $mdDialog) {
                    $scope.closeDialog = function () {
                        $mdDialog.hide();
                    }
                }
          });
    };


    $scope.tryToLogin = function () {
        $mdDialog.hide();
    };

    $scope.GoToSavedEvents = function () {
        alert("a");
        var res = $http.post('/Home/SavedEvents');
        res.success(function (data, status, headers, config) {

        });
        res.error(function (data, status, headers, config) {
            alert("failure message: " + JSON.stringify({ data: data }));
        });
    };

 });

var myCtrl = app.controller('myCtrl', function ($scope, $http, $mdToast) {

    $scope.init = function () {
        var res = $http.post('/Home/recieveLocations', JSON.stringify($scope.searchValues));
        res.success(function (data, status, headers, config) {
            $scope.states = data.map(function (state) {
                return { abbrev: state };
            });;
        });
        res.error(function (data, status, headers, config) {
            alert("failure message: " + JSON.stringify({ data: data }));
        });

        var res1 = $http.post('/Home/recieveTypes', JSON.stringify($scope.searchValues));
        res1.success(function (data, status, headers, config) {
            $scope.EventTypes = data.map(function (EventType) {
                return { abbrev: EventType };
            });
        });
        res1.error(function (data, status, headers, config) {
            alert("failure message: " + JSON.stringify({ data: data }));
        });
    }
    // runs once per controller instantiation
    $scope.init();


    $scope.numOfEvents = 1;
    $scope.maxNumOfEvents = 6;

    $scope.results;
    $scope.noResults = false;
    $scope.goToSearch = function () {
        var res = $http.post('/Home/search', null);
    }
    $scope.AddEventToSearch = function () {
        if ($scope.numOfEvents < $scope.maxNumOfEvents) {
            $scope.numOfEvents = $scope.numOfEvents + 1;
        }
    }
    $scope.hey = function () {
        alert("a");
    };
    $scope.RemoveEventToSearch = function (x) {
        if ($scope.numOfEvents > 1) {
            $scope.numOfEvents = $scope.numOfEvents - 1;
        }
        $scope.userEventVals.splice(x, 1);
        $scope.userFreeTextVals.splice(x, 1);
    }

    $scope.openToast = function ($event) {
        $mdToast.show($mdToast.simple().textContent('Path saved!'));
        // Could also do $mdToast.showSimple('Hello');
    };

    $scope.getNumber = function (num) {
        return new Array(num);
    }

    $scope.userEventVals = [];
    $scope.userFreeTextVals = [];

    $scope.search1 = function () {
        $scope.searchValues = {
            location: $scope.location,
            startDate: $scope.startDate,
            endDate: $scope.endDate,
            EventType: $scope.userEventVals,
            freeText: $scope.userFreeTextVals,
        };

        var res = $http.post('/Home/searchRes', JSON.stringify($scope.searchValues));
        res.success(function (data, status, headers, config) {
            $scope.results = data;
            if (data === "") {
                $scope.noResults = true;
            }
            else {
                $scope.noResults = false;
            }
        });
        res.error(function (data, status, headers, config) {
            alert("failure message: " + JSON.stringify({ data: data }));
        });
    }

    $scope.AddEventField = function () {
        $scope.numOfEvents = $scope.numOfEvents + 1;
    }
    $scope.location = 'CA';
    $scope.states = ('AL AK AZ AR CA CO CT DE FL GA HI ID IL IN IA KS KY LA ME MD MA MI MN MS ' +
  'MO MT NE NV NH NJ NM NY NC ND OH OK OR PA RI SC SD TN TX UT VT VA WA WV WI ' +
  'WY').split(' ').map(function (state) {
      return { abbrev: state };
  });

    $scope.selectEventTypes = 'Music';
    $scope.EventTypes = ('Music Show Sports').split(' ').map(function (EventType) {
        return { abbrev: EventType };
    });
});

