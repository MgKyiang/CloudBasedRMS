using CloudBasedRMS.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudBasedRMS.GenericRepositories
{
  public  interface IRestaurantProfileRepository:IRepository<RestaurantProfile>
    {
        //Here Customized Methods
        RestaurantProfile GetRestaurantProfile();

        IEnumerable<RestaurantProfile> GetAllRestaurantProfile();

        bool DeleteCurrentRestaurantProfile();
    }
}
