using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudBasedRMS.Core;
namespace CloudBasedRMS.GenericRepositories
{
  public  class VehicleTypeRepository:Repository<VehicleType>,IVehicleTypeRepository
    {
        public ApplicationDbContext ApplicationDbContext
        {
            get { return dbContext as ApplicationDbContext; }
        }
        public VehicleTypeRepository(ApplicationDbContext _dbContext) : base(_dbContext)
        {
        }
        //define Customize method
    }
}
