using Microsoft.EntityFrameworkCore;
using WebApplication1.Models.ChoiceHelpers;
using WebApplication1.Models.ChoiceHelpers.Conditions;
using WebApplication1.Models.Users;

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
        public DbSet<CarType> CarTypes { get; set; }
        public DbSet<ConditionsSet> ConditionsSets { get; set; }
        public DbSet<APIQualityCondition> APIQualityConditions { get; set; }
        public DbSet<OilTypeCondition> OilTypeConditions { get; set; }
        public DbSet<SAEViscosityCondition> SAEViscosityConditions { get; set; }
        public DbSet<Permission> Permissions {  get; set; }
        public DbSet<Users.User> Users { get; set; }
        public DbSet<Users.Role> Role { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
