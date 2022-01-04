using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudBasedRMS.GenericRepositories;
namespace CloudBasedRMS.Services
{
 public class VehicleTypeServices:BaseServices
    {
        public IVehicleTypeRepository VehicleType { get;private set; }
        public VehicleTypeServices()
        {
            VehicleType = _unitOfWork.VehicleTypes;
        }
    }
}
