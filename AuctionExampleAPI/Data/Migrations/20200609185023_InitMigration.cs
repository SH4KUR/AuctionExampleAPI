using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace AuctionExampleAPI.Data.Migrations
{
    public partial class InitMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "item",
                columns: table => new
                {
                    item_id = table.Column<int>(nullable: false, defaultValueSql: "nextval('item_id_seq'::regclass)"),
                    name = table.Column<string>(maxLength: 30, nullable: false),
                    start_price = table.Column<int>(nullable: false),
                    current_price = table.Column<int>(nullable: false),
                    start_time = table.Column<DateTime>(nullable: false),
                    end_time = table.Column<DateTime>(nullable: false),
                    status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_item", x => x.item_id);
                });

            migrationBuilder.CreateTable(
                name: "rate",
                columns: table => new
                {
                    rate_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    item_id = table.Column<int>(nullable: false),
                    user_name = table.Column<string>(maxLength: 30, nullable: false),
                    rate_time = table.Column<DateTime>(nullable: false),
                    price = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rate", x => x.rate_id);
                    table.ForeignKey(
                        name: "rate_item_id_fkey",
                        column: x => x.item_id,
                        principalTable: "item",
                        principalColumn: "item_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_rate_item_id",
                table: "rate",
                column: "item_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "rate");

            migrationBuilder.DropTable(
                name: "item");
        }
    }
}
