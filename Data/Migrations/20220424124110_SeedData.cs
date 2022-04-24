using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "23467589-8655-49b9-99ca-2e64d85da55f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "5fe3695c-a37d-47dd-a2c8-2a195b6e022f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "1600cc25-ec72-40ce-9297-87f0a1e4aa85");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "Description", "IsDeleted", "ModifiedAt", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 4, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Shoes category", false, new DateTime(2022, 4, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Shoes" },
                    { 2, new DateTime(2022, 4, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Suits category", false, new DateTime(2022, 4, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Suits" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "Description", "DiscountId", "IsDeleted", "ModifiedAt", "Name", "PictureUrl", "Price", "Quantity" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2022, 4, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Shoes", 0, false, new DateTime(2022, 4, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Adidas", "https://fadzrinmadu.github.io/hosted-assets/product-detail-page-design-with-image-slider-html-css-and-javascript/shoe_1.jpg", 0m, 0 },
                    { 2, 2, new DateTime(2022, 4, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Suit", 0, false, new DateTime(2022, 4, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Gosling's suite", "https://www3.pictures.stylebistro.com/gi/Place+Beyond+Pines+Premiere+Arrivals+2012+NpkDUEe9LBRl.jpg", 0m, 0 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "9d4b8dba-9be1-40b4-83db-98070a431f9e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "158d75c0-9880-4a4c-9e9b-04de46185efb");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "43d6c381-d02e-40fe-9682-c19f00cae53a");
        }
    }
}
