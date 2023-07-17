using AutoMapper;
using BusinessLayer.Model.Models;
using WebApi.Models;

namespace WebApi
{
    public class AppServicesProfile : Profile
    {
        public AppServicesProfile()
        {
            CreateMapper();
        }

        private void CreateMapper()
        {
            CreateMap<BaseInfo, BaseDto>();
            CreateMap<CompanyInfo, CompanyDto>();
            CreateMap<EmployeeInfo, EmployeeDto>()
               .ForMember(desc => desc.OccupationName, opt => opt.MapFrom(src => src.Occupation))
               .ForMember(desc => desc.PhoneNumber, opt => opt.MapFrom(src => src.Phone))
               .ForMember(desc => desc.LastModifiedDateTime, opt => opt.MapFrom(src => src.LastModified));
            CreateMap<ArSubledgerInfo, ArSubledgerDto>();
        }
    }
}