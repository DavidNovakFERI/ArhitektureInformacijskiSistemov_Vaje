using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
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
                    Naziv = table.Column<string>(type: "TEXT", nullable: false),
                    Drzava = table.Column<string>(type: "TEXT", nullable: false),
                    LetoUstanovitve = table.Column<int>(type: "INTEGER", nullable: false)
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
                    Ime = table.Column<string>(type: "TEXT", nullable: false),
                    Priimek = table.Column<string>(type: "TEXT", nullable: false),
                    LetoRojstva = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vozniki", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VoznikiVEkipi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    VoznikId = table.Column<int>(type: "INTEGER", nullable: false),
                    EkipaId = table.Column<int>(type: "INTEGER", nullable: false),
                    LetoOd = table.Column<int>(type: "INTEGER", nullable: false),
                    LetoDo = table.Column<int>(type: "INTEGER", nullable: false),
                    SteviloZmag = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoznikiVEkipi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VoznikiVEkipi_Ekipe_EkipaId",
                        column: x => x.EkipaId,
                        principalTable: "Ekipe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VoznikiVEkipi_Vozniki_VoznikId",
                        column: x => x.VoznikId,
                        principalTable: "Vozniki",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VoznikiVEkipi_EkipaId",
                table: "VoznikiVEkipi",
                column: "EkipaId");

            migrationBuilder.CreateIndex(
                name: "IX_VoznikiVEkipi_VoznikId",
                table: "VoznikiVEkipi",
                column: "VoznikId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VoznikiVEkipi");

            migrationBuilder.DropTable(
                name: "Ekipe");

            migrationBuilder.DropTable(
                name: "Vozniki");
        }
    }
}
