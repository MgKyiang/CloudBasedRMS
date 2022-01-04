using CloudBasedRMS.Core;
using System.Data.Entity;

namespace CloudBasedRMS.GenericRepositories
{
    public class KOTPickUpRepository : Repository<KOTPickUp>, IKOTPickUpRepository
    {
        public KOTPickUpRepository(DbContext _dbContext) : base(_dbContext)
        {
        }
    }
}
