using CloudBasedRMS.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudBasedRMS.GenericRepositories
{
  public  class EmployeeRepository:Repository<Employee>,IEmployeeRepository
    {
        public ApplicationDbContext ApplicationDbContext
        {
            get { return _dbContext as ApplicationDbContext; }
        }
        public EmployeeRepository(ApplicationDbContext _dbContext) : base(_dbContext)
        {
        }
        //define Customize method
        public Employee GetEmployeeByEmployeeName(string EmployeeName)
        {
            return ApplicationDbContext.Employee.Where(x => x.Name== EmployeeName).SingleOrDefault();
        }

    }
}
