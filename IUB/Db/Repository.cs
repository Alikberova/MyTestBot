using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IUB.Db
{
    public class Repository
    {
        private readonly ActivityContext _context;

        public Repository(ActivityContext context)
        {
            _context = context;
        }

        //select count(Activity) from [dbo].[Activities] 
        //SELECT DISTINCT TOP (1000) * FROM [dbo].[Activities] order by CreatedDate desc
        public async Task<Activity.Activity> Get(string activityProperty, object value)
        {
            var activities = (await GetAll())
                .Where(a => a.GetType().GetProperty(activityProperty).GetValue(a).ToString() == value.ToString())
                .ToList();

            return await GetRandom(activities);
        }

        public async Task<Activity.Activity> GetRandom(List<Activity.Activity> activities = null)
        {
            if (activities == null)
            {
                activities = await GetAll();
            }
            var rand = new Random();
            var skip = (int)(rand.NextDouble() * activities.Count());

            return activities.OrderBy(o => o.Id).Skip(skip).Take(1).First();
        }

        private async Task<List<Activity.Activity>> GetAll()
        {
            return await _context.Activities.ToListAsync();
        }
    }
}
