using CloudBasedRMS.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CloudBasedRMS.View.Controllers.ViewModel
{
    public class UserProfileViewModel
    {
        public Customer Customer { get; set; }
        public Employee Employee { get; set; }
        public UsersMember UsersMember { get; set; }
    }
}