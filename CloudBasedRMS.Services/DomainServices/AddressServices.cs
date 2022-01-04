using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudBasedRMS.GenericRepositories;
namespace CloudBasedRMS.Services
{
 public   class AddressServices:BaseServices
    {
        public IAddressRepository Address { get; private set; }
        public AddressServices()
        {
            Address = unitOfWork.Address;
        }
    }
}
