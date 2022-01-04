using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudBasedRMS.GenericRepositories;
namespace CloudBasedRMS.Services
{
  public  class OrderItemsServices:BaseServices
    {
        public IOrderItemsRepository OrderItems { get; private set; }
        public IOrderMasterRepository OrderMaster { get; private set; }
        public IFoodItems_DetailsRepository FoodItems_Details { get; private set; }
        public ICategoryRepository Category { get; private set; }
        public OrderItemsServices()
        {
            OrderItems = _unitOfWork.OrderItems;
            OrderMaster = _unitOfWork.OrderMaster;
            FoodItems_Details = _unitOfWork.FoodItems_Details;
            Category = _unitOfWork.Categories;
        }
    }
}
