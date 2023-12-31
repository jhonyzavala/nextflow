﻿using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi_nextflow.DTOs;
using webapi_nextflow.Entity;

namespace webapi_nextflow.Controllers
{
    [ApiController]
    [Route("api/workflows/{workflowid}/items")]
    public class ItemsController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ItemsController( ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ItemDTO>>> Get(string workflowid)
        {
            var exists = await context.Workflows.AnyAsync(x => x.Id == workflowid && x.Owner==getUser());

            if (!exists)
            {
                return BadRequest($"Workflow with Id {workflowid} does not exist or not the owner ");
            }
            
            var items = await context.Items.Where(a=>a.WorkflowId==workflowid).ToListAsync();

            return mapper.Map<List<ItemDTO>>(items);       
        }

        
        [HttpGet("{id:int}")] 
        public async Task<ActionResult<ItemDTO>> Get(string workflowid, int id)
        {
           
            var exists = await context.Workflows.AnyAsync(x => x.Id == workflowid && x.Owner==getUser());

            if (!exists)
            {
                return BadRequest($"Workflow with Id {workflowid} does not exist or not the owner ");
            }           
           
            var item = await context.Items.FirstOrDefaultAsync(x=>x.Id==id && x.WorkflowId == workflowid);

            if (item==null)
            {
                return NotFound();
            }            

            return mapper.Map<ItemDTO>(item);
        }

        [HttpPost]
        public async Task<ActionResult> Post(string workflowid, ItemCreateDTO itemCreateDTO)
        {                        

            var exists = await context.Workflows.AnyAsync(x => x.Id == workflowid && x.Owner==getUser() );

            if (!exists)
            {
                return BadRequest($"Workflow with Id {workflowid} does not exist or not the owner ");
            }          

            var item=mapper.Map<Item>(itemCreateDTO);
            item.WorkflowId=workflowid;            
            context.Add(item);
            await context.SaveChangesAsync();

            return Ok();
        }

        
        [HttpPut("{id:int}")] // api/workflows/{workflowid}/items/1 
        public async Task<ActionResult> Put(string workflowid, int id, ItemCreateDTO itemCreateDTO)
        {
 
            var existsWorkflow = await context.Workflows.AnyAsync(x => x.Id == workflowid && x.Owner==getUser() );

            if (!existsWorkflow)
            {
                return BadRequest($"Workflow with Id {workflowid} does not exist or not the owner");
            }             
            
            var exists = await context.Items.AnyAsync(x => x.Id == id && x.WorkflowId == workflowid);

            if (!exists)
            {
                return NotFound();
            }
            

            var item = mapper.Map<Item>(itemCreateDTO);
            item.WorkflowId=workflowid;
            item.Id=id;            

            context.Update(item);
            await context.SaveChangesAsync();
            return Ok();
        }



        [HttpPatch("{id:int}")]
        public async Task<ActionResult> Patch(string workflowid, int id, JsonPatchDocument<ItemCreateDTO> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }

            var existsWorkflow = await context.Workflows.AnyAsync(x => x.Id == workflowid && x.Owner==getUser() );

            if (!existsWorkflow)
            {
                return BadRequest($"Workflow with Id {workflowid} does not exist or not the owner");
            }                    

            var itemDB = await context.Items.FirstOrDefaultAsync(x => x.Id == id && x.WorkflowId == workflowid );

            if (itemDB == null)
            {
                return NotFound();
            }

            var itemCreateDTO = mapper.Map<ItemCreateDTO>(itemDB);

            patchDocument.ApplyTo(itemCreateDTO, ModelState);

            var isValid = TryValidateModel(itemCreateDTO);

            if (!isValid)
            {
                return BadRequest(ModelState);
            }

            mapper.Map(itemCreateDTO, itemDB);

            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(string workflowid, int id)
        {

            var existsWorkflow = await context.Workflows.AnyAsync(x => x.Id == workflowid && x.Owner==getUser() );

            if (!existsWorkflow)
            {
                return BadRequest($"Workflow with Id {workflowid} does not exist or not the owner");
            }                     

            var exists = await context.Items.AnyAsync(x => x.Id == id && x.WorkflowId == workflowid);

            if (!exists)
            {
                return NotFound();
            }

            context.Remove(new Item() { Id = id });
            await context.SaveChangesAsync();
            return NoContent();
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public string getUser() {
            return "jhony.zavala@pemex.com";
        }  
            

    }
}
