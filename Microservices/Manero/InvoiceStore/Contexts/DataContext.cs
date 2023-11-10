using InvoiceStore.Models.Entitites;
using Microsoft.EntityFrameworkCore;

namespace InvoiceStore.Contexts;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<InvoiceEntity> Invoices { get; set; }
    public DbSet<InvoiceRowsEntity> InvoiceRows { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<InvoiceRowsEntity>()
            .HasKey(x => new { x.InvoiceNumber, x.ArticleNumber });
    }
}
