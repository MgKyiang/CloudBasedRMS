using CloudBasedRMS.Core;
using System.Collections.Generic;

namespace CloudBasedRMS.View.Controllers.ViewModel
{
  public  class OrderListViewModel
    {
        //for master-detail listing view
        public OrderMaster order { get; set; }
        public List<OrderItems> orderDetails { get; set; }
    }
}
