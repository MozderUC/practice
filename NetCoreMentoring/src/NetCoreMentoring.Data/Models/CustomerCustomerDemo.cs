using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreMentoring.Data.Models
{
    public class CustomerCustomerDemo
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
        [InverseProperty(nameof(Models.Customer.CustomerCustomerDemo))]
        public virtual Customer Customer { get; set; }

        [ForeignKey(nameof(CustomerTypeId))]
        [InverseProperty(nameof(CustomerDemographic.CustomerCustomerDemo))]
        public virtual CustomerDemographic CustomerType { get; set; }
    }
}