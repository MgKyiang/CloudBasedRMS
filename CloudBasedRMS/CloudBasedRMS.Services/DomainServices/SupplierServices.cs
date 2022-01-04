using CloudBasedRMS.GenericRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudBasedRMS.Services
{
  public  class SupplierServices:BaseServices
    {
        public ISupplierRepository Supplier { get; set; }
        public IAddressRepository Address { get; set; }

        public SupplierServices()
        {
            Supplier = _unitOfWork.Supplier;
            Address = _unitOfWork.Address;
        }
    }
}
