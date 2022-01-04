using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudBasedRMS.View.Controllers.Helper
{
   public class CBRMSHelper
    {
        public DateTime ConvertDateTime(string ddMMyyyy)
        {
            return DateTime.ParseExact(ddMMyyyy, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        }
        public string ConvertToMonth(int m)
        {
            string month = string.Empty;
            switch (m)
            {
                case 1:return month="January";
                case 2:return month = "Febaury";
                case 3:return month = "March";
                case 4:return month = "Aprial";
                case 5:return month = "May";
                case 6: return month = "June";
                case 7: return month = "July";
                case 8: return month = "August";
                case 9: return month = "September";
                case 10: return month = "October";
                case 11: return month = "November";            
                case 12: return month = "December";
            }
            return month;
        }

        public List<string> ConvertToMonth(IEnumerable<int> mlist)
        {
            List<string> month = new List<string>();
            foreach (var disitems in mlist)
            {
                if (disitems == 1)
                {
                    month.Add("January");
                }else if (disitems == 2)
                {
                    month.Add("Febaury");
                }
                else if (disitems == 3)
                {
                    month.Add("March");
                }
                else if (disitems == 4)
                {
                    month.Add("Aprial");
                }
                else if (disitems == 5)
                {
                    month.Add("May");
                }
                else if (disitems == 6)
                {
                    month.Add("June");
                }
                else if (disitems == 7)
                {
                    month.Add("July");
                }
                else if (disitems == 8)
                {
                    month.Add("August");
                }
                else if (disitems == 9)
                {
                    month.Add("September");
                }
                else if (disitems == 10)
                {
                    month.Add("October");
                }
                else if (disitems == 11)
                {
                    month.Add("Nobember");
                }
                else if (disitems == 12)
                {
                    month.Add("December");
                }

            }
                
            return month;
        }
    }
}
