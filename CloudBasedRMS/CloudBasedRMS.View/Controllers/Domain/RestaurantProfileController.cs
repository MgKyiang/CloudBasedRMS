using CloudBasedRMS.Core;
using CloudBasedRMS.Services;
using CloudBasedRMS.View.Controllers.ViewModel;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace CloudBasedRMS.View.Controllers
{
    public class RestaurantProfileController : ControllerAuthorizeBase
    {
        private RestaurantProfileServices restaurantProfileServices;
        public RestaurantProfileController()
        {
            restaurantProfileServices = new RestaurantProfileServices();
        }
        public ActionResult Index()
        {
            var data = restaurantProfileServices.RestaurantProfile.GetByAll().Where(x => x.Active == true).ToList();
            if (data.Count == 0) ViewBag.IsRecord = "Yes";
            return View(data);
        }
        // GET:RestaurantProfiles/Create
        public ActionResult Create()
        {
            RestaurantProfile model = restaurantProfileServices.RestaurantProfile.GetRestaurantProfile();
            RestaurantProfileViewModel restaurantProfileviewmodel = new RestaurantProfileViewModel();
            if (model != null)
            {

                restaurantProfileviewmodel.RestaurantProfileID = model.RestaurantProfileID;
                restaurantProfileviewmodel.RestaurantName = model.RestaurantName;
                restaurantProfileviewmodel.ContactAddress = model.ContactAddress;
                restaurantProfileviewmodel.EmailAddress = model.EmailAddress;
                restaurantProfileviewmodel.FacebookAddress = model.FacebookAddress;
                restaurantProfileviewmodel.Fax = model.Fax;
                restaurantProfileviewmodel.Phone = model.Phone;
                //restaurantProfileviewmodel.Logo = model.Logo;
            }
            return View(restaurantProfileviewmodel);
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        //[ValidateInput(false)]
        public ActionResult Create(RestaurantProfileViewModel restaurantProfileViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    HttpPostedFileBase LogoFile = Request.Files["Logo"];
                    if (LogoFile.ContentLength == 0)
                    {
                        ModelState.AddModelError("CustomError", "Restaurant Logo is required.Please select a valid logo picture on attachement.");
                        return View(restaurantProfileViewModel);
                    }
                    else if (LogoFile.ContentLength > 2 * 102481024)
                    {
                        ModelState.AddModelError("CustomError", "File size must be less than 2 MB.");
                        return View(restaurantProfileViewModel);
                    }
                    else
                    {
                        string[] formats = { ".jpeg", ".png", ".jpg", ".bmp", ".jpg" };
                        string extension = Path.GetExtension(LogoFile.FileName);
                        if (!formats.Contains(extension))
                        {
                            ModelState.AddModelError("CustomError", "Only jpeg, png, jpg, bmp format are allowed.Please select a valid logo picture.");
                            return View(restaurantProfileViewModel);
                        }
                        string filePath = Server.MapPath("~/Images/RestaurantLogo/") + LogoFile.FileName;
                        LogoFile.SaveAs(filePath);
                        Bitmap bmp = (Bitmap)Image.FromFile(filePath);
                        bmp.Save(Server.MapPath("~/Images/RestaurantLogo/RestaurantLogo.jpg"), System.Drawing.Imaging.ImageFormat.Png);
                        bmp.Dispose();
                        FileInfo fileInfo = new FileInfo(filePath);
                        fileInfo.Delete();
                    }

                    RestaurantProfile model = new RestaurantProfile
                    {
                        RestaurantProfileID = Guid.NewGuid().ToString(),
                        RestaurantName = restaurantProfileViewModel.RestaurantName,
                        ContactAddress = restaurantProfileViewModel.ContactAddress,
                        EmailAddress = restaurantProfileViewModel.EmailAddress,
                        FacebookAddress = restaurantProfileViewModel.FacebookAddress,
                        Phone = restaurantProfileViewModel.Phone,
                        Fax = restaurantProfileViewModel.Fax,
                        CreatedDate = DateTime.Now,
                        CreatedUserID = CurrentApplicationUser.Id,
                        Active = true
                    };
                    if (restaurantProfileServices.RestaurantProfile.DeleteCurrentRestaurantProfile())
                    {
                        restaurantProfileServices.SaveToDb(LogoFile, model);
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("CustomError", ex.Message);
                return View(restaurantProfileViewModel);
            }
        }
    }
}