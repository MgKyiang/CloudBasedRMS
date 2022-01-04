using CloudBasedRMS.GenericRepositories;

namespace CloudBasedRMS.Services
{
    public class TransactionLogServices
    {
        private UnitOfWork _unitOfWork;
        public ITransactionLogRepository Logs { get; private set; }
        public TransactionLogServices()
        {
            _unitOfWork = new UnitOfWork();

            Logs = _unitOfWork.Logs;
        }
        public void Save()
        {
            _unitOfWork.Save();
        }
    }
}
