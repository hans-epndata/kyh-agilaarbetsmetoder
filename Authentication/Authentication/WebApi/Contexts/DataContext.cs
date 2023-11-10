using Microsoft.EntityFrameworkCore;
using WebApi.Models.Keys;
using WebApi.Models.Products;
using WebApi.Models.Users;

namespace WebApi.Contexts;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<ProductEntity> Products { get; set; }
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<KeyEntity> ApiKeys { get; set; }
}
