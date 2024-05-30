using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shopping.DAL.Migrations
{
    /// <inheritdoc />
    public partial class furkanınyaptigi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "PictureFile",
                table: "Products",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<string>(
                name: "PictureName",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e4af44f8-8b76-4536-aa6d-caf0a6172ada", "AQAAAAIAAYagAAAAEBCLX5wHR+eA4XHSK1BXextl88UHfpn/uzV8Oi3e8W4QmTVGvlCRy+jAqAb6SoOm6w==", "adc50b47-2380-4d83-8c35-df2579ce8ac0" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PictureFile",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PictureName",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4a25955c-221b-42e9-8b3f-f9df98588ac4", "AQAAAAIAAYagAAAAEBb30ZyY480ORlUhgrz1GwmTlhq+DbdH82RV/k7yEIbTSid4Vmy/vKgoTDQ8oaCznQ==", "0216c7e5-8839-4d94-bfda-b3856e5ff321" });
        }
    }
}
