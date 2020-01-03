using Microsoft.EntityFrameworkCore.Migrations;

namespace Testnt.IdentityServer.Data.Migrations.Main.TestntMainDb
{
    public partial class IdentityUserCustomProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsEnabled",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEnabled",
                table: "AspNetUsers");
        }
    }
}
