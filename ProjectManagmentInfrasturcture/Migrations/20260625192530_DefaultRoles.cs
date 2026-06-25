using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectManagmentInfrasturcture.Migrations
{
    /// <inheritdoc />
    public partial class DefaultRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "IsDefault", "IsDeleted", "Name", "NormalizedName" },
                values: new object[] { "12cb051d-9cd6-4117-9c9a-b1b77ff8bbb9", "85d2ec47-988b-4a95-8ae9-ec2845b73884", false, false, "User", "USER" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "12cb051d-9cd6-4117-9c9a-b1b77ff8bbb9");
        }
    }
}
