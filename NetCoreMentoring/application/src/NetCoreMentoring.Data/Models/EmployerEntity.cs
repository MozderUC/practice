using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreMentoring.Data.Models
{
    [Table("Employees")]
    public class EmployerEntity
    {
        public EmployerEntity()
        {
            EmployeeTerritories = new HashSet<EmployeeTerritoryEntity>();
            InverseReportsToNavigation = new HashSet<EmployerEntity>();
            Orders = new HashSet<OrderEntity>();
        }

        [Key] [Column("EmployeeID")] public int EmployeeId { get; set; }

        [Required] [StringLength(20)] public string LastName { get; set; }

        [Required] [StringLength(10)] public string FirstName { get; set; }

        [StringLength(30)] public string Title { get; set; }

        [StringLength(25)] public string TitleOfCourtesy { get; set; }

        [Column(TypeName = "datetime")] public DateTime? BirthDate { get; set; }

        [Column(TypeName = "datetime")] public DateTime? HireDate { get; set; }

        [StringLength(60)] public string Address { get; set; }

        [StringLength(15)] public string City { get; set; }

        [StringLength(15)] public string Region { get; set; }

        [StringLength(10)] public string PostalCode { get; set; }

        [StringLength(15)] public string Country { get; set; }

        [StringLength(24)] public string HomePhone { get; set; }

        [StringLength(4)] public string Extension { get; set; }

        [Column(TypeName = "image")] public byte[] Photo { get; set; }

        [Column(TypeName = "ntext")] public string Notes { get; set; }

        public int? ReportsTo { get; set; }

        [StringLength(255)] public string PhotoPath { get; set; }

        [ForeignKey(nameof(ReportsTo))]
        [InverseProperty(nameof(InverseReportsToNavigation))]
        public virtual EmployerEntity ReportsToNavigation { get; set; }

        [InverseProperty("Employer")] public virtual ICollection<EmployeeTerritoryEntity> EmployeeTerritories { get; set; }

        [InverseProperty(nameof(ReportsToNavigation))]
        public virtual ICollection<EmployerEntity> InverseReportsToNavigation { get; set; }

        [InverseProperty("Employer")] public virtual ICollection<OrderEntity> Orders { get; set; }
    }
}