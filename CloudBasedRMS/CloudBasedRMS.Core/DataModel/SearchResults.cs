using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudBasedRMS.Core
{
 public class SearchResults
    {
        public List<string> Suggestions { get; set; }
        public List<Category> category { get; set; }
    }
}
