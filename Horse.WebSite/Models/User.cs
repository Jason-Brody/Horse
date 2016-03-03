namespace Horse.WebSite.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            UserMachines = new HashSet<UserMachine>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string NTAccount { get; set; }

        [StringLength(50)]
        public string DisplayName { get; set; }

        [StringLength(255)]
        public string Pic { get; set; }

        [StringLength(100)]
        public string Manager { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        public int? EmployeeId { get; set; }

        public DateTime? CreateDt { get; set; }

        public DateTime? LastUpdateDt { get; set; }

        [StringLength(50)]
        public string BusinessUnit { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserMachine> UserMachines { get; set; }
    }
}
