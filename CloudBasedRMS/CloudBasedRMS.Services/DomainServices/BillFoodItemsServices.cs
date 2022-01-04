using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudBasedRMS.GenericRepositories;
namespace CloudBasedRMS.Services
{
public    class BillFoodItemsServices:BaseServices
    {
        public IBillFoodItemsRepository BillFoodItems { get; set; }
        public IFoodItems_DetailsRepository FoodItems { get; set; }
        public BillFoodItemsServices()
        {
            BillFoodItems = _unitOfWork.BillFoodItems;
            FoodItems = _unitOfWork.FoodItems_Details;
        }
    }
}
