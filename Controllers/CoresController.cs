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
            
            //verifica si hay tareas en paralelo pendientes
            //var transition = await context.Transitions.Where(a=>a.CurrentItem==item.Id).Select(z=>z.NextItem).ToListAsync();
            //if ( transition.Count()>1 ) {                
              //  var parallelExist = await context.WorkFlowItems.Where(x=>transition.Contains(x.ItemId)).ToListAsync(); 
            //}
            //else {

            
                //si no hay tareas en paralelo pendientes,  despacha las tareas siguientes
           // }
            
           return Ok();
        }


        [ApiExplorerSettings(IgnoreApi = true)]
        public string getUser() {
            return "jhony.zavala@pemex.com";
        }  

    }
}

