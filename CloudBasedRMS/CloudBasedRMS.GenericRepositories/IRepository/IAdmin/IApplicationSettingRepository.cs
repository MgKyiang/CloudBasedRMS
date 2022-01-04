using CloudBasedRMS.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudBasedRMS.GenericRepositories
{
    public interface IApplicationSettingRepository : IRepository<ApplicationSetting>
    {
        //Here Customized Methods
        IEnumerable<ApplicationSetting> GetApplicationSettingByKeyName(string Key);
    }
}
