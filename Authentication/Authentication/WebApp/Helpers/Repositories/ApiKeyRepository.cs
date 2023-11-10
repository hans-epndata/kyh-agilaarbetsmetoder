using System.Text.Json;
using WebApp.Contexts;
using WebApp.Helpers.Misc;
using WebApp.Models.Entities;

namespace WebApp.Helpers.Repositories
{
    public class ApiKeyRepository : Repo<KeyEntity>
    {
        public ApiKeyRepository(DataContext context) : base(context)
        {
        }

        public override Task<KeyEntity> CreateAsync(KeyEntity entity)
        {
            entity.Key = Base64.Encode(JsonSerializer.Serialize(entity));
            return base.CreateAsync(entity);
        }
    }
}
