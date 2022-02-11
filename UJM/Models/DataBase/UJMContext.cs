using Microsoft.EntityFrameworkCore;
using UJM.Models.Entities;

namespace UJM.Models.DataBase
{
    public class UJMContext : DbContext
    {
        private readonly IConfiguration Configuration;
        public UJMContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public UJMContext()
        {
        }

        public UJMContext(DbContextOptions<UJMContext> options)
            : base(options)
        {
        }


        public DbSet<UserEntity> UserEntity { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(Configuration["MySqlConnections"], new MySqlServerVersion(new Version(8, 0, 25)));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //MappingEntityTypes(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }
    }
}
