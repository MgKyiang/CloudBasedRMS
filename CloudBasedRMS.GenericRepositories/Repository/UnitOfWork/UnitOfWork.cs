using CloudBasedRMS.Core;
using System;
using System.Data.Entity;
namespace CloudBasedRMS.GenericRepositories
{
    public class UnitOfWork : IDisposable
    {
        //private variables ApplicaitonDbContext
        private ApplicationDbContext dbContext;

        //DbContextTransaction
        private DbContextTransaction dbContextTransaction;

        //Administration Interface properties
        public IAuthorizationsRepository Authorizations { get; private set; }
        public ITransactionLogRepository Logs { get; private set; }
        public IUserInRoleRepository UserInRole { get; private set; }
        public IErrorLogRepository ErrorLogs { get; private set; }
        public IApplicationSettingRepository ApplicationSettings { get; private set; }
        public IUsersMemberRepository UsersMember { get; set; }

        public IAPIAuthorizationRepository APIAuthorization { get; set; }
        public IContactUsRepository ContactUs { get; set; }
        //Business domain Interface properties
        //Set Up
        public ICategoryRepository Categories { get; private set; }
        public IVehicleTypeRepository VehicleTypes { get; private set; }

        public IAddressRepository Address { get; private set; }
        public IRankRepository Rank { get; set; }
        public IEventRepository Event { get; set; }
        public IBillFoodItemsRepository BillFoodItems { get; set; }
        public IRestaurantProfileRepository RestaurantProfile { get; set; }

        public IKitchenRepository Kitchen { get; set; }
        //Flat 
        public IVehicleRepository Vehicles { get; private set; }
        public IFoodItems_DetailsRepository FoodItems_Details { get; private set; }
        public IEmployeeRepository Employee { get; private set; }
        public ITableRepository Table { get; private set; }
        public ICustomerRepository Customer { get; private set; }
        public ISaleBillRepository SaleBill { get; set; }
        public ISupplierRepository Supplier { get; set; }
        //Transction
        public IOrderMasterRepository OrderMaster { get; private set; }
        public IOrderItemsRepository OrderItems { get; private set; }

        public IKOTPickUpRepository KOTPickUp { get; set; }
        //Constructor of UnitOfWork
        public UnitOfWork()
        {
            this.dbContext = new ApplicationDbContext();
            Authorizations = new AuthorizationsRepository(dbContext);
            Logs = new TransactionLogRepository(dbContext);
            ErrorLogs = new ErrorLogRepository(dbContext);
            UserInRole = new UserInRoleRepository(dbContext);
            ApplicationSettings = new ApplicationSettingRepository(dbContext);
            UsersMember = new UsersMemberRepsitory(dbContext);
            ContactUs = new ContactUsRepository(dbContext);
            APIAuthorization = new APIAuthorizationRepository(dbContext);
            //Business domain class here
            //SetUp 
            Categories = new CategoryRepository(dbContext);
            VehicleTypes = new VehicleTypeRepository(dbContext);
            Address = new AddressRepository(dbContext);
            Rank = new RankRepository(dbContext);
            Event = new EventRepository(dbContext);
            BillFoodItems = new BillFoodItemsRepository(dbContext);
            RestaurantProfile = new RestaurantProfileRepository(dbContext);
            Kitchen = new KitchenRepository(dbContext);
            //Flat
            Vehicles = new VehicleRepository(dbContext);
            FoodItems_Details = new FoodItems_DetailsRepository(dbContext);
            Employee = new EmployeeRepository(dbContext);
            Table = new TableRepository(dbContext);
            Customer = new CustomerRepository(dbContext);
            SaleBill = new SaleBillRepository(dbContext);
            Supplier = new SupplierRepository(dbContext);
            //Transaction
            OrderMaster = new OrderMasterRepository(dbContext);
            OrderItems = new OrderItemsRepository(dbContext);
            KOTPickUp = new KOTPickUpRepository(dbContext);
        }
        /// <summary>
        /// DbContext SaveChange
        /// </summary>
        public void Save()
        {
            try
            {
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void Dispose()
        {
            dbContext.Dispose();
        }
        /// <summary>
        /// DbContext BeginTransaction
        /// </summary>
        public void BeginTransaction()
        {
            dbContextTransaction = dbContext.Database.BeginTransaction();
        }
        /// <summary>
        /// DbContext Commit Transaction
        /// </summary>
        public void Commit()
        {
            dbContextTransaction.Commit();
        }
        /// <summary>
        /// DbContext RollBack Transaction
        /// </summary>
        public void Rollback()
        {
            dbContextTransaction.Rollback();
        }
    }
}
