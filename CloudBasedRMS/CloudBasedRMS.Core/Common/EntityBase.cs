using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CloudBasedRMS.Core
{
    public class EntityBase
    {
        [ScaffoldColumn(false)]
        public string CreatedUserID { get; set; }

        [ForeignKey("CreatedUserID")]
        public virtual ApplicationUser CreatedUser { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "CreatedDate")]
        public DateTime CreatedDate { get; set; }

        [ScaffoldColumn(false)]
        public string UpdatedUserID { get; set; }

        [ForeignKey("UpdatedUserID")]
        public virtual ApplicationUser UpdatedUser { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "UpdatedDate")]
        public DateTime? UpdatedDate { get; set; }

        [ScaffoldColumn(false)]
        public bool Active { get; set; }

        [ScaffoldColumn(false)]
        [Timestamp]
        public byte[] RowVersion { get; set; }

    }
}
