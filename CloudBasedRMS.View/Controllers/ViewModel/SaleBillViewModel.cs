using CloudBasedRMS.Core;
using CloudBasedRMS.View.App_LocalResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CloudBasedRMS.View.Controllers.ViewModel
{
 public   class SaleBillViewModel
    {
        public string SaleBillID { get; set; }
        [Required(ErrorMessage = "Require Sale Bill No.")]
        [Display(Name = "SaleBillNo", ResourceType = typeof(Resource))]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string SaleBillNo { get; set; }
        [Required(ErrorMessage = "Require Sale Bill Date.")]
        [Display(Name = "SaleBillDate", ResourceType = typeof(Resource))]
        public DateTime SaleBillDate { get; set; }
        [Required(ErrorMessage = "Require Employee Name.")]
        [Display(Name = "EmployeeName", ResourceType = typeof(Resource))]
        public string EmployeeID { get; set; }
        public Employee Employee { get; set; }
        [Required(ErrorMessage = "Require Order Master.")]
        [Display(Name = "OrderMaster", ResourceType = typeof(Resource))]
        public string OrderMasterID { get; set; }
        public OrderMaster OrderMaster { get; set; }

        [Required(ErrorMessage = "Require Customer Name.")]
        [Display(Name = "CustomerName", ResourceType = typeof(Resource))]
        public string CustomerID { get; set; }
        public Customer Customer { get; set; }

        [Required(ErrorMessage = "Require Amount.")]
        [Display(Name = "Amount", ResourceType = typeof(Resource))]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Require Tax.")]
        [Display(Name = "Tax", ResourceType = typeof(Resource))]
        public decimal Tax { get; set; }

        [Required(ErrorMessage = "Require Discount.")]
        [Display(Name = "Discount", ResourceType = typeof(Resource))]
        public decimal Discount { get; set; }

        [Required(ErrorMessage = "Require NetAmount.")]
        [Display(Name = "NetAmount", ResourceType = typeof(Resource))]
        public decimal NetAmount { get; set; }
    }
}
