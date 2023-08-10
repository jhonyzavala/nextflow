using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi_nextflow.Entity;

namespace webapi_nextflow.Controllers
{
    [ApiController]
    [Route("api/workflows")]
    public class WorkflowController : ControllerBase
    {

        public ApplicationDbContext context { get; }

        public WorkflowController(ApplicationDbContext context)
        {
            this.context = context;
        }               

        [HttpGet]
        public async Task<ActionResult<List<Workflow>>> Get()
        {
            return await context.Workflows.ToListAsync();
        }

        [HttpGet("id")]
        public async Task<ActionResult<Workflow>> Get( string id)
        {
            var workflow = await context.Workflows.Include(a=>a.Items).FirstOrDefaultAsync(x => x.Id == id);

            if (workflow ==null)
            {
                return NotFound();
            }

            return Ok(workflow);
        }


        [HttpPost]
        public async Task<ActionResult> Post( Workflow workflow )
        {
            workflow.Id = Guid.NewGuid().ToString();
            context.Add(workflow);
            await context.SaveChangesAsync();

            return Ok();
        }


    }
}
