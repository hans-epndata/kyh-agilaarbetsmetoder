using Microsoft.EntityFrameworkCore;
using ProductStore.Models;

namespace ProductStore.Contexts;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<ProductEntity> Products { get; set; }
}