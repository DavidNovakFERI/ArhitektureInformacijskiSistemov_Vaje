using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class Index : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Vozniki_ImePriimek",
                table: "Vozniki",
                column: "ImePriimek");

            migrationBuilder.CreateIndex(
                name: "IX_Ekipe_Drzava",
                table: "Ekipe",
                column: "Drzava");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Vozniki_ImePriimek",
                table: "Vozniki");

            migrationBuilder.DropIndex(
                name: "IX_Ekipe_Drzava",
                table: "Ekipe");
        }
    }
}
