using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudBasedRMS.Core
{
    [Table("RestaurantProfile")]
public    class RestaurantProfile:EntityBase
    {
        [Key]
        public string RestaurantProfileID { get; set; }
        public string RestaurantName { get; set; }
        public string ContactAddress { get; set; }
        public string EmailAddress { get; set; }
        public string FacebookAddress { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public byte[] Logo { get; set; }
    }
}
