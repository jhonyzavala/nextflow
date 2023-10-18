using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi_nextflow.DTOs;
using webapi_nextflow.Entity;
using webapi_nextflow.Utilities;

namespace webapi_nextflow.Controllers
{
    [ApiController]    
    [Route("api/workflows/{workflowid}/participants")]
    public class ParticipantsController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ParticipantsController( ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("{taskid:int}")]
        public async Task<ActionResult<List<ParticipantDTO>>> Get(string workflowid, int taskid)
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


            var participants = await context.Participants.Where(x=>x.TaskId==taskid).ToListAsync();

            
            return mapper.Map<List<ParticipantDTO>>(participants);     
        }

        [HttpPost]
        public async Task<ActionResult> Post(string workflowid, ParticipantCreateDTO participantCreateDTO)
        {
           var exists = await context.Workflows.AnyAsync(x => x.Id == workflowid && x.Owner== getUser() );

            if (!exists)
            {
                return BadRequest($"Workflow with Id {workflowid} does not exist or not the owner  ");
            }

            var existTask = await context.Tasks.AnyAsync(x=>x.Id == participantCreateDTO.TaskId && x.Item.WorkflowId == workflowid );

            if (!existTask) {

                return BadRequest($"The task with Id {participantCreateDTO.TaskId} does not belong to the workflow  ");
            }

            
            var existsParticipant = await context.Participants.AnyAsync(x =>x.TaskId == participantCreateDTO.TaskId && x.ParticipantId==participantCreateDTO.ParticipantId);

            if (existsParticipant)
            {
                return BadRequest($"A collaborator with the id {participantCreateDTO.ParticipantId} already exists");
            }

            var participant = mapper.Map<Participant>(participantCreateDTO);
            
            context.Add(participant);
            await context.SaveChangesAsync();

            return Ok();
        }
       

        [HttpDelete("{id:int}")] // api/workflows/{workflowid}/collaborators/1 
        public async Task<ActionResult> Delete(string workflowid, int taskid, string participantId)
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

            
            var existsParticipant = await context.Participants.AnyAsync(x => x.TaskId == taskid && x.ParticipantId == participantId );

            if (!existsParticipant)
            {
                return NotFound();
            }

            context.Remove(new Participant() { TaskId = taskid, ParticipantId = participantId  });
            await context.SaveChangesAsync();
            return Ok();
        }  

        public string getUser() {

            return "jhony.zavala@pemex.com";

        }  
             

    }
}