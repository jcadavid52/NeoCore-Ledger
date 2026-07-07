using NeoCore.Domain.Agregados;
using NeoCore.Domain.Repositorios;

namespace NeoCore.Infrastructure.OutputPoint.Database.SqlServer.Repositorios
{
    public class CuentaContableRepositorio : ICuentaContableRepositorio
    {
        private readonly IEventoAlmacenRepositorio _eventoAlmacenRepositorio;

        public CuentaContableRepositorio(IEventoAlmacenRepositorio eventoAlmacenRepositorio)
        {
            _eventoAlmacenRepositorio = eventoAlmacenRepositorio;
        }

        public async Task<CuentaContable> CargarAsync(Guid idCuenta, CancellationToken cancellationToken)
        {
            var eventos = await _eventoAlmacenRepositorio.CargarAsync(idCuenta, cancellationToken);

            if (eventos == null || !eventos.Any())
            {
                throw new Exception($"No se encontró historial para la cuenta {idCuenta}");
            }

            var cuenta = new CuentaContable(idCuenta);

            cuenta.CargarDesdeHistorial(eventos.OrderBy(e => e.Version));

            return cuenta;
        }

        public async Task GuardarAsync(CuentaContable cuentaContable, CancellationToken cancellationToken)
        {
            var eventosNuevos = cuentaContable.ObtenerEventosNoConfirmados();

            if (!eventosNuevos.Any()) return;

            int versionEsperada = cuentaContable.Version - eventosNuevos.Count;

            await _eventoAlmacenRepositorio.AgregarAsync(
                cuentaContable.IdCuenta,
                eventosNuevos,
                versionEsperada,
                cancellationToken);

            cuentaContable.LimpiarEventosNoConfirmados();

            // Despachamos los eventos hacia el bus para que los proyectores actualicen los Read Models (Saldos)
        }
    }
}
