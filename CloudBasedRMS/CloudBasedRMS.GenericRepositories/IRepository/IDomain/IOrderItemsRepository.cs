using CloudBasedRMS.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudBasedRMS.GenericRepositories
{
  public  interface IOrderItemsRepository:IRepository<OrderItems>
    {
        IEnumerable<OrderItems> GetOrderItemsByOrderMasterID(string OrderMasterID);
    }
}
