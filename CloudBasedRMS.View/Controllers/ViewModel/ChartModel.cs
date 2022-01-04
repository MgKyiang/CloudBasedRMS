using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CloudBasedRMS.View.Controllers.ViewModel
{
    public class ChartModel
    {
        public Dictionary<string, string> columns { get; set; }
        public DataTable rows { get; set; }
        public Dictionary<string, string> Options { get; set; }
        public ChartModel()
        {
            columns = new Dictionary<string, string>();
            Options = new Dictionary<string, string>();
        }
    }
}