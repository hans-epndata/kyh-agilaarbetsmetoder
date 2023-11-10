using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvoiceStore.Models.Entitites;

public class InvoiceEntity
{

    [Key, Required] public int InvoiceNumber { get; set; }
    [Required, Column(TypeName = "money")] public decimal TotaltAmount { get; set; }
    [Required] public string CustomerNumber { get; set; } = null!;
    public string? CustomerName { get; set; }

    [Required] public string Billing_StreetName { get; set; } = null!;
    [Required] public string Billing_PostalCode { get; set; } = null!;
    [Required] public string Billing_City { get; set; } = null!;
    [Required] public string Billing_Country { get; set; } = null!;

    public string? Delivery_StreetName { get; set; }
    public string? Delivery_PostalCode { get; set; }
    public string? Delivery_City { get; set; }
    public string? Delivery_Country { get; set; }

    public ICollection<InvoiceRowsEntity> InvoiceRows { get; set; } = new List<InvoiceRowsEntity>();    



    public static implicit operator InvoiceEntity(InvoiceFormRegistration form)
    {
        var entity = new InvoiceEntity
        {
            CustomerNumber = form.CustomerNumber,
            CustomerName = form.CustomerName,

            Billing_StreetName = form.BillingAddress.StreetName,
            Billing_PostalCode = form.BillingAddress.PostalCode,
            Billing_City = form.BillingAddress.City,
            Billing_Country = form.BillingAddress.Country,

            Delivery_StreetName = form.DeliveryAddress?.StreetName,
            Delivery_PostalCode = form.DeliveryAddress?.PostalCode,
            Delivery_City = form.DeliveryAddress?.City,
            Delivery_Country = form.DeliveryAddress?.Country,
        };

        foreach (var row in form.ShoppingCart)
            entity.InvoiceRows.Add(new InvoiceRowsEntity
            {
                ArticleNumber = row.ArticleNumber,
                Quantity = row.Quantity,
                Price = row.Price
            });

        return entity;
    }

}
