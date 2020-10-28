using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreMentoring.Data.Models
{
    [Table("Territories")]
    public class TerritoryEntity
    {
        public TerritoryEntity()
        {
            EmployeeTerritories = new HashSet<EmployeeTerritoryEntity>();
        }

        [Key]
        [Column("TerritoryID")]
        [StringLength(20)]
        public string TerritoryId { get; set; }

        [Required] [StringLength(50)] public string TerritoryDescription { get; set; }

        [Column("RegionID")] public int RegionId { get; set; }

        [ForeignKey(nameof(RegionId))]
        [InverseProperty("Territories")]
        public virtual RegionEntity Region { get; set; }

        [InverseProperty("Territory")] public virtual ICollection<EmployeeTerritoryEntity> EmployeeTerritories { get; set; }
    }
}