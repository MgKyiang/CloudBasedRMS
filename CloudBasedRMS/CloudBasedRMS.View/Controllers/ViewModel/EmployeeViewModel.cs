using CloudBasedRMS.Core;
using CloudBasedRMS.View.App_LocalResources;
using CloudBasedRMS.View.Controllers.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CloudBasedRMS.View.Controllers.ViewModel
{
    public class EmployeeViewModel
    {
        public string EmployeeID { get; set; }
        [Required(ErrorMessage = "Require Employee No.")]
        [Remote("CheckEmployeeNoExists", "Employee", ErrorMessage = "Employee No already exists!")]
        [Display(Name = "EmployeeNo", ResourceType = typeof(Resource))]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string EmployeeNo { get; set; }

        [Required(ErrorMessage = "Require Name.")]
        [Display(Name = "EmployeeName", ResourceType = typeof(Resource))]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Require Image.")]
        [Display(Name = "EmployeePhoto", ResourceType = typeof(Resource))]
        public string ImagePath { get; set; }
        [Required(ErrorMessage = "Require Designation.")]
        [Display(Name = "Designation", ResourceType = typeof(Resource))]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string Designation { get; set; }

        [Required(ErrorMessage = "Require Work Type.")]
        [Display(Name = "WorkType", ResourceType = typeof(Resource))]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string WorkType { get; set; }

        [Required(ErrorMessage = "Require Address.")]
        [Display(Name = "Address", ResourceType = typeof(Resource))]
        public string AddressID { get; set; }
        public Address Address { get; set; }

        [Required(ErrorMessage = "Require Phone No.")]
        [Display(Name = "PhoneNo", ResourceType = typeof(Resource))]
        public string PhoneNo { get; set; }
        [Required(ErrorMessage = "Require Mobile No.")]
        [Display(Name = "MobileNo", ResourceType = typeof(Resource))]
        public string MobileNo { get; set; }

        [Required(ErrorMessage = "Require BasicPay.")]
        [Display(Name = "BasicPay", ResourceType = typeof(Resource))]
        public decimal BasicPay { get; set; }

        [Required(ErrorMessage = "Require Sex.")]
        [Display(Name = "Sex", ResourceType = typeof(Resource))]
        public string Sex { get; set; }
        [Required(ErrorMessage = "Require NRC No.")]
        [Display(Name = "NRCNo", ResourceType = typeof(Resource))]
        public string NRC { get; set; }
        [Required(ErrorMessage = "Require Date of Birth.")]
        [Display(Name = "DateofBirth", ResourceType = typeof(Resource))]
        public DateTime DOB { get; set; }
        [Required(ErrorMessage = "Require Join Date.")]
        [CurrentDate]
        [Display(Name = "JoinDate", ResourceType = typeof(Resource))]
        public DateTime JoinDate { get; set; }
        [Required(ErrorMessage = "Require Rank.")]
        [Display(Name = "Rank", ResourceType = typeof(Resource))]
        public string RankID { get; set; }
        public Rank Rank { get; set; }
    }
}
