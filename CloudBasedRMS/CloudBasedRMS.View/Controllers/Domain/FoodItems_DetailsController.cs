namespace CloudBasedRMS.View.Controllers
{
    using System;
    using System.Linq;
    using Core;
    using Services;
    using System.Web.Mvc;
    using ViewModel;
    using System.Data.Entity.Infrastructure;
    using PagedList;
    using System.Collections.Generic;
    using System.Web;
    using System.IO;
    public   class FoodItems_DetailsController:ControllerAuthorizeBase
    {
        public FoodItems_DetailsServices foodItems_DetailsServices;
        public FoodItems_DetailsController()
        {
            foodItems_DetailsServices = new FoodItems_DetailsServices();
        }
        // GET: Vehicle
        public ActionResult Index(string searchby1, string search1, string searchby2, string search2, string searchby3, string search3, string searchby4, string search4, string sortby, int? page, string status)
        {
            var data = foodItems_DetailsServices.FoodItems_Details.GetByAll().Where(x => x.Active == true).ToList();
            //Sorting ViewBag
            ViewBag.CodeSortby = sortby == "CodeAsc" ? "CodeDesc" : "CodeAsc";
            ViewBag.DescriptionSortby = sortby == "DescriptionAsc" ? "DescriptionDesc" : "DescriptionAsc";
            ViewBag.CategorySortby = sortby == "CategoryAsc" ? "CategoryDesc" : "CategoryAsc";

            ViewBag.RateSortBy = sortby == "RateAsc" ? "RateDesc" : "RateAsc";
            ViewBag.OfferBy = sortby == "OfferAsc" ? "OfferDesc" : "OfferAsc";
            ViewBag.NoteSortBy = sortby == "NoteAsc" ? "NoteDesc" : "NoteAsc";
            ViewBag.IsJamSortBy = sortby == "IsJamAsc" ? "IsJamDesc" : "IsJamAsc";
            ViewBag.SpicySortBy = sortby == "SpicyAsc" ? "SpicyDesc" : "SpicyAsc";
            ViewBag.IsTodaySpecialSortBy = sortby == "IsTodaySpecialAsc" ? "IsTodaySpecialDesc" : "IsTodaySpecialAsc";
            ViewBag.NewPriceSortBy = sortby == "NewPriceAsc" ? "NewPriceDesc" : "NewPriceAsc";
            ViewBag.OldPriceSortBy = sortby == "OldPriceAsc" ? "OldPriceDesc" : "OldPriceAsc";
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
                    case "CategoryAsc":
                        data = data.OrderBy(x => x.Category.Description).ToList();
                        break;
                    case "CategoryDesc":
                        data = data.OrderByDescending(x => x.Category.Description).ToList();
                        break;

                    case "RateAsc":
                        data = data.OrderBy(x => x.Rate).ToList();
                        break;
                    case "RateDesc":
                        data = data.OrderByDescending(x => x.Rate).ToList();
                        break;
                    case "OfferAsc":
                        data = data.OrderBy(x => x.Offer).ToList();
                        break;
                    case "OfferDesc":
                        data = data.OrderByDescending(x => x.Offer).ToList();
                        break;
                    case "NoteAsc":
                        data = data.OrderBy(x => x.Note).ToList();
                        break;
                    case "NoteDesc":
                        data = data.OrderByDescending(x => x.Note).ToList();
                        break;
                    case "IsJamAsc":
                        data = data.OrderBy(x => x.IsJam).ToList();
                        break;
                    case "IsJamDesc":
                        data = data.OrderByDescending(x => x.IsJam).ToList();
                        break;
                    case "IsTodaySpecialAsc":
                        data = data.OrderBy(x => x.IsTodaySpecial).ToList();
                        break;
                    case "IsTodaySpecialDesc":
                        data = data.OrderByDescending(x => x.IsTodaySpecial).ToList();
                        break;
                    case "NewPriceAsc":
                        data = data.OrderBy(x => x.NewPrice).ToList();
                        break;
                    case "NewPriceDesc":
                        data = data.OrderByDescending(x => x.NewPrice).ToList();
                        break;
                    case "OldPriceAsc":
                        data = data.OrderBy(x => x.OldPrice).ToList();
                        break;
                    case "OldPriceDesc":
                        data = data.OrderByDescending(x => x.OldPrice).ToList();
                        break;
                }
            }
            return View(data.ToPagedList(page ?? 1, ApplicationSettingPagingSize));
        }
        //Searching Helper
        private List<FoodItems_Details> GetSearchingData(string searchby, string search, List<FoodItems_Details> data)
        {
            switch (searchby)
            {
                case "Code":
                    data = data.Where(x => x.Code.ToLower().Contains(search.ToLower())).ToList();
                    break;

                case "Description":
                    data = data.Where(x => x.Description.ToLower().Contains(search.ToLower())).ToList();
                    break;
                case "Category":
                    data = data.Where(x => x.Category.Description.ToLower().Contains(search.ToLower())).ToList();
                    break;
                case "Rate":
                    data = data.Where(x => x.Rate==Convert.ToDecimal(search)).ToList();
                    break;
                case "Offer":
                    data = data.Where(x => x.Offer.ToLower().Contains(search.ToLower())).ToList();
                    break;
                case "Note":
                    data = data.Where(x => x.Note.ToLower().Contains(search.ToLower())).ToList();
                    break;
                case "IsJam":
                    data = data.Where(x => x.IsJam == Convert.ToBoolean(search)).ToList();
                    break;
                case "Spicy":
                    data = data.Where(x => x.Spicy.ToLower().Contains(search.ToLower())).ToList();
                    break;
                case "IsTodaySpecial":
                    data = data.Where(x => x.IsTodaySpecial==Convert.ToBoolean(search)).ToList();
                    break;
                case "OldPrice":
                    data = data.Where(x => x.OldPrice == Convert.ToDecimal(search)).ToList();
                    break;
                case "NewPrice":
                    data = data.Where(x => x.NewPrice==Convert.ToDecimal(search)).ToList();
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
        #region Create
        // GET: Vehicle/Create
        public ActionResult Create()
        {
            DDLDataBind();
            return View();

        }
        public void DDLDataBind()
        {
            ViewBag.Categories = new SelectList(foodItems_DetailsServices.Category.GetByAll().Where(x => x.Active == true).ToList(), "CategoryID", "Description");
            ViewBag.Kitchen = new SelectList(foodItems_DetailsServices.Kitchen.GetByAll().Where(x => x.Active == true).ToList(), "KitchenID", "KitchenName");
        }
        // POST: Vehicle/Create
        [HttpPost]
        public ActionResult Create(FoodItems_DetailsViewModel viewmodel, HttpPostedFileBase file)
        {
            // TODO: Add insert logic here
            if (CheckValidate(viewmodel,file))
            {
                bool checkdata = foodItems_DetailsServices.FoodItems_Details.GetByAll().Any(x => x.Code == viewmodel.Code && x.Active == true);
                if (!checkdata && file.ContentLength > 0)
                {
                    int MaxContentLength = 1024 * 1024 * 3;
                    var allowedExtensions = new[] { ".Jpg", ".png", ".jpg", "jpeg" };
                    var fileName = Path.GetFileName(file.FileName); //getting only file name(ex-ganesh.jpg)  
                    var extension = Path.GetExtension(file.FileName); //getting the extension(ex-.jpg)
                    if (file.ContentLength > MaxContentLength)
                    {
                        ModelState.AddModelError("File", "Your file is too large, maximum allowed size is: " + MaxContentLength + " MB");
                        DDLDataBind();
                        return View(viewmodel);
                    }
                    if (allowedExtensions.Contains(extension)) //check what type of extension  
                    {
                        string name = Path.GetFileNameWithoutExtension(fileName); //getting file name without extension  
                        string myfile = name + "_" + DateTime.Now.ToString("yymmssff") + extension; //appending the name with current date time                                 
                        var path = Path.Combine(Server.MapPath("~/Images/"), myfile);    // store the file inside ~/project folder(Img)  
                        var imageurl = "~/Images/" + myfile;
                        FoodItems_Details model = new FoodItems_Details()
                        {
                            FoodItemID = Guid.NewGuid().ToString(),
                            Code = viewmodel.Code,
                            Description = viewmodel.Description,
                            ImagePath = imageurl, //getting complete url  
                            CategoryID = viewmodel.CategoryID,
                            KitchenID = viewmodel.KitchenID,
                            Offer = viewmodel.Offer,
                            Note = viewmodel.Note,
                            IsJam = viewmodel.IsJam,
                            Spicy = viewmodel.Spicy,
                            Rate = viewmodel.Rate,
                            IsTodaySpecial = viewmodel.IsTodaySpecial,
                            OldPrice = viewmodel.OldPrice,
                            NewPrice = viewmodel.NewPrice,
                            CreatedDate = DateTime.Now,
                            CreatedUserID = CurrentApplicationUser.Id,
                            Active = true
                        };
                        foodItems_DetailsServices.FoodItems_Details.Add(model);
                        foodItems_DetailsServices.Save();
                        file.SaveAs(path);
                        Success(string.Format("<b>{0}</b> was successfully saved to the system.", viewmodel.Description), true);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        this.DDLDataBind();
                        ModelState.AddModelError("Files", "Only jpeg, png, jpg, Jpg format are allowed.Please select a valid logo picture.");
                    }
                }//check data exists
                else
                {
                    Warning(string.Format("<b>{0}</b> was already existed in the system.", viewmodel.Description), true);
                    return RedirectToAction("Index");
                }
            }

            DDLDataBind();
            return View();
        }

        private bool CheckValidate(FoodItems_DetailsViewModel model, HttpPostedFileBase file)
        {
           if(string.IsNullOrEmpty(model.Code)||string.IsNullOrEmpty(model.Description)||
                string.IsNullOrEmpty(model.KitchenID) || string.IsNullOrEmpty(model.CategoryID) || model.OldPrice == 0
                ||file==null)
            {
                return false;
            }
            return true;
        }
        #endregion
        // GET: Vehicle/Edit/5
        public ActionResult Edit(string Id)
        {
            if (!string.IsNullOrEmpty(Id))
            {
                FoodItems_Details model = foodItems_DetailsServices.FoodItems_Details.GetByID(Id);
                FoodItems_DetailsViewModel viewmodel = new FoodItems_DetailsViewModel();
                viewmodel.FoodItemID = model.FoodItemID;
                viewmodel.Code = model.Code;
                viewmodel.Description = model.Description;
                viewmodel.Offer = model.Offer;
                viewmodel.Rate = model.Rate;
                viewmodel.Spicy = model.Spicy;
                viewmodel.IsJam = model.IsJam;
                viewmodel.Note = model.Note;
                viewmodel.IsTodaySpecial = model.IsTodaySpecial;
                viewmodel.OldPrice = model.OldPrice;
                viewmodel.NewPrice = model.NewPrice;
                DDLDataBind();
                return View(viewmodel);
            }
            return View();
        }

        // POST: Vehicle/Edit/5
        [HttpPost]
        public ActionResult Edit(FoodItems_DetailsViewModel viewmodel)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    FoodItems_Details model = foodItems_DetailsServices.FoodItems_Details.GetByID(viewmodel.FoodItemID);
                    model.Code = viewmodel.Code;
                    model.Description = viewmodel.Description;
                    model.UpdatedDate = DateTime.Now;
                    model.UpdatedUserID = CurrentApplicationUser.Id;
                    model.CategoryID = viewmodel.CategoryID;
                    model.KitchenID = viewmodel.KitchenID;
                    model.Offer = viewmodel.Offer;
                    model.Rate = viewmodel.Rate;
                    model.Note = viewmodel.Note;
                    model.Spicy = viewmodel.Spicy;
                    model.IsJam = viewmodel.IsJam;
                    model.IsTodaySpecial = viewmodel.IsTodaySpecial;
                    model.OldPrice = viewmodel.OldPrice;
                    model.NewPrice = viewmodel.NewPrice;
                    foodItems_DetailsServices.FoodItems_Details.Update(model);
                    foodItems_DetailsServices.Save();
                    Success(string.Format("<b>{0}</b> was successfully updated to the system.", viewmodel.Description), true);
                    return RedirectToAction("Index");
                }

            }
            catch (RetryLimitExceededException)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator");
            }
            return View();
        }

        // GET: Vehicle/Delete/5
        public ActionResult Delete(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                FoodItems_Details model = foodItems_DetailsServices.FoodItems_Details.GetByID(id);
                FoodItems_DetailsViewModel viewmodel = new FoodItems_DetailsViewModel();
                viewmodel.FoodItemID = model.FoodItemID;
                viewmodel.Code = model.Code;
                viewmodel.Description = model.Description;
                viewmodel.Offer = model.Offer;
                viewmodel.Rate = model.Rate;
                viewmodel.Spicy = model.Spicy;
                viewmodel.IsJam = model.IsJam;
                viewmodel.Note = model.Note;
                viewmodel.IsTodaySpecial = model.IsTodaySpecial;
                viewmodel.NewPrice = model.NewPrice;
                viewmodel.OldPrice = model.OldPrice;
                viewmodel.Category = foodItems_DetailsServices.Category.GetByID(model.CategoryID);
                viewmodel.Kitchen = foodItems_DetailsServices.Kitchen.GetByID(model.KitchenID);
                return View(viewmodel);
            }
            return View();
        }

        // POST: Vehicle/Delete/5
        [HttpPost]
        public ActionResult Delete(FoodItems_DetailsViewModel viewmodel)
        {
            try
            {
                // TODO: Add delete logic here
                if (!string.IsNullOrEmpty(viewmodel.FoodItemID))
                {
                    FoodItems_Details model = foodItems_DetailsServices.FoodItems_Details.GetByID(viewmodel.FoodItemID);
                    model.Active = false;
                    foodItems_DetailsServices.FoodItems_Details.Update(model);
                    foodItems_DetailsServices.Save();
                    Success(string.Format("<b>{0}</b> was successfully deleted from the system.", viewmodel.Description), true);
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator");
            }
            return View();
        }
    }
}
