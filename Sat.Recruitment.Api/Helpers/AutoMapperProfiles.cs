using AutoMapper;
using Sat.Recruitment.Api.DTOs;

namespace API.Helpers
{
  public class AutoMapperProfiles : Profile
  {
    public AutoMapperProfiles()
    {
        CreateMap<UserDto,UserDto>();   
    }
  }
}