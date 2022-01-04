using CloudBasedRMS.Core;
using CloudBasedRMS.Services;
using CloudBasedRMS.WebAPI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CloudBasedRMS.WebAPI.ApiControllers.Domain
{
    [RoutePrefix("api/ApiFoodItems")]
    public class ApiFoodItemsController : APIControllerBase
    {
        public FoodItems_DetailsServices foodItems_DetailsServices;
        public ApiFoodItemsController()
        {
            foodItems_DetailsServices = new FoodItems_DetailsServices();
        }
        [Route("GetAllFoodItems")]
        public IHttpActionResult GetAllFoodItems()
        {
            IEnumerable<FoodItems_Details> data = foodItems_DetailsServices.FoodItems_Details.GetByAll().Where(x => x.Active == true)
                .OrderBy(x => x.Description)
                .OrderBy(x => x.IsTodaySpecial)
                .ToList();
            return Ok(data);
        }
    }
}
