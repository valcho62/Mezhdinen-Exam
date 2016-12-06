using System.Data.Entity.ModelConfiguration.Conventions;
using System.Security.AccessControl;
using Models;

namespace Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class MassDefectContex : DbContext
    {
     
        public MassDefectContex()
            : base("name=MassDefectContex")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<MassDefectContex>());
        }
        public virtual DbSet<Star> Stars { get; set; }
        public virtual DbSet<Anomaly> Anomalies { get; set; }
        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<Planet> Planets { get; set; }
        public virtual DbSet<SolarSystem> SolarSystems { get; set; }

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().ToTable("Persons");
            modelBuilder.Entity<Anomaly>().HasMany(p => p.Victims).WithMany(ano => ano.Anomalies).Map(m =>
            {
                m.MapLeftKey("AnomalyId");
                m.MapRightKey("PersonId");
                m.ToTable("AnomalyVictims");
            });
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }

   
}