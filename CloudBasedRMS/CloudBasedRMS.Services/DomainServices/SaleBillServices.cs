using CloudBasedRMS.GenericRepositories;
namespace CloudBasedRMS.Services
{
    public  class SaleBillServices:BaseServices
    {
        public ISaleBillRepository SaleBill { get; set; }
        public IEmployeeRepository Employee { get; set; }
        public IOrderMasterRepository OrderMaster { get; set; }
        public ICustomerRepository Customer { get; set; }
        public IFoodItems_DetailsRepository FoodItems { get; set; }
        public IOrderItemsRepository OrderItems { get; set; }
        public IBillFoodItemsRepository BillFoosItems { get; set; }
        public SaleBillServices()
        {
            SaleBill = _unitOfWork.SaleBill;
            Employee = _unitOfWork.Employee;
            OrderMaster = _unitOfWork.OrderMaster;
            Customer = _unitOfWork.Customer;
            FoodItems = _unitOfWork.FoodItems_Details;
            OrderItems = _unitOfWork.OrderItems;
            BillFoosItems = _unitOfWork.BillFoodItems;
        }
    }
}
