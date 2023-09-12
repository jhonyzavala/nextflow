
using AutoMapper;
using webapi_nextflow.DTOs;
using webapi_nextflow.Entity;

namespace webapi_nextflow.Utilities;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<GroupsCreateDTO, Group>();

        CreateMap<ItemCreateDTO,Item>();
        CreateMap<Item,ItemDTO>();
        
        CreateMap<TaskCreateDTO, Entity.Task>();            
        CreateMap<Entity.Task, TaskDTO>();
               
    }
}

