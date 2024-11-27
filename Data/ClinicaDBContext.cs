using Microsoft.EntityFrameworkCore;
using ClinicaAPI.Model;

namespace ClinicaAPI.Data
{
    public class ClinicaDBContext : DbContext
    {
        public ClinicaDBContext(DbContextOptions<ClinicaDBContext> options) : base(options) { }

        public DbSet<User> Usuarios { get; set; }
        public DbSet<Patient> Pacientes { get; set; }
        public DbSet<Doctor> Doutores { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder){

            modelBuilder.Entity<User>()
                .ToTable("Usuarios");

            modelBuilder.Entity<Patient>()
                .ToTable("Pacientes")  
                .HasBaseType<User>();
            
            modelBuilder.Entity<Doctor>()
                .ToTable("Medicos")   
                .HasBaseType<User>();   
        }
    }
}

