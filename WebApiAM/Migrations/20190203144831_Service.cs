using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiAM.Migrations
{
    public partial class Service : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Fk_service",
                table: "Events",
                nullable: false,
                defaultValue: 0);
            

            migrationBuilder.CreateTable(
                name: "Service",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Lat = table.Column<decimal>(type: "decimal(10, 6)", nullable: false),
                    Lng = table.Column<decimal>(type: "decimal(10, 6)", nullable: false),
                    Title = table.Column<string>(type: "varchar(100)", nullable: true),
                    DatePlace = table.Column<DateTime>(nullable: false),
                    ImageUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Service", x => x.Id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {            
            migrationBuilder.DropTable(
                name: "Service");

            migrationBuilder.DropIndex(
                name: "IX_Events_Fk_service",
                table: "Events");

            
            migrationBuilder.DropColumn(
                name: "Fk_service",
                table: "Events");            
        }
    }
}
