using System.Collections.Generic;
namespace CloudBasedRMS.View.Controllers.ViewModel
{
    using Core;
    public class DashboardViewModel
    {
        public FoodItems_Details FoodItems { get; set; }
        public List<BillFoodItems> BillFoods { get; set; }
        public TwoDimensionalData TwoDimensionalData { get; set; }
        public ChartModel ChartModel { get; set; }
        public List<OrderMaster> OrderMaster { get; set; }
        public List<PopularFoodTotalPriceModel> pmodel { get; set; }
        public List<Tables> tablelist { get; set; }
        public List<KOTPickUp> KOTPickUp { get; set; }
    }
}