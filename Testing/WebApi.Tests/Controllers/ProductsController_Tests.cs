using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text;
using System.Text.Json;
using WebApi.Contexts;
using WebApi.Controllers;
using WebApi.Models;
using WebApi.Repositories;
using WebApi.Services;

namespace WebApi.Tests.Controllers;

public class ProductsController_Tests
{
    private DataContext DataContext()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
         .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
         .Options;

        return new DataContext(options);
    }

    [Fact]
    public async Task CreateShould_ReturnBadRequest_IfModelStateIsInvalid()
    {
        // Arrange
        var context = DataContext();
        IProductRepository repository = new ProductRepository(context);
        IProductService service = new ProductService(repository);
        ProductsController controller = new ProductsController(service);

        controller.ModelState.AddModelError("Name", "Name is required");

        // Act
        var result = await controller.Create(new ProductRegistrationForm());

        // Assert
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task CreateShould_ReturnConflict_IfProductWithSameArticleNumberAlreadyExists()
    {
        // Arrange
        var context = DataContext();
        IProductRepository repository = new ProductRepository(context);
        IProductService service = new ProductService(repository);
        ProductsController controller = new ProductsController(service);

        var form = new ProductRegistrationForm
        {
            ArticleNumber = "123456789",
            Name = "Test Product",
            Description = "Test Description",
        };
        await service.CreateProductAsync(form);


        // Act
        var result = await controller.Create(form);

        // Assert
        Assert.IsType<ConflictResult>(result);
    }

    [Fact]
    public async Task CreateShould_ReturnCreated_IfProductIsCreated()
    {
        // Arrange
        var context = DataContext();
        IProductRepository repository = new ProductRepository(context);
        IProductService service = new ProductService(repository);
        ProductsController controller = new ProductsController(service);

        var form = new ProductRegistrationForm
        {
            ArticleNumber = "123456789",
            Name = "Test Product",
            Description = "Test Description",
        };

        // Act
        var result = await controller.Create(form);

        // Assert
        Assert.IsType<CreatedResult>(result);
    }

    [Fact]
    public async Task GetAllShould_ReturnOkWithListOfProduct()
    {
        // Arrange
        var context = DataContext();
        IProductRepository repository = new ProductRepository(context);
        IProductService service = new ProductService(repository);
        ProductsController controller = new ProductsController(service);
        var form_1 = new ProductRegistrationForm
        {
            ArticleNumber = "1",
            Name = "Test Product",
            Description = "Test Description",
        };
        var form_2 = new ProductRegistrationForm
        {
            ArticleNumber = "2",
            Name = "Test Product",
            Description = "Test Description",
        };
        await controller.Create(form_1);
        await controller.Create(form_2);

        // Act
        var result = await controller.GetAll();

        // Assert
        Assert.IsType<OkObjectResult>(result);
        result.Equals(HttpStatusCode.OK);

        var okResult = result as OkObjectResult;
        Assert.True(okResult!.Value is IEnumerable<Product>);

        var products = okResult!.Value as IEnumerable<Product>;
        Assert.Equal(2, products!.Count());
    }

}
