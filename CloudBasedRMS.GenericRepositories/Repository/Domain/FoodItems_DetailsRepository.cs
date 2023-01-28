using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudBasedRMS.Core;
namespace CloudBasedRMS.GenericRepositories
{
  public  class FoodItems_DetailsRepository:Repository<FoodItems_Details>,IFoodItems_DetailsRepository
    {
        public ApplicationDbContext ApplicationDbContext
        {
            get { return _dbContext as ApplicationDbContext; }
        }
        public FoodItems_DetailsRepository(ApplicationDbContext _dbContext) : base(_dbContext)
        {
        }
        //define Customize method
        public List< FoodItems_Details> GetByCategoryID(string CategoryID)
        {
            return ApplicationDbContext.FoodItems_Details.Where(x=>x.CategoryID==CategoryID).ToList();
        }
    }
}
