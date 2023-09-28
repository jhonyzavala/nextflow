
using AutoMapper;
using webapi_nextflow.DTOs;
using webapi_nextflow.Entity;

namespace webapi_nextflow.Utilities;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<WorkflowCreateDTO, Workflow>();
        CreateMap<Workflow, WorkflowDTO>();
        
        CreateMap<GroupsCreateDTO, Group>();
        CreateMap<Group, GroupDTO>();

        CreateMap<ItemCreateDTO,Item>();
        CreateMap<Item,ItemDTO>();
        
        CreateMap<TaskCreateDTO, Entity.Task>();            
        CreateMap<Entity.Task, TaskDTO>();
               
        CreateMap<SpecificStatusCreateDTO, SpecificStatus>();            
        CreateMap<SpecificStatus, SpecificStatusDTO>();

        CreateMap<StageCreateDTO, Stage>();
        CreateMap<Stage, StageDTO>();

        CreateMap<GroupsUserCreateDTO, GroupsUser>();
        CreateMap<GroupsUser, GroupsUserDTO>();

        CreateMap<StatusFlowItemCreateDTO, StatusFlowItem>();
        CreateMap<StatusFlowItem, StatusFlowItemDTO>();

        CreateMap<WorkFlowItemCreateDTO, WorkFlowItem>();
        CreateMap<WorkFlowItem, WorkFlowItemDTO>();

        CreateMap<TransitionCreateDTO, Transition>();
        CreateMap<Transition, TransitionDTO>();

    }
}

