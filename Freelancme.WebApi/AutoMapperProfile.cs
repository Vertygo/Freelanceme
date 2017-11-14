using AutoMapper;
using Freelanceme.Domain;
using Freelancme.WebApi.V1.Dto;

namespace Freelancme.WebApi
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<TimeTracking, TimeTrackingDetails>();
        }
    }
}
