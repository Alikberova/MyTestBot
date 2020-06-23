using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyTestBot.BoredApi;
using MyTestBot.Db;
using Serilog;

namespace MyTestBot.Web.Controllers
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
        public async Task<ActionResult<IEnumerable<ActivityModel>>> GetActivities()
        {
            return await _context.Activities.ToListAsync();
        }

        // GET: api/Activity/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ActivityModel>> GetActivityModel(Guid? id)
        {
            var activityModel = await _context.Activities.FindAsync(id);

            if (activityModel == null)
            {
                return NotFound();
            }

            return activityModel;
        }

        // PUT: api/Activity/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActivityModel(Guid? id, ActivityModel activityModel)
        {
            //todo later replace PriceEnum.Unspecified to Free
            //todo later make ui for editing activities
            if (id != activityModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(activityModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!ActivityModelExists(id))
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
        public async Task<ActionResult<ActivityModel>> PostActivityModel(ActivityModel activityModel)
        {
            _context.Activities.Add(activityModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetActivityModel", new { id = activityModel.Id }, activityModel);
        }

        // DELETE: api/Activity/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ActivityModel>> DeleteActivityModel(Guid? id)
        {
            var activityModel = await _context.Activities.FindAsync(id);
            if (activityModel == null)
            {
                return NotFound();
            }

            _context.Activities.Remove(activityModel);
            await _context.SaveChangesAsync();

            return activityModel;
        }

        private bool ActivityModelExists(Guid? id)
        {
            return _context.Activities.Any(e => e.Id == id);
        }
    }
}
