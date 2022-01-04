using CloudBasedRMS.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudBasedRMS.GenericRepositories
{
 public   class TableRepository:Repository<Tables>,ITableRepository
    {
        public ApplicationDbContext ApplicationDbContext
        {
            get { return dbContext as ApplicationDbContext; }
        }
        public TableRepository(ApplicationDbContext _dbContext) : base(_dbContext)
        {
        }
        //define Customize method
    }
}
