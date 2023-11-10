using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvoiceStore.Migrations
{
    /// <inheritdoc />
    public partial class Invoices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    InvoiceNumber = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotaltAmount = table.Column<decimal>(type: "money", nullable: false),
                    CustomerNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Billing_StreetName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Billing_PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Billing_City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Billing_Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Delivery_StreetName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Delivery_PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Delivery_City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Delivery_Country = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.InvoiceNumber);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceRows",
                columns: table => new
                {
                    InvoiceNumber = table.Column<int>(type: "int", nullable: false),
                    ArticleNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "money", nullable: false),
                    InvoiceEntityInvoiceNumber = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceRows", x => new { x.InvoiceNumber, x.ArticleNumber });
                    table.ForeignKey(
                        name: "FK_InvoiceRows_Invoices_InvoiceEntityInvoiceNumber",
                        column: x => x.InvoiceEntityInvoiceNumber,
                        principalTable: "Invoices",
                        principalColumn: "InvoiceNumber");
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceRows_InvoiceEntityInvoiceNumber",
                table: "InvoiceRows",
                column: "InvoiceEntityInvoiceNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoiceRows");

            migrationBuilder.DropTable(
                name: "Invoices");
        }
    }
}
