using CloudBasedRMS.Core;
namespace CloudBasedRMS.GenericRepositories
{
  public  class VehicleRepository:Repository<Vehicle>,IVehicleRepository
    {
        public ApplicationDbContext ApplicationDbContext
        {
            get { return dbContext as ApplicationDbContext; }
        }
        public VehicleRepository(ApplicationDbContext _dbContext) : base(_dbContext)
        {
        }
        //define Customize method
    }
}
