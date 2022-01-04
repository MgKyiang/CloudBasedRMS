using CloudBasedRMS.GenericRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudBasedRMS.Services
{
   public class TableServices:BaseServices
    {
        public ITableRepository Table { get; private set; }
        public IEmployeeRepository Employee { get; set; }
        public TableServices()
        {
            Table = unitOfWork.Table;
            Employee = unitOfWork.Employee;
        }
    }
}
