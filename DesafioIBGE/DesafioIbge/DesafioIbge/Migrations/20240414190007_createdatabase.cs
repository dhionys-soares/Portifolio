using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DesafioIbge.Migrations
{
    /// <inheritdoc />
    public partial class createdatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ibge",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", fixedLength: true, maxLength: 7, nullable: false),
                    State = table.Column<string>(type: "VARCHAR(2)", fixedLength: true, maxLength: 2, nullable: true),
                    City = table.Column<string>(type: "NVARCHAR(80)", maxLength: 80, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ibge", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "NVARCHAR(80)", maxLength: 80, nullable: false),
                    Email = table.Column<string>(type: "VARCHAR(160)", maxLength: 160, nullable: false),
                    Senha = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IBGE_City",
                table: "Ibge",
                column: "City");

            migrationBuilder.CreateIndex(
                name: "IX_IBGE_Id",
                table: "Ibge",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_IBGE_State",
                table: "Ibge",
                column: "State");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ibge");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
