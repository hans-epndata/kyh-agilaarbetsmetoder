using WebApi.Contexts;
using WebApi.Models.Users;

namespace WebApi.Helpers.Repositories;

public class UserRepository : Repo<UserEntity>
{
    public UserRepository(DataContext context) : base(context)
    {
    }
}
