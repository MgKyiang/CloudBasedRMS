using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.ComponentModel.DataAnnotations;

namespace CloudBasedRMS.Core
{
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole()
        {
            base.Id = Guid.NewGuid().ToString();
        }
        public ApplicationRole(string roleName) : this()
        {
            base.Name = roleName;
        }

        [Display(Name = "CreatedDate")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "UpdatedDate")]
        public DateTime? UpdatedDate { get; set; }

        [Display(Name = "BuildIn")]
        public bool IsBuildIn { get; set; }

    }
}
