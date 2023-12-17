namespace Elearning.Entittes.DbContexts
{
    using Elearning.Entittes.Models;

    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;

    using System.Reflection;

    public partial class ElearningContext : IdentityDbContext<ApplicationUser>
    {
        public ElearningContext()
        {
        }
        public ElearningContext(DbContextOptions<ElearningContext> options) : base(options)
        {
        }


        public virtual DbSet<Log> Logs { get; set; }
        public DbSet<ApplicationUser> AppUsers { get; set; }
        public virtual DbSet<OTPData> OTPDatas { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }
}