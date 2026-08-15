using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NeoCore.Infrastructure.OutputPoint.Database.SqlServer.Migraciones
{
    /// <inheritdoc />
    public partial class AgregarIdempotencia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Idempotencias",
                schema: "ms-libro-mayor",
                columns: table => new
                {
                    IdClave = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NombreComando = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    CreadoEn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Idempotencias", x => x.IdClave);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Idempotencias",
                schema: "ms-libro-mayor");
        }
    }
}
