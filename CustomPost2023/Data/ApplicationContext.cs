using CustomPost2023.Data.Models;
using Microsoft.EntityFrameworkCore;
public class ApplicationContext : DbContext
{
    public DbSet<user> users { get; set; } = null!;
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
        Database.EnsureCreated();   // создаем базу данных при первом обращении
    }
    public ApplicationContext()
    {
        Database.EnsureCreated();
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=CustomDB2;Username=postgres;Password=123321");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<User>().HasData(
        //        new User { Id = 1, Name = "Tom", Age = 37 },
        //        new User { Id = 2, Name = "Bob", Age = 41 },
        //        new User { Id = 3, Name = "Sam", Age = 24 }
        //);
    }
}