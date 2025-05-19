using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZWalksApi.Migrations
{
    /// <inheritdoc />
    public partial class AddingImagesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_walks_difficulties_difficultyid",
                table: "walks");

            migrationBuilder.RenameColumn(
                name: "difficultyid",
                table: "walks",
                newName: "difficultyId");

            migrationBuilder.RenameIndex(
                name: "IX_walks_difficultyid",
                table: "walks",
                newName: "IX_walks_difficultyId");

            migrationBuilder.CreateTable(
                name: "image",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileExtension = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileSizeInBytes = table.Column<long>(type: "bigint", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_image", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_walks_difficulties_difficultyId",
                table: "walks",
                column: "difficultyId",
                principalTable: "difficulties",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_walks_difficulties_difficultyId",
                table: "walks");

            migrationBuilder.DropTable(
                name: "image");

            migrationBuilder.RenameColumn(
                name: "difficultyId",
                table: "walks",
                newName: "difficultyid");

            migrationBuilder.RenameIndex(
                name: "IX_walks_difficultyId",
                table: "walks",
                newName: "IX_walks_difficultyid");

            migrationBuilder.AddForeignKey(
                name: "FK_walks_difficulties_difficultyid",
                table: "walks",
                column: "difficultyid",
                principalTable: "difficulties",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
