using Microsoft.EntityFrameworkCore;
using WebApi.Contexts;
using WebApi.Models;
using WebApi.Repositories;
using WebApi.Services;

namespace WebApi.Tests.Services;

public class ProductService_Tests
{
    private DataContext DataContext()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
         .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
         .Options;

        return new DataContext(options);
    }

    [Fact]
    public async Task CreateProductAsyncShould_CreateOneProductEntity_ReturnProduct()
    {
        // Arrange
        var context = DataContext();
        IProductRepository repository = new ProductRepository(context);
        IProductService service = new ProductService(repository);

        var form = new ProductRegistrationForm
        {
            ArticleNumber = "123",
            Name = "Test Product",
            Description = "Test Description"
        };

        // Act
        var result = await service.CreateProductAsync(form);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<Product>(result);
    }

    [Fact]
    public async Task GetProductByArticleNumberAsyncShould_ReturnOneProduct()
    {
        var context = DataContext();
        IProductRepository repository = new ProductRepository(context);
        IProductService service = new ProductService(repository);

        var form = new ProductRegistrationForm
        {
            ArticleNumber = "123",
            Name = "Test Product",
            Description = "Test Description"
        };
        
        await service.CreateProductAsync(form);

        // Act
        var result = await service.GetProductByArticleNumberAsync("123");

        // Assert
        Assert.NotNull(result);
        Assert.IsType<Product>(result);
        Assert.Equal(form.ArticleNumber, result.ArticleNumber);

    }

    [Fact]
    public async Task GetProductsAsyncSholud_ReturnListOfProduct()
    {
        var context = DataContext();
        IProductRepository repository = new ProductRepository(context);
        IProductService service = new ProductService(repository);

        var form_1 = new ProductRegistrationForm
        {
            ArticleNumber = "1",
            Name = "Test Product",
            Description = "Test Description"
        };
        var form_2 = new ProductRegistrationForm
        {
            ArticleNumber = "2",
            Name = "Test Product",
            Description = "Test Description"
        };

        await service.CreateProductAsync(form_1);
        await service.CreateProductAsync(form_2);

        // Act
        var result = await service.GetProductsAsync();

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Count() == 2);
    }

    [Fact]
    public async Task ProductExitsAsyncSholud_ReturnTrue_IfProductEntityExists()
    {
        var context = DataContext();
        IProductRepository repository = new ProductRepository(context);
        IProductService service = new ProductService(repository);

        var form = new ProductRegistrationForm
        {
            ArticleNumber = "1",
            Name = "Test Product",
            Description = "Test Description"
        };

        await service.CreateProductAsync(form);
        

        // Act
        var result = await service.ProductExistsAsync("1");

        // Assert
        Assert.True(result);
    }
}
