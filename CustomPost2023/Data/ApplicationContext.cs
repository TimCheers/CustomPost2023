using CustomPost2023.Data.Models;
using Microsoft.EntityFrameworkCore;
public class ApplicationContext : DbContext
{
    public DbSet<user> user { get; set; } = null!;
    public DbSet<custom_post> custom_post { get; set; } = null!;
    public DbSet<product> product { get; set; } = null!;
    public DbSet<product_type> product_type { get; set; } = null!;
    public DbSet<vehicle_type> vehicle_type { get; set; } = null!;
    public DbSet<status> status { get; set; } = null!;
    public DbSet<history> history { get; set; } = null!;
    public DbSet<export_countries> export_countries { get; set; } = null!;
    public DbSet<loggs> loggs { get; set; } = null!;
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