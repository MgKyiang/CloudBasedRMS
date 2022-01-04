using CloudBasedRMS.View.App_LocalResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CloudBasedRMS.View.Controllers.ViewModel
{
    public class SaleBillReportByFromToDateViewModel
    {
        [Required]
        [Display(Name = "SaleBillFromDate", ResourceType = typeof(Resource))]
        [DataType(DataType.Date),DisplayFormat(DataFormatString = "{0: dd/mm/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime SaleBillFromDate { get; set; }
        [Required]
        [Display(Name = "SaleBillToDate", ResourceType = typeof(Resource))]
        [DataType(DataType.Date),DisplayFormat(DataFormatString = "{0: dd/mm/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime SaleBillToDate { get; set; }
    }
}