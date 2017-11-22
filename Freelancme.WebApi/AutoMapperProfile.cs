using AutoMapper;
using Freelanceme.Domain;
using Freelanceme.WebApi.V1.Dto;

namespace Freelanceme.WebApi
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<TimeTracking, TimeTrackingDetails>();
            CreateMap<Freelanceme.Domain.Client, ClientInfo>()
                .ForMember(obj => obj.ContactName, (IMemberConfigurationExpression<Freelanceme.Domain.Client, ClientInfo, string> opt) => opt.MapFrom(prop => prop.IsTaxPayer ? $"{prop.Name} {prop.Surname}" : string.Empty))
                .ForMember(obj => obj.Name, (IMemberConfigurationExpression<Freelanceme.Domain.Client, ClientInfo, string> opt) => opt.MapFrom(prop => prop.IsCompany ? prop.CompanyName : $"{prop.Name} {prop.Surname}"))
                .ForMember(obj => obj.Address, (IMemberConfigurationExpression<Freelanceme.Domain.Client, ClientInfo, string> opt) => opt.MapFrom(prop => prop.Address.Street));

            CreateMap<Freelanceme.Domain.TimeTracking, V1.Dto.TimeLog>().ReverseMap();
            CreateMap<Freelanceme.Domain.Client, V1.Dto.Client>().ReverseMap();
            CreateMap<Freelanceme.Domain.Address, V1.Dto.Client>().ReverseMap();
            CreateMap<Freelanceme.Domain.Project, V1.Dto.ProjectInfo>();
        }
    }
}
