using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CloudBasedRMS.View.Controllers.ViewModel
{
    public class TwoDimensionalData
    {     
        private List<int[]> _data;

        public List<int[]> Data
        {
            get { return _data; }
            set { _data = value; }
        }

        public TwoDimensionalData()
        {
            _data = new List<int[]>();

        }

    }
}