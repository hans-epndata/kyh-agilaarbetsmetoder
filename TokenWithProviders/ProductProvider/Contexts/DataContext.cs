using Microsoft.EntityFrameworkCore;
using ProductProvider.Models;

namespace ProductProvider.Contexts;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<ProductEntity> Products { get; set; }
}
