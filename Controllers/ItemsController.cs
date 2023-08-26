using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi_nextflow.Entity;

namespace webapi_nextflow.Controllers
{
    [ApiController]
    [Route("api/workflows/{workflowid}/items")]
    public class ItemsController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public ItemsController( ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Item>>> Get(string workflowid)
        {
            var exists = await context.Workflows.AnyAsync(x => x.Id == workflowid);

            if (!exists)
            {
                return BadRequest($"Workflow with Id {workflowid} does not exist ");
            }
            
            return await context.Items.Where(a=>a.WorkflowId==workflowid).ToListAsync();
        }

       // [HttpGet("nextitem/{id:int}/{event:int}")]
        

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Item>> Get(int id)
        {
            var item = await context.Items.FirstOrDefaultAsync(x=>x.Id==id);

            if (item==null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult> Post(Item item)
        {
            context.Add(item);
            await context.SaveChangesAsync();

            return Ok();

        }

    }
}
