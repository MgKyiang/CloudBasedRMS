using CloudBasedRMS.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudBasedRMS.GenericRepositories
{
  public  class OrderItemsRepository:Repository<OrderItems>,IOrderItemsRepository
    {
        public ApplicationDbContext ApplicationDbContext
        {
            get { return dbContext as ApplicationDbContext; }
        }
        public OrderItemsRepository(ApplicationDbContext _dbContext) : base(_dbContext)
        {
        }

        public IEnumerable<OrderItems> GetOrderItemsByOrderMasterID(string OrderMasterID)
        {
            return ApplicationDbContext.OrderItems.Where(x => x.OrderMasterID == OrderMasterID).ToList();
        }
        //define Customize method
    }
}
