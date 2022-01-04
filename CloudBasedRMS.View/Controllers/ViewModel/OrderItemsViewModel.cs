using CloudBasedRMS.Core;
using CloudBasedRMS.View.App_LocalResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudBasedRMS.View.Controllers.ViewModel
{
 public   class OrderItemsViewModel
    {
        public string OrderItemsID { get; set; }
        [Required]
        public string OrderMasterID { get; set; }
        public  OrderMaster OrderMaster { get; set; }

        [Required(ErrorMessage = "Require Food Item.")]
        [Display(Name = "FoodItem", ResourceType = typeof(Resource))]
        public string FoodItemID { get; set; }

        [Required(ErrorMessage = "Require Quantity.")]
        [Display(Name = "Quantity", ResourceType = typeof(Resource))]
        public decimal Quantity { get; set; }

        [Required(ErrorMessage = "Require Rate Per Items.")]
        [Display(Name = "RatePerItems", ResourceType = typeof(Resource))]
        public decimal RatePerItems { get; set; }

        [Required(ErrorMessage = "Require Amount.")]
        [Display(Name = "Amount", ResourceType = typeof(Resource))]
        public decimal Amount { get; set; }

        public string FoodItems { get; set; }
        public int Index { get; set; }

    }
}
