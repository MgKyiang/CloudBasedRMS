using CloudBasedRMS.GenericRepositories;
namespace CloudBasedRMS.Services
{
    public class ErrorLogServices
    {
        private UnitOfWork unitOfWork;
        public IErrorLogRepository ErrorLogs { get; private set; }
        public ErrorLogServices()
        {
            unitOfWork = new UnitOfWork();

            ErrorLogs = unitOfWork.ErrorLogs;
        }        public void Save()
        {
            unitOfWork.Save();
        }
    }
}
