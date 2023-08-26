using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi_nextflow.Entity;

namespace webapi_nextflow.Controllers
{
    [ApiController]
    [Route("api/approvaltype")]
    public class ApprovalTypesController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public ApprovalTypesController( ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<ApprovalType>>> Get()
        {
            return await context.ApprovalTypes.ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ApprovalType>> Get(int id)
        {
            var approvalType = await context.ApprovalTypes.FirstOrDefaultAsync(x=>x.Id==id);

            if (approvalType==null)
            {
                return NotFound();
            }

            return Ok(approvalType);
        }

        [HttpPost]
        public async Task<ActionResult> Post(ApprovalType approvalType)
        {
            context.Add(approvalType);
            await context.SaveChangesAsync();

            return Ok();

        }

    }
}
