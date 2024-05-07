using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<MotorOil> MotorOils { get; set; } = null!;
        public DbSet<Company> Companies { get; set; }
        public DbSet<MotorOilMerch> MotorOilMerches { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<MotorOilStats.SAEViscosity> SAEViscosities {get;set;}
        public DbSet<MotorOilStats.APIQualityClass> APIQualityClasses { get;set;}
        //public DbSet<Users.User> Users { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
