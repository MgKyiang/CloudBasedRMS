using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloudBasedRMS.Core
{
    [Table("UserInRole")]
    public class UserInRole : EntityBase
    {
        [Key]
        public string UserInRoleID { get; set; }

        public string UserID { get; set; }
        [ForeignKey("UserID")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        public string RoleID { get; set; }

        [ForeignKey("RoleID")]
        public virtual ApplicationRole ApplicationRole { get; set; }
    }
}
