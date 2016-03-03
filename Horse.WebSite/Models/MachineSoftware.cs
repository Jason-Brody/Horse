namespace Horse.WebSite.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MachineSoftware
    {
        public int Id { get; set; }

        public int Mid { get; set; }

        public int Sid { get; set; }

        public virtual Machine Machines { get; set; }

        public virtual Software Softwares { get; set; }
    }
}
