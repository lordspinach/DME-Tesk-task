using Microsoft.EntityFrameworkCore;

namespace TestTaskLib.Models.DataDb
{
    public class ApplicationContext : DbContext
    {
        public DbSet<RandomUser> RandomUsers { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<File> Files { get; set; }
        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=GamayunovTestTask;Trusted_Connection=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppUser>()
                .HasData(new AppUser { Id = 1, UserName = "Admin", Password = "Super@Pass1", Role = "Admin" });
        }
    }
}
