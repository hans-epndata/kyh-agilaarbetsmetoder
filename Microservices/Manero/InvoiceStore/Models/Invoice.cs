using InvoiceStore.Models.Entitites;

namespace InvoiceStore.Models;

public class Invoice : InvoiceFormRegistration
{
    public int InvoiceNumber { get; set; }
    public decimal TotaltAmount { get; set; }

    public static implicit operator Invoice(InvoiceEntity entity)
    {
        var invoice = new Invoice
        {
            InvoiceNumber = entity.InvoiceNumber,
            CustomerName = entity.CustomerName,
            CustomerNumber = entity.CustomerNumber,
            BillingAddress = new Address
            {
                StreetName = entity.Billing_StreetName,
                PostalCode = entity.Billing_PostalCode,
                City = entity.Billing_City,
                Country = entity.Billing_Country
            },
            DeliveryAddress = new Address
            {
                StreetName = entity.Delivery_StreetName!,
                PostalCode = entity.Delivery_PostalCode!,
                City = entity.Delivery_City!,
                Country = entity.Delivery_Country!
            },
        };

        foreach (var item in entity.InvoiceRows)
            invoice.ShoppingCart.Add(new CartItem
            {
                ArticleNumber = item.ArticleNumber,
                Quantity = item.Quantity,
                Price = item.Price,
            });

        return invoice;
    }
}
