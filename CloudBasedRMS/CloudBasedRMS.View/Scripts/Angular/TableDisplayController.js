
angular.module("myapplication").controller("TableController", function ($scope, UserService) {
    $scope.User = null;
    UserService.getUsers().then(function (d) {
        $scope.User = d.data; 
    },function(){
        alert("error occured try again");
    });
})
.factory("UserService", function ($http) {
    var fact = {};
    fact.getUsers = function () {
        return $http.get('/Users/getUsers');
    }
    return fact;
});
    