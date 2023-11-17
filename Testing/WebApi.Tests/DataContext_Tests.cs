using Microsoft.EntityFrameworkCore;
using WebApi.Contexts;
using WebApi.Models.Entities;

namespace WebApi.Tests;

public class DataContext_Tests
{
    private DataContext DataContext()
    {
           var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new DataContext(options);
    }

    [Fact]
    public async Task AddAsyncShould_AddProductToDatabase_AndReturnProductEntity()
    {
        // Arrange
        var context = DataContext();
        var product = new ProductEntity
        {
            ArticleNumber = "123",
            Name = "Test",
            Description = "Test",
            IsEnabled = true
        };

        // Act
        await context.Products.AddAsync(product);
        await context.SaveChangesAsync();

        // Assert
        var productEntity = await context.Products.FirstOrDefaultAsync();
        
        Assert.NotNull(productEntity);
        Assert.Equal(product.ArticleNumber, productEntity!.ArticleNumber);

        context.Database.EnsureDeleted();
        context.Dispose();
    }

    [Fact]
    public async Task FirstOrDefaultAsyncShould_ReturnOneProductEntity_FromDatabase()
    {
        // Arrange
        var context = DataContext();
        var product = new ProductEntity
        {
            ArticleNumber = "123",
            Name = "Test",
            Description = "Test",
            IsEnabled = true
        };

        await context.Products.AddAsync(product);
        await context.SaveChangesAsync();

        // Act
        var productEntity = await context.Products.FirstOrDefaultAsync();

        // Assert
        Assert.NotNull(productEntity);

        context.Database.EnsureDeleted();
        context.Dispose();
    }
}
