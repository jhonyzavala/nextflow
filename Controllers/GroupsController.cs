using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi_nextflow.Entity;

namespace webapi_nextflow.Controllers
{
    [ApiController]
    [Route("api/groups")]
    public class GroupsController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public GroupsController( ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Group>>> Get()
        {
            return await context.Groups.ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Group>> Get(int id)
        {
            var group = await context.Groups.FirstOrDefaultAsync(x=>x.Id==id);

            if (group==null)
            {
                return NotFound();
            }

            return Ok(group);
        }

        [HttpPost]
        public async Task<ActionResult> Post(Group group)
        {
            context.Add(group);
            await context.SaveChangesAsync();

            return Ok();

        }

    }
}
