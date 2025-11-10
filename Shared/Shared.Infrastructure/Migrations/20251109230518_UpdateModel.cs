using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shared.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            // Добавляем временный столбец
            migrationBuilder.AddColumn<int>(
                name: "RoleInt",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            // Переносим значения из string в int
            migrationBuilder.Sql(@"
        UPDATE ""Users"" SET ""RoleInt"" =
            CASE ""Role""
                WHEN 'Администратор' THEN 0
                WHEN 'Ученик' THEN 1
                WHEN 'Учитель' THEN 2
                ELSE 0
            END;
    ");

            // Удаляем старый string-столбец
            migrationBuilder.DropColumn(
                name: "Role",
                table: "Users");

            // Переименовываем временный столбец
            migrationBuilder.RenameColumn(
                name: "RoleInt",
                table: "Users",
                newName: "Role");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Добавляем временный string-столбец
            migrationBuilder.AddColumn<string>(
                name: "RoleStr",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            // Переносим значения из int в string
            migrationBuilder.Sql(@"
        UPDATE ""Users"" SET ""RoleStr"" =
            CASE ""Role""
                WHEN 0 THEN 'Администратор'
                WHEN 1 THEN 'Ученик'
                WHEN 2 THEN 'Учитель'
                ELSE 'Администратор'
            END;
    ");

            // Удаляем int-столбец
            migrationBuilder.DropColumn(
                name: "Role",
                table: "Users");

            // Переименовываем обратно
            migrationBuilder.RenameColumn(
                name: "RoleStr",
                table: "Users",
                newName: "Role");
        }
    }
}
