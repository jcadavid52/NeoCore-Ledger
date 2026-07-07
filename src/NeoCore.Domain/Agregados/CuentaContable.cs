using NeoCore.Domain.Eventos;
using NeoCore.Domain.Excepciones;
using NeoCore.Domain.Interfaces;

namespace NeoCore.Domain.Agregados
{
    public class CuentaContable
    {
        private readonly List<IEventoDominio> _eventosDominio = new();

        public Guid IdCuenta { get; private set; }
        public int Version { get; private set; } = -1;
        public decimal Balance { get; private set; }

        public CuentaContable(Guid idCuenta)
        {
            IdCuenta = idCuenta;
        }

        public void Retirar(decimal monto)
        {
            if (monto <= 0)
                throw new DominioExcepcion("Monto inválido");

            if (Balance < monto)
                throw new DominioExcepcion("Saldos insuficientes");

            var evento = new RetirarDinero(monto, IdCuenta);

            LevantarEvento(evento);
        }

        public void CargarDesdeHistorial(IEnumerable<IEventoDominio> eventos)
        {
            foreach (var evento in eventos)
            {
                Aplicar(evento);
                Version = evento.Version;
            }
        }

        private void Aplicar(IEventoDominio evento)
        {
            switch (evento)
            {
                case RetirarDinero e:
                    Balance -= e.Monto;
                    break;
                case DepositarDinero e:
                    Balance += e.Monto;
                    break;
            }
        }

        private void LevantarEvento(IEventoDominio evento)
        {
            AplicarEvento(evento);

            _eventosDominio.Add(evento);

            Version++;
        }

        private void AplicarEvento(IEventoDominio evento)
        {
            switch (evento)
            {
                case RetirarDinero retirarDinero:
                    Aplicar(retirarDinero);
                    break;
                case DepositarDinero depositarDinero:
                    Aplicar(depositarDinero);
                    break;
            }
        }

        public IReadOnlyCollection<IEventoDominio> ObtenerEventosNoConfirmados()
            => _eventosDominio.AsReadOnly();

        public void LimpiarEventosNoConfirmados()
            => _eventosDominio.Clear();
    }
}
