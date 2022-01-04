using CloudBasedRMS.View.App_LocalResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudBasedRMS.View.Controllers.ViewModel
{
  public  class AddressViewModel
    {
        public string AddressID { get; set; }
        [Required(ErrorMessage = "Require City.")]
        [Display(Name = "City", ResourceType = typeof(Resource))]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        public string City { get; set; }
        [Required(ErrorMessage = "Require Township.")]

        [Display(Name = "Township", ResourceType = typeof(Resource))]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string Township { get; set; }
        [Required(ErrorMessage = "Require Place.")]
        [Display(Name = "Place", ResourceType = typeof(Resource))]
        public string Place { get; set; }

        [Required(ErrorMessage = "Require ZipCode.")]
        [Display(Name = "ZipCode", ResourceType = typeof(Resource))]
        public int ZipCode { get; set; }

        [Required(ErrorMessage = "Require Area.")]

        [Display(Name = "Area", ResourceType = typeof(Resource))]
        public string Area { get; set; }
    }
}
