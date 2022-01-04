using CloudBasedRMS.GenericRepositories;
using System;

namespace CloudBasedRMS.Services
{
    public class BaseServices: IBaseServices
    {
        protected UnitOfWork unitOfWork;

        public BaseServices()
        {
            unitOfWork = new UnitOfWork();
        }

        public void BeginTransaction()
        {
            this.unitOfWork.BeginTransaction();
        }

        public bool Commit()
        {
            try
            {
                unitOfWork.Commit();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }

        public void Rollback()
        {
            unitOfWork.Rollback();
        }

        public void Save()
        {
            unitOfWork.Save();
        }
    }
}
