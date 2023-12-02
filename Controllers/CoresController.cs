using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi_nextflow.Entity;

namespace webapi_nextflow.Controllers
{
    [ApiController]
    [Route("api/workflows/{workflowid}/cores")]
    public class CoresController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public CoresController( ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpPost("blue/nextitem")]
        public async Task<ActionResult> Post(string workflowid, string workFlowItemId, string userId, string JsonObject, int statusId, string comment  )
        {            

            var exists = await context.Workflows.AnyAsync(x => x.Id == workflowid && x.Owner==getUser());

            if (!exists)
            {
                return BadRequest($"Workflow with Id {workflowid} does not exist or not the owner ");
            }                    

            var workFlowItems = await context.WorkFlowItems.FirstOrDefaultAsync(x=>x.Id==workFlowItemId);

            // Note : Validate the structure of the object, so that its content is not null, every object must have an id

    
            bool lblParallelPending = false;            

            var transitionIds = await context.Transitions.Where(a=>a.CurrentItem== workFlowItems.Transition.CurrentItem).Select(z=>z.Id).ToListAsync();

            if ( transitionIds.Count()>1 ) {  //if there are task in parallel                
                                
                var parallelTask = await context.WorkFlowItems.Where(x=>transitionIds.Contains(x.TransitionId) && x.EndDate!=null).ToListAsync();                 
                //Consider the object
                if ( parallelTask.Count()>0 ){
                    lblParallelPending=true;
                }
            }

            if( !lblParallelPending) {

            }
            
            
           return Ok();
        }


        [ApiExplorerSettings(IgnoreApi = true)]
        public string getUser() {
            return "jhony.zavala@pemex.com";
        }  

    }
}

