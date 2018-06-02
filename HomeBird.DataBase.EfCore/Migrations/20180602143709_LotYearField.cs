using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HomeBird.DataBase.EfCore.Migrations
{
    public partial class LotYearField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "Lots",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Layings_IncubatorId",
                table: "Layings",
                column: "IncubatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Layings_Incubators_IncubatorId",
                table: "Layings",
                column: "IncubatorId",
                principalTable: "Incubators",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Layings_Incubators_IncubatorId",
                table: "Layings");

            migrationBuilder.DropIndex(
                name: "IX_Layings_IncubatorId",
                table: "Layings");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "Lots");
        }
    }
}
