using CloudBasedRMS.GenericRepositories;
namespace CloudBasedRMS.Services
{
    public  class VehicleServices:BaseServices
    {
        public IVehicleRepository vehicle { get;private set; }
        public IVehicleTypeRepository vehicleType { get;private set; }
        public VehicleServices()
        {
            vehicle = _unitOfWork.Vehicles;
            vehicleType = _unitOfWork.VehicleTypes;
        }
    }
}
