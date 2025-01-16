using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class PrvaKreacija : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ekipe",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Ime = table.Column<string>(type: "TEXT", nullable: false),
                    Drzava = table.Column<string>(type: "TEXT", nullable: false),
                    letoUstanovitve = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ekipe", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vozniki",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ImePriimek = table.Column<string>(type: "TEXT", nullable: false),
                    StevilkaAvta = table.Column<int>(type: "INTEGER", nullable: false),
                    letoRojstva = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vozniki", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VoznikVEkipi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    VoznikId = table.Column<int>(type: "INTEGER", nullable: false),
                    EkipaId = table.Column<int>(type: "INTEGER", nullable: false),
                    letoVstopa = table.Column<int>(type: "INTEGER", nullable: false),
                    letoIzpada = table.Column<int>(type: "INTEGER", nullable: false),
                    steviloDirk = table.Column<int>(type: "INTEGER", nullable: false),
                    steviloZmag = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoznikVEkipi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VoznikVEkipi_Ekipe_EkipaId",
                        column: x => x.EkipaId,
                        principalTable: "Ekipe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VoznikVEkipi_Vozniki_VoznikId",
                        column: x => x.VoznikId,
                        principalTable: "Vozniki",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VoznikVEkipi_EkipaId",
                table: "VoznikVEkipi",
                column: "EkipaId");

            migrationBuilder.CreateIndex(
                name: "IX_VoznikVEkipi_VoznikId",
                table: "VoznikVEkipi",
                column: "VoznikId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VoznikVEkipi");

            migrationBuilder.DropTable(
                name: "Ekipe");

            migrationBuilder.DropTable(
                name: "Vozniki");
        }
    }
}
