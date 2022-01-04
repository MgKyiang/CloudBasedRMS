using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudBasedRMS.Core;
using CloudBasedRMS.View.App_LocalResources;

namespace CloudBasedRMS.View.Controllers.ViewModel
{
 public   class VehicleViewModel
    {
        public string VehicleID { get; set; }

        [Required(ErrorMessage = "Require Registration No.")]
        [Display(Name = "RegistrationNo", ResourceType = typeof(Resource))]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string RegistrationNo { get; set; }

        [Display(Name = "Status", ResourceType = typeof(Resource))]
        public string Status { get; set; }

        [Required(ErrorMessage = "Require Vehicle Type.")]
        [Display(Name = "VehicleType", ResourceType = typeof(Resource))]
        public string VehicleTypeID { get; set; }
        public VehicleType  VehicleType { get; set; }

    }
}
