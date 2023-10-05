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
            
            var stages = await context.Stages.Where(x=>x.WorkflowId==workflowid).ToListAsync();
            
            return mapper.Map<List<StageDTO>>(stages);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<StageDTO>> Get(string workflowid , int id)
        {
            var stage = await context.Stages.Include(a=>a.Workflow).FirstOrDefaultAsync(x=>x.Id==id && x.WorkflowId == workflowid);

            if (stage ==null)
            {
                return NotFound();
            }            

            return mapper.Map<StageDTO>(stage);

        }

        [HttpPost]

        public async Task<ActionResult> Post(string workflowid, StageCreateDTO stageCreateDTO)
        {                        

            var existsWorkflow = await context.Workflows.AnyAsync(x => x.Id == workflowid);

            if (!existsWorkflow)
            {
                return BadRequest($"Workflow with Id {workflowid} does not exist ");
            }            

            var stage=mapper.Map<Stage>(stageCreateDTO);
            stage.WorkflowId=workflowid;            
            context.Add(stage);
            await context.SaveChangesAsync();

            return Ok();
        }

        
        [HttpPut("{id:int}")] // api/workflows/{workflowid}/stages/1 
        public async Task<ActionResult> Put(string workflowid, int id, StageCreateDTO stageCreateDTO)
        {
 
            var existsWorkflow = await context.Workflows.AnyAsync(x => x.Id == workflowid);

            if (!existsWorkflow)
            {
                return BadRequest($"Workflow with Id {workflowid} does not exist ");
            }            
            
            var exists = await context.Stages.AnyAsync(x => x.Id == id && x.WorkflowId == workflowid );

            if (!exists)
            {
                return NotFound();
            }
            
            var stage = mapper.Map<Stage>(stageCreateDTO);
            stage.WorkflowId=workflowid;
            stage.Id=id;            

            context.Update(stage);
            await context.SaveChangesAsync();
            return Ok();
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(string workflowid, int id)
        {

            var existsWorkflow = await context.Workflows.AnyAsync(x => x.Id == workflowid);

            if (!existsWorkflow)
            {
                return NotFound();
            }                    

            var exists = await context.SpecificStatus.AnyAsync(x => x.Id == id && x.WorkflowId == workflowid);

            if (!exists)
            {
                return NotFound();
            }

            context.Remove(new SpecificStatus { Id = id });
            await context.SaveChangesAsync();
            return NoContent();
        }

       
    }
}
