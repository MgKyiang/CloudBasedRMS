using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudBasedRMS.Core
{
    [Table("Kitchen")]
 public   class Kitchen:EntityBase
    {
        [Key]
        public string KitchenID { get; set; }
        public string KitchenName { get; set; }
        public string KitchenDescription { get; set; }
        public string EmployeeID { get; set; }

        [ForeignKey("EmployeeID")]
        public virtual Employee Employee { get; set; }
    }
}
