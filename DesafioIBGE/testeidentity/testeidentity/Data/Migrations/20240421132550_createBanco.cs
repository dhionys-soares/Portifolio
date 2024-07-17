using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace testeidentity.Data.Migrations
{
    /// <inheritdoc />
    public partial class createBanco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "testeIbges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cidade = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_testeIbges", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "testeIbges");
        }
    }
}
