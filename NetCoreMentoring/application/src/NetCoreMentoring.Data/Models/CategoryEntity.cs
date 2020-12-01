﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreMentoring.Data.Models
{
    [Table("Categories")]
    public class CategoryEntity
    {
        public CategoryEntity()
        {
            Products = new HashSet<ProductEntity>();
        }

        [Key] [Column("CategoryID")] public int CategoryId { get; set; }

        [Required] [StringLength(15)] public string CategoryName { get; set; }

        [Column(TypeName = "ntext")] public string Description { get; set; }

        [Column(TypeName = "image")] public byte[] Picture { get; set; }

        [InverseProperty("Category")] public virtual ICollection<ProductEntity> Products { get; set; }
    }
}