using CloudBasedRMS.View.App_LocalResources;
using System.ComponentModel.DataAnnotations;
namespace CloudBasedRMS.View.Controllers.ViewModel
{
    public class RestaurantProfileViewModel
    {  
        public string RestaurantProfileID { get; set; }

        [Required(ErrorMessage = "Required Restaurant Name.")]
        [Display(Name = "RestaurantName", ResourceType = typeof(Resource))]
        [StringLength(200, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string RestaurantName { get; set; }

        [Required(ErrorMessage = "Required Restaurant Address.")]
        [Display(Name = "Address", ResourceType = typeof(Resource))]
        [StringLength(500, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string ContactAddress { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "Required Email Address.")]
        [Display(Name = "Email", ResourceType = typeof(Resource))]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string EmailAddress { get; set; }
        [Required(ErrorMessage = "Required Facebook Address.")]
        [Display(Name = "Facebook", ResourceType = typeof(Resource))]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]

        public string FacebookAddress { get; set; }
        [Required(ErrorMessage = "Required Phone.")]
        [Display(Name = "PhoneNo", ResourceType = typeof(Resource))]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Required Fax.")]
        [Display(Name = "Fax", ResourceType = typeof(Resource))]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string Fax { get; set; }
      
    }
}