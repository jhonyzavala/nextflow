using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi_nextflow.DTOs;
using webapi_nextflow.Entity;
using webapi_nextflow.Utilities;

namespace webapi_nextflow.Controllers
{
    [ApiController]    
    [Route("api/workflows/{workflowid}/collaborators")]
    public class CollaboratorsController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public CollaboratorsController( ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("{taskid:int}")]
        public async Task<ActionResult<List<CollaboratorDTO>>> Get(string workflowid, int taskid)
        {
            var exists = await context.Workflows.AnyAsync(x => x.Id == workflowid && x.Owner== getUser());

            if (!exists)
            {
                return BadRequest($"Workflow with Id {workflowid} does not exist or not the owner ");
                
            }
            
            var existTask = await context.Tasks.AnyAsync(x=>x.Id == taskid && x.Item.WorkflowId == workflowid );

            if (!existTask) {

                return BadRequest($"The task with Id {taskid} does not belong to the workflow  ");
            }


            var collaborators = await context.Collaborators.Where(x=>x.TaskId==taskid).ToListAsync();

            
            return mapper.Map<List<CollaboratorDTO>>(collaborators);     
        }

        [HttpPost]
        public async Task<ActionResult> Post(string workflowid, CollaboratorCreateDTO collaboratorCreateDTO)
        {
           var exists = await context.Workflows.AnyAsync(x => x.Id == workflowid && x.Owner== getUser() );

            if (!exists)
            {
                return BadRequest($"Workflow with Id {workflowid} does not exist or not the owner  ");
            }

            var existTask = await context.Tasks.AnyAsync(x=>x.Id == collaboratorCreateDTO.TaskId && x.Item.WorkflowId == workflowid );

            if (!existTask) {

                return BadRequest($"The task with Id {collaboratorCreateDTO.TaskId} does not belong to the workflow  ");
            }

            
            var existsColaborator = await context.Collaborators.AnyAsync(x =>x.TaskId == collaboratorCreateDTO.TaskId && x.CollaboratorId==collaboratorCreateDTO.CollaboratorId);

            if (existsColaborator)
            {
                return BadRequest($"A collaborator with the id {collaboratorCreateDTO.CollaboratorId} already exists");
            }

            var collaborator = mapper.Map<Collaborator>(collaboratorCreateDTO);
            
            context.Add(collaborator);
            await context.SaveChangesAsync();

            return Ok();
        }
       

        [HttpDelete("{id:int}")] // api/workflows/{workflowid}/collaborators/1 
        public async Task<ActionResult> Delete(string workflowid, int taskid, string collaboratorId)
        {
            
           var exists = await context.Workflows.AnyAsync(x => x.Id == workflowid && x.Owner== getUser() );

            if (!exists)
            {
                return BadRequest($"Workflow with Id {workflowid} does not exist or not the owner  ");
            }

            var existTask = await context.Tasks.AnyAsync(x=>x.Id == taskid && x.Item.WorkflowId == workflowid );

            if (!existTask) {

                return BadRequest($"The task with Id {taskid} does not belong to the workflow  ");
            }

            
            var existsColaborador = await context.Collaborators.AnyAsync(x => x.TaskId == taskid && x.CollaboratorId == collaboratorId );

            if (!existsColaborador)
            {
                return NotFound();
            }

            context.Remove(new Collaborator() { TaskId = taskid, CollaboratorId = collaboratorId  });
            await context.SaveChangesAsync();
            return Ok();
        }  

        public string getUser() {

            return "jhony.zavala@pemex.com";

        }  
             

    }
}