using CloudBasedRMS.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudBasedRMS.GenericRepositories
{
  public  class RestaurantProfileRepository:Repository<RestaurantProfile>,IRestaurantProfileRepository
    {
        public ApplicationDbContext ApplicationDbContext
        {
            get { return dbContext as ApplicationDbContext; }
        }
        public RestaurantProfileRepository(ApplicationDbContext _dbContext) : base(_dbContext)
        {
        }

        public RestaurantProfile GetRestaurantProfile()
        {
            return ApplicationDbContext.RestaurantProfile.Where(x => x.Active == true).SingleOrDefault();
        }

        public IEnumerable<RestaurantProfile> GetAllRestaurantProfile()
        {
            return ApplicationDbContext.RestaurantProfile.Where(x => x.Active == true).ToList();
        }

        public bool DeleteCurrentRestaurantProfile()
        {
            try
            {
                foreach (RestaurantProfile companyProfile in GetAllRestaurantProfile())
                {
                    companyProfile.Active = false;
                    ApplicationDbContext.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
                throw new Exception("An Error Occur When Updating Database.");
            }

        }
        private void SaveImage()
        {
        }
    }
}
