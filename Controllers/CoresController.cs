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


            // Obtiene el estado actual
            var item= workFlowItems.Item;
            bool lblParallelPending = false;            

            var transition = await context.Transitions.Where(a=>a.CurrentItem==item.Id).Select(z=>z.NextItem).ToListAsync();

            if ( transition.Count()>1 ) {  //si hay tareas en paralelo                
                var parallelPending = await context.WorkFlowItems.Where(x=>transition.Contains(x.ItemId) && x.EndDate!=null).ToListAsync(); 
                if ( parallelPending.Count()>0 ){
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

