app.service("EmployeeService", function ($http) {
    this.getEmployee = function () {
        debugger;
        return $http.get("/Employee/GetAllEmployee");
    };
});