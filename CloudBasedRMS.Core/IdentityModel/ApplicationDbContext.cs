using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace CloudBasedRMS.Core
{
    // Code-Based Configuration and Dependency resolution
    //Configure to Connect with MySQL Database,if you want to connect MySQL,remove this Attribute ,
    //Change web.config and App.config 
    //[DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() : base("CloudBasedRMS")
        {
            //this.Configuration.ProxyCreationEnabled = false;
            //this.Configuration.LazyLoadingEnabled = false;
            //Database.SetInitializer(new AppDataContextInitializer());
        }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        //Administration
        public DbSet<Authorizations> Authorizations { get; set; }
        public DbSet<TransactionLog> TransactionLogs { get; set; }
        public DbSet<UserInRole> UserInRole { get; set; }
        public DbSet<ErrorLog> ErrorLogs { get; set; }
        public DbSet<ApplicationSetting> ApplicationSettings { get; set; }
        public DbSet<UsersMember> UsersMember { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }

        public DbSet<APIAuthorization> APIAuthorization { get; set; }
        //Code Setup
        public DbSet<Category> Categories { get; set; }
        public DbSet<VehicleType> VehicleTypes { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Rank> Rank { get; set; }
        public DbSet<Event> Event { get; set; }
        public DbSet<BillFoodItems> BillFoodItems { get; set; }
        public DbSet<RestaurantProfile> RestaurantProfile { get; set; }

        public DbSet<Kitchen> Kitchen { get; set; }
        //Flat Entry
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<FoodItems_Details> FoodItems_Details { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Tables> Table { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<SaleBill> SaleBill { get; set; }
        public DbSet<Supplier> Suplier { get; set; }
        //Transaction Entry[Master-Items]
        public DbSet<OrderMaster> OrderMaster { get; set; }
        public DbSet<OrderItems> OrderItems { get; set; }
        //Process Entry
        public DbSet<KOTPickUp> KOTPickUp { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
