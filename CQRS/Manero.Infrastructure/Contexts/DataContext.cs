using Manero.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Manero.Infrastructure.Contexts;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{

    public DbSet<ProductEntity> Products { get; set; }
}
