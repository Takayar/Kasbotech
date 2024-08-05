using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kasbotech_RandCandle.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Candles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CandleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CandleOpen = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CandleClose = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CandleHigh = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CandleLow = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candles", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Candles");
        }
    }
}
