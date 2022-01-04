using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CloudBasedRMS.View.Controllers.ViewModel
{
    public class GalleryViewModel
    {
        public string Fileimg { get; set; }
        public IEnumerable<string> Images { get; set; }
    }
}