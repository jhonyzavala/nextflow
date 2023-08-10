using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi_nextflow.Entity;

namespace webapi_nextflow.Controllers
{
    [ApiController]
    [Route("api/items")]
    public class ItemsController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public ItemsController( ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Item>>> Get()
        {
            return await context.Items.ToListAsync();
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
