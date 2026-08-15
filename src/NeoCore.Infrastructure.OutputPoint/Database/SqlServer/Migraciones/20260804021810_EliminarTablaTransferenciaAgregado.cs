using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NeoCore.Infrastructure.OutputPoint.Database.SqlServer.Migraciones
{
    /// <inheritdoc />
    public partial class EliminarTablaTransferenciaAgregado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransferenciaAgregado",
                schema: "ms-libro-mayor");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TransferenciaAgregado",
                schema: "ms-libro-mayor",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaResolucion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IdCuentaDestino = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdCuentaOrigen = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Monto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RazonRechazo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferenciaAgregado", x => x.Id);
                });
        }
    }
}
