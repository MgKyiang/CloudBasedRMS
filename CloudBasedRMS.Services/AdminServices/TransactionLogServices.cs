using CloudBasedRMS.GenericRepositories;

namespace CloudBasedRMS.Services
{
    public class TransactionLogServices
    {
        private UnitOfWork unitOfWork;
        public ITransactionLogRepository Logs { get; private set; }
        public TransactionLogServices()
        {
            unitOfWork = new UnitOfWork();
            Logs = unitOfWork.Logs;
        }
        public void Save()
        {
            unitOfWork.Save();
        }
    }
}
