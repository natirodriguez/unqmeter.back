using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UnqMeterAPI.Models;

namespace UnqMeterAPI.Repository
{
    public class RepositoryContext : IdentityDbContext<User>
    {
        public RepositoryContext(DbContextOptions options): base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Presentacion>();
            modelBuilder.Entity<Slyde>();
            modelBuilder.Entity<OpcionesSlyde>();
            modelBuilder.Entity<Respuesta>();
            modelBuilder.Entity<DescripcionRespuesta>(); 
        }

        public virtual DbSet<Presentacion> Presentaciones { get; set; }
        public virtual DbSet<Slyde> Slydes { get; set; }
        public virtual DbSet<OpcionesSlyde> OpcionesSlydes { get; set; }
        public virtual DbSet<Respuesta> Respuestas { get; set; }
        public virtual DbSet<DescripcionRespuesta> DescripcionRespuestas { get; set; }
    }
}
