using System.Collections.Generic;

namespace WebService.DTOs
{
    public enum ServiceCode
    {
        OK = 200,
        NOTFOUND = 404,
        EXISTS = 409,
        ERROR = 500,
        ADDED,
        DELETED,
        UPDATED
    }


    public class ServiceResult
    {
        public ServiceCode Status { get; set; } = ServiceCode.OK;
        public Product Product { get; set; }
        public List<Product> Products { get; set; }
    }
}