using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBTest3.Migrations
{
    public partial class TicketStatusNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tickets_ticketStatuses_StatusId",
                table: "tickets");

            migrationBuilder.AlterColumn<long>(
                name: "StatusId",
                table: "tickets",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_tickets_ticketStatuses_StatusId",
                table: "tickets",
                column: "StatusId",
                principalTable: "ticketStatuses",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tickets_ticketStatuses_StatusId",
                table: "tickets");

            migrationBuilder.AlterColumn<long>(
                name: "StatusId",
                table: "tickets",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_tickets_ticketStatuses_StatusId",
                table: "tickets",
                column: "StatusId",
                principalTable: "ticketStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
