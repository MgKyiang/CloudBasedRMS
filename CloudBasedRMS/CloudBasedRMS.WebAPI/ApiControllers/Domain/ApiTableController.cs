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
    [RoutePrefix("api/ApiTable")]
    public class ApiTableController :APIControllerBase
    {
        public TableServices tableServices;
        public ApiTableController()
        {
            tableServices = new TableServices();
        }
        [Route("GetAllTables")]
        public IHttpActionResult GetAllTables()
        {
           
            IEnumerable<Tables> data = tableServices.Table.GetByAll().Where(x => x.Active == true)
                .OrderBy(x => x.TableNo)
                .OrderBy(x => x.Status)
                .ToList();
            return Ok(data);
        }
    }
}
