using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi_nextflow.Entity;

namespace webapi_nextflow.Controllers
{
    [ApiController]
    [Route("api/condicionals")]
    public class ConditionalsController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public ConditionalsController( ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Conditional>>> Get()
        {
            return await context.Conditionals.ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Conditional>> Get(int id)
        {
            var conditional = await context.Conditionals.FirstOrDefaultAsync(x=>x.Id==id);

            if (conditional==null)
            {
                return NotFound();
            }

            return Ok(conditional);
        }

        [HttpPost]
        public async Task<ActionResult> Post(Conditional conditional)
        {
            context.Add(conditional);
            await context.SaveChangesAsync();

            return Ok();

        }

        [HttpPut("{id:int}")] // api/conditionals/1 
        public async Task<ActionResult> Put(Conditional conditional,  int id)
        {
            if (conditional.Id != id)
            {
                return BadRequest("The workflow id does not match the url id");
            }

            var exists = await context.Conditionals.AnyAsync(x => x.Id == id);

            if (!exists)
            {
                return NotFound();
            }

            context.Update(conditional);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")] // api/conditionals/2
        public async Task<ActionResult> Delete(int id)
        {
            var exists = await context.Conditionals.AnyAsync(x => x.Id == id);

            if (!exists)
            {
                return NotFound();
            }

            context.Remove(new Conditional() { Id = id });
            await context.SaveChangesAsync();
            return Ok();
        }

    }
}
