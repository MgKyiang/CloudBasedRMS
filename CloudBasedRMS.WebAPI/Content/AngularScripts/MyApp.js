(function () {
    //Create a Module 
    var app = angular.module('MyApp', ['ngRoute']);  // Will use ['ng-Route'] when we will implement routing
    //Create a Controller
    app.controller('HomeController', function ($scope) {  // here $scope is used for share data between view and controller
        $scope.Message = "Cloud Based RMS is a Real Online Restaurant  Management System for servicing Table and Delivery service for your hangury stage.";
    });
})();