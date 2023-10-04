using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi_nextflow.DTOs;
using webapi_nextflow.Entity;

// pending implementation of voting role

namespace webapi_nextflow.Controllers
{
    [ApiController]
    [Route("api/voting")]
    public class VotingController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public VotingController( ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<TaskDTO>>> Get(string workflowid)
        {
            var exists = await context.Workflows.AnyAsync(x => x.Id == workflowid);

            if (!exists)
            {
                return BadRequest($"Workflow with Id {workflowid} does not exist ");
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
        public async Task<ActionResult<TaskDTO>> Get(int id)
        {
            var task = await context.Tasks.FirstOrDefaultAsync(x=>x.Id==id);

            if (task==null)
            {
                return NotFound();
            }            

            return mapper.Map<TaskDTO>(task);
        }

        [HttpPost]
        public async Task<ActionResult> Post(string workflowid, TaskCreateDTO taskCreateDTO)
        {                        

            var existsWorkflow = await context.Workflows.AnyAsync(x => x.Id == workflowid);

            if (!existsWorkflow)
            {
                return BadRequest($"Workflow with Id {workflowid} does not exist ");
            }

            var task=mapper.Map<Entity.Task>(taskCreateDTO);                
            context.Add(task);
            await context.SaveChangesAsync();

            return Ok();
        }

        
        [HttpPut("{id:int}")] // api/workflows/{workflowid}/tasks/1 
        public async Task<ActionResult> Put(string workflowid, int id, TaskCreateDTO taskCreateDTO)
        {
 
            var existsWorkflow = await context.Workflows.AnyAsync(x => x.Id == workflowid);

            if (!existsWorkflow)
            {
                return BadRequest($"Workflow with Id {workflowid} does not exist ");
            }            
            
            var exists = await context.Tasks.AnyAsync(x => x.Id == id);

            if (!exists)
            {
                return NotFound();
            }

            // Validate that the task belongs to the workflow - earring            

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

            var existsWorkflow = await context.Workflows.AnyAsync(x => x.Id == workflowid);

            if (!existsWorkflow)
            {
                return BadRequest($"Workflow with Id {workflowid} does not exist ");
            }                    

            var taskDB = await context.Tasks.FirstOrDefaultAsync(x => x.Id == id);

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

            // Validate that the task belongs to the workflow - earring         

            mapper.Map(taskCreateDTO, taskDB);

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

            var exists = await context.Tasks.AnyAsync(x => x.Id == id);

            if (!exists)
            {
                return NotFound();
            }

            context.Remove(new Entity.Task() { Id = id });
            await context.SaveChangesAsync();
            return NoContent();
        }


    }
}
