
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CloudBasedRMS.View.Controllers.Common
{
    public class CurrentDateAttribute:ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime datetime = Convert.ToDateTime(value);
            return datetime <= DateTime.Now;
        }
    }
}