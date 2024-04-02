using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fitee_backend.Migrations
{
    /// <inheritdoc />
    public partial class addpace : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "pace",
                table: "running",
                type: "interval",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "pace",
                table: "running");
        }
    }
}
