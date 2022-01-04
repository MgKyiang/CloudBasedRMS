using CloudBasedRMS.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CloudBasedRMS.View.Controllers.ViewModel
{
    public class KOTViewModel
    {
        public FoodItems_Details fooditems { get; set; }
        public Kitchen Kitchen { get; set; }
        public OrderItems OrderItems { get; set; }
    }
}