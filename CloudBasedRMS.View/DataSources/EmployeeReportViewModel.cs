using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CloudBasedRMS.View.DataSources
{
    public class EmployeeReportViewModel
    {
        
        public string EmployeeNo { get; set; }

        public string Name { get; set; }
     
        public string ImagePath { get; set; }
   
        public string Designation { get; set; }

        public string WorkType { get; set; }

        public string AddressName { get; set; }

        public string PhoneNo { get; set; }
      
        public string MobileNo { get; set; }

        public decimal BasicPay { get; set; }

        public string Sex { get; set; }
      
        public string NRC { get; set; }
     
        public DateTime DOB { get; set; }
   
        public DateTime JoinDate { get; set; }
    
        public string RankName{ get; set; }
 
    }
}