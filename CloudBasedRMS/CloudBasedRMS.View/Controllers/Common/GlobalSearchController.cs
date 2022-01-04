using CloudBasedRMS.Core;
using CloudBasedRMS.Services.AdminServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CloudBasedRMS.View.Controllers.Common
{
    public class GlobalSearchController:ControllerAuthorizeBase
    {
        public SearchServices searchService;
        public GlobalSearchController()
        {
            searchService = new SearchServices();
        }
        [HttpGet]
       [ChildActionOnly]
        public ActionResult SearchWidget()
        {
            return PartialView("_Search");
        }

        [HttpGet]
        public ActionResult Search(string item)
        {
            SearchResults searchResults = new SearchResults();
            searchResults.category = searchService.Categoryrepo.GetByAll().Where(x => x.Active == true && x.Description.Contains(item)).ToList();           
            return PartialView("_SearchResults", searchResults);
        }
    }
}