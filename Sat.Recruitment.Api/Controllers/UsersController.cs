using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.Api.DTOs;
using Sat.Recruitment.Api.Entities;
using Sat.Recruitment.Api.Intefases;
using Sat.Recruitment.Api.Utils;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public partial class UsersController : ControllerBase
    {
        private readonly IUserSevice _userSevice;
        private readonly IMapper _mapper;
        public UsersController(IUserSevice userSevice)
        {
            this._userSevice = userSevice;
        }

        [HttpPost]
        [Route("/create-user")]
        public async Task<MethodResult> CreateUser(UserDto userDto)
        {
            var user = new User(); 
            _mapper.Map(userDto, user);
            await _userSevice.CreateUserAsync(user);
            var result = new MethodResult();

            return result;
        }
    }
}
