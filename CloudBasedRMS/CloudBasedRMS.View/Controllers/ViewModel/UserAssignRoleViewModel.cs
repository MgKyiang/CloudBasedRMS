using CloudBasedRMS.Core;
using System.Collections.Generic;

namespace CloudBasedRMS.View.Controllers.ViewModel
{
    public class UserAssignRoleViewModel
    {
        public ApplicationUser ApplicationUser { get; set; }
        public List<ApplicationRole> ApplicationRoles { get; set; }
    }
}
