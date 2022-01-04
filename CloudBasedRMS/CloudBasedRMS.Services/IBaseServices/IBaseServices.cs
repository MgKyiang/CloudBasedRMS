using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        void Commit();

        void Rollback();
    }
}
