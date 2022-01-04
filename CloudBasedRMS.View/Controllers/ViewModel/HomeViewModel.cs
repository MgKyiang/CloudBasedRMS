using CloudBasedRMS.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CloudBasedRMS.View.Controllers.ViewModel
{
    public class HomeViewModel
    {
        public List<FoodItems_Details> foodItems_Details { get; set; }
        public List<Employee> employee { get; set; }
    }
}