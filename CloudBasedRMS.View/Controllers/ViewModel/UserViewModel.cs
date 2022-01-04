using CloudBasedRMS.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CloudBasedRMS.View.Controllers.ViewModel
{
    public class UserViewModel
    {
        public string Id { get; set; }
        [Remote("CheckUserNameExists", "Users", ErrorMessage = "User Name already exists!")]
        [Display(Name = "UserName")]
        [Required(ErrorMessage = "Require UserName.")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string UserName { get; set; }


        [Required(ErrorMessage = "Require Password.")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Require Email.")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "PhoneNumber")]
        public string PhoneNumber { get; set; }

        [Display(Name = "FullName")]
        public string FullName { get; set; }

        [Display(Name = "NRCNo")]
        public string NRCNo { get; set; }

        [Display(Name = "Designation")]
        public string Designation { get; set; }


        [Display(Name = "Fax")]
        public string Fax { get; set; }

        [Display(Name = "PasswordQuestion")]
        public string PasswordQuestion { get; set; }

        [Display(Name = "PasswordAnswer")]
        public string PasswordAnswer { get; set; }

        public List<ApplicationRole> Roles { get; set; }

        [Required(ErrorMessage = "Require Role.")]
        [Display(Name = "RoleName")]
        public string RoleName { get; set; }

        public string RoleID { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }
}
