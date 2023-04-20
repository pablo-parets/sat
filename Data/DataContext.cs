using System.Collections.Generic;
using System.Data.Entity;

namespace Sat.Recruitment.Api.Data
{
    public class DataContext
    {
        public DbSet<User> Likes { get; set; }
    }
}
