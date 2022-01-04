using CloudBasedRMS.GenericRepositories;

namespace CloudBasedRMS.Services.DomainServices
{
    public class KitchenServices : BaseServices
    {
        public IKitchenRepository Kitchen { get; set; }
        public IEmployeeRepository Employee { get; set; }
        public IOrderItemsRepository OrderItems { get; set; }
        public IFoodItems_DetailsRepository FoodItems { get; set; }
        public IOrderMasterRepository OrderMaster { get; set; }
        public KitchenServices()
        {
            Kitchen = _unitOfWork.Kitchen;
            Employee = _unitOfWork.Employee;
            OrderItems = _unitOfWork.OrderItems;
            FoodItems = _unitOfWork.FoodItems_Details;
            OrderMaster = _unitOfWork.OrderMaster;
        }
    }
}
