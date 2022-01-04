using CloudBasedRMS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CloudBasedRMS.View.Controllers
{
    public class ChartsConcrete : ICharts
    {
        public BillFoodItemsServices billFoodItemsServices;
        public ChartsConcrete()
        {
            billFoodItemsServices = new BillFoodItemsServices();
        }
        public void FoodItemsWiseSale(out string FoodItemsCountLists, out string FoodItemsLists)
        {
            var  data= billFoodItemsServices.BillFoodItems.GetByAll().Where(x => x.Active == true).ToList();
            var fooditemsSaleCount = data.Select(x => x.FoodITemsDetails.Description).Distinct();
            List<decimal> fooditemrepo = new List<decimal>();
            foreach(var item in fooditemsSaleCount)
            {
                fooditemrepo.Add(data.Count(x => x.FoodITemsDetails.Description == item));
            }
            FoodItemsCountLists = string.Join(",",fooditemsSaleCount);
            FoodItemsLists = string.Join(",", fooditemrepo);

        }
    }
}