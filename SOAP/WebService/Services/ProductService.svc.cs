using System.Collections.Generic;
using System.Linq;
using WebService.DTOs;
using WebService.Factories;

namespace WebService.Services
{
    public class ProductService : IProductService
    {
        private static readonly List<Product> _products = new List<Product>();

        public ServiceResult AddProductToList(Product product)
        {
            var result = ServiceResultFactory.Create();

            if (!_products.Any(x => x.ArticleNumber == product.ArticleNumber))
            {
                _products.Add(product);
                result.Status = ServiceCode.ADDED;
            }
            else
            {
                result.Status = ServiceCode.EXISTS;
            }

            return result;
        }

        public ServiceResult DeleteProductFromList(string articleNumber)
        {
            var result = ServiceResultFactory.Create();

            var product = _products.FirstOrDefault(x => x.ArticleNumber == articleNumber);
            if (product != null)
            {
                _products.Remove(product);
                result.Status= ServiceCode.DELETED;
            }
            else
            {
                result.Status = ServiceCode.NOTFOUND;
            }

            return result;
        }

        public ServiceResult GetProductFromList(string articleNumber)
        {
            var result = ServiceResultFactory.Create();

            var product = _products.FirstOrDefault(x => x.ArticleNumber == articleNumber);
            if (product != null)
            {
                result.Status = ServiceCode.OK;
                result.Product = product;
            }
            else
            {
                result.Status = ServiceCode.NOTFOUND;
            }

            return result;
        }

        public ServiceResult GetProductsFromList()
        {
            var result = ServiceResultFactory.Create();

            if (_products != null)
            {
                result.Status = ServiceCode.OK;
                result.Products = _products;
            }
            else
            {
                result.Status = ServiceCode.NOTFOUND;
            }

            return result;
        }

        public ServiceResult UpdateProductInList(Product product)
        {
            var result = ServiceResultFactory.Create();

            var existingProduct = _products.FirstOrDefault(x => x.ArticleNumber == product.ArticleNumber);
            if (existingProduct != null)
            {
                existingProduct = product;

                result.Status = ServiceCode.UPDATED;
                result.Product = existingProduct;
            }
            else
            {
                result.Status = ServiceCode.NOTFOUND;
            }

            return result;
        }
    }
}
