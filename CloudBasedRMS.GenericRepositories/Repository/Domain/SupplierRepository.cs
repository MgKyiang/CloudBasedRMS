using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CloudBasedRMS.Core;

namespace CloudBasedRMS.GenericRepositories
{
    public class SupplierRepository : Repository<Supplier>, ISupplierRepository
    {

        public SupplierRepository(DbContext _dbContext) : base(_dbContext)
        {
        }

    }
}
