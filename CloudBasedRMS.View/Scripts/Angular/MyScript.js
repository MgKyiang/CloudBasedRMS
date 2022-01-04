/// <reference path="../angular.js" />
//(function () {
//    //Module 
//    var app = angular.module('akog02App', []);
//    // Controller
//    app.controller('akog02Controller', function ($scope) {
//        $scope.Message = "Congratulation you have created your first application using AngularJs";
//    });
//})();
var MyApp = angular.module('akog02App', []);
MyApp.controller('akog02Controller', function ($scope, EmployeeService) { //inject $scope and RecordServce.
    $scope.Employee = null;
    EmployeeService.GetRecord().then(function (d) {
        $scope.Employee = d.data; // Success
        alert(d.data);
    }, function () {
        alert('Failed'); // Failed
    });
});
//The concept of the factory service is same as service layer in ASP.NET MVC Application.
MyApp.factory('EmployeeService', function ($http) {
    var fac = {};
    //GetRecord function will call the GetStudentRecord action method.
    fac.GetRecord = function () {
        return $http.get('/Employee/GetAllEmployee');
    }
    return fac;
});