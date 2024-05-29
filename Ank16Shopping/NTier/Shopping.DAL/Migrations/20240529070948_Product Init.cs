using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shopping.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ProductInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "eb15e6e3-f613-42fb-9cb0-71d3ebca3958", "AQAAAAIAAYagAAAAEP2pL54gc5B3UP/Yf2k0eqD08gPOyAFSxxbhVObaSU+UO08ydBiJ9mT1oz7b6SMuwA==", "c5774798-8931-4ad2-a086-0f2c0ce32911" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "27873f88-69f0-4321-b7f9-be69b546066c", "AQAAAAIAAYagAAAAEGsWEhd8EEBFrFdK7/Kcjv7goT8FraRyq5IrnvuJ2WW1MNsVw7aHHCf4qWaOkhjNKg==", "f7e2008e-50f9-475f-9699-82adb0a0a06e" });
        }
    }
}
