using Microsoft.EntityFrameworkCore;
using Dodic.Domain.Entity;
using Dodic.Domain.Helpers;
namespace Dodic.DAL
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=MyDatabase;Username=postgres;Password=1111");
                optionsBuilder.LogTo(Console.WriteLine); // Логирование SQL-запросов
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(builder =>
            {
                builder.ToTable("Users").HasKey(x => x.id);

                builder.HasData(new User
                {
                    id = 1,
                    Name = "Admin",
                    Password = HashPasswordHelper.HashPassword("654321"),
                    PhoneNumber = "79835956730",
                    Email = "kostorev2004@mail.ru",
                    Birthdate = "16.12.2004"
                });
            });
        }
    }

}


