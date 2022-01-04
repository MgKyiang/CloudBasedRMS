using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloudBasedRMS.Core
{
    [Table("VehicleType")]
  public  class VehicleType:EntityBase
    {
        [Key]
        public string VehicleTypeID { get; set; }
        [MaxLength(500), MinLength(2)]
        public string Code { get; set; }
        [MaxLength(4000), MinLength(2)]
        public string Description { get; set; }
    }
}
