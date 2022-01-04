using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloudBasedRMS.Core
{
    [Table("TransactionLog")]
    public class TransactionLog : EntityBase
    {
        [Key]
        [ScaffoldColumn(false)]
        public string LogID { get; set; }
        [Display(Name = "Url")]
        public string Url { get; set; }
        [Display(Name = "ServerName")]
        public string ServerName { get; set; }
        [Display(Name = "HostName")]
        public string HostName { get; set; }
        [Display(Name = "Port")]
        public string Port { get; set; }
        [Display(Name = "HttpRequestType")]
        public string HttpRequestType { get; set; }
        [Display(Name = "ControllerName")]
        public string ControllerName { get; set; }
        [Display(Name = "ActionName")]
        public string ActionName { get; set; }
        [Display(Name = "HTTPRequestRawData")]
        public string RawData { get; set; }

    }
}
