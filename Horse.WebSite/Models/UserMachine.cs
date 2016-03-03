namespace Horse.WebSite.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class UserMachine
    {
        public int Id { get; set; }

        public int Uid { get; set; }

        public int Mid { get; set; }

        public virtual Machine Machine { get; set; }

        public virtual User User { get; set; }
    }
}
