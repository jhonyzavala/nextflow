using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi_nextflow.DTOs;
using webapi_nextflow.Entity;

namespace webapi_nextflow.Controllers
{
    [ApiController]
    [Route("api/statusflowitem")]
    public class StatusFlowItemController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public StatusFlowItemController( ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<StatusFlowItemDTO>>> Get()
        {           
            var statusFlowItems = await context.StatusFlowItems.ToListAsync();
     
            return mapper.Map<List<StatusFlowItemDTO>>(statusFlowItems);       
        }

       
        [HttpGet("{id:int}")] 
        public async Task<ActionResult<StatusFlowItemDTO>> Get(int id)
        {
            var statusFlowItem = await context.StatusFlowItems.FirstOrDefaultAsync(x=>x.Id==id);

            if (statusFlowItem==null)
            {
                return NotFound();
            }            

            return mapper.Map<StatusFlowItemDTO>(statusFlowItem);
        }

    }
}
