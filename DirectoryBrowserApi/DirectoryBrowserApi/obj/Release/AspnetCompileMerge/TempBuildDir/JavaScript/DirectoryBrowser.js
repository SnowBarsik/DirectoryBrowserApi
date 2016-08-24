//angulaJs controller directory borwser

(function () {

    var app = angular.module("mainApp", []);

    var DirectoryBrowser = function ($scope, $http, $q) {

        var canceller;

        var getRoot = function() {
            $http.get("http://localhost:53349/api/directories")
                .then(onRoot, onError);
        };

        var getDir = function(id) {
            $http.get("http://localhost:53349/api/directories/" + id)
                .then(onDir, onError);
        };

        var getCounter = function (id) {
            if (canceller) canceller.resolve();
            canceller = $q.defer();
            $http.get("http://localhost:53349/api/counter/" + id, { timeout: canceller.promise })
                .then(onCounter, onCountError);
        };

        $scope.explore = function (id) {
            $scope.fileCounter = null;
            getDir(id);
            getCounter(id);
        };

        $scope.start = getRoot;

        var onCounter = function(response) {
            $scope.fileCounter = response.data;
        };

        var onRoot = function (response) {
            $scope.directory = response.data;
            hideError();
        };

        var onDir = function(response) {
            $scope.directory = response.data;
            hideError();
        };

        var onError = function(response) {
            $scope.errors = response.data;
        };

        var onCountError = function(response) {
            $scope.counterErrors = response.data;
        };

        var hideError = function() {
            $scope.errors = null;
        };

        getRoot();

    };


    app.controller("DirectoryBrowser", DirectoryBrowser);

}());

