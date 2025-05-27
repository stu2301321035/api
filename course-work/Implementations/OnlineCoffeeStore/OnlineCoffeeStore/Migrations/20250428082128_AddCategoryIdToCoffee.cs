using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OnlineCoffeeStore.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoryIdToCoffee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "CategoryName" },
                values: new object[,]
                {
                    { 1, "Espresso" },
                    { 2, "Latte" },
                    { 3, "Mocha" }
                });

            migrationBuilder.InsertData(
                table: "Coffees",
                columns: new[] { "Id", "CategoryId", "Ingredients", "Name", "Price", "Status" },
                values: new object[,]
                {
                    { 1, 1, "Espresso", "Espresso Shot", 2.0, 0 },
                    { 2, 2, "Espresso, Milk, Ice", "Iced Latte", 3.5, 0 },
                    { 3, 3, "Espresso, Milk, Chocolate Syrup, Ice", "Mocha Frappe", 4.0, 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Coffees",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Coffees",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Coffees",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
