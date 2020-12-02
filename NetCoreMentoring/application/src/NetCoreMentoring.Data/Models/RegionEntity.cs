using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreMentoring.Data.Models
{
    [Table("Region")]
    public class RegionEntity
    {
        public RegionEntity()
        {
            Territories = new HashSet<TerritoryEntity>();
        }

        [Key] [Column("RegionID")] public int RegionId { get; set; }

        [Required] [StringLength(50)] public string RegionDescription { get; set; }

        [InverseProperty("Region")] public virtual ICollection<TerritoryEntity> Territories { get; set; }
    }
}