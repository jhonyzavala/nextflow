using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi_nextflow.DTOs;
using webapi_nextflow.Entity;
using webapi_nextflow.Utilities;

namespace webapi_nextflow.Controllers
{
    [ApiController]    
    [Route("api/workflows/{workflowid}/groups")]
    public class GroupsController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public GroupsController( ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<Group>>> Get(string workflowid)
        {
            var exists = await context.Workflows.AnyAsync(x => x.Id == workflowid);

            if (!exists)
            {
                return BadRequest($"Workflow with Id {workflowid} does not exist ");
            }
            
            return await context.Groups.Where(x=>x.WorkflowId==workflowid).ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Group>> Get(string workflowid, int id)
        {
            
            var exists = await context.Workflows.AnyAsync(x => x.Id == workflowid);

            if (!exists)
            {
                return BadRequest($"Workflow with Id {workflowid} does not exist ");
            }
            
            var group = await context.Groups.Where(a=>a.WorkflowId==workflowid).FirstOrDefaultAsync(x=>x.Id==id);

            if (group==null)
            {
                return NotFound();
            }

            return Ok(group);
        }

        [HttpPost]
        public async Task<ActionResult> Post(string workflowid, GroupsCreateDTO groupCreateDTO)
        {
           var exists = await context.Workflows.AnyAsync(x => x.Id == workflowid);

            if (!exists)
            {
                return BadRequest($"Workflow with Id {workflowid} does not exist ");
            }
            
            var existsGroup = await context.Groups.AnyAsync(x =>x.WorkflowId==workflowid && x.Name == groupCreateDTO.Name);

            if (existsGroup)
            {
                return BadRequest($"A group with the name {groupCreateDTO.Name} already exists");
            }

            var group = mapper.Map<Group>(groupCreateDTO);
            group.WorkflowId=workflowid;
            context.Add(group);
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{id}")] // api/workflows/{workflowid}/groups/1 
        public async Task<ActionResult> Put(string workflowid, GroupsCreateDTO groupCreateDTO, int id)
        {
 
            var existsWorkflow = await context.Workflows.AnyAsync(x => x.Id == workflowid);

            if (!existsWorkflow)
            {
                return BadRequest($"Workflow with Id {workflowid} does not exist ");
            }            
            
            var exists = await context.Groups.AnyAsync(x => x.Id == id);

            if (!exists)
            {
                return NotFound();
            }

            var existsGroupName = await context.Groups.AnyAsync(x =>x.WorkflowId==workflowid && x.Name == groupCreateDTO.Name);

            if (existsGroupName)
            {
                return BadRequest($"A group with the name {groupCreateDTO.Name} already exists");
            }

            var group = mapper.Map<Group>(groupCreateDTO);
            group.WorkflowId=workflowid;
            group.Id=id;            

            context.Update(group);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")] // api/workflows/{workflowid}/groups/1 
        public async Task<ActionResult> Delete(int id)
        {
            var exists = await context.Groups.AnyAsync(x => x.Id == id);

            if (!exists)
            {
                return NotFound();
            }

            context.Remove(new Group() { Id = id });
            await context.SaveChangesAsync();
            return Ok();
        }        

    }
}