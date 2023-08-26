using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi_nextflow.Entity;

namespace webapi_nextflow.Controllers
{
    [ApiController]
    [Route("api/groupsusers")]
    public class GroupsUsersController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public GroupsUsersController( ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<GroupsUser>>> Get()
        {
            return await context.GroupsUsers.ToListAsync();
        }

        [HttpGet("{userid}")]
        public async Task<ActionResult<GroupsUser>> Get(string userid)
        {
            var groupsUser = await context.GroupsUsers.FirstOrDefaultAsync(x=>x.UserId==userid);

            if (groupsUser==null)
            {
                return NotFound();
            }

            return Ok(groupsUser);
        }

        [HttpPost]
        public async Task<ActionResult> Post(GroupsUser groupsUser)
        {
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
