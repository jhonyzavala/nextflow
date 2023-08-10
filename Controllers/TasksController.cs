using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi_nextflow.Entity;

namespace webapi_nextflow.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    public class TasksController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public TasksController( ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Entity.Task>>> Get()
        {
          return await context.Tasks.ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Entity.Task>> Get(int id)
        {
            var task = await context.Tasks.FirstOrDefaultAsync(x=>x.Id==id);

            if (task==null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        [HttpPost]
        public async Task<ActionResult> Post(Entity.Task task)
        {
            context.Add(task);
            await context.SaveChangesAsync();

            return Ok();

        }

    }
}
