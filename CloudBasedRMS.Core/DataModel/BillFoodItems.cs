using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudBasedRMS.Core
{
    [Table("BillFoodItems")]
  public  class BillFoodItems:EntityBase
    {
        [Key]
        public string BillFoodItemsID { get; set; }
        public string SaleBillID { get; set; }
        [ForeignKey("SaleBillID")]
        public virtual SaleBill SaleBill { get; set; }
        public string FoodItemID { get; set; }
        [ForeignKey("FoodItemID")]
        public virtual FoodItems_Details FoodITemsDetails { get; set; }
        public decimal Quantity { get; set; }
        public decimal RatePerItems { get; set; }
        public decimal Amount { get; set; }
    }
}
