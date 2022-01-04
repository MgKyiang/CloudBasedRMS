using CloudBasedRMS.Core;
using CloudBasedRMS.Services;
using CloudBasedRMS.View.Controllers.Common;
using CloudBasedRMS.View.Controllers.ViewModel;
using Microsoft.AspNet.Identity;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
namespace CloudBasedRMS.View.Controllers
{
    public class UsersController : ControllerAuthorizeBase
    {
        #region instance define
        //create instance
        private UserInRole userInRole;
        public UsersMemberServices UsersMemberServices;
        public EmployeeServices EmployeeServices;
        public CustomerServices CustomerServices;
        #endregion

        //create default constructor
        public UsersController()
        {
            userInRole = new UserInRole();
            UsersMemberServices = new UsersMemberServices();
            EmployeeServices = new EmployeeServices();
            CustomerServices = new CustomerServices();
        }
        public JsonResult getUsers()
        {
            List<UserViewModel> userlist = (from r in IdentityRoleManager.Roles.ToList()
                                            from u in IdentityUserManager.Users.Where(x => x.Active == true).ToList()
                                            where r.Id == u.Roles.Select(x => x.RoleId).SingleOrDefault()
                                            select new UserViewModel
                                            {
                                                Id = u.Id,
                                                UserName = u.UserName,
                                                FullName = u.FullName,
                                                Email = u.Email,
                                                RoleID = r.Id,
                                                RoleName = r.Name,
                                                PhoneNumber = u.PhoneNumber,
                                                NRCNo = u.NRCNo,
                                                Designation = u.Designation,
                                                Fax = u.Fax,
                                                PasswordQuestion = u.PasswordQuestion,
                                                PasswordAnswer = u.PasswordAnswer,
                                                CreatedDate = u.CreatedDate,
                                                UpdatedDate = u.UpdatedDate
                                            }).ToList();
            return new JsonResult { Data = userlist, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult UserList()
        {
            return View();
        }
        // GET: Admin/Users
        public ActionResult Index(string searchby1, string search1, string searchby2, string search2, string searchby3, string search3, string searchby4, string search4, string sortby, int? page, string status)
        {
            //Sorting ViewBag
            ViewBag.FullNameSortby = sortby == "FullNameAsc" ? "FullNameDesc" : "FullNameAsc";
            ViewBag.UserNameSortby = sortby == "UserNameAsc" ? "UserNameDesc" : "UserNameAsc";
            ViewBag.EmailSortby = sortby == "EmailAsc" ? "EmailDesc" : "EmailAsc";
            ViewBag.RoleNameSortby = sortby == "RoleNameAsc" ? "RoleNameDesc" : "RoleNameAsc";
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
            List<UserViewModel> data = (from r in IdentityRoleManager.Roles.ToList()
                                        from u in IdentityUserManager.Users.Where(x => x.Active == true).ToList()
                                        where r.Id == u.Roles.Select(x => x.RoleId).SingleOrDefault()
                                        select new UserViewModel
                                        {
                                            Id = u.Id,
                                            UserName = u.UserName,
                                            FullName = u.FullName,
                                            Email = u.Email,
                                            RoleID = r.Id,
                                            RoleName = r.Name,
                                            PhoneNumber = u.PhoneNumber,
                                            NRCNo = u.NRCNo,
                                            Designation = u.Designation,
                                            Fax = u.Fax,
                                            PasswordQuestion = u.PasswordQuestion,
                                            PasswordAnswer = u.PasswordAnswer,
                                            CreatedDate = u.CreatedDate,
                                            UpdatedDate = u.UpdatedDate
                                        }).ToList();
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
                    case "FullNameAsc":
                        data = data.OrderBy(x => x.FullName).ToList();
                        break;
                    case "FullNameDesc":
                        data = data.OrderByDescending(x => x.FullName).ToList();
                        break;
                    case "UserNameAsc":
                        data = data.OrderBy(x => x.UserName).ToList();
                        break;
                    case "UserNameDesc":
                        data = data.OrderByDescending(x => x.UserName).ToList();
                        break;
                    case "EmailAsc":
                        data = data.OrderBy(x => x.Email).ToList();
                        break;
                    case "EmailDesc":
                        data = data.OrderByDescending(x => x.Email).ToList();
                        break;
                    case "RoleNamelAsc":
                        data = data.OrderBy(x => x.RoleName).ToList();
                        break;
                    case "RoleNameDesc":
                        data = data.OrderByDescending(x => x.RoleName).ToList();
                        break;
                }
            }
            return View(data.ToPagedList(page ?? 1, ApplicationSettingPagingSize));
        }

