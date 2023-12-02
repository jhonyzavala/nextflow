using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi_nextflow.DTOs;
using webapi_nextflow.Entity;

namespace webapi_nextflow.Controllers
{
    [ApiController]
    [Route("api/workflows/{workflowid}/workflowitem")]
    public class WorkFlowItemController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public WorkFlowItemController( ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<WorkFlowItemDTO>>> Get(string workflowid)
        {
            var exists = await context.Workflows.AnyAsync(x => x.Id == workflowid && x.Owner==getUser() );

            if (!exists)
            {
                return BadRequest($"Workflow with Id {workflowid} does not exist  or not the owner ");
            }
           
           
           var workFlowItems = await context.WorkFlowItems.Where(x=>x.Transition.CurrentItemNavigation.WorkflowId==workflowid).ToListAsync();
          
                
            return mapper.Map<List<WorkFlowItemDTO>>(workFlowItems);       
        }

                    
        [HttpGet("{id}")] 
        public async Task<ActionResult<WorkFlowItemDTO>> GetId(string workflowid, string id)
        {
            
            var exists = await context.Workflows.AnyAsync(x => x.Id == workflowid && x.Owner==getUser() );

            if (!exists)
            {
                return BadRequest($"Workflow with Id {workflowid} does not exist  or not the owner ");
            }
                       
             // * * * * * * 
            var workflowitem = await context.WorkFlowItems.FirstOrDefaultAsync(x=>x.Id==id);

            if ( workflowitem==null )
            {
                return NotFound();
            }            

            return mapper.Map<WorkFlowItemDTO>(workflowitem);
        }

        // Get by UserId

       [HttpGet("{userId}")] 
        public async Task<ActionResult<List<WorkFlowItemDTO>>> Get(string workflowid, string userId) {

            var exists = await context.Workflows.AnyAsync(x => x.Id == workflowid && x.Owner==getUser() );

            if (!exists)
            {
                return BadRequest($"Workflow with Id {workflowid} does not exist  or not the owner ");
            }

            // * * * * * * 
            var workFlowItems = await context.WorkFlowItems.Where(x=>x.UserId==userId && x.Transition.CurrentItemNavigation.WorkflowId==workflowid).ToListAsync(); 
                
            return mapper.Map<List<WorkFlowItemDTO>>(workFlowItems);             
                       
        }


        [HttpPost]
        public async Task<ActionResult> Post(string workflowid, WorkFlowItemDTO workFlowItemDTO)
        {                        

            var exists = await context.Workflows.AnyAsync(x => x.Id == workflowid && x.Owner==getUser() );

            if (!exists)
            {
                return BadRequest($"Workflow with Id {workflowid} does not exist or not the owner ");
            }           


            var item = await context.Items.AnyAsync(x=>x.Id==workFlowItemDTO.ItemId && x.WorkflowId==workflowid);           

            if (!item) {
                 return BadRequest($"Item with Id {workFlowItemDTO.ItemId} does not correspond to this workflow  with Id {workflowid} ");
            }

            var workFlowItem = mapper.Map<WorkFlowItem>(workFlowItemDTO);     
            workFlowItem.Id = Guid.NewGuid().ToString();           
            context.Add(workFlowItem);

            await context.SaveChangesAsync();

            return Ok();
        }

        
        [HttpPut("{id:int}")] // api/workflows/{workflowid}/tasks/1 
        public async Task<ActionResult> Put(string workflowid, string id, WorkFlowItemCreateDTO workFlowItemCreateDTO)
        {
 
            var existsWorkflow = await context.Workflows.AnyAsync(x => x.Id == workflowid && x.Owner==getUser());

            if (!existsWorkflow)
            {
                return BadRequest($"Workflow with Id {workflowid} does not exist or not the owner");
            }            
            
            var exists = await context.WorkFlowItems.AnyAsync(x => x.Id == id && x.Transition.CurrentItemNavigation.WorkflowId==workflowid);

            if (!exists)
            {
                return NotFound();
            }
       
            var workFlowItem = mapper.Map<WorkFlowItem>(workFlowItemCreateDTO);          
            workFlowItem.Id=id;            

            context.Update(workFlowItem);
            await context.SaveChangesAsync();
            return Ok();
        }


        [HttpPatch("{id:int}")]
        public async Task<ActionResult> Patch(string workflowid, string id, JsonPatchDocument<WorkFlowItemCreateDTO> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }

            var existsWorkflow = await context.Workflows.AnyAsync(x => x.Id == workflowid && x.Owner==getUser());

            if (!existsWorkflow)
            {
                return BadRequest($"Workflow with Id {workflowid} does not exist or not the owner");
            }            

            var workFlowItemDB = await context.WorkFlowItems.FirstOrDefaultAsync(x => x.Id == id && x.Transition.CurrentItemNavigation.WorkflowId==workflowid);

            if (workFlowItemDB == null)
            {
                return NotFound();
            }

            var workFlowItemCreateDTO = mapper.Map<WorkFlowItemCreateDTO>(workFlowItemDB);

            patchDocument.ApplyTo(workFlowItemCreateDTO, ModelState);

            var isValid = TryValidateModel(workFlowItemCreateDTO);

            if (!isValid)
            {
                return BadRequest(ModelState);
            }
            
            mapper.Map(workFlowItemCreateDTO, workFlowItemDB);

            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(string workflowid, string id)
        {
            var existsWorkflow = await context.Workflows.AnyAsync(x => x.Id == workflowid && x.Owner==getUser());

            if (!existsWorkflow)
            {
                return BadRequest($"Workflow with Id {workflowid} does not exist or not the owner");
            }          
            

            var exists = await context.WorkFlowItems.AnyAsync(x => x.Id == id && x.Transition.CurrentItemNavigation.WorkflowId==workflowid );

            if (!exists)
            {
                return NotFound();
            }

            context.Remove(new WorkFlowItem() { Id = id });
            await context.SaveChangesAsync();
            return NoContent();
        }

        [ApiExplorerSettings(IgnoreApi = true)]
         public string getUser() {
            return "jhony.zavala@pemex.com";
        }  
    }
}
