using CloudBasedRMS.View.App_LocalResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CloudBasedRMS.View.Controllers.ViewModel
{
    public class EventViewModel
    {
        public string EventID { get; set; }
        
        [Required(ErrorMessage = "Require Title.")]
        [Display(Name = "Title", ResourceType = typeof(Resource))]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        public string Title { get; set; }
      
        [Required(ErrorMessage = "Require Start Date.")]
        [Display(Name = "Start Date")]
        public DateTime Start { get; set; }
        [Required(ErrorMessage = "Require End Date.")]
        [Display(Name = "End Date")]
        public DateTime End { get; set; }
        [Required(ErrorMessage = "Require ThemeColor.")]
        [Display(Name = "ThemeColor",ResourceType = typeof(Resource))]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        public string ThemeColor { get; set; }
        [Required(ErrorMessage = "Require Is FullDay.")]
        [Display(Name = "IsFullDay", ResourceType = typeof(Resource))]
        public bool IsFullDay { get; set; }
    }
}