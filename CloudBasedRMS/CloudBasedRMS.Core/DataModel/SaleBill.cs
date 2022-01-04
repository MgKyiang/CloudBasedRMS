using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CloudBasedRMS.Core
{
    [Table("SaleBill")]
 public   class SaleBill:EntityBase
    {
        [Key]
        public string SaleBillID { get; set; }
        public string SaleBillNo { get; set; }
        public DateTime SaleBillDate { get; set; }
        public string EmployeeID { get; set; }
        [ForeignKey("EmployeeID")]
        public virtual Employee Employee { get; set; }
        public string OrderMasterID { get; set; }
        [ForeignKey("OrderMasterID")]
        public virtual OrderMaster OrderMaster { get; set; }
        public string CustomerID { get; set; }
        [ForeignKey("CustomerID")]
        public virtual Customer Customer { get; set; }
        public decimal Amount { get; set; }
        public decimal Tax { get; set; }
        public decimal Discount { get; set; }
        public decimal NetAmount { get; set; }
    }
}
