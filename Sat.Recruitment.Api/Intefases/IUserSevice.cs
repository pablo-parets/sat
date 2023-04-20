using Sat.Recruitment.Api.Entities;
using Sat.Recruitment.Api.Utils;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Intefases
{
    public interface IUserSevice
    {
        Task<MethodResult> CreateUserAsync(User user);
    }
}
