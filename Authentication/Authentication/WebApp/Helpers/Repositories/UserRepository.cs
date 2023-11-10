using WebApp.Contexts;
using WebApp.Models.Entities;

namespace WebApp.Helpers.Repositories
{
    public class UserRepository : Repo<UserEntity>
    {
        public UserRepository(DataContext context) : base(context)
        {
        }
    }
}
