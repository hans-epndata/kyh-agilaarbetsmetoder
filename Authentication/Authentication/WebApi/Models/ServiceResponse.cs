using WebApi.Helpers.Enums;

namespace WebApi.Models;

public class ServiceResponse
{
    public ResponseStatusCode Status { get; set; } = ResponseStatusCode.OK;
    public string? Message { get; set; }
    public object? Result { get; set; }
}
