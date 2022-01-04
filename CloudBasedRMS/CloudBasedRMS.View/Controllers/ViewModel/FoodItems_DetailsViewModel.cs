using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudBasedRMS.Core;
using CloudBasedRMS.View.App_LocalResources;

namespace CloudBasedRMS.View.Controllers.ViewModel
{
   public class FoodItems_DetailsViewModel
    {
        public string FoodItemID { get; set; }

        [Required(ErrorMessage = "Require Food Item Code.")]
        [Display(Name = "FoodItemCode", ResourceType = typeof(Resource))]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string Code { get; set; }

        [Required(ErrorMessage = "Require Food Item Description.")]
        [Display(Name = "FoodItemDescription", ResourceType = typeof(Resource))]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Require Image.")]
        [Display(Name = "FoodItemPhoto", ResourceType = typeof(Resource))]
        public string ImagePath { get; set; }

        [Required(ErrorMessage = "Require Rate.")]
        [Display(Name = "Rate", ResourceType = typeof(Resource))]
        public decimal Rate { get; set; }

        [Required(ErrorMessage = "Require Offer.")]
        [Display(Name = "Offer", ResourceType = typeof(Resource))]
        public string Offer { get; set; }

        [Display(Name = "Note", ResourceType = typeof(Resource))]
        public string Note { get; set; }

        [Display(Name = "IsJam", ResourceType = typeof(Resource))]
        public bool IsJam { get; set; }

        [Display(Name = "Spicy", ResourceType = typeof(Resource))]
        public string Spicy { get; set; }
        [Required(ErrorMessage = "Require Old Price.")]
        [Display(Name = "OldPrice", ResourceType = typeof(Resource))]
        public decimal OldPrice { get; set; }

        [Required(ErrorMessage = "Require New Price.")]
        [Display(Name = "NewPrice", ResourceType = typeof(Resource))]
        public decimal NewPrice { get; set; }
        [Display(Name = "IsTodaySpecial", ResourceType = typeof(Resource))]
        public bool IsTodaySpecial { get; set; }

        [Required(ErrorMessage = "Require Category Description.")]
        [Display(Name = "CategoryDescription", ResourceType = typeof(Resource))]
        public string CategoryID { get; set; }
        public Category  Category { get; set; }


        [Required(ErrorMessage = "Require Kitchen Name.")]
        [Display(Name = "KitchenName", ResourceType = typeof(Resource))]
        public string KitchenID { get; set; }
        public Kitchen Kitchen { get; set; }

    }
}
