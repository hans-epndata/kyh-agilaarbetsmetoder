using RESTFUL_WebApi.Models;

namespace RESTFUL_WebApi.Factories;

public static class ProductFactory
{
    public static Product Create()
    {
        return new Product();
    }
}
