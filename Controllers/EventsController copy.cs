using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi_nextflow.Entity;

namespace webapi_nextflow.Controllers
{
    [ApiController]
    [Route("api/events")]
    public class EventsController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public EventsController( ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Event>>> Get()
        {
            return await context.Events.ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Event>> Get(int id)
        {
            var _event = await context.Events.FirstOrDefaultAsync(x=>x.Id==id);

            if (_event==null)
            {
                return NotFound();
            }

            return Ok(_event);
        }
             

    }
}
