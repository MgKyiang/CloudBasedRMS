using CloudBasedRMS.GenericRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudBasedRMS.Services
{
  public  class EmployeeServices:BaseServices
    {
        public IEmployeeRepository Employee { get; set; }
        public IAddressRepository Address { get; set; }
        public IRankRepository Rank { get; set; }
        public EmployeeServices()
        {
            Employee = _unitOfWork.Employee;
            Address = _unitOfWork.Address;
            Rank = _unitOfWork.Rank;
        }
    }
}
