using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InformationPlatform.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddPostUsersRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Users_DbUserId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_DbUserId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "DbUserId",
                table: "Posts");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedById",
                table: "Posts",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Posts_CreatedById",
                table: "Posts",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Users_CreatedById",
                table: "Posts",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Users_CreatedById",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_CreatedById",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Posts");

            migrationBuilder.AddColumn<Guid>(
                name: "DbUserId",
                table: "Posts",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_DbUserId",
                table: "Posts",
                column: "DbUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Users_DbUserId",
                table: "Posts",
                column: "DbUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
