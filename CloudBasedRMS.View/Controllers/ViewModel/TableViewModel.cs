using CloudBasedRMS.Core;
using CloudBasedRMS.View.App_LocalResources;
using System.ComponentModel.DataAnnotations;

namespace CloudBasedRMS.View.Controllers.ViewModel
{
    public    class TableViewModel
    {
        public string TableID { get; set; }
        [Required(ErrorMessage = "Require Table No.")]
        [Display(Name = "TableNo", ResourceType = typeof(Resource))]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string TableNo { get; set; }
        [Required(ErrorMessage = "Require Capacity.")]
        [Display(Name = "Capacity", ResourceType = typeof(Resource))]
        public decimal Capacity { get; set; }
        [Required(ErrorMessage = "Require Is Available.")]
        [Display(Name = "IsAvailable", ResourceType = typeof(Resource))]
        public bool IsAvailable { get; set; }
        [Required(ErrorMessage = "Status.")]
        [Display(Name = "Status", ResourceType = typeof(Resource))]
        public string Status { get; set; }
        [Required(ErrorMessage = "Require  Employee.")]
        [Display(Name = "EmployeeName", ResourceType = typeof(Resource))]
        public string EmployeeID { get; set; }
        public Employee Employee { get; set; }
    }
}
