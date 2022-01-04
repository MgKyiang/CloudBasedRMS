using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudBasedRMS.Core;
namespace CloudBasedRMS.GenericRepositories
{
 public   interface IAddressRepository:IRepository<Address>
    {
        //Here Customized Methods
         //Address GetCityName(string AddressID);
    }
}
