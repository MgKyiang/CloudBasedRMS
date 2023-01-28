using CloudBasedRMS.Core;
using System.Collections.Generic;
using System.Linq;

namespace CloudBasedRMS.GenericRepositories
{
    public class ApplicationSettingRepository : Repository<ApplicationSetting>, IApplicationSettingRepository
    {
        public ApplicationDbContext ApplicationDbContext
        {
            get { return _dbContext as ApplicationDbContext; }
        }
        public ApplicationSettingRepository(ApplicationDbContext _dbContext) : base(_dbContext)
        {
        }
        //define Customize method
        public IEnumerable<ApplicationSetting> GetApplicationSettingByKeyName(string Key)
        {
            return ApplicationDbContext.ApplicationSettings.Where(x => x.Key == Key);
        }
    }
}
