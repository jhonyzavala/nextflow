using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi_nextflow.DTOs;
using webapi_nextflow.Entity;

namespace webapi_nextflow.Controllers
{
    [ApiController]
    [Route("api/workflows/{workflowid}/statusflowitem")]
    public class StatusFlowItemController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public StatusFlowItemController( ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<StatusFlowItemDTO>>> Get(string workflowid)
        {
            var exists = await context.Workflows.AnyAsync(x => x.Id == workflowid);

            if (!exists)
            {
                return BadRequest($"Workflow with Id {workflowid} does not exist ");
            }


            var statusFlowItems = await context.StatusFlowItems.ToListAsync();

                
            return mapper.Map<List<StatusFlowItemDTO>>(statusFlowItems);       
        }

       
        [HttpGet("{id:int}")] 
        public async Task<ActionResult<StatusFlowItemDTO>> Get(int id)
        {
            var statusFlowItem = await context.StatusFlowItems.FirstOrDefaultAsync(x=>x.Id==id);

            if (statusFlowItem==null)
            {
                return NotFound();
            }            

            return mapper.Map<StatusFlowItemDTO>(statusFlowItem);
        }

        [HttpPost]
        public async Task<ActionResult> Post(string workflowid, StatusFlowItemCreateDTO statusFlowItemCreateDTO)
        {                        

            var existsWorkflow = await context.Workflows.AnyAsync(x => x.Id == workflowid);

            if (!existsWorkflow)
            {
                return BadRequest($"Workflow with Id {workflowid} does not exist ");
            }

            var statusFlowItem=mapper.Map<StatusFlowItem>(statusFlowItemCreateDTO);                
            context.Add(statusFlowItem);
            await context.SaveChangesAsync();

            return Ok();
        }

        
        [HttpPut("{id:int}")] // api/workflows/{workflowid}/tasks/1 
        public async Task<ActionResult> Put(string workflowid, int id, StatusFlowItemCreateDTO statusFlowItemCreateDTO)
        {
 
            var existsWorkflow = await context.Workflows.AnyAsync(x => x.Id == workflowid);

            if (!existsWorkflow)
            {
                return BadRequest($"Workflow with Id {workflowid} does not exist ");
            }            
            
            var exists = await context.StatusFlowItems.AnyAsync(x => x.Id == id);

            if (!exists)
            {
                return NotFound();
            }

            // Validate that the task belongs to the workflow - earring            

            var statusFlowItem = mapper.Map<Entity.Task>(statusFlowItemCreateDTO);          
            statusFlowItem.Id=id;            

            context.Update(statusFlowItem);
            await context.SaveChangesAsync();
            return Ok();
        }



        [HttpPatch("{id:int}")]
        public async Task<ActionResult> Patch(string workflowid, int id, JsonPatchDocument<StatusFlowItemCreateDTO> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }

            var existsWorkflow = await context.Workflows.AnyAsync(x => x.Id == workflowid);

            if (!existsWorkflow)
            {
                return BadRequest($"Workflow with Id {workflowid} does not exist ");
            }                    

            var statusFlowItemDB = await context.StatusFlowItems.FirstOrDefaultAsync(x => x.Id == id);

            if (statusFlowItemDB == null)
            {
                return NotFound();
            }

            var statusFlowItemCreateDTO = mapper.Map<StatusFlowItemCreateDTO>(statusFlowItemDB);

            patchDocument.ApplyTo(statusFlowItemCreateDTO, ModelState);

            var isValid = TryValidateModel(statusFlowItemCreateDTO);

            if (!isValid)
            {
                return BadRequest(ModelState);
            }

            // Validate that the task belongs to the workflow - earring         

            mapper.Map(statusFlowItemCreateDTO, statusFlowItemDB);

            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(string workflowid, int id)
        {

            var existsWorkflow = await context.Workflows.AnyAsync(x => x.Id == workflowid);

            if (!existsWorkflow)
            {
                return NotFound();
            }                    

            var exists = await context.StatusFlowItems.AnyAsync(x => x.Id == id);

            if (!exists)
            {
                return NotFound();
            }

            context.Remove(new StatusFlowItem() { Id = id });
            await context.SaveChangesAsync();
            return NoContent();
        }


    }
}
