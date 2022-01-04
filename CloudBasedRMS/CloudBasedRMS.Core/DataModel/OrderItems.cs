using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloudBasedRMS.Core
{
    [Table("OrderItems")]
    public class OrderItems : EntityBase
    {
        [Key]
        public string OrderItemsID { get; set; }
        public string OrderMasterID { get; set; }
        [ForeignKey("OrderMasterID")]
        public virtual OrderMaster OrderMaster { get; set; }
        public string FoodItemID { get; set; }
        [ForeignKey("FoodItemID")]
        public virtual FoodItems_Details FoodItems_Details { get; set; }
        public decimal Quantity { get; set; }
        public decimal RatePerItems { get; set; }
        public decimal Amount { get; set; }
    }
}
