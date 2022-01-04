(function () {
    //Create a Module 
            var app =angular.module('MyApp', ['ngRoute']);  // Will use ['ng-Route'] when we will implement routing
            app.controller('AccountController', function ($scope, AddressService) { //inject AddressService
                $scope.Address = null;
                AddressService.GetAllAddress().then(function (d) {
                    $scope.Address = d.data; // Success
                }, function () {
                    alert('Failed'); // Failed
                });
            })
            app.factory('AddressService', function ($http) { // here I have created a factory which is a populer way to create and configure services
                var fac = {};
                fac.GetAllAddress = function () {
                    return $http.get('/Account/getAddress');
                }
                return fac;
            });
            })();