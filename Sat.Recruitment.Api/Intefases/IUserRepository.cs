using Sat.Recruitment.Api.Entities;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Intefases
{
    public interface IUserRepository
    {
        Task<bool> IsEmpyAsync();
        Task<bool> IsDuplicatedAsync(User user);
        Task<bool> CreateUserAsync(User user);
    }
}
