using Microsoft.EntityFrameworkCore.Migrations;

namespace cakeworld.Migrations
{
    public partial class buyerPassword : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Buyers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Buyers");
        }
    }
}
