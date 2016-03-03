namespace Horse.WebSite.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class HorseData : DbContext
    {
        public HorseData()
            : base("name=HorseData")
        {
        }

        public virtual DbSet<MachineFolder> MachineFolders { get; set; }
        public virtual DbSet<Machine> Machines { get; set; }
        public virtual DbSet<MachineSoftware> MachineSoftwares { get; set; }
        public virtual DbSet<Software> Softwares { get; set; }
        public virtual DbSet<UserMachine> UserMachines { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Machine>()
                .HasMany(e => e.MachineFolders)
                .WithOptional(e => e.Machines)
                .HasForeignKey(e => e.Mid);

            modelBuilder.Entity<Machine>()
                .HasMany(e => e.MachineSoftwares)
                .WithRequired(e => e.Machines)
                .HasForeignKey(e => e.Mid)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Machine>()
                .HasMany(e => e.UserMachines)
                .WithRequired(e => e.Machine)
                .HasForeignKey(e => e.Mid)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Software>()
                .HasMany(e => e.MachineSoftwares)
                .WithRequired(e => e.Softwares)
                .HasForeignKey(e => e.Sid)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.UserMachines)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.Uid)
                .WillCascadeOnDelete(false);
        }
    }
}
