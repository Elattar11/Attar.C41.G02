using Attar.C41.G02.DAL.Models;
using Attar.C41.G02.PL.ViewModels;
using AutoMapper;

namespace Attar.C41.G02.PL.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap();
            CreateMap<DepartmentViewModel, Department>().ReverseMap();
                //.ForMember(d => d.Name , o => o.MapFrom(s => s.Name));
        }
    }
}
