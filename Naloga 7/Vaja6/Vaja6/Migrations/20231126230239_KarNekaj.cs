using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vaja6.Migrations
{
    /// <inheritdoc />
    public partial class KarNekaj : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vozniki",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Ime = table.Column<string>(type: "TEXT", nullable: false),
                    Priimek = table.Column<string>(type: "TEXT", nullable: false),
                    Starost = table.Column<int>(type: "INTEGER", nullable: false),
                    ŠtevilkaFormule = table.Column<int>(type: "INTEGER", nullable: false),
                    ŠteviloZmag = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vozniki", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vozniki");
        }
    }
}
