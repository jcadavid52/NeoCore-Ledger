using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NeoCore.Infrastructure.OutputPoint.Database.SqlServer.Migraciones
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ms-libro-mayor");

            migrationBuilder.CreateTable(
                name: "Eventos",
                schema: "ms-libro-mayor",
                columns: table => new
                {
                    IdEvento = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TipoMensaje = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Dato = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdAgregado = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false),
                    OcurrioEn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eventos", x => x.IdEvento);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Eventos_Agregado_Version",
                schema: "ms-libro-mayor",
                table: "Eventos",
                columns: new[] { "IdAgregado", "Version" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Eventos_OcurrioEn",
                schema: "ms-libro-mayor",
                table: "Eventos",
                column: "OcurrioEn");

            migrationBuilder.CreateIndex(
                name: "IX_Eventos_TipoMensaje",
                schema: "ms-libro-mayor",
                table: "Eventos",
                column: "TipoMensaje");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Eventos",
                schema: "ms-libro-mayor");
        }
    }
}