        [HttpGet]
        public ActionResult Create(string status)
        {
            //show message
            ViewBag.Status = status;
            UserViewModel model = new UserViewModel();
            model.Roles = IdentityRoleManager.Roles.ToList();
            return View(model);
        }
        //Remote Validation in MVC:that is grate in mvc.
        public ActionResult CheckUserNameExists(UserViewModel model)
        {
            bool UserExists = false;
            try
            {
                bool usercheck = IdentityUserManager.Users.Any(x => x.Active == true && x.UserName.Replace(" ", string.Empty) == model.UserName.Replace(" ", string.Empty));
                if (!usercheck)
                {
                    UserExists = false;
                }
                else
                {
                    UserExists = true;
                }
                return Json(!UserExists, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                bool usercheck = IdentityUserManager.Users.Any(x => x.Active == true && x.UserName.Replace(" ", string.Empty) == model.UserName.Replace(" ", string.Empty));
                if (!usercheck)
                {
                    var appUser = new ApplicationUser();
                    appUser.Id = Guid.NewGuid().ToString();
                    appUser.UserName = model.UserName;
                    appUser.Email = model.Email;
                    appUser.PhoneNumber = model.PhoneNumber;
                    appUser.FullName = model.FullName;
                    appUser.NRCNo = model.NRCNo;
                    appUser.Designation = model.Designation;
                    appUser.Fax = model.Fax;
                    appUser.PasswordQuestion = model.PasswordQuestion;
                    appUser.PasswordAnswer = CommonUtils.Encrypt(model.PasswordAnswer);
                    appUser.Active = true;
                    appUser.CreatedDate = DateTime.Now;

                    IdentityResult result = IdentityUserManager.Create(appUser, model.Password);

                    if (result.Succeeded)
                    {
                        IdentityUserManager.AddToRole(appUser.Id, model.RoleName);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return View();
                    }
                }
                else
                {
                    return RedirectToAction("Create", new { status = "UserName is Already Register!" });
                }


            }
            return View();
        }

        [HttpGet]
        public ActionResult Edit(string Id, string status)
        {
            //For Show Error Message
            if (!string.IsNullOrEmpty(status))
            {
                ViewBag.Status = status;
            }

            if (!string.IsNullOrEmpty(Id))
            {
                ApplicationUser user = IdentityUserManager.Users.Where(x => x.Id == Id && x.Active == true).SingleOrDefault();
                //get PasswordAnswer
                string PasswordAnswer = string.Empty;
                if (!string.IsNullOrEmpty(user.PasswordAnswer))
                    PasswordAnswer = CommonUtils.Decrypt(user.PasswordAnswer);
                else PasswordAnswer = user.PasswordAnswer;

                UserViewModel model = new UserViewModel()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    FullName = user.FullName,
                    NRCNo = user.NRCNo,
                    Designation = user.Designation,
                    Fax = user.Fax,
                    PasswordQuestion = user.PasswordQuestion,
                    PasswordAnswer = PasswordAnswer,
                    //get role info by userid
                    Roles = IdentityUserManager.Users
                      .Where(u => u.Id == Id)
                      .SelectMany(u => u.Roles)
                      .Join(IdentityRoleManager.Roles, ur => ur.RoleId, r => r.Id, (ur, r) => r)
                      .ToList()
                };

                return View(model);
            }

            return View();
        }

        public ActionResult Edit(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = IdentityUserManager.Users.Where(x => x.Id == model.Id && x.Active == true).SingleOrDefault();

                bool usercheck = IdentityUserManager.Users.Any(x => x.Active == true && model.UserName.Replace(" ", string.Empty) != user.UserName.Replace(" ", string.Empty) && x.UserName.Replace(" ", string.Empty) == model.UserName.Replace(" ", string.Empty));

                if (!usercheck)
                {
                    //get existedRoles
                    List<string> existedRoleName = IdentityUserManager.GetRoles(user.Id).ToList();

                    //remove all role
                    foreach (var role in existedRoleName)
                    {
                        IdentityUserManager.RemoveFromRole(user.Id, role);
                    }
                    var passwordHasher = new PasswordHasher();
                    user.PasswordHash = passwordHasher.HashPassword(model.Password);

                    user.UserName = model.UserName;
                    user.FullName = model.FullName;
                    user.Email = model.Email;
                    user.Fax = model.Fax;
                    user.NRCNo = model.NRCNo;
                    user.PhoneNumber = model.PhoneNumber;
                    user.PasswordQuestion = model.PasswordQuestion;
                    user.PasswordAnswer = CommonUtils.Encrypt(model.PasswordAnswer);
                    user.Designation = model.Designation;
                    user.UpdatedDate = DateTime.Now;

                    IdentityResult result = IdentityUserManager.Update(user);

                    if (result.Succeeded)
                    {
                        IdentityUserManager.AddToRole(user.Id, model.RoleName);

                        return RedirectToAction("Index", new { status = "Upate Successful!" });
                    }
                }
                else
                {
                    return RedirectToAction("Edit", new { Id = model.Id, status = "UserName is Already Register!" });
                }
            }

