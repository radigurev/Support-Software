using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBTest3.Migrations
{
    public partial class AddedFieldsForTableProjects : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NumberForContact",
                table: "projects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProjectLeaderName",
                table: "projects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberForContact",
                table: "projects");

            migrationBuilder.DropColumn(
                name: "ProjectLeaderName",
                table: "projects");
        }
    }
}
