using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi_nextflow.DTOs;
using webapi_nextflow.Entity;

// pending implementation of voting role

namespace webapi_nextflow.Controllers
{
    [ApiController]
    [Route("api/voting")]
    public class VotingController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public VotingController( ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<VotingDTO>>> Get()
        {
            var votings = await context.Votings.ToListAsync();

                
            return mapper.Map<List<VotingDTO>>(votings);       
        }

       
        [HttpGet("{id}")] 
        public async Task<ActionResult<VotingDTO>> Get(string id)
        {
            var voting = await context.Votings.FirstOrDefaultAsync(x=>x.Id==id);

            if (voting==null)
            {
                return NotFound();
            }            

            return mapper.Map<VotingDTO>(voting);
        }

        [HttpPost]
        public async Task<ActionResult> Post( VotingCreateDTO votingCreateDTO)
        {                        

            var voting=mapper.Map<Voting>(votingCreateDTO);                
            context.Add(voting);
            await context.SaveChangesAsync();

            return Ok();
        }

        
        [HttpPut("{id}")] // api/voting/1 
        public async Task<ActionResult> Put( string id, VotingCreateDTO votingCreateDTO)
        { 
            
            var exists = await context.Votings.AnyAsync(x => x.Id == id);

            if (!exists)
            {
                return NotFound();
            }

            // Pending validate that the task belongs to the workflow - earring            

            var voting = mapper.Map<Voting>(votingCreateDTO);          
            voting.Id=id;            

            context.Update(voting);
            await context.SaveChangesAsync();
            return Ok();
        }



        [HttpPatch("{id}")]
        public async Task<ActionResult> Patch( string id, JsonPatchDocument<VotingCreateDTO> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }                       

            var votingDB = await context.Votings.FirstOrDefaultAsync(x => x.Id == id);

            if (votingDB == null)
            {
                return NotFound();
            }

            var votingCreateDTO = mapper.Map<VotingCreateDTO>(votingDB);

            patchDocument.ApplyTo(votingCreateDTO, ModelState);

            var isValid = TryValidateModel(votingCreateDTO);

            if (!isValid)
            {
                return BadRequest(ModelState);
            }

            // Validate that the task belongs to the workflow - earring         

            mapper.Map(votingCreateDTO, votingDB);

            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {

            var exists = await context.Votings.AnyAsync(x => x.Id == id);

            if (!exists)
            {
                return NotFound();
            }

            context.Remove(new Voting() { Id = id });
            await context.SaveChangesAsync();
            return NoContent();
        }

    }
}
