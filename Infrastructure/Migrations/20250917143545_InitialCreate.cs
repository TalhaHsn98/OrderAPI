using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Order_History",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Order_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CustomerId = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PaymentMethodId = table.Column<int>(type: "int", nullable: true),
                    PaymentName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ShippingAddress = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ShippingMethod = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BillAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Order_Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order_History", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Order_Details",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Order_Id = table.Column<int>(type: "int", nullable: false),
                    Product_Id = table.Column<int>(type: "int", nullable: false),
                    Product_name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Qty = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order_Details", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_Details_Order_History_Order_Id",
                        column: x => x.Order_Id,
                        principalTable: "Order_History",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Order_History",
                columns: new[] { "Id", "BillAmount", "CustomerId", "CustomerName", "Order_Date", "Order_Status", "PaymentMethodId", "PaymentName", "ShippingAddress", "ShippingMethod" },
                values: new object[,]
                {
                    { 1, 115.00m, "CUST-1001", "Alice", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Created", 1, "Credit Card", "123 Main St, Toledo, OH", "Ground" },
                    { 2, 220.00m, "CUST-1002", "Bob", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Shipped", 2, "PayPal", "456 Oak St, Cleveland, OH", "Air" }
                });

            migrationBuilder.InsertData(
                table: "Order_Details",
                columns: new[] { "Id", "Discount", "Order_Id", "Price", "Product_Id", "Product_name", "Qty" },
                values: new object[,]
                {
                    { 1, 0m, 1, 45.00m, 10, "Keyboard", 1 },
                    { 2, 5.00m, 1, 35.00m, 11, "Mouse", 2 },
                    { 3, 0m, 2, 220.00m, 12, "Monitor", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Order_Details_Order_Id",
                table: "Order_Details",
                column: "Order_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Order_Details");

            migrationBuilder.DropTable(
                name: "Order_History");
        }
    }
}
