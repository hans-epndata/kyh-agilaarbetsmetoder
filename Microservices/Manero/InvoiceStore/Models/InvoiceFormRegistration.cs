namespace InvoiceStore.Models;

public class InvoiceFormRegistration
{
    public string CustomerNumber { get; set; } = null!;
    public string? CustomerName { get; set; }
    public Address BillingAddress { get; set; } = null!;
    public Address? DeliveryAddress { get; set; }
    public List<CartItem> ShoppingCart { get; set; } = new();
}
