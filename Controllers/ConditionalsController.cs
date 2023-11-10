using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi_nextflow.DTOs;
using webapi_nextflow.Entity;

namespace webapi_nextflow.Controllers
{
    [ApiController]
    [Route("api/workflows/{workflowid}/condicionals")]
    public class ConditionalsController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ConditionalsController( ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<Conditional>>> Get(string workflowid)
        {

            var exists = await context.Workflows.AnyAsync(x => x.Id == workflowid && x.Owner== getUser());

            if (!exists)
            {
                return BadRequest($"Workflow with Id {workflowid} does not exist or not the owner ");
                
            }          
                        
            return await context.Conditionals.Where(x=>x.Item.WorkflowId== workflowid ).ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Conditional>> Get(string workflowid, int id)
        {
            var exists = await context.Workflows.AnyAsync(x => x.Id == workflowid && x.Owner== getUser());

            if (!exists)
            {
                return BadRequest($"Workflow with Id {workflowid} does not exist or not the owner ");
                
            }                 
            
            var conditional = await context.Conditionals.FirstOrDefaultAsync(x=>x.Id==id && x.Item.WorkflowId==workflowid) ;

            if (conditional==null)
            {
                return NotFound();
            }

            return Ok(conditional);
        }

        [HttpPost]
        public async Task<ActionResult> Post(string workflowid, ConditionalCreteDTO conditionalCreteDTO)
        {
            
            //Pending validate format json condicional


            var exists = await context.Workflows.AnyAsync(x => x.Id == workflowid && x.Owner== getUser());

            if (!exists)
            {
                return BadRequest($"Workflow with Id {workflowid} does not exist or not the owner ");
                
            }   

            var conditional=mapper.Map<Conditional>(conditionalCreteDTO);       
            context.Add(conditional);
            await context.SaveChangesAsync();

            return Ok();

        }

        [HttpPut("{id:int}")] // api/workflows/{workflowid}/conditionals/1 
        public async Task<ActionResult> Put(string workflowid, ConditionalCreteDTO conditionalCrete,  int id)
        {
            
            var existsWorkflow = await context.Workflows.AnyAsync(x => x.Id == workflowid && x.Owner==getUser() );

            if (!existsWorkflow)
            {
                return BadRequest($"Workflow with Id {workflowid} does not exist or not the owner ");
            }           

            var exists = await context.Conditionals.AnyAsync(x => x.Id == id && x.Item.WorkflowId==workflowid );

            if (!exists)
            {
                return NotFound();
            }

            var conditional = mapper.Map<Conditional>(conditionalCrete);          
            conditional.Id=id;            
            
            context.Update(conditional);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")] // api/workflows/{workflowid}/conditionals/2
        public async Task<ActionResult> Delete(string workflowid, int id)
        {
            
            var existsWorkflow = await context.Workflows.AnyAsync(x => x.Id == workflowid && x.Owner==getUser() );

            if (!existsWorkflow)
            {
                return BadRequest($"Workflow with Id {workflowid} does not exist or not the owner ");
            }           

            var exists = await context.Conditionals.AnyAsync(x => x.Id == id && x.Item.WorkflowId==workflowid );

            if (!exists)
            {
                return NotFound();
            }

            context.Remove(new Conditional() { Id = id });
            await context.SaveChangesAsync();
            return Ok();
        }

        [ApiExplorerSettings(IgnoreApi = true)]        
        public string getUser() {
            return "jhony.zavala@pemex.com";
        }          

    }
}
