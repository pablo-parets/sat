using Microsoft.EntityFrameworkCore;
using Sat.Recruitment.Api.Entities;
using Sat.Recruitment.Api.Intefases;
using System.Linq;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Data
{

    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateUserAsync(User user)
        {
            await _context.Users.AddAsync(user);  
            return await _context.SaveChangesAsync() > 1;
        }

        public async Task<bool> IsDuplicatedAsync(User user)
        {
            return await _context.Users
                            .Where( x =>
                                    x.Email == user.Email || 
                                    x.Phone == user.Phone)
                            .Where( x => x.Name == user.Name && 
                                    x.Address == user.Address
                                  )
                            .AnyAsync();
        }

        public async Task<bool> IsEmpyAsync()
        {
             return await _context.Users.AnyAsync();
        }
    }
}
