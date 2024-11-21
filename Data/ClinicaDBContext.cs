using Microsoft.EntityFrameworkCore;
using ClinicaAPI.Model;

namespace ClinicaAPI.Data
{
    public class ClinicaDBContext : DbContext
    {
        public ClinicaDBContext(DbContextOptions<ClinicaDBContext> options) : base(options) { }

        public DbSet<User> Usuarios { get; set; }
    }
}