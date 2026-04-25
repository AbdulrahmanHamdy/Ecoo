using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ECOO.Migrations
{
    /// <inheritdoc />
    public partial class Create : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CustomerEmail = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ShippingAddress = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Reusable Goods" },
                    { 2, "Organic Food" },
                    { 3, "Eco Apparel" },
                    { 4, "Zero-Waste Kitchen" },
                    { 5, "Natural Beauty" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "ImageUrl", "Name", "Price" },
                values: new object[,]
                {
                    { 1, 1, "Durable, BPA-free bamboo & stainless steel bottle. Keeps drinks cold for 24 hrs.", "/images/products/bamboo-bottle.jpg", "Bamboo Water Bottle", 24.99m },
                    { 2, 1, "Replace single-use plastic wrap. Reusable, washable, compostable.", "/images/products/beeswax-wraps.jpg", "Beeswax Food Wraps (3-pack)", 14.99m },
                    { 3, 2, "Ceremonial-grade matcha from shade-grown Japanese tea leaves. 100g.", "/images/products/matcha.jpg", "Organic Matcha Powder", 19.99m },
                    { 4, 2, "Extra-virgin, single-origin olive oil in a recyclable glass bottle. 500ml.", "/images/products/olive-oil.jpg", "Cold-Pressed Olive Oil", 12.49m },
                    { 5, 3, "Sturdy, naturally dyed hemp bag. Holds up to 20 kg. Certified organic.", "/images/products/hemp-tote.jpg", "Hemp Tote Bag", 18.00m },
                    { 6, 3, "Soft tee made from 100% post-consumer recycled cotton. Available S–XL.", "/images/products/recycled-tshirt.jpg", "Recycled Cotton T-Shirt", 29.99m },
                    { 7, 4, "Bamboo handle + plant-fibre bristles. Fully compostable after use.", "/images/products/dish-brush.jpg", "Compostable Dish Brush", 9.99m },
                    { 8, 4, "Set of 4 reusable straws + cleaning brush in a cotton pouch.", "/images/products/steel-straws.jpg", "Stainless Steel Straw Set", 11.99m },
                    { 9, 5, "100% organic cold-pressed rosehip oil. Vitamin-C rich. 30ml amber glass bottle.", "/images/products/rosehip-oil.jpg", "Rosehip Face Oil", 22.00m },
                    { 10, 5, "Palm-oil-free, vegan shampoo bar. Replaces 3 plastic bottles. All hair types.", "/images/products/shampoo-bar.jpg", "Solid Shampoo Bar", 13.50m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductId",
                table: "OrderItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
