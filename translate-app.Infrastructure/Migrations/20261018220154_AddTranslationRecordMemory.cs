using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace translate_app.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTranslationRecordMemory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TranslationRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NormalizedSource = table.Column<string>(type: "character varying(600)", maxLength: 600, nullable: false),
                    TargetLanguage = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ContextTag = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    TranslatedText = table.Column<string>(type: "text", nullable: false),
                    IsHumanVerified = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TranslationRecords", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TranslationRecords_IsHumanVerified",
                table: "TranslationRecords",
                column: "IsHumanVerified");

            migrationBuilder.CreateIndex(
                name: "IX_TranslationRecords_NormalizedSource_TargetLanguage_ContextT~",
                table: "TranslationRecords",
                columns: new[] { "NormalizedSource", "TargetLanguage", "ContextTag" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TranslationRecords_TargetLanguage_ContextTag",
                table: "TranslationRecords",
                columns: new[] { "TargetLanguage", "ContextTag" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TranslationRecords");
        }
    }
}
