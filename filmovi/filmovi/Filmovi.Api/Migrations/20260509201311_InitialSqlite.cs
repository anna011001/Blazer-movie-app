using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Filmovi.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialSqlite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Filmovi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Naziv = table.Column<string>(type: "TEXT", nullable: false),
                    SifraFilma = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Filmovi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Kina",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Naziv = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kina", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KinoFilmovi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    KinoId = table.Column<int>(type: "INTEGER", nullable: false),
                    FilmId = table.Column<int>(type: "INTEGER", nullable: false),
                    DatumProjekcije = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CijenaKarte = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KinoFilmovi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KinoFilmovi_Filmovi_FilmId",
                        column: x => x.FilmId,
                        principalTable: "Filmovi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KinoFilmovi_Kina_KinoId",
                        column: x => x.KinoId,
                        principalTable: "Kina",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KinoFilmovi_FilmId",
                table: "KinoFilmovi",
                column: "FilmId");

            migrationBuilder.CreateIndex(
                name: "IX_KinoFilmovi_KinoId",
                table: "KinoFilmovi",
                column: "KinoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KinoFilmovi");

            migrationBuilder.DropTable(
                name: "Filmovi");

            migrationBuilder.DropTable(
                name: "Kina");
        }
    }
}
