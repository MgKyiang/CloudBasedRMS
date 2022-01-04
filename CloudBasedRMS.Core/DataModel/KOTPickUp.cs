using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloudBasedRMS.Core
{
    [Table("KOTPickUp")]
    public class KOTPickUp
    {
        [Key]
        public string KOTPickUpID { get; set; }

        public string OrderMasterID { get; set; }
        [ForeignKey("OrderMasterID")]
        public virtual OrderMaster OrderMaster { get; set; }

        public string OrderItemsID { get; set; }
        [ForeignKey("OrderItemsID")]
        public virtual OrderItems orderItems { get; set; }

        public bool IsReadyPickup { get; set; }
    }
}
