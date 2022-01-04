using CloudBasedRMS.Core;
using CloudBasedRMS.View.App_LocalResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudBasedRMS.View.Controllers.ViewModel
{
  public  class CustomerViewModel
    {
        public string CustomerID { get; set; }
        [Required(ErrorMessage = "Require Name.")]
        [Display(Name = "CustomerName", ResourceType = typeof(Resource))]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Require Email.")]
        [EmailAddress]
        [Display(Name = "Email", ResourceType = typeof(Resource))]
        public string Email { get; set; }
        [Required(ErrorMessage = "Require Address.")]
        [Display(Name = "Address", ResourceType = typeof(Resource))]
        public string AddressID { get; set; }
        public Address Address { get; set; }
        [Required(ErrorMessage = "Require PhoneNo.")]
        [Display(Name = "PhoneNo", ResourceType = typeof(Resource))]
        public string PhoneNo { get; set; }
        [Required(ErrorMessage = "Require MobileNo.")]
        [Display(Name = "MobileNo", ResourceType = typeof(Resource))]
        public string MobileNo { get; set; }
        [Display(Name = "Note", ResourceType = typeof(Resource))]
        public string Note { get; set; }
    }
}
