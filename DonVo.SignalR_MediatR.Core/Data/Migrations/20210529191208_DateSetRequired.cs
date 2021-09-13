using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DonVo.SignalR_MediatR.Core.Data.Migrations
{
    public partial class DateSetRequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateSet",
                table: "Grades",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateSet",
                table: "Grades");
        }
    }
}
