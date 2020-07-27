using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IUB.Db;
using Serilog;

namespace IUB.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        private readonly ActivityContext _context;

        public ActivityController(ActivityContext context)
        {
            _context = context;
        }

        // GET: api/Activity
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Activity.Activity>>> GetActivities()
        {
            return await _context.Activities.ToListAsync();
        }

        // GET: api/Activity/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Activity.Activity>> GetActivity(Guid? id)
        {
            var activity = await _context.Activities.FindAsync(id);

            if (activity == null)
            {
                return NotFound();
            }

            return activity;
        }

        // PUT: api/Activity/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActivity(Guid? id, Activity.Activity activity)
        {
            //todo later replace PriceEnum.Unspecified to Free
            //todo later make ui for editing activities
            if (id != activity.Id)
            {
                return BadRequest();
            }

            _context.Entry(activity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!ActivityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    Log.Error(ex, ex.Message);
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Activity
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Activity.Activity>> PostActivity(Activity.Activity activity)
        {
            _context.Activities.Add(activity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetActivityModel", new { id = activity.Id }, activity);
        }

        // DELETE: api/Activity/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Activity.Activity>> DeleteActivity(Guid? id)
        {
            var activity = await _context.Activities.FindAsync(id);
            if (activity == null)
            {
                return NotFound();
            }

            _context.Activities.Remove(activity);
            await _context.SaveChangesAsync();

            return activity;
        }

        private bool ActivityExists(Guid? id)
        {
            return _context.Activities.Any(e => e.Id == id);
        }
    }
}
