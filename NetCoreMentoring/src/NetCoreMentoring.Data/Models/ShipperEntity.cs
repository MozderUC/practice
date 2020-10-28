using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreMentoring.Data.Models
{
    [Table("Shippers")]
    public class ShipperEntity
    {
        public ShipperEntity()
        {
            Orders = new HashSet<OrderEntity>();
        }

        [Key] [Column("ShipperID")] public int ShipperId { get; set; }

        [Required] [StringLength(40)] public string CompanyName { get; set; }

        [StringLength(24)] public string Phone { get; set; }

        [InverseProperty("ShipViaNavigation")] public virtual ICollection<OrderEntity> Orders { get; set; }
    }
}