using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HomeBird.DataBase.EfCore.Migrations
{
    public partial class TransferSomeFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "Layings");

            migrationBuilder.DropColumn(
                name: "EggPrice",
                table: "Layings");

            migrationBuilder.AddColumn<decimal>(
                name: "EggPrice",
                table: "Lots",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreationDate",
                table: "Layings",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LayingDate",
                table: "Layings",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EggPrice",
                table: "Lots");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Layings");

            migrationBuilder.DropColumn(
                name: "LayingDate",
                table: "Layings");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "Layings",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "EggPrice",
                table: "Layings",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
