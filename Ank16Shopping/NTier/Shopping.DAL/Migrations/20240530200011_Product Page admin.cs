using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shopping.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ProductPageadmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "517e9448-c985-4a5c-a551-9efd016f094b", "AQAAAAIAAYagAAAAEOAi/q3NnHLu7YUMhcUqlqoeq/BTAcCMbgcTAzsZNF03Ja64B7x2WTo3whAZCmcuKg==", "65b9a189-8681-4192-9ed3-abd5fc5d4648" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e4af44f8-8b76-4536-aa6d-caf0a6172ada", "AQAAAAIAAYagAAAAEBCLX5wHR+eA4XHSK1BXextl88UHfpn/uzV8Oi3e8W4QmTVGvlCRy+jAqAb6SoOm6w==", "adc50b47-2380-4d83-8c35-df2579ce8ac0" });
        }
    }
}
