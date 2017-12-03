using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HomeBird.DataBase.EfCore.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Incubators",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Title = table.Column<string>(maxLength: 1024, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incubators", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lots",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AvgAdultPrice = table.Column<decimal>(nullable: true),
                    AvgDailyPrice = table.Column<decimal>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    Identifier = table.Column<string>(maxLength: 1024, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Loses = table.Column<int>(nullable: true),
                    Profit = table.Column<decimal>(nullable: true),
                    SoldCount = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lots", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Broods",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BroodDate = table.Column<DateTimeOffset>(nullable: false),
                    Count = table.Column<int>(nullable: false),
                    CreationDate = table.Column<DateTimeOffset>(nullable: false),
                    DeadCount = table.Column<int>(nullable: false),
                    DeadPercent = table.Column<decimal>(nullable: false),
                    EmptyCount = table.Column<int>(nullable: false),
                    EmptyPercent = table.Column<decimal>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LotId = table.Column<int>(nullable: false),
                    Percent = table.Column<decimal>(nullable: false),
                    PlacePrice = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Broods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Broods_Lots_LotId",
                        column: x => x.LotId,
                        principalTable: "Lots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Layings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Count = table.Column<int>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    EggPrice = table.Column<decimal>(nullable: false),
                    IncubatorId = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LotId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Layings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Layings_Lots_LotId",
                        column: x => x.LotId,
                        principalTable: "Lots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Overheads",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<decimal>(nullable: false),
                    Comment = table.Column<string>(maxLength: 2048, nullable: true),
                    CreationDate = table.Column<DateTimeOffset>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LotId = table.Column<int>(nullable: false),
                    OverheadDate = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Overheads", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Overheads_Lots_LotId",
                        column: x => x.LotId,
                        principalTable: "Lots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Purchases",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(maxLength: 2048, nullable: true),
                    Amount = table.Column<decimal>(nullable: false),
                    Count = table.Column<int>(nullable: false),
                    CreationDate = table.Column<DateTimeOffset>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LotId = table.Column<int>(nullable: false),
                    PurchaseDate = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Purchases_Lots_LotId",
                        column: x => x.LotId,
                        principalTable: "Lots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<decimal>(nullable: false),
                    Buyer = table.Column<string>(maxLength: 2048, nullable: true),
                    Comment = table.Column<string>(maxLength: 2048, nullable: true),
                    Count = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LotId = table.Column<int>(nullable: false),
                    SaleDate = table.Column<DateTimeOffset>(nullable: false),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sales_Lots_LotId",
                        column: x => x.LotId,
                        principalTable: "Lots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Broods_LotId",
                table: "Broods",
                column: "LotId");

            migrationBuilder.CreateIndex(
                name: "IX_Layings_LotId",
                table: "Layings",
                column: "LotId");

            migrationBuilder.CreateIndex(
                name: "IX_Overheads_LotId",
                table: "Overheads",
                column: "LotId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_LotId",
                table: "Purchases",
                column: "LotId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_LotId",
                table: "Sales",
                column: "LotId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Broods");

            migrationBuilder.DropTable(
                name: "Incubators");

            migrationBuilder.DropTable(
                name: "Layings");

            migrationBuilder.DropTable(
                name: "Overheads");

            migrationBuilder.DropTable(
                name: "Purchases");

            migrationBuilder.DropTable(
                name: "Sales");

            migrationBuilder.DropTable(
                name: "Lots");
        }
    }
}
