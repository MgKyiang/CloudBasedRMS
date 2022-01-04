using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CloudBasedRMS.Core
{
    [Table("Authorizations")]
    public class Authorizations : EntityBase
    {
        [Key]
        [ScaffoldColumn(false)]
        public string AuthorizationsID { get; set; }

        [Required(ErrorMessage = "Require ControllerName.")]
        [DataType(DataType.Text)]
        [Display(Name = "ControllerName")]
        [MaxLength(100), MinLength(2)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string ControllerName { get; set; }

        [Required(ErrorMessage = "Require ActionName.")]
        [DataType(DataType.Text)]
        [Display(Name = "ActionName")]
        [MaxLength(100), MinLength(2)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string ActionName { get; set; }

        [Display(Name = "Allow(or)Deny")]
        public bool IsAllow { get; set; }

        public string RoleID { get; set; }

        [ForeignKey("RoleID")]
        public virtual ApplicationRole ApplicationRole { get; set; }

        [Display(Name = "UseLog")]
        public bool IsUseLog { get; set; }

        //public virtual ICollection<MenusInRole> MenusInRole { get; set; }
    }
}
