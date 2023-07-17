using AutoMapper;
using BusinessLayer.Model.Models;
using DataAccessLayer.Model.Models;
using System;

namespace BusinessLayer
{
    public class BusinessProfile : Profile
    {
        public BusinessProfile()
        {
            CreateMapper();
        }

        private void CreateMapper()
        {
            CreateMap<DataEntity, BaseInfo>();
            CreateMap<Company, CompanyInfo>();
            CreateMap<Employee, EmployeeInfo>()
               .ForMember(e => e.OccupationName, (opt) => opt.MapFrom(e => e.Occupation))
               .ForMember(e => e.PhoneNumber, (opt) => opt.MapFrom(e => e.Phone))
               .ForMember(e => e.LastModifiedDateTime, (opt) => opt.MapFrom(e => e.LastModified.ToString()));

            CreateMap<EmployeeInfo, Employee>()
                .ForMember(e => e.Occupation, (opt) => opt.MapFrom(e => e.OccupationName))
                .ForMember(e => e.Phone, (opt) => opt.MapFrom(e => e.PhoneNumber))
                .ForMember(e => e.LastModified, (opt) => opt.MapFrom(e => DateTime.Parse(e.LastModifiedDateTime)));
            CreateMap<ArSubledger, ArSubledgerInfo>();
        }
    }

}