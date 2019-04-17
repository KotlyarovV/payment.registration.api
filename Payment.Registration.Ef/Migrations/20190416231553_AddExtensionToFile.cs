using Microsoft.EntityFrameworkCore.Migrations;

namespace Payment.Registration.Ef.Migrations
{
    public partial class AddExtensionToFile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Extension",
                table: "File",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Extension",
                table: "File");
        }
    }
}
