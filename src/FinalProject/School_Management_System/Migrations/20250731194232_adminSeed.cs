using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class adminSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: -1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -1);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Password", "UserName" },
                values: new object[] { -2, "$2a$11$oOCLQLJaIJYCI/uRPa82jeBBwWy5TMg54UVp2eEFNlIhhCbP6J.qS", "admin" });

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "Id", "Name" },
                values: new object[] { -2, "Super Admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: -2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -2);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Password", "UserName" },
                values: new object[] { -1, "1234", "admin" });

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "Id", "Name" },
                values: new object[] { -1, null });
        }
    }
}
