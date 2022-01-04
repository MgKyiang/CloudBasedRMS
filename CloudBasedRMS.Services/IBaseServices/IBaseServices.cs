/// <summary>
/// Mr.Kyaing Dev Date:21-06-2017
/// </summary>
namespace CloudBasedRMS.Services
{
    public   interface IBaseServices
    {
        void Save();

        void Dispose();

        void BeginTransaction();

        bool Commit();

        void Rollback();
    }
}
