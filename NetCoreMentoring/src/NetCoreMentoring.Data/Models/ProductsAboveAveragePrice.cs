using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreMentoring.Data.Models
{
    public partial class ProductsAboveAveragePrice
    {
        [Required]
        [StringLength(40)]
        public string ProductName { get; set; }
        [Column(TypeName = "money")]
        public decimal? UnitPrice { get; set; }
    }
}
