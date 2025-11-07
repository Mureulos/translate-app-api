using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace translate_app.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveLocalizedNameFromLanguage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LocalizedName",
                table: "Language");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LocalizedName",
                table: "Language",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
