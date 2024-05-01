using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BisleriumPvtLtdBackendSample1.Migrations
{
    /// <inheritdoc />
    public partial class change5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AddedDate",
                table: "BlogReactions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddedDate",
                table: "BlogReactions");
        }
    }
}
