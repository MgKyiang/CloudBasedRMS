app.controller("BillFoodItemsController", function ($scope, BillFoodItemsService) {
    GetBillFoodItems();

    function GetBillFoodItems() {
        debugger;
        var getBillFoodItems = BillFoodItemsService.getBillFoodItems();
            getBillFoodItems.then(function (result) {
            $scope.BillFoodItems = result.data;
        }, function () {
            alert('Data not found');
        });
    }
});