using Microsoft.EntityFrameworkCore;
 
namespace FirstBeltExam.Models
{
    public class YourContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public YourContext(DbContextOptions<YourContext> options) : base(options) { }
        public DbSet<User> User { get; set; }
        public DbSet<Activity> Activity { get; set; }
        public DbSet<FunMaker> FunMaker { get; set; }
    }
}