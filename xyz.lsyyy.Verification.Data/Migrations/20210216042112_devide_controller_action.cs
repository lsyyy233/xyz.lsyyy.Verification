using Microsoft.EntityFrameworkCore.Migrations;

namespace xyz.lsyyy.Verification.Data.Migrations
{
    public partial class devide_controller_action : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Actions");

            migrationBuilder.AddColumn<string>(
                name: "ActionName",
                table: "Actions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ControllerName",
                table: "Actions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActionName",
                table: "Actions");

            migrationBuilder.DropColumn(
                name: "ControllerName",
                table: "Actions");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Actions",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);
        }
    }
}
