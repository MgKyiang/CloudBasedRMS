using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
namespace CloudBasedRMS.Core
{
    [Table("Employee")]
 public   class Employee:EntityBase
    {
        [Key]
        public string EmployeeID { get; set; }
        public string EmployeeNo { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public string RankID { get; set; }
        [ForeignKey("RankID")]
        public virtual Rank Rank { get; set; }
        public string WorkType { get; set; }
        public string AddressID { get; set; }
        [ForeignKey("AddressID")]
        public virtual Address Address { get; set; }
        public string PhoneNo { get; set; }
        public string MobileNo { get; set; }
        public decimal BasicPay { get; set; }
        public string Sex { get; set; }
        public string NRC { get; set; }
        public DateTime DOB { get; set; }
        public DateTime JoinDate { get; set; }
    }
}
