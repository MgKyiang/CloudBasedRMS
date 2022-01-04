using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudBasedRMS.GenericRepositories;
namespace CloudBasedRMS.Services
{
   public class FoodItems_DetailsServices:BaseServices
    {
        public IFoodItems_DetailsRepository FoodItems_Details { get;private set; }
        public ICategoryRepository Category { get;  private set; }
        public IKitchenRepository Kitchen { get; set; }
        public FoodItems_DetailsServices()
        {
            FoodItems_Details = unitOfWork.FoodItems_Details;
            Category = unitOfWork.Categories;
            Kitchen = unitOfWork.Kitchen;
        }
    }
}
