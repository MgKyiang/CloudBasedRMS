using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CloudBasedRMS.Core
{
    [Table("Address")]
  public  class Address:EntityBase
    {
        [Key]
        public string AddressID { get; set; }
        public string City { get; set; }
        public string Township { get; set; }
        public string Place { get; set; }
        public int ZipCode { get; set; }
        public string Area { get; set; }
    }
}
