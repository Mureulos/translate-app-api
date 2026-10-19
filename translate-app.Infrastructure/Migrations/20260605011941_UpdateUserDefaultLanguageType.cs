using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace translate_app.Infrastructure.Migrations
{
    public partial class UpdateUserDefaultLanguageType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE \"Users\" SET \"DefaultLanguage\" = '0';");
            migrationBuilder.Sql("ALTER TABLE \"Users\" ALTER COLUMN \"DefaultLanguage\" TYPE integer USING \"DefaultLanguage\"::integer;");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("ALTER TABLE \"Users\" ALTER COLUMN \"DefaultLanguage\" TYPE text USING \"DefaultLanguage\"::text;");
        }
    }
}