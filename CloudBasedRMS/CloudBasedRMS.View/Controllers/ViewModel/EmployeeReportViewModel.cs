using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CloudBasedRMS.View.Controllers.ViewModel
{
    public class EmployeeReportViewModel
    {
        [Required]
        [Display(Name = "Rank")]
        public string RankID { get; set; }
    }
}