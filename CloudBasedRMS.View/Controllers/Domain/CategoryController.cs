
namespace CloudBasedRMS.View.Controllers
{
    using ViewModel;
    using Core;
    using Services;
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using PagedList;
    using System.Collections.Generic;

    public   class CategoryController:ControllerAuthorizeBase
   {
        public CategoryServices CategoriesServices;
        public CategoryController()
        {
            CategoriesServices = new CategoryServices();
        }
        [HttpGet]
        public ActionResult Index(string searchby1, string search1, string searchby2, string search2, string searchby3, string search3, string searchby4, string search4, string sortby, int? page, string status)
        {
            //Sorting ViewBag
            ViewBag.CodeSortby = sortby == "CodeAsc" ? "CodeDesc" : "CodeAsc";
            ViewBag.DescriptionSortby = sortby == "DescriptionAsc" ? "DescriptionDesc" : "DescriptionAsc";
            ViewBag.CreatedUserNameSortby = sortby == "CreatedUserNameAsc" ? "CreatedUserNameDesc" : "CreatedUserNameAsc";

            //For Showing Alert
            ViewBag.Searchby1 = searchby1;
            ViewBag.Search1 = search1;
            ViewBag.Searchby2 = searchby2;
            ViewBag.Search2 = search2;
            ViewBag.Searchby3 = searchby3;
            ViewBag.Search3 = search3;
            ViewBag.Searchby4 = searchby4;
            ViewBag.Search4 = search4;

            ViewBag.sortby = sortby;
            ViewBag.Status = status;
            var data =CategoriesServices.Categoryrepo.GetByAll().Where(x=>x.Active==true).ToList();
            //Searching
            if (!string.IsNullOrEmpty(searchby1) && !string.IsNullOrEmpty(search1))
            {
                data = GetSearchingData(searchby1, search1, data);
            }
            if (!string.IsNullOrEmpty(searchby2) && !string.IsNullOrEmpty(search2))
            {
                data = GetSearchingData(searchby2, search2, data);
            }
            if (!string.IsNullOrEmpty(searchby3) && !string.IsNullOrEmpty(search3))
            {
                data = GetSearchingData(searchby3, search3, data);
            }
            if (!string.IsNullOrEmpty(searchby4) && !string.IsNullOrEmpty(search4))
            {
                data = GetSearchingData(searchby4, search4, data);
            }
            //Sorting
            if (!string.IsNullOrEmpty(sortby))
            {
                switch (sortby)
                {
                    case "CodeAsc":
                        data = data.OrderBy(x => x.Code).ToList();
                        break;
                    case "CodeDesc":
                        data = data.OrderByDescending(x => x.Code).ToList();
                        break;
                    case "DescriptionAsc":
                        data = data.OrderBy(x => x.Description).ToList();
                        break;
                    case "DescriptionDesc":
                        data = data.OrderByDescending(x => x.Description).ToList();
                        break;
                    case "CreatedUserNameAsc":
                        data = data.OrderBy(x => x.CreatedUser).ToList();
                        break;
                    case "CreatedUserNameDesc":
                        data = data.OrderByDescending(x => x.CreatedUser.UserName).ToList();
                        break;
                }
            }
            return View(data.ToPagedList(page ?? 1, ApplicationSettingPagingSize));
        }
        [HttpGet]
        public  ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoryViewModel viewmodel)
        {
            if (ModelState.IsValid)
            {
              bool checkdata = CategoriesServices.Categoryrepo.GetByAll().Any(x => x.Code == viewmodel.Code&&x.Active==true);
                if (!checkdata)
                {
                    Category Entity = new Category();
                    Entity.CategoryID = Guid.NewGuid().ToString();
                    Entity.Code = viewmodel.Code;
                    Entity.Description = viewmodel.Description;
                    Entity.Active = true;
                    Entity.CreatedUserID = CurrentApplicationUser.Id;
                    Entity.CreatedDate = DateTime.Now;
                    CategoriesServices.Categoryrepo.Add(Entity);
                    CategoriesServices.Save();
                    Success(string.Format("<b>{0}</b> was successfully saved to the system.", viewmodel.Description), true);
                    return RedirectToAction("Index");
                }
                else
                {
                    Warning(string.Format("<b>{0}</b> was already existed in the system.", viewmodel.Description), true);
                    return RedirectToAction("Index");
                    
                }
            }
            Danger("Looks like something went wrong. Please check your form.");
            return View();
        }

