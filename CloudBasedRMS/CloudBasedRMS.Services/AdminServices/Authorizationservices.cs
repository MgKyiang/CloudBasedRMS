using CloudBasedRMS.GenericRepositories;

namespace CloudBasedRMS.Services
{
    public class Authorizationservices
    {
        private UnitOfWork _unitOfWork;
        public IAuthorizationsRepository Authorizations { get; private set; }
        public Authorizationservices()
        {
            _unitOfWork = new UnitOfWork();
            Authorizations = _unitOfWork.Authorizations;
        }
        public void Save()
        {
            _unitOfWork.Save();
        }
    }
}
