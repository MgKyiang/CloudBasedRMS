using CloudBasedRMS.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudBasedRMS.GenericRepositories
{
  public  interface IBillFoodItemsRepository:IRepository<BillFoodItems>
    {
        //Here Customized Methods
        IEnumerable<BillFoodItems> GetBySaleBillID(string SaleBillID);
    }
}
