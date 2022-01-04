using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudBasedRMS.Core
{
    [Table("Rank")]
  public  class Rank:EntityBase
    {
        [Key]
        public string RankID { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }
}
