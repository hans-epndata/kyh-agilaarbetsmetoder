using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WebService.DTOs;

namespace WebService.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IProductService" in both code and config file together.
    [ServiceContract]
    public interface IProductService
    {
        [OperationContract]
        ServiceResult AddProductToList(Product product);

        [OperationContract]
        ServiceResult GetProductsFromList();

        [OperationContract]
        ServiceResult GetProductFromList(string articleNumber);

        [OperationContract]
        ServiceResult UpdateProductInList(Product product);

        [OperationContract]
        ServiceResult DeleteProductFromList(string articleNumber);
    }
}
