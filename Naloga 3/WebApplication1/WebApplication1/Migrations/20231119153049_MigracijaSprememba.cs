using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class MigracijaSprememba : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StevilkaAvta",
                table: "Vozniki",
                newName: "Stevilka");

            migrationBuilder.RenameColumn(
                name: "letoUstanovitve",
                table: "Ekipe",
                newName: "Leto");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Stevilka",
                table: "Vozniki",
                newName: "StevilkaAvta");

            migrationBuilder.RenameColumn(
                name: "Leto",
                table: "Ekipe",
                newName: "letoUstanovitve");
        }
    }
}
