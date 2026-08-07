using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NeoCore.Infrastructure.OutputPoint.Database.SqlServer.Migraciones
{
    /// <inheritdoc />
    public partial class AgregarTablaTransferenciaEstado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TransferenciaEstado",
                schema: "ms-libro-mayor",
                columns: table => new
                {
                    CorrelationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EstadoActual = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    CuentaOrigenId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CuentaDestinoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Monto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SaldoBloqueado = table.Column<bool>(type: "bit", nullable: false),
                    DestinoAcreditado = table.Column<bool>(type: "bit", nullable: false),
                    OrigenLiquidado = table.Column<bool>(type: "bit", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MotivoRechazo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferenciaEstado", x => x.CorrelationId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransferenciaEstado",
                schema: "ms-libro-mayor");
        }
    }
}
