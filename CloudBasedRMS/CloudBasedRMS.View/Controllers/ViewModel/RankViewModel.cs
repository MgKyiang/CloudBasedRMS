using CloudBasedRMS.View.App_LocalResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudBasedRMS.View.Controllers.ViewModel
{
  public class RankViewModel
    {
        public string RankID { get; set; }
        [Required(ErrorMessage = "Require Code.")]
        [Display(Name = "Code", ResourceType = typeof(Resource))]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string Code { get; set; }
        [Required(ErrorMessage = "Require Description.")]
        [Display(Name = "Description", ResourceType = typeof(Resource))]
        public string Description { get; set; }
    }
}