        [HttpGet]
        public ActionResult Delete(string Id)
        {
            if (!string.IsNullOrEmpty(Id))
            {
                Category entity = CategoriesServices.Categoryrepo.GetByID(Id);
                CategoryViewModel model = new CategoryViewModel()
                {
                    CategoryID = entity.CategoryID,
                    Code = entity.Code,
                    Description = entity.Description
                };
                return View(model);
            }
            return View();
        }
        [HttpPost]
        public ActionResult Delete(CategoryViewModel viewmodel)
        {
            if (!string.IsNullOrEmpty(viewmodel.CategoryID))
            {
                Category entity = CategoriesServices.Categoryrepo.GetByID(viewmodel.CategoryID);
                entity.Active = false;
                CategoriesServices.Categoryrepo.Update(entity);
                CategoriesServices.Save();
                Success(string.Format("<b>{0}</b> was successfully deleted from the system.", entity.Description), true);
                return RedirectToAction("Index");
               
            }
            return View();
        }
       [HttpGet]
       public ActionResult Edit(string Id)
        {
            if (!string.IsNullOrEmpty(Id))
            {
                Category entity = CategoriesServices.Categoryrepo.GetByID(Id);
                CategoryViewModel model = new CategoryViewModel()
                {
                    CategoryID = entity.CategoryID,
                    Code = entity.Code,
                    Description = entity.Description
                };
                return View(model);
            }return View();
        }
        [HttpPost]
        public ActionResult Edit(CategoryViewModel viewmodel)
        {
            if (ModelState.IsValid)
            {
                Category entity =CategoriesServices.Categoryrepo.GetByID(viewmodel.CategoryID);
                entity.Code = viewmodel.Code;
                entity.Description = viewmodel.Description;
                entity.UpdatedDate = DateTime.Now;
                entity.UpdatedUserID = CurrentApplicationUser.Id;
                CategoriesServices.Categoryrepo.Update(entity);
                CategoriesServices.Save();
                Success(string.Format("<b>{0}</b> was successfully updated to the system.", entity.Description), true);
                return RedirectToAction("Index");

            }
            return View();
        }
        //Searching Helper
        private List<Category> GetSearchingData(string searchby, string search, List<Category> data)
        {
            switch (searchby)
            {
                case "Code":
                    data = data.Where(x => x.Code.ToLower().Contains(search.ToLower())).ToList();
                    break;

                case "Description":
                    data = data.Where(x => x.Description.ToLower().Contains(search.ToLower())).ToList();
                    break;
               
                case "CreatedUserName":
                    data = data.Where(x => x.CreatedUser.UserName.ToLower().Contains(search.ToLower())).ToList();
                    break;

                case "CreatedDate":
                    DateTime createdDate;
                    if (DateTime.TryParse(search, out createdDate))
                    {
                        data = data.Where(x => x.CreatedDate == createdDate).ToList();
                    }
                    break;
            }
            return data;
        }

        #region Create Category Record
        [HttpPost]
        public JsonResult CreateCategory(CategoryViewModel viewmodel)
        {
            if (ModelState.IsValid)
            {
                bool checkaddress = CategoriesServices.Categoryrepo.GetByAll().Any(x => x.Code == viewmodel.Code && x.Active == true);
                if (!checkaddress)
                {
                    Category model = new Category()
                    {
                        CategoryID = Guid.NewGuid().ToString(),
                        Code = viewmodel.Code,
                        Description = viewmodel.Description,
                     
                        Active = true,
                        CreatedDate = DateTime.Now,
                        CreatedUserID = CurrentApplicationUser.Id
                    };
                    if (CategoriesServices.Categoryrepo.Add(model))
                    {
                        CategoriesServices.Save();
                        return Json(true, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
