using CloudBasedRMS.Core;
using CloudBasedRMS.View.App_LocalResources;
using System.ComponentModel.DataAnnotations;

namespace CloudBasedRMS.View.Controllers.ViewModel
{
    public class SupplierViewModel
    {
        public string SupplierID { get; set; }
        [Required(ErrorMessage = "Require Supplier Name.")]
        [Display(Name = "SupplierName", ResourceType = typeof(Resource))]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string SupplierName { get; set; }
        [Required(ErrorMessage = "Require Phone.")]
        [Display(Name = "PhoneNo", ResourceType = typeof(Resource))]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Require Address.")]
        [Display(Name = "Address", ResourceType = typeof(Resource))]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string AddressID { get; set; }
        public Address Address { get; set; }
    }
}