using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Minibox.Core.Data.Database.Main.MigrationHistory
{
    /// <inheritdoc />
    public partial class AddLanguage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "lang");

            migrationBuilder.CreateTable(
                name: "Language",
                schema: "lang",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Language", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LanguageKey",
                schema: "lang",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    DefaultValue = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanguageKey", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LanguageTranslation",
                schema: "lang",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LanguageKeyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanguageTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LanguageTranslation_LanguageKey_LanguageKeyId",
                        column: x => x.LanguageKeyId,
                        principalSchema: "lang",
                        principalTable: "LanguageKey",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LanguageKey_Key",
                schema: "lang",
                table: "LanguageKey",
                column: "Key");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageTranslation_LanguageKeyId",
                schema: "lang",
                table: "LanguageTranslation",
                column: "LanguageKeyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Language",
                schema: "lang");

            migrationBuilder.DropTable(
                name: "LanguageTranslation",
                schema: "lang");

            migrationBuilder.DropTable(
                name: "LanguageKey",
                schema: "lang");
        }
    }
}
