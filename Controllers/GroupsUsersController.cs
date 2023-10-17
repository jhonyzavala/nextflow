using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi_nextflow.DTOs;
using webapi_nextflow.Entity;

namespace webapi_nextflow.Controllers
{
    [ApiController]
    [Route("api/groupsusers")]
    public class GroupsUsersController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public GroupsUsersController( ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<GroupsUser>>> Get()
        {
            return await context.GroupsUsers.ToListAsync();
        }

        [HttpGet("{groupid:int}")]
        public async Task<ActionResult<GroupsUserDTO>> Get(int groupid)
        {
            var groupsUser = await context.GroupsUsers.FirstOrDefaultAsync(x=>x.GroupId==groupid);

            if (groupsUser==null)
            {
                return NotFound();
            }

            return mapper.Map<GroupsUserDTO>(groupsUser);      
        }

        [HttpPost]
        public async Task<ActionResult> Post(GroupsUserCreateDTO groupsUserCreateDTO)
        {
            var groupsUser = mapper.Map<GroupsUser>(groupsUserCreateDTO);
            context.Add(groupsUser);
            await context.SaveChangesAsync();

            return Ok();

        }


        [HttpDelete("{groupid:int}/{userid}")] // api/groupsusers/1/2
        public async Task<ActionResult> Delete(int groupid, string userid)
        {
            var exists = await context.GroupsUsers.AnyAsync(x => x.GroupId == groupid && x.UserId==userid);

            if (!exists)
            {
                return NotFound();
            }

            context.Remove(new GroupsUser() { GroupId = groupid, UserId = userid });
            await context.SaveChangesAsync();
            return Ok();
        }

    }
}
