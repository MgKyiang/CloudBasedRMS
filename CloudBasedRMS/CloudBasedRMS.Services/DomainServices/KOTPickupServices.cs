using CloudBasedRMS.GenericRepositories;

namespace CloudBasedRMS.Services
{
    public class KOTPickupServices : BaseServices
    {
        public IKOTPickUpRepository KOTPickUp { get; set; }
        public KOTPickupServices()
        {
            KOTPickUp = _unitOfWork.KOTPickUp;
        }
    }
}
