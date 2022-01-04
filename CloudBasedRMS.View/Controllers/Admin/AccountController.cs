namespace CloudBasedRMS.View.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin.Security;
    using Core;
    using ViewModel;
    using Services;
    using System;
    using TweetSharp;
    using Facebook;
    using System.Web.Security;
    using Common;

    [Authorize]
    public class AccountController : ControllerBase
    {
        #region create instance of services
        public CustomerServices CustomerServices;
        public AddressServices AddressServices;
        public UsersMemberServices UsersMemberServices;
        #endregion

        #region Default Constractor
        public AccountController()
        {
            CustomerServices = new CustomerServices();
            AddressServices = new AddressServices();
            UsersMemberServices = new UsersMemberServices();
        }
        #endregion

        #region for Address drop down list event
        public JsonResult GetAddress()
        {
            var address = AddressServices.Address.GetByAll().Where(x => x.Active == true).ToList();
           // return new JsonResult { Data = address, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            return Json(address.AsEnumerable().ToList(), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region GET Login
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            LoginViewModel login = checkCookie();
            if (login == null)
            {
                BindAddress();
                return View();
            }
            else
            {
               var result=IdentitySignInManager.PasswordSignIn(login.UserName, login.Password, login.RememberMe, shouldLockout: false);
                switch (result)
                {
                    case SignInStatus.Success:
                        return RedirectToLocal(returnUrl, login);
                }              
                BindAddress();
                return View();
            }
        }
        #endregion

        #region bind Address
        private void BindAddress()
        {
            ViewBag.Address = new SelectList(AddressServices.Address.GetByAll().Where(x => x.Active == true).ToList(), "AddressID", "City");
        }
        #endregion

        #region POST Login
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                BindAddress();
                return View(model);              
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            
            var result = await IdentitySignInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl,model);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    TempData["msg"] =App_LocalResources.Resource.Invalidloginattempt;
                    BindAddress();
                    return View(model);
            }
        }
        #endregion

        #region checkCookie
        private LoginViewModel checkCookie()
        {
            //set the Cookie value
            LoginViewModel loginViewModel = null;
            string UserName = string.Empty;
            string Passwrod = string.Empty ;
            if (Request.Cookies["UserName"] != null)
                UserName = Request.Cookies["UserName"].Value;

            if (Request.Cookies["Passwrod"] != null)
                Passwrod = CommonUtils.Decrypt(Request.Cookies["Passwrod"].Value);

            if (!string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Passwrod))
                loginViewModel = new LoginViewModel() { UserName = UserName, Password = Passwrod };
            return loginViewModel;            
        }
        #endregion

        #region GET VerifyCode
        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await IdentitySignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }
        #endregion

        #region POST VerifyCode
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await IdentitySignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }
        #endregion

        #region POST Registor
        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                bool usercheck = IdentityUserManager.Users.Any(x => x.Active == true && x.UserName.Replace(" ", string.Empty) == model.Name.Replace(" ", string.Empty));
                var checkcustomer = CustomerServices.Customer.GetByAll().Any(x => x.Active == true && (model.Name == x.Name || model.Email == x.Email));
                if (!usercheck && !checkcustomer) {
                    //First >>Create Application User[User Acc]
                    var appUser = new ApplicationUser();
                    appUser.Id = Guid.NewGuid().ToString();
                    appUser.UserName = model.Name;
                    appUser.Email = model.Email;
                    appUser.PhoneNumber = model.PhoneNo;
                    appUser.FullName = model.Name;
                    appUser.Designation ="Customer";
                    appUser.Fax = model.MobileNo;
                    appUser.Active = true;
                    appUser.CreatedDate = DateTime.Now;
                    //Create the User with specify user name and password
                    var result = await IdentityUserManager.CreateAsync(appUser, model.Password);
                    if (result.Succeeded)
                    {
                        //Second Create Role for Created Application User[User Acc]
                        var _customerrole = IdentityRoleManager.FindByName("customer");
                        IdentityUserManager.AddToRole(appUser.Id,_customerrole.Name);
                        await IdentitySignInManager.SignInAsync(appUser, isPersistent: false, rememberBrowser: false);
                        //Third Create the Customer Record for Created Applicton User Name[User Acc]
                        Customer customer = new Customer();
                        customer.CustomerID = Guid.NewGuid().ToString();
                        customer.Name = model.Name;
                        customer.Email = model.Email;
                        customer.AddressID = model.AddressID;
                        customer.PhoneNo =model.PhoneNo;
                        customer.MobileNo =model.MobileNo;
                        customer.Note = model.Note;
                        customer.Active = true;
                        customer.CreatedUserID = appUser.Id;
                        customer.CreatedDate = DateTime.Now;
                        //Set the Users Member Entity
                        UsersMember usersMember = new UsersMember()
                        {
                            UsersMemberID = Guid.NewGuid().ToString(),
                            UserID = appUser.Id,
                            UserInMemberID = customer.CustomerID,
                            CreatedDate = DateTime.Now,
                            CreatedUserID = appUser.Id,
                            Active = true,
                            MemberStatus="c"
                        };
                        //Create the Users Member Record!
                        UsersMemberServices.UsersMember.Add(usersMember);
                        UsersMemberServices.Save();
                        //Create the Customer Record.
                        CustomerServices.Customer.Add(customer);
                        CustomerServices.Save();
                        // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                        // Send an email with this link
                        // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                        // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                        // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");
                        TempData["msg"] =App_LocalResources.Resource.Registorprocesssuccessfully;
                        return RedirectToAction("Login");
                    }
                }
                else
                {
                    TempData["msg"] = App_LocalResources.Resource.UserNameisalreadyregister;
                    return RedirectToAction("Login");
                }                     
            }
            // If we got this far, something failed, redisplay form
            BindAddress();
            return View("Login",model);
        }
        #endregion

        #region ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await IdentityUserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }
        #endregion

        #region GET ForgotPassword
        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }
        #endregion


        #region POST ForgotPassword
        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
       // [ValidateAntiForgeryToken]
        public  ActionResult ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user =IdentityUserManager.FindByEmail(model.Email);
                if (user == null) { TempData["FPmsg"] =App_LocalResources.Resource.donotrevealthattheuserdoesnotexist; }
                if (user != null && !(IdentityUserManager.IsEmailConfirmed(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
                // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                // return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View("Login");
        }
        #endregion

        #region GET: /Account/ForgotPasswordConfirmation
        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }
        #endregion

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await IdentityUserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await IdentityUserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await IdentitySignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await IdentityUserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await IdentitySignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await IdentitySignInManager.AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }
            // Sign in the user with this external login provider if the user already has a login
            var result = await IdentitySignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await IdentitySignInManager.AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await IdentityUserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await IdentityUserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await IdentitySignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        #region POST LogOff
        // POST: /Account/LogOff
        //  [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            IdentitySignInManager.AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            //remove cookie
            if (Response.Cookies["UserName"] != null)
            {
                HttpCookie chkUserName = new HttpCookie("UserName");
                chkUserName.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(chkUserName);
            }
            if (Response.Cookies["Passwrod"] != null)
            {
                HttpCookie chkPasswrod = new HttpCookie("Passwrod");
                chkPasswrod.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(chkPasswrod);
            }
            return RedirectToAction("Login", "Account");
        }
        #endregion
        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        #region Twitter login
        [HttpGet]
        public ActionResult TwitterAuth()
        {
            string Key = "gBdUrfCDekapXGJIn9ExzblUp";//Twitter API Key
            string Secret = "RypLIlXhfHjUHQVa5STQsMqrEonm2L4J2JMrNZ14HwfTdxaBrs";//Twitter API Secret

            TwitterService service = new TwitterService(Key, Secret);
            OAuthRequestToken requestToken = service.GetRequestToken("http://localhost:8000/Dashboard/TwitterCallback");
            Uri uri = service.GetAuthenticationUrl(requestToken);
            return Redirect(uri.ToString());
        }
        #endregion

        #region facebook login

        private Uri RediredtUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookCallback");
                return uriBuilder.Uri;
            }
        }


        [AllowAnonymous]
        public ActionResult Facebook()
        {
            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = "Your App ID",
                client_secret = "Your App Secret key",
                redirect_uri = RediredtUri.AbsoluteUri,
                response_type = "code",
                scope = "email"

            });
            return Redirect(loginUrl.AbsoluteUri);
        }

        public ActionResult FacebookCallback(string code)
        {
            var fb = new FacebookClient();
            dynamic result = fb.Post("oauth/access_token", new
            {
                client_id = "Your App ID",
                client_secret = "Your App Secret key",
                redirect_uri = RediredtUri.AbsoluteUri,
                code = code
            });
            var accessToken = result.access_token;
            Session["AccessToken"] = accessToken;
            fb.AccessToken = accessToken;
            dynamic me = fb.Get("me?fields=link,first_name,currency,last_name,email,gender,locale,timezone,verified,picture,age_range");
            string email = me.email;
            TempData["email"] = me.email;
            TempData["first_name"] = me.first_name;
            TempData["lastname"] = me.last_name;
            TempData["picture"] = me.picture.data.url;
            FormsAuthentication.SetAuthCookie(email, false);
            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }           
            return RedirectToAction("Index", "Dashboard");
        }

        private ActionResult RedirectToLocal(string returnUrl, LoginViewModel model)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            if (model.RememberMe)
            {
                HttpCookie chkUserName = new HttpCookie("UserName");
                chkUserName.Expires = DateTime.Now.AddSeconds(3600);
                chkUserName.Value = model.UserName;
                Response.Cookies.Add(chkUserName);
                HttpCookie chkPasswrod = new HttpCookie("Passwrod");
                chkPasswrod.Expires = DateTime.Now.AddSeconds(3600);
                chkPasswrod.Value = CommonUtils.Encrypt(model.Password);
                Response.Cookies.Add(chkPasswrod);
            }
            return RedirectToAction("Index", "Dashboard");
        }
        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}