using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreMentoring.Data.Models
{
    [Table("EmployeeTerritories")]
    public class EmployeeTerritory
    {
        [Key] [Column("EmployeeID")] public int EmployeeId { get; set; }

        [Key]
        [Column("TerritoryID")]
        [StringLength(20)]
        public string TerritoryId { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        [InverseProperty(nameof(Models.Employer.EmployeeTerritories))]
        public virtual Employer Employer { get; set; }

        [ForeignKey(nameof(TerritoryId))]
        [InverseProperty(nameof(Models.Territory.EmployeeTerritories))]
        public virtual Territory Territory { get; set; }
    }
}