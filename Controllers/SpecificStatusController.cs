using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi_nextflow.DTOs;
using webapi_nextflow.Entity;

namespace webapi_nextflow.Controllers
{
    [ApiController]    
    [Route("api/workflows/{workflowid}/specificstatus")]
    public class SpecificStatusController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public SpecificStatusController( ApplicationDbContext context,  IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<SpecificStatusDTO>>> Get(string workflowid)
        {
            var exists = await context.Workflows.AnyAsync(x => x.Id == workflowid);

            if (!exists)
            {
                return BadRequest($"Workflow with Id {workflowid} does not exist ");
            }

            var specificStatus = await context.SpecificStatus.Where(a=>a.WorkflowId==workflowid).ToListAsync();

            return mapper.Map<List<SpecificStatusDTO>>(specificStatus);      
            
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<SpecificStatusDTO>> Get(int id)
        {
            var specific = await context.SpecificStatus.FirstOrDefaultAsync(x=>x.Id==id);

            if (specific==null)
            {
                return NotFound();
            }
            
            return mapper.Map<SpecificStatusDTO>(specific);
        }

        [HttpPost]
        public async Task<ActionResult> Post(string workflowid, SpecificStatusCreateDTO specificStatusCreateDTO)
        {                        

            var existsWorkflow = await context.Workflows.AnyAsync(x => x.Id == workflowid);

            if (!existsWorkflow)
            {
                return BadRequest($"Workflow with Id {workflowid} does not exist ");
            }

            var specificStatus = mapper.Map<SpecificStatus>(specificStatusCreateDTO);                
            specificStatus.WorkflowId = workflowid;
            
            context.Add(specificStatus);
            await context.SaveChangesAsync();

            return Ok();
        }
        

        [HttpPut("{id:int}")] // api/workflows/{workflowid}/specificstatus/1 
        public async Task<ActionResult> Put(string workflowid, int id, SpecificStatusCreateDTO specificStatusCreateDTO)
        { 
            var existsWorkflow = await context.Workflows.AnyAsync(x => x.Id == workflowid);

            if (!existsWorkflow)
            {
                return BadRequest($"Workflow with Id {workflowid} does not exist ");
            }            
            
            var exists = await context.SpecificStatus.AnyAsync(x => x.Id == id);

            if (!exists)
            {
                return NotFound();
            }

            // Validate that the specificStatus belongs to the workflow - earring            

            var specificStatus = mapper.Map<SpecificStatus>(specificStatusCreateDTO);          
            specificStatus.Id=id;            

            context.Update(specificStatus);
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

            var exists = await context.SpecificStatus.AnyAsync(x => x.Id == id);

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
