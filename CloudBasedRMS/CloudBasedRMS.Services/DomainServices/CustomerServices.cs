using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudBasedRMS.GenericRepositories;
namespace CloudBasedRMS.Services
{
 public   class CustomerServices:BaseServices
    {
        public ICustomerRepository Customer { get;private set; }
        public IAddressRepository Address { get;private set; }
        public CustomerServices()
        {
            Customer = _unitOfWork.Customer;
            Address = _unitOfWork.Address;
        }
    }
}
