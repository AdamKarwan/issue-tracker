using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.DTO;
using Api.Models;
using AutoMapper;

namespace Api.Maps
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TaskModel, TaskDto>();
            CreateMap<CreateTaskDto, TaskModel>();
        }
    }
}
