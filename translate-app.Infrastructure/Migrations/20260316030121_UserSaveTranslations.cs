using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace translate_app.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UserSaveTranslations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Translation",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Translation_UserId",
                table: "Translation",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Translation_Users_UserId",
                table: "Translation",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Translation_Users_UserId",
                table: "Translation");

            migrationBuilder.DropIndex(
                name: "IX_Translation_UserId",
                table: "Translation");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Translation",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");
        }
    }
}
