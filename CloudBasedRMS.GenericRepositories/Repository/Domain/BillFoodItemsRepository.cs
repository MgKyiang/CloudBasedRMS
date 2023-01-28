using CloudBasedRMS.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudBasedRMS.GenericRepositories
{
  public class BillFoodItemsRepository:Repository<BillFoodItems>,IBillFoodItemsRepository
    {
        public ApplicationDbContext ApplicationDbContext{ get { return _dbContext as ApplicationDbContext; }}

        public BillFoodItemsRepository(ApplicationDbContext _dbContext) : base(_dbContext)
        {
        }
        //define Customize method
        public IEnumerable<BillFoodItems> GetBySaleBillID(string SaleBillID)
        {
            return ApplicationDbContext.BillFoodItems.Where(x => x.SaleBillID == SaleBillID);
        }
    }
}
