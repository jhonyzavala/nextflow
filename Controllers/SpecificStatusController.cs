using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi_nextflow.Entity;

namespace webapi_nextflow.Controllers
{
    [ApiController]
    [Route("api/specificstatus")]
    public class SpecificStatusController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public SpecificStatusController( ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<SpecificStatus>>> Get()
        {
            return await context.SpecificStatus.ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<SpecificStatus>> Get(int id)
        {
            var specific = await context.SpecificStatus.FirstOrDefaultAsync(x=>x.Id==id);

            if (specific==null)
            {
                return NotFound();
            }

            return Ok(specific);
        }

        [HttpPost]
        public async Task<ActionResult> Post(SpecificStatus specific)
        {
            context.Add(specific);
            await context.SaveChangesAsync();

            return Ok();

        }

    }
}
