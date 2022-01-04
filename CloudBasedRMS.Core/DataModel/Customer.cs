using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudBasedRMS.Core
{
    [Table("Customer")]
 public   class Customer:EntityBase
    {
        [Key]
        public string CustomerID { get; set; }
        [MaxLength(500), MinLength(2)]
        public string Name { get; set; }
        public string Email { get; set; }
        public string AddressID { get; set; }
        [ForeignKey("AddressID")]
        public virtual Address Address { get; set; }
        public string PhoneNo { get; set; }
        public string MobileNo { get; set; }
        [MaxLength(500), MinLength(2)]
        public string Note { get; set; }

    }
}
