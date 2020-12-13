using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreMentoring.Core.Models
{
    [Table("Suppliers")]
    public class Supplier
    {
        [Key]
        [Column("SupplierID")]
        public int SupplierId { get; set; }

        [Required]
        [StringLength(40)]
        public string CompanyName { get; set; }

        [InverseProperty("Supplier")]
        public virtual ICollection<Product> Products { get; set; }
    }
}