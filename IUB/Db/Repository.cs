using IUB.BoredApi;
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
        public async Task<ActivityModel> Get(string activityProperty, object value)
        {
            var activities = (await GetAll())
                .Where(a => a.GetType().GetProperty(activityProperty).GetValue(a).ToString() == value.ToString())
                .ToList();

            return await GetRandom(activities);
        }

        public async Task<ActivityModel> GetRandom(List<ActivityModel> activities = null)
        {
            if (activities == null)
            {
                activities = await GetAll();
            }
            var rand = new Random();
            var skip = (int)(rand.NextDouble() * activities.Count());

            var result = activities.OrderBy(o => o.Id).Skip(skip).Take(1).First();
            return result;
        }

        private async Task<List<ActivityModel>> GetAll()
        {
            return await _context.Activities.ToListAsync();
        }
    }
}
