namespace InvoiceStore.Models;

public class CartItem
{
    public string ArticleNumber { get; set; } = null!;
    public int Quantity { get; set; } = 1;
    public decimal Price { get; set; }
}
