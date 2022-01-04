using CloudBasedRMS.GenericRepositories;

namespace CloudBasedRMS.Services
{
    public class BaseServices: IBaseServices
    {
        protected UnitOfWork _unitOfWork;

        public BaseServices()
        {
            _unitOfWork = new UnitOfWork();
        }

        public void BeginTransaction()
        {
            this._unitOfWork.BeginTransaction();
        }

        public void Commit()
        {
            _unitOfWork.Commit();
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        public void Rollback()
        {
            _unitOfWork.Rollback();
        }

        public void Save()
        {
            _unitOfWork.Save();
        }
    }
}
