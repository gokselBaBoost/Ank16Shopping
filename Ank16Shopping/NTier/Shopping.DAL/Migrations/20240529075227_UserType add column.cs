using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shopping.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UserTypeaddcolumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserType",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "UserType" },
                values: new object[] { "4a25955c-221b-42e9-8b3f-f9df98588ac4", "AQAAAAIAAYagAAAAEBb30ZyY480ORlUhgrz1GwmTlhq+DbdH82RV/k7yEIbTSid4Vmy/vKgoTDQ8oaCznQ==", "0216c7e5-8839-4d94-bfda-b3856e5ff321", 2 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserType",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "eb15e6e3-f613-42fb-9cb0-71d3ebca3958", "AQAAAAIAAYagAAAAEP2pL54gc5B3UP/Yf2k0eqD08gPOyAFSxxbhVObaSU+UO08ydBiJ9mT1oz7b6SMuwA==", "c5774798-8931-4ad2-a086-0f2c0ce32911" });
        }
    }
}
