using Microsoft.EntityFrameworkCore;
using WebApi.Contexts;
using WebApi.Models.Entities;
using WebApi.Repositories;

namespace WebApi.Tests.Repositories;

public class ProductRepository_Tests
{
    private DataContext DataContext()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
         .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
         .Options;

        return new DataContext(options);
    }


    [Fact]
    public async Task CreateAsyncShould_CreateOneProductEntity_ReturnProductEntity()
    {
        // Arrange
        var context = DataContext();
        IProductRepository repository = new ProductRepository(context);

        var entity = new ProductEntity
        {
            ArticleNumber = "123",
            Name = "Test Product",
            Description = "Test Description"
        };

        // Act
        var result = await repository.CreateAsync(entity);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<ProductEntity>(result);
    }

    [Fact]
    public async Task GetAsyncShould_ReturnOneProductEntity()
    {
        // Arrange
        var context = DataContext();
        IProductRepository repository = new ProductRepository(context);
        var entity = new ProductEntity
        {
            ArticleNumber = "123",
            Name = "Test Product",
            Description = "Test Description"
        };
        await repository.CreateAsync(entity);

        // Act
        var result = await repository.GetAsync(x => x.ArticleNumber == entity.ArticleNumber);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<ProductEntity>(result);
        Assert.Equal(entity.ArticleNumber, result.ArticleNumber);

    }

    [Fact]
    public async Task GetAllAsyncShould_ReturnListOfProductEntity()
    {
        // Arrange
        var context = DataContext();
        IProductRepository repository = new ProductRepository(context);
        var entity_1 = new ProductEntity
        {
            ArticleNumber = "1",
            Name = "Test Product",
            Description = "Test Description"
        };
        var entity_2 = new ProductEntity
        {
            ArticleNumber = "2",
            Name = "Test Product",
            Description = "Test Description"
        };
        await repository.CreateAsync(entity_1);
        await repository.CreateAsync(entity_2);

        // Act
        var result = await repository.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Count() == 2);

    }

    [Fact]
    public async Task ExistsAsyncShould_ReturnTrue_IfProductEntityExists()
    {
        // Arrange
        var context = DataContext();
        IProductRepository repository = new ProductRepository(context);
        var entity = new ProductEntity
        {
            ArticleNumber = "123",
            Name = "Test Product",
            Description = "Test Description"
        };
        await repository.CreateAsync(entity);

        // Act
        var result = await repository.ExistsAsync(x => x.ArticleNumber == entity.ArticleNumber);

        // Assert
        Assert.True(result);
    }
}
