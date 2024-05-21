using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shopping.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Adminuserupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "NormalizedName",
                value: "ADMIN");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "Email", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "Surname", "UserName" },
                values: new object[] { "489d5be5-20d6-42e8-85bd-25f8897f6e9b", "admin@mail.com", "AdminName", "ADMIN@MAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEAnYYc5cgn1mXwNhvlLvRgUuy2/FuKGKN6PBjk2JpFrjL1y7/KeDxUw1nynTcxcjFg==", "2d673475-382b-4aeb-94a3-dc0b09ad461e", "AdminSurname", "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "NormalizedName",
                value: "ADMİN");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "Email", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "Surname", "UserName" },
                values: new object[] { "5e96bfbd-8b36-4741-9329-cfb7b17b9e9e", "Admin@mail.com", "Admin", "ADMİN@MAİL.COM", "ADMİN", "AQAAAAIAAYagAAAAELpfXP9zXLq53nGTzlqlR2CKTg/mJNVd1Ifjm9irvpeIOS5pV8EQgC3FOCxVa8X1yQ==", null, "Admin", "Admin" });
        }
    }
}
