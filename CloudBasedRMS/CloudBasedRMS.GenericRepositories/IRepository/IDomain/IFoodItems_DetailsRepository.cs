using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudBasedRMS.Core;
namespace CloudBasedRMS.GenericRepositories
{
  public  interface IFoodItems_DetailsRepository:IRepository<FoodItems_Details>
    {
        //Customize method here
       List< FoodItems_Details> GetByCategoryID(string CategoryID);
    }
}
