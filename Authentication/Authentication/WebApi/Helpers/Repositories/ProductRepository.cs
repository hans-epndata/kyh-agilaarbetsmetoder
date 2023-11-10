using System.Linq.Expressions;
using WebApi.Contexts;
using WebApi.Models.Products;

namespace WebApi.Helpers.Repositories;

public class ProductRepository : Repo<ProductEntity>
{
    public ProductRepository(DataContext context) : base(context)
    {
    }
}
