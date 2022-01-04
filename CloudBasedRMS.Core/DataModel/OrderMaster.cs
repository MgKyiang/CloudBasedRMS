using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace CloudBasedRMS.Core
{
    [Table("OrderMaster")]
 public   class OrderMaster:EntityBase
    {
        [Key]
        public string OrderMasterID { get; set; }
        public string OrderNo { get; set; }
        public DateTime? OrderDate { get; set; }
        public string Description { get; set; }
        public bool IsParcel { get; set; }
        public string TableID { get; set; }
        [ForeignKey("TableID")]
        public virtual Tables Table { get; set; }
        public virtual ICollection<OrderItems> OrderItems { get; set; }

        public string OrderStatus { get; set; }
        public bool IsBillPaid { get; set; }
    }
}
