using Microsoft.EntityFrameworkCore;
using MyTestBot.BoredApi;

namespace MyTestBot.Db
{
    public class ActivityContext : DbContext
    {
        public DbSet<ActivityModel> Activities { get; set; }

        public ActivityContext(DbContextOptions<ActivityContext> options) : base(options)
        {
        }
    }
}
