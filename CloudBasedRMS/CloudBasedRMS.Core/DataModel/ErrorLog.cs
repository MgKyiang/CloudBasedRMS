using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloudBasedRMS.Core
{
    [Table("ErrorLog")]
    public class ErrorLog : EntityBase
    {
        [Key]
        [ScaffoldColumn(false)]
        public string ErrorLogID { get; set; }

        [Display(Name = "Url")]
        public string Url { get; set; }

        [Display(Name = "ControllerName")]
        public string ControllerName { get; set; }

        [Display(Name = "ActionName")]
        public string ActionName { get; set; }

        [Display(Name = "ErrorMessage")]
        public string ErrorMessage { get; set; }

        [Display(Name = "UserName")]
        public string IdentityName { get; set; }

        [Display(Name = "MachineName")]
        public string MachineName { get; set; }

        [Display(Name = "ClientMachineName")]
        public string ClientMachineName { get; set; }

        [Display(Name = "ClientIPAddress")]
        public string ClientIPAddress { get; set; }
    }
}
