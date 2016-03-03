namespace Horse.WebSite.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MachineFolder
    {
        public int Id { get; set; }

        public int? Mid { get; set; }

        [StringLength(100)]
        public string Path { get; set; }

        public virtual Machine Machines { get; set; }
    }
}
