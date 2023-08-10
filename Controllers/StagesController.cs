using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using webapi_nextflow.Entity;

namespace webapi_nextflow.Controllers
{
    [ApiController]
    [Route("api/stages")]
    public class StagesController : ControllerBase
    {
        public ApplicationDbContext context { get; }

        public StagesController( ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Stage>>> Get()
        {
            return await context.Stages.ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Stage>> Get(int id)
        {
            var stage = await context.Stages.Include(a=>a.Workflow).FirstOrDefaultAsync(x=>x.Id==id);

            if (stage ==null)
            {
                return NotFound();
            }

            return Ok(stage);

        }

        [HttpPost]
        public async Task<ActionResult> Post( Stage stage)
        {
            context.Add(stage);
            await context.SaveChangesAsync();

            return Ok();
        }


       
    }
}
