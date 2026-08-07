using NeoCore.Domain.Agregados;
using NeoCore.Domain.Excepciones;
using NeoCore.Domain.Repositorios;

namespace NeoCore.Infrastructure.OutputPoint.Database.SqlServer.Repositorios
{
    public class LibroContableRepositorio : ILibroContableRepositorio
    {
        private readonly IEventoAlmacenRepositorio _eventoAlmacenRepositorio;
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;

        public LibroContableRepositorio(
            IEventoAlmacenRepositorio eventoAlmacenRepositorio,
            IUnidadDeTrabajo unidadDeTrabajo)
        {
            _eventoAlmacenRepositorio = eventoAlmacenRepositorio;
            _unidadDeTrabajo = unidadDeTrabajo;
        }

        public async Task<LibroContableAgregado> CargarAsync(Guid idCuenta, CancellationToken cancellationToken)
        {
            var eventos = await _eventoAlmacenRepositorio.CargarAsync(idCuenta, cancellationToken);

            if (eventos == null || !eventos.Any())
            {
                throw new ExcepcionNoEncontrado($"No se encontró historial para la cuenta {idCuenta}");
            }


            var cuenta = new LibroContableAgregado(idCuenta);

            cuenta.CargarDesdeHistorial(eventos.OrderBy(e => e.Version));

            return cuenta;
        }

        public async Task GuardarAsync(LibroContableAgregado cuentaContable, CancellationToken cancellationToken)
        {
            var eventosNuevos = cuentaContable.ObtenerEventosNoConfirmados;

            if (!eventosNuevos.Any()) return;

            int versionEsperada = cuentaContable.Version - eventosNuevos.Count;

            await _eventoAlmacenRepositorio.AgregarAsync(
                cuentaContable.IdCuenta,
                eventosNuevos,
                versionEsperada,
                cancellationToken);

            _unidadDeTrabajo.RegistrarAgregado(cuentaContable);
        }
    }
}
