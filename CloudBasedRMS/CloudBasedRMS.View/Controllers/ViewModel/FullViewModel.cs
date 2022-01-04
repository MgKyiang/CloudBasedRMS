using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CloudBasedRMS.View.Controllers.ViewModel
{
    public class FullViewModel
    {
        public FullViewModel()
        {
            CreationDateTime = DateTime.Now;
        }

        /// <summary>
        /// This will contain localised string value
        /// </summary>
        public string LocalisedString { get; set; }

        /// <summary>
        /// For see difference of cretion time
        /// </summary>
        public DateTime CreationDateTime { get; set; }
    }
}