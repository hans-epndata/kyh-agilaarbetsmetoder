using System.Net;
using System.Text;
using System.Text.Json;
using WebApi.Models;
using WebApi.Tests.Helpers;

namespace WebApi.Tests.Controllers;

public class ApiEndpoint_ProductsController_Tests(CustomWebApplicationFactory factory) : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task ApiEndpoint_api_products__Should_ReturnBadRequest_IfModelStateIsInvalid()
    {
        // Arrange
        var form = new ProductRegistrationForm { };
        var content = new StringContent(JsonSerializer.Serialize(form), Encoding.UTF8, "application/json");


        // Act
        var response = await _client.PostAsync("/api/products", content);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task ApiEndpoint_api_products__Should_Created_IfProductWasCreated()
    {
        // Arrange
        var form = new ProductRegistrationForm { ArticleNumber = "1", Name = "Test", Description = "Test Product" };
        var content = new StringContent(JsonSerializer.Serialize(form), Encoding.UTF8, "application/json");


        // Act
        var result = await _client.PostAsync("/api/products", content);
        
        // Assert
        Assert.Equal(HttpStatusCode.Created, result.StatusCode);
    }
}
