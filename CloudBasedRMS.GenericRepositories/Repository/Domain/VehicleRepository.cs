using CloudBasedRMS.Core;
namespace CloudBasedRMS.GenericRepositories
{
  public  class VehicleRepository:Repository<Vehicle>,IVehicleRepository
    {
        public ApplicationDbContext ApplicationDbContext
        {
            get { return _dbContext as ApplicationDbContext; }
        }
        public VehicleRepository(ApplicationDbContext _dbContext) : base(_dbContext)
        {
        }
        //define Customize method
    }
}
