using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudBasedRMS.Services
{
 public   interface ICharts
    {
        void FoodItemsWiseSale(out string FoodItemsCountLists,out string FoodItemsLists);
    }
}
