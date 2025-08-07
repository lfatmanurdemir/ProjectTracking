using AutoMapper;
using ProjectTracking.Core.Entities;
using ProjectTracking.DTO.EmployeeDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTracking.Core.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Employee, EmployeeCreateDTO>().ReverseMap();
            CreateMap<Employee, EmployeeUpdateDTO>().ReverseMap();
        }
    }
}
