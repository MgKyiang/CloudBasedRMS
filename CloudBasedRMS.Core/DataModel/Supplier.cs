using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudBasedRMS.Core
{
    [Table("Supplier")]
    public class Supplier:EntityBase
    {
        [Key]
        public string SupplierID { get; set; }
        public string SupplierName { get; set; }
        public string Phone { get; set; }
        public string AddressID { get; set; }
        [ForeignKey("AddressID")]
        public virtual Address address { get; set; }
    }
}
