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
        public SaleBillServices(){
            SaleBill = unitOfWork.SaleBill;
            Employee = unitOfWork.Employee;
            OrderMaster = unitOfWork.OrderMaster;
            Customer = unitOfWork.Customer;
            FoodItems = unitOfWork.FoodItems_Details;
            OrderItems = unitOfWork.OrderItems;
            BillFoosItems = unitOfWork.BillFoodItems;
        }
    }
}
