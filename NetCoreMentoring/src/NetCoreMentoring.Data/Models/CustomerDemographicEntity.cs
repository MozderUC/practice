using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreMentoring.Data.Models
{
    [Table("CustomerDemographics")]
    public class CustomerDemographicEntity
    {
        public CustomerDemographicEntity()
        {
            CustomerCustomerDemo = new HashSet<CustomerCustomerDemoEntity>();
        }

        [Key]
        [Column("CustomerTypeID")]
        [StringLength(10)]
        public string CustomerTypeId { get; set; }

        [Column(TypeName = "ntext")] public string CustomerDesc { get; set; }

        [InverseProperty("CustomerType")]
        public virtual ICollection<CustomerCustomerDemoEntity> CustomerCustomerDemo { get; set; }
    }
}