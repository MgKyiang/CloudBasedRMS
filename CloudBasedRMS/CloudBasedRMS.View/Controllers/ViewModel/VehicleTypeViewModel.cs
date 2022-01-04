using CloudBasedRMS.View.App_LocalResources;
using System.ComponentModel.DataAnnotations;

namespace CloudBasedRMS.View.Controllers.ViewModel
{
    public class VehicleTypeViewModel
    {
        [ScaffoldColumn(false)]
        public string VehicleTypeID { get; set; }

        [Required(ErrorMessage = "Require Code.")]
        [DataType(DataType.Text)]
        [Display(Name = "Code", ResourceType = typeof(Resource))]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string Code { get; set; }
        [Required(ErrorMessage = "Require Description.")]
        [DataType(DataType.Text)]
        [Display(Name = "Description", ResourceType = typeof(Resource))]
        public string Description { get; set; }
    }
}
