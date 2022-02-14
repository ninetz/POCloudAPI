using AutoMapper;
using POCloudAPI.DTO;
using POCloudAPI.Entities;

namespace POCloudAPI.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() {
            CreateMap<APIUser, MemberDTO>();
            CreateMap<APIFile, APIFileDTO>();
        
        }
    }
}