            return RedirectToAction("Edit", new { Id = model.Id, status = "Invalid Update!" });
        }


        [HttpPost]
        public JsonResult Delete(string Id)
        {
            if (!string.IsNullOrEmpty(Id))
            {
                ApplicationUser user = IdentityUserManager.Users.Where(x => x.Id == Id && x.Active == true).SingleOrDefault();
                if (user != null)
                {
                    user.Active = false;
                    IdentityResult reuslt = IdentityUserManager.Update(user);
                    if (reuslt.Succeeded)
                    {
                        return Json(true, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(false, JsonRequestBehavior.AllowGet);
                    }
                }
            }

            return Json(false, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult AssignRole(string userId)
        {
            ApplicationUser user = IdentityUserManager.Users.Where(x => x.Id == userId && x.Active == true).SingleOrDefault();
            List<ApplicationRole> roles = IdentityRoleManager.Roles.ToList();
            UserAssignRoleViewModel model = new UserAssignRoleViewModel()
            {
                ApplicationUser = user,
                ApplicationRoles = roles
            };

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AssignRole(string userId, string roleId)
        {
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(roleId))
            {
                try
                {
                    //getuser
                    ApplicationUser user = IdentityUserManager.Users.Where(x => x.Id == userId).SingleOrDefault();
                    //get Old RoleId
                    string existedRoleId = user.Roles.Select(x => x.RoleId).SingleOrDefault();
                    //Get Old Role
                    ApplicationRole existedRole = IdentityRoleManager.FindById(existedRoleId);
                    //Remove Old Role From User
                    IdentityUserManager.RemoveFromRole(user.Id, existedRole.Name);
                    //Get New Role
                    ApplicationRole newRole = IdentityRoleManager.Roles.Where(x => x.Id == roleId).SingleOrDefault();
                    //Add New Role To user
                    IdentityUserManager.AddToRole(user.Id, newRole.Name);

                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }


            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetUsersByRole(string roleID)
        {
            if (!string.IsNullOrEmpty(roleID))
            {
                List<ApplicationUser> data = IdentityUserManager.Users.Where(x => x.Roles.Select(i => i.RoleId).Contains(roleID)).ToList();
                return View(data);
            }
            return View();
        }

        public ActionResult UserProfile(string id)
        {
            if (id != null)
            {
                UsersMember model = UsersMemberServices.UsersMember.GetByUserID(id);
                if (model != null)
                {
                    UserProfileViewModel userprofileviewmodel = new UserProfileViewModel();
                    userprofileviewmodel.UsersMember = UsersMemberServices.UsersMember.GetByUserID(id);
                    if (model.MemberStatus == "e")
                    {
                        userprofileviewmodel.Employee = EmployeeServices.Employee.GetByID(model.UserInMemberID);
                        return View(userprofileviewmodel);
                    }
                    else
                    {
                        userprofileviewmodel.Customer = CustomerServices.Customer.GetByID(model.UserInMemberID);
                        return View(userprofileviewmodel);
                    }
                }

            }
            return View();
        }

        //Searching Helper
        private List<UserViewModel> GetSearchingData(string searchby, string search, List<UserViewModel> data)
        {
            switch (searchby)
            {
                case "FullName":
                    data = data.Where(x => x.FullName.Contains(search)).ToList();
                    break;
                case "UserName":
                    data = data.Where(x => x.UserName.Contains(search)).ToList();
                    break;
                case "Email":
                    data = data.Where(x => x.Email.Contains(search)).ToList();
                    break;
                case "PhoneNumber":
                    data = data.Where(x => x.PhoneNumber.Contains(search)).ToList();
                    break;
                case "NRCNo":
                    data = data.Where(x => x.NRCNo.Contains(search)).ToList();
                    break;
                case "Designation":
                    data = data.Where(x => x.Designation.Contains(search)).ToList();
                    break;
                case "Fax":
                    data = data.Where(x => x.Fax.Contains(search)).ToList();
                    break;
                case "PasswordQuestion":
                    data = data.Where(x => x.PasswordQuestion.Contains(search)).ToList();
                    break;
                case "PasswordAnswer":
                    data = data.Where(x => x.PasswordAnswer.Contains(search)).ToList();
                    break;
                case "RoleName":
                    data = data.Where(x => x.RoleName.Contains(search)).ToList();
                    break;
                case "CreatedDate":
                    DateTime createdDate;
                    if (DateTime.TryParse(search, out createdDate))
                    {
                        data = data.Where(x => x.CreatedDate == createdDate).ToList();
                    }
                    break;
                case "UpdatedDate":
                    DateTime updatedDate;
                    if (DateTime.TryParse(search, out updatedDate))
                    {
                        data = data.Where(x => x.UpdatedDate == updatedDate).ToList();
                    }
                    break;
            }
            return data;
        }
    }
}