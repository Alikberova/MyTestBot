using Microsoft.EntityFrameworkCore;
using IUB.BoredApi;

namespace IUB.Db
{
    public class ActivityContext : DbContext
    {
        public DbSet<ActivityModel> Activities { get; set; }

        public ActivityContext(DbContextOptions<ActivityContext> options) : base(options)
        {
        }
    }
}
