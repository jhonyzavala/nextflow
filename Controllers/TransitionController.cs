using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi_nextflow.DTOs;
using webapi_nextflow.Entity;

namespace webapi_nextflow.Controllers
{
    [ApiController]
    [Route("api/transition")]
    public class TransitionController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public TransitionController( ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<TransitionDTO>>> Get()
        {
         
            var transitions = await context.Transitions.ToListAsync();
                
            return mapper.Map<List<TransitionDTO>>(transitions);       
        }

       
        [HttpGet("{id:int}")] 
        public async Task<ActionResult<TransitionDTO>> Get(int id)
        {
            var transition = await context.Transitions.FirstOrDefaultAsync(x=>x.Id==id);

            if (transition==null)
            {
                return NotFound();
            }            

            return mapper.Map<TransitionDTO>(transition);
        }

        [HttpPost]
        public async Task<ActionResult> Post(TransitionCreateDTO transitionCreateDTO)
        {         
            var transition=mapper.Map<Transition>(transitionCreateDTO);                
            context.Add(transition);
            await context.SaveChangesAsync();

            return Ok();
        }

        
        [HttpPut("{id:int}")] // api/workflows/{workflowid}/tasks/1 
        public async Task<ActionResult> Put(string workflowid, int id, TransitionCreateDTO transitionCreateDTO)
        {
 
            var existsWorkflow = await context.Workflows.AnyAsync(x => x.Id == workflowid);

            if (!existsWorkflow)
            {
                return BadRequest($"Workflow with Id {workflowid} does not exist ");
            }            
            
            var exists = await context.Transitions.AnyAsync(x => x.Id == id);

            if (!exists)
            {
                return NotFound();
            }

            // Validate that the task belongs to the workflow - earring            

            var transition = mapper.Map<Transition>(transitionCreateDTO);          
            transition.Id=id;            

            context.Update(transition);
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

            var exists = await context.Transitions.AnyAsync(x => x.Id == id);

            if (!exists)
            {
                return NotFound();
            }

            //Pending Verify owner

            context.Remove(new Transition() { Id = id });
            await context.SaveChangesAsync();
            return NoContent();
        }


    }
}
