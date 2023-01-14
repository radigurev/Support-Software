using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBTest3.Migrations
{
    public partial class RemovedLocationAsEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_locations_IdLocation",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "locations");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_IdLocation",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IdLocation",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdLocation",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_locations", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_IdLocation",
                table: "AspNetUsers",
                column: "IdLocation");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_locations_IdLocation",
                table: "AspNetUsers",
                column: "IdLocation",
                principalTable: "locations",
                principalColumn: "Id");
        }
    }
}
