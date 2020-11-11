using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreMentoring.Data.Models
{
    [Table("Products")]
    public class ProductEntity
    {
        public ProductEntity()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        [Key] [Column("ProductID")] public int ProductId { get; set; }

        [Required] [StringLength(40)] public string ProductName { get; set; }

        [Column("SupplierID")] public int? SupplierId { get; set; }

        [Column("CategoryID")] public int? CategoryId { get; set; }

        [StringLength(20)] public string QuantityPerUnit { get; set; }

        [Column(TypeName = "money")] public decimal? UnitPrice { get; set; }

        public short? UnitsInStock { get; set; }
        public short? UnitsOnOrder { get; set; }
        public short? ReorderLevel { get; set; }
        public bool Discontinued { get; set; }

        [ForeignKey(nameof(CategoryId))]
        [InverseProperty(nameof(Models.CategoryEntity.Products))]
        public virtual CategoryEntity Category { get; set; }

        [ForeignKey(nameof(SupplierId))]
        [InverseProperty(nameof(Models.SupplierEntity.Products))]
        public virtual SupplierEntity Supplier { get; set; }

        [InverseProperty("Product")] public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}