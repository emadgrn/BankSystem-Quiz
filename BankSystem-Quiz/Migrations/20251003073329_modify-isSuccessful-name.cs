using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankSystem_Quiz.Migrations
{
    /// <inheritdoc />
    public partial class modifyisSuccessfulname : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isSuccessful",
                table: "Transactions",
                newName: "IsSuccessful");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsSuccessful",
                table: "Transactions",
                newName: "isSuccessful");
        }
    }
}
