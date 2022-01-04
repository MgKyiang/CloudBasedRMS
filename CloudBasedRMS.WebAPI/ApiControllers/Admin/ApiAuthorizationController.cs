using CloudBasedRMS.Core;
using CloudBasedRMS.Services.AdminServices;
using CloudBasedRMS.WebAPI.Common;
using CloudBasedRMS.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace CloudBasedRMS.WebAPI.ApiControllers.Admin
{
    [RoutePrefix("api/ApiAuthorization")]
    public class ApiAuthorizationController : APIControllerBase
    {
        APIAuthorizationServices apiAuthorizationServices;
        public ApiAuthorizationController()
        {
            apiAuthorizationServices = new APIAuthorizationServices();
        }
        [Route("GetByAll")]
        public IHttpActionResult GetByAll()
        {    
            IEnumerable<APIAuthorization> data = apiAuthorizationServices.APIAuthorizations.GetByAll().Where(x => x.Active == true)
                .OrderBy(x => x.ControllerName)
                .OrderBy(x => x.ActionName)
                .OrderBy(x => x.ApplicationRole.Name)
                .ToList();
            return Ok(data);
        }

        [Route("GetApiData")]
        public IHttpActionResult GetApiData()
        {
            List<ApplicationRole> roles = RoleManager.Roles.ToList();

            //Assembly File String Path
            string assemblyFilePath = System.Web.HttpContext.Current.Request.MapPath("~\\bin\\CloudBasedRMS.WebAPI.dll");

            //Load Assembly
            Assembly webApiAssemblyDll = Assembly.LoadFile(assemblyFilePath);

            //1. ApiController must be base on ApiControllerBase
            //2. ActionMethod must be IHttpActionResult
            List<APIAuthorizationViewModel> dynamicLoadApi = (from i in webApiAssemblyDll.GetTypes().Where(x => x.IsClass == true && x.BaseType.Name.ToLower() == "ApiControllerBase".ToLower())
                                                              from j in i.GetMethods().Where(x => x.ReturnType.Name.ToLower() == "IHttpActionResult".ToLower())
                                                              from r in RoleManager.Roles.ToList()
                                                              select new APIAuthorizationViewModel
                                                              {
                                                                  ControllerName = i.Name.Replace("Controller", string.Empty),
                                                                  ActionName = j.Name,
                                                                  RoleID = r.Id,
                                                                  RoleName = r.Name,
                                                                  TransactionLog = true,
                                                                  AllowOrDeny = true
                                                              }).Distinct().ToList();


            //get all exiting apiauthorization data
            List<APIAuthorizationViewModel> existedData = (from i in apiAuthorizationServices.APIAuthorizations.GetByAll().ToList()
                                                           select new APIAuthorizationViewModel
                                                           {
                                                               ControllerName = i.ControllerName,
                                                               ActionName = i.ActionName,
                                                               RoleID = i.ApplicationRole.Id,
                                                               RoleName = i.ApplicationRole.Name,
                                                               TransactionLog = true,
                                                               AllowOrDeny = true
                                                           }).ToList();
            var data = dynamicLoadApi.Except(existedData, new ApiAuthorizationComparer()).OrderBy(x => x.ControllerName).OrderBy(x => x.ActionName).ToList();
            return Ok(data);
        }

        [Route("GetRoles")]
        public IHttpActionResult GetRoles()
        {
            var data = RoleManager.Roles.ToList();
            return Ok(data);
        }

        [Route("PostAutoCreateApi")]
        public IHttpActionResult PostAutoCreateApi(List<APIAuthorizationViewModel> model)
        {
            if (model.Count > 0 && model[1].ControllerName != string.Empty && model[1].ControllerName != null &&
                 model[1].ActionName != string.Empty && model[1].ActionName != null)
            {
                List<APIAuthorization> entities = (from i in model
                                                   select new APIAuthorization
                                                   {
                                                       APIAuthorizationID = Guid.NewGuid().ToString(),
                                                       ControllerName = i.ControllerName,
                                                       ActionName = i.ActionName,
                                                       RoleID = i.RoleID,
                                                       TransactionLog = i.TransactionLog,
                                                       AllowOrDeny = i.AllowOrDeny,
                                                       CreatedUserID = CurrentUser.Id,
                                                       CreatedDate = DateTime.Now,
                                                       Active = true
                                                   }).ToList();

                apiAuthorizationServices.APIAuthorizations.AddRange(entities);
                apiAuthorizationServices.Save();
                return Ok(true);
            }
            else
            {
                return Ok(false);
            }

        }

        [Route("Post")]
        public IHttpActionResult Post(List<APIAuthorizationViewModel> model)
        {
            if (model.Count > 0 && model[0].ControllerName != string.Empty && model[0].ControllerName != null &&
                model[0].ActionName != string.Empty && model[0].ActionName != null)
            {
                foreach (var i in model)
                {
                    APIAuthorization entity = apiAuthorizationServices.APIAuthorizations.GetByID(i.ID);
                    entity.TransactionLog = i.TransactionLog;
                    entity.AllowOrDeny = i.AllowOrDeny;
                    entity.UpdatedDate = DateTime.Now;
                    entity.UpdatedUserID = CurrentUser.Id;

                    apiAuthorizationServices.APIAuthorizations.Update(entity);
                    apiAuthorizationServices.Save();
                }
            }
            else
            {
                return Ok(false);
            }
            return Ok(true);

        }

        /// <summary>
        /// Manually Create
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("PostManuallyCreateApi")]
        public IHttpActionResult PostManuallyCreateApi(APIAuthorizationViewModel model)
        {
            List<APIAuthorization> entities = new List<APIAuthorization>();

            if (!string.IsNullOrEmpty(model.ControllerName) && !string.IsNullOrEmpty(model.ActionName) && model.RolesId.Count > 0)
            {
                if (model.RolesId.Count > 0)
                {
                    foreach (string role in model.RolesId)
                    {
                        APIAuthorization entity = new APIAuthorization()
                        {
                            APIAuthorizationID = Guid.NewGuid().ToString(),
                            ControllerName = model.ControllerName,
                            ActionName = model.ActionName,
                            TransactionLog = model.TransactionLog,
                            AllowOrDeny = model.AllowOrDeny,
                            RoleID = role,
                            CreatedDate = DateTime.Now,
                            CreatedUserID = CurrentUser.Id,
                            Active = true
                        };

                        //Add To Entities
                        entities.Add(entity);
                    }
                }
                else
                {
                    return Ok(false);
                }

                if (entities.Count > 0)
                {
                    apiAuthorizationServices.APIAuthorizations.AddRange(entities);
                    //Save Change
                    apiAuthorizationServices.Save();
                }
                else
                {
                    return Ok(false);
                }
            }
            else
            {
                return Ok(false);
            }

            return Ok(true);
        }
    }

    public class ApiAuthorizationComparer : IEqualityComparer<APIAuthorizationViewModel>
    {
        public bool Equals(APIAuthorizationViewModel x, APIAuthorizationViewModel y)
        {
            if (x.ControllerName.Replace(" ", string.Empty).ToLower() == y.ControllerName.Replace(" ", string.Empty).ToLower() &&
                x.ActionName.Replace(" ", string.Empty).ToLower() == y.ActionName.Replace(" ", string.Empty).ToLower() &&
                x.RoleID.ToLower() == y.RoleID.ToLower())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public int GetHashCode(APIAuthorizationViewModel obj)
        {
            return obj.ControllerName.GetHashCode();
        }
    }
}