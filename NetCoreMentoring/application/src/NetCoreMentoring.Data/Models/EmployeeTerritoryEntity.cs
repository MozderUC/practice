using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreMentoring.Data.Models
{
    [Table("EmployeeTerritories")]
    public class EmployeeTerritoryEntity
    {
        [Key] [Column("EmployeeID")] public int EmployeeId { get; set; }

        [Key]
        [Column("TerritoryID")]
        [StringLength(20)]
        public string TerritoryId { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        [InverseProperty(nameof(Models.EmployerEntity.EmployeeTerritories))]
        public virtual EmployerEntity Employer { get; set; }

        [ForeignKey(nameof(TerritoryId))]
        [InverseProperty(nameof(Models.TerritoryEntity.EmployeeTerritories))]
        public virtual TerritoryEntity Territory { get; set; }
    }
}