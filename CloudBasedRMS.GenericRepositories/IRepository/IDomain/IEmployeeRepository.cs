using CloudBasedRMS.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudBasedRMS.GenericRepositories
{
  public  interface IEmployeeRepository:IRepository<Employee>
    {
        //Here Customized Methods
        Employee GetEmployeeByEmployeeName(string EmployeeName);
    }
}
