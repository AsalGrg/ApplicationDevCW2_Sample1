﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BisleriumPvtLtdBackendSample1.Migrations
{
    /// <inheritdoc />
    public partial class someChanges1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AddedDate",
                table: "CommentReactions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "NotificationCheckedTimings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LastCheckTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationCheckedTimings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotificationCheckedTimings_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NotificationCheckedTimings_UserId",
                table: "NotificationCheckedTimings",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotificationCheckedTimings");

            migrationBuilder.DropColumn(
                name: "AddedDate",
                table: "CommentReactions");
        }
    }
}
