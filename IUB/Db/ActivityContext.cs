using Microsoft.EntityFrameworkCore;
using IUB.Activity;

namespace IUB.Db
{
    public class ActivityContext : DbContext
    {
        public DbSet<Activity.Activity> Activities { get; set; }

        public ActivityContext(DbContextOptions<ActivityContext> options) : base(options)
        {
        }
    }
}
