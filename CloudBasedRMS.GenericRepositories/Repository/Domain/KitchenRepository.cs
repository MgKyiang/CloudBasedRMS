using CloudBasedRMS.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudBasedRMS.GenericRepositories
{
  public  class KitchenRepository:Repository<Kitchen>,IKitchenRepository
    {
        public ApplicationDbContext ApplicationDbContext
        {
            get { return _dbContext as ApplicationDbContext; }
        }
        public KitchenRepository(ApplicationDbContext _dbContext) : base(_dbContext)
        {
        }
    }
}
