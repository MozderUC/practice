using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreMentoring.Data.Models
{
    [Table("Order Details")]
    public class OrderDetail
    {
        [Key] [Column("OrderID")] public int OrderId { get; set; }

        [Key] [Column("ProductID")] public int ProductId { get; set; }

        [Column(TypeName = "money")] public decimal UnitPrice { get; set; }

        public short Quantity { get; set; }
        public float Discount { get; set; }

        [ForeignKey(nameof(OrderId))]
        [InverseProperty(nameof(Models.OrderEntity.OrderDetails))]
        public virtual OrderEntity Order { get; set; }

        [ForeignKey(nameof(ProductId))]
        [InverseProperty(nameof(Models.ProductEntity.OrderDetails))]
        public virtual ProductEntity Product { get; set; }
    }
}