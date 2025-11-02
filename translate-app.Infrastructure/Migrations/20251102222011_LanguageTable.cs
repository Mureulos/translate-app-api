using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace translate_app.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class LanguageTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Language",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocalizedName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Language", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Translation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Translation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SourceLanguageId = table.Column<int>(type: "int", nullable: true),
                    TargetLanguageId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Translation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Translation_Language_SourceLanguageId",
                        column: x => x.SourceLanguageId,
                        principalTable: "Language",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Translation_Language_TargetLanguageId",
                        column: x => x.TargetLanguageId,
                        principalTable: "Language",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Translation_SourceLanguageId",
                table: "Translation",
                column: "SourceLanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Translation_TargetLanguageId",
                table: "Translation",
                column: "TargetLanguageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Translation");

            migrationBuilder.DropTable(
                name: "Language");
        }
    }
}
