using RESTFUL_WebApi.Models;

namespace RESTFUL_WebApi.Services;

public interface IProductService
{
    ServiceResult AddProductToList(Product product);
    ServiceResult GetProductsFromList();
    ServiceResult GetProductFromList(string articleNumber);
    ServiceResult UpdateProductInList(Product product);
    ServiceResult DeleteProductFromList(string articleNumber);
}
