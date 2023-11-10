using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvoiceStore.Models.Entitites;

public class InvoiceRowsEntity
{
    [Required] public int InvoiceNumber { get; set; }
    [Required] public string ArticleNumber { get; set; } = null!;
    [Required] public int Quantity { get; set; }
    [Required, Column(TypeName = "money")] public decimal Price { get; set; }
}