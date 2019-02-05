using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiAM.Migrations
{
    public partial class ChangesEvent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Service_Fk_service",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_Fk_service",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Fk_service",
                table: "Events");

            migrationBuilder.AddColumn<int>(
                name: "ServiceId",
                table: "Events",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Events_ServiceId",
                table: "Events",
                column: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Service_ServiceId",
                table: "Events",
                column: "ServiceId",
                principalTable: "Service",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Service_ServiceId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_ServiceId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "Events");

            migrationBuilder.AddColumn<int>(
                name: "Fk_service",
                table: "Events",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Events_Fk_service",
                table: "Events",
                column: "Fk_service");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Service_Fk_service",
                table: "Events",
                column: "Fk_service",
                principalTable: "Service",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
