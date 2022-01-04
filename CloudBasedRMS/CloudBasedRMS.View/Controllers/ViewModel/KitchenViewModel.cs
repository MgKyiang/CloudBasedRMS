using CloudBasedRMS.Core;
using CloudBasedRMS.View.App_LocalResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CloudBasedRMS.View.Controllers.ViewModel
{
    public class KitchenViewModel
    {
        public string KitchenID { get; set; }
        [Required(ErrorMessage = "Require Kitchen Name.")]
        [Display(Name = "KitchenName", ResourceType = typeof(Resource))]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        public string KitchenName { get; set; }
        [Required(ErrorMessage = "Require Kitchen Description.")]
        [Display(Name = "KitchenDescription", ResourceType = typeof(Resource))]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        public string KitchenDescription { get; set; }


        [Required(ErrorMessage = "Require Employee Name.")]
        [Display(Name = "EmployeeName", ResourceType = typeof(Resource))]
        public string EmployeeID { get; set; }
        public Employee Employee { get; set; }
    }
}