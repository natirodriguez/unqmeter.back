﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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
        }

        public virtual DbSet<Presentacion> Presentaciones { get; set; }
        public virtual DbSet<Slyde> Slydes { get; set; }
        public virtual DbSet<OpcionesSlyde> OpcionesSlydes { get; set; }   
    }
}
