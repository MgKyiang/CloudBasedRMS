app.service("BillFoodItemsService", function ($http) {
    this.getBillFoodItems = function () {
        debugger;
        return $http.get("/Dashboard/GetBillFoodItems");
    };
});