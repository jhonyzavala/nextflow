using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi_nextflow.Entity;
using webapi_nextflow.DTOs;

namespace webapi_nextflow.Controllers
{
    [ApiController]
    [Route("api/workflows")]
    public class WorkflowController : ControllerBase
    {

        public ApplicationDbContext context { get; }
        public IMapper mapper { get; }

        public WorkflowController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this. mapper = mapper;
        }               

        [HttpGet]
        public async Task<ActionResult<List<WorkflowDTO>>> Get()
        {
            // filtered by the author
            var workflows = await context.Workflows.Where(x=>x.Owner==getUser()).ToListAsync();

            return mapper.Map<List<WorkflowDTO>>(workflows);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WorkflowDTO>> Get( string id)
        {
            var workflow = await context.Workflows.Include(a=>a.Items).FirstOrDefaultAsync(x => x.Id == id && x.Owner==getUser());

            if (workflow ==null)
            {
                return NotFound();
            }
            
            return mapper.Map<WorkflowDTO>(workflow);
        }


        [HttpPost]
        public async Task<ActionResult> Post( WorkflowCreateDTO workflowCreateDTO )
        {
            
            var workflow = mapper.Map<Workflow>(workflowCreateDTO);   

            workflow.Id = Guid.NewGuid().ToString();  

            // Pending assign owner

            context.Add(workflow);
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{id}")] // api/workflows/1 
        public async Task<ActionResult> Put(WorkflowCreateDTO workflowCreateDTO, string id)
        {            

            var exists = await context.Workflows.AnyAsync(x => x.Id == id);
            // Pending valid owner

            if (!exists)
            {
                return NotFound();
            }
           
            var workflow = mapper.Map<Workflow>(workflowCreateDTO);          
            workflow.Id=id; 

            context.Update(workflow);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")] // api/workflows/2
        public async Task<ActionResult> Delete(string id)
        {
            var exists = await context.Workflows.AnyAsync(x => x.Id == id);
            // Pending valid owner

            if (!exists)
            {
                return NotFound();
            }

            context.Remove(new Workflow() { Id = id });
            await context.SaveChangesAsync();
            return Ok();
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public string getUser() {
            return "jhony.zavala@pemex.com";
        }  


    }
}
