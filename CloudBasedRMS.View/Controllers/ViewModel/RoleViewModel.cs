using CloudBasedRMS.View.App_LocalResources;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CloudBasedRMS.View.Controllers.ViewModel
{
    public class RoleViewModel
    {
        public string Id { get; set; }
        [Remote("CheckRoleNameExists", "Roles", ErrorMessage = "Role Name already exists!")]
        [Required(ErrorMessage = "Require RoleName.")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [Display(Name = "RoleName", ResourceType = typeof(Resource))]
        public string RoleName { get; set; }
    }
}
