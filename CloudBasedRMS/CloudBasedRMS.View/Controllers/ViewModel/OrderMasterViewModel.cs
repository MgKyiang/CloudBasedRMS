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
   public class OrderMasterViewModel
    {
        public string OrderMasterID { get; set; }
        [Required(ErrorMessage = "Require Order No.")]
        [Display(Name = "OrderNo", ResourceType = typeof(Resource))]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string OrderNo { get; set; }
        [Required(ErrorMessage = "Require Order Date.")]
        [Display(Name = "OrderDate", ResourceType = typeof(Resource))]
        public DateTime? OrderDate { get; set; }

        [Required(ErrorMessage = "Require Description.")]
        [Display(Name = "Description", ResourceType = typeof(Resource))]
        public string Description { get; set; }

        [Required(ErrorMessage = "Require Is Parcel.")]
        [Display(Name = "IsParcel", ResourceType = typeof(Resource))]
        public bool IsParcel { get; set; }
       
        [Required(ErrorMessage = "Require Table.")]
        [Display(Name = "Table", ResourceType = typeof(Resource))]
        public string TableID { get; set; }

        public string OrderStatus { get; set; }
        public Tables Tables { get; set; }
        public List<Tables> TablesList { get; set; }
        public List<OrderItemsViewModel> OrderItems { get; set; }

       
    }
}
