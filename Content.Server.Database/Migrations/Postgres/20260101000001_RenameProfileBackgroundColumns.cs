using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Content.Server.Database.Migrations.Postgres
{
    /// <inheritdoc />
    public partial class RenameProfileBackgroundColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
DO $$
BEGIN
    IF EXISTS (SELECT 1 FROM information_schema.columns WHERE table_name = 'profile' AND column_name = 'employer') THEN
        ALTER TABLE profile RENAME COLUMN employer TO planet;
    END IF;

    IF EXISTS (SELECT 1 FROM information_schema.columns WHERE table_name = 'profile' AND column_name = 'lifepath') THEN
        ALTER TABLE profile RENAME COLUMN lifepath TO background;
    END IF;

    IF EXISTS (SELECT 1 FROM information_schema.columns WHERE table_name = 'profile' AND column_name = 'nationality') THEN
        ALTER TABLE profile RENAME COLUMN nationality TO citizenship;
    END IF;
END $$;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
DO $$
BEGIN
    IF EXISTS (SELECT 1 FROM information_schema.columns WHERE table_name = 'profile' AND column_name = 'planet') THEN
        ALTER TABLE profile RENAME COLUMN planet TO employer;
    END IF;

    IF EXISTS (SELECT 1 FROM information_schema.columns WHERE table_name = 'profile' AND column_name = 'background') THEN
        ALTER TABLE profile RENAME COLUMN background TO lifepath;
    END IF;

    IF EXISTS (SELECT 1 FROM information_schema.columns WHERE table_name = 'profile' AND column_name = 'citizenship') THEN
        ALTER TABLE profile RENAME COLUMN citizenship TO nationality;
    END IF;
END $$;");
        }
    }
}
