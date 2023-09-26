using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using webapi_nextflow.Entity;
using webapi_nextflow.DTOs;

namespace webapi_nextflow.Controllers
{
    [ApiController]
    [Route("api/workflows/{workflowid}/stages")]
    public class StagesController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public StagesController( ApplicationDbContext context, IMapper mapper )
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<StageDTO>>> Get( string workflowid )
        {
            var exists = await context.Workflows.AnyAsync(x => x.Id == workflowid);

            if (!exists)
            {
                return BadRequest($"Workflow with Id {workflowid} does not exist ");
            }
            
            var stages = await context.Stages.ToListAsync();
            
            return mapper.Map<List<StageDTO>>(stages);
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
