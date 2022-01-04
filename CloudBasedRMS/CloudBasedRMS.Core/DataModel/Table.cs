using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudBasedRMS.Core
{
    [Table("Tables")]
 public   class Tables:EntityBase
    {
        [Key]
        public string TableID { get; set; }
        public string TableNo { get; set; }
        public decimal Capacity { get; set; }
        public bool IsAvailable { get; set; }
        public string Status { get; set; }
        public string EmployeeID { get; set; }
        [ForeignKey("EmployeeID")]
        public virtual Employee Employee { get; set; }
    }
}
