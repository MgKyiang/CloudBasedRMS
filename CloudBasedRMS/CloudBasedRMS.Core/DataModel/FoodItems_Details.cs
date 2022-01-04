using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloudBasedRMS.Core
{
    [Table("FoodItems_Details")]
  public  class FoodItems_Details:EntityBase
    {
        [Key]
        public string FoodItemID { get; set; }
        [MaxLength(500), MinLength(2)]
        public string Code { get; set; }
        [MaxLength(4000), MinLength(2)]
        public string Description { get; set; }

        public string ImagePath { get; set; }
        public string CategoryID { get; set; }

        [ForeignKey("CategoryID")]
        public virtual Category Category { get; set; }
        public decimal Rate { get; set; }
        [MaxLength(20), MinLength(1)]
        public string Offer { get; set; }
        [MaxLength(4000), MinLength(2)]
        public string Note { get; set; }
        public bool IsJam { get; set; }
        [MaxLength(6), MinLength(2)]
        public string Spicy { get; set; }
        public decimal OldPrice { get; set; }
        public decimal NewPrice { get; set; }
        public bool IsTodaySpecial { get; set; }

        public string KitchenID { get; set; }
        [ForeignKey("KitchenID")]
        public virtual Kitchen Kitchen { get; set; }
    }
}
