using CloudBasedRMS.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudBasedRMS.GenericRepositories
{
 public   interface IOrderMasterRepository:IRepository<OrderMaster>
    {
        bool Update(OrderMaster ordermaster, List<OrderItems> orderitems);
        List<OrderMaster> GetByOrderNo(string OrderNo);
    }
}
