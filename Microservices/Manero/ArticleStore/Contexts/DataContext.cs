using ArticleStore.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ArticleStore.Contexts;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<ArticleEntity> Articles { get; set; }
}
