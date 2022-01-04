using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CloudBasedRMS.Core
{
    [Table("Vehicle")]
    public   class Vehicle:EntityBase
    {
        [Key]
        public string VehicleID { get; set; }
        [MaxLength(500), MinLength(2)]
        public string RegistrationNo { get; set; }
        public string VehicleTypeID { get; set; }
        public string Status { get; set; }
        [ForeignKey("VehicleTypeID")]
        public virtual VehicleType VehicleType{ get; set; }
    }
}
