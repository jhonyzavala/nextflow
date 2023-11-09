using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi_nextflow.DTOs;
using webapi_nextflow.Entity;

namespace webapi_nextflow.Controllers
{
    [ApiController]
    [Route("api/workflows/{workflowid}/tasks")]
    public class TasksController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public TasksController( ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<TaskDTO>>> Get(string workflowid)
        {
            var exists = await context.Workflows.AnyAsync(x => x.Id == workflowid && x.Owner==getUser());

            if (!exists)
            {
                return BadRequest($"Workflow with Id {workflowid} does not exist or not the owner ");
            }

            var tasks = await (
                                    from a in context.Tasks
                                    join b in context.Items on a.Id equals b.Id
                                    where b.WorkflowId == workflowid
                                    select a            
                                ).ToListAsync();
                
            return mapper.Map<List<TaskDTO>>(tasks);       
        }

       
        [HttpGet("{id:int}")] 
        public async Task<ActionResult<TaskDTO>> Get(string workflowid, int id)
        {
            var exists = await context.Workflows.AnyAsync(x => x.Id == workflowid && x.Owner==getUser() );

            if (!exists)
            {
                return BadRequest($"Workflow with Id {workflowid} does not exist or not the owner");
            }            
            
            // Check Sintaxy
            var task = await context.Tasks.Include(x=>x.Item).FirstOrDefaultAsync(x=>x.Id==id && x.Item.WorkflowId == workflowid);

            if (task==null)
            {
                return NotFound();
            }            

            return mapper.Map<TaskDTO>(task);
        }

        [HttpPost]
        public async Task<ActionResult> Post(string workflowid, TaskCreateDTO taskCreateDTO)
        {                        

            var existsWorkflow = await context.Workflows.AnyAsync(x => x.Id == workflowid && x.Owner==getUser() );

            if (!existsWorkflow)
            {
                return BadRequest($"Workflow with Id {workflowid} does not exist or not the owner ");
            }

            var task=mapper.Map<Entity.Task>(taskCreateDTO);                
            context.Add(task);
            await context.SaveChangesAsync();

            return Ok();
        }

        
        [HttpPut("{id:int}")] // api/workflows/{workflowid}/tasks/1 
        public async Task<ActionResult> Put(string workflowid, int id, TaskCreateDTO taskCreateDTO)
        {
 
            var existsWorkflow = await context.Workflows.AnyAsync(x => x.Id == workflowid && x.Owner==getUser() );

            if (!existsWorkflow)
            {
                return BadRequest($"Workflow with Id {workflowid} does not exist or not the owner ");
            }
       
            var exists = await context.Tasks.Include(x=>x.Item).AnyAsync(x => x.Id == id && x.Item.WorkflowId == workflowid);

            if (!exists)
            {
                return NotFound();
            }
       
            var task = mapper.Map<Entity.Task>(taskCreateDTO);          
            task.Id=id;            

            context.Update(task);
            await context.SaveChangesAsync();
            return Ok();
        }


        [HttpPatch("{id:int}")]
        public async Task<ActionResult> Patch(string workflowid, int id, JsonPatchDocument<TaskCreateDTO> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }

            var existsWorkflow = await context.Workflows.AnyAsync(x => x.Id == workflowid && x.Owner==getUser() );

            if (!existsWorkflow)
            {
                return BadRequest($"Workflow with Id {workflowid} does not exist or not the owner ");
            }               

            
            var taskDB = await context.Tasks.FirstOrDefaultAsync(x => x.Id == id && x.Item.WorkflowId == workflowid);

            if (taskDB == null)
            {
                return NotFound();
            }

            var taskCreateDTO = mapper.Map<TaskCreateDTO>(taskDB);

            patchDocument.ApplyTo(taskCreateDTO, ModelState);

            var isValid = TryValidateModel(taskCreateDTO);

            if (!isValid)
            {
                return BadRequest(ModelState);
            }

            mapper.Map(taskCreateDTO, taskDB);

            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(string workflowid, int id)
        {

            var existsWorkflow = await context.Workflows.AnyAsync(x => x.Id == workflowid && x.Owner==getUser() );

            if (!existsWorkflow)
            {
                return BadRequest($"Workflow with Id {workflowid} does not exist or not the owner ");
            }                               

            var exists = await context.Tasks.AnyAsync(x => x.Id == id && x.Item.WorkflowId == workflowid);

            if (!exists)
            {
                return NotFound();
            }

            context.Remove(new Entity.Task() { Id = id });
            await context.SaveChangesAsync();
            return NoContent();
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public string getUser() {
            return "jhony.zavala@pemex.com";
        }  

    }
}
