using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreMentoring.Data.Models
{
    [Table("CustomerCustomerDemo")]
    public class CustomerCustomerDemoEntity
    {
        [Key]
        [Column("CustomerID")]
        [StringLength(5)]
        public string CustomerId { get; set; }

        [Key]
        [Column("CustomerTypeID")]
        [StringLength(10)]
        public string CustomerTypeId { get; set; }

        [ForeignKey(nameof(CustomerId))]
        [InverseProperty(nameof(Models.CustomerEntity.CustomerCustomerDemo))]
        public virtual CustomerEntity Customer { get; set; }

        [ForeignKey(nameof(CustomerTypeId))]
        [InverseProperty(nameof(CustomerDemographicEntity.CustomerCustomerDemo))]
        public virtual CustomerDemographicEntity CustomerType { get; set; }
    }
}