using Microsoft.EntityFrameworkCore;
using UJM.IdentityServer4.Model.Entities;

namespace UJM.IdentityServer4.Model.DataBase
{
    public class AuthorzeContext : DbContext
    {
        private readonly IConfiguration Configuration;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public AuthorzeContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public AuthorzeContext()
        {
        }

        public AuthorzeContext(DbContextOptions<AuthorzeContext> options)
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

        ////动态添加实体
        //private readonly string ModelAssemblyName = "Rpa.MicroServer.Model";
        //private void MappingEntityTypes(ModelBuilder modelBuilder)
        //{
        //    if (string.IsNullOrEmpty(ModelAssemblyName))
        //        return;
        //    var assembly = System.Reflection.Assembly.Load(ModelAssemblyName);
        //    var types = assembly?.GetTypes();
        //    var list = types?.Where(t =>
        //        t.IsClass && !t.IsGenericType && !t.IsAbstract).ToList();
        //    //&& t.GetInterfaces().Any(m => m.IsAssignableFrom(typeof(BaseModel<>)))).ToList();
        //    if (list != null && list.Any())
        //    {
        //        list.ForEach(t =>
        //        {
        //            if (modelBuilder.Model.FindEntityType(t) == null)
        //                modelBuilder.Model.AddEntityType(t);
        //        });
        //    }
        //}

    }
}
