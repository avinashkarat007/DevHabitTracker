using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevHabitTracker.Migrations
{
    /// <inheritdoc />
    public partial class AddHabitTagCompositeKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HabitId",
                table: "Tags",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "HabitTags",
                columns: table => new
                {
                    HabitId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TagId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedATUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HabitTags", x => new { x.HabitId, x.TagId });
                    table.ForeignKey(
                        name: "FK_HabitTags_Habits_HabitId",
                        column: x => x.HabitId,
                        principalTable: "Habits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HabitTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tags_HabitId",
                table: "Tags",
                column: "HabitId");

            migrationBuilder.CreateIndex(
                name: "IX_HabitTags_TagId",
                table: "HabitTags",
                column: "TagId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Habits_HabitId",
                table: "Tags",
                column: "HabitId",
                principalTable: "Habits",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Habits_HabitId",
                table: "Tags");

            migrationBuilder.DropTable(
                name: "HabitTags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_HabitId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "HabitId",
                table: "Tags");
        }
    }
}
