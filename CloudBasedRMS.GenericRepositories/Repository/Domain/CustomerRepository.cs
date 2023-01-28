using CloudBasedRMS.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudBasedRMS.GenericRepositories
{
   public class CustomerRepository:Repository<Customer>,ICustomerRepository
    {
        public ApplicationDbContext ApplicationDbContext
        {
            get { return _dbContext as ApplicationDbContext; }
        }
        public CustomerRepository(ApplicationDbContext _dbContext) : base(_dbContext)
        {
        }
        //define Customize method
    }
}
