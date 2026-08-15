using NeoCore.Domain.Abstracciones;
using NeoCore.Domain.EventosDominio.LibroContable;
using NeoCore.Domain.Excepciones;
using NeoCore.Domain.Interfaces;
using NeoCore.SharedKernel.LibroContable;

namespace NeoCore.Domain.Agregados
{
    public class LibroContableAgregado : AgregadoBase
    {
        public Guid IdCuenta { get; private set; }
        public int Version { get; private set; } = -1;
        public decimal Balance { get; private set; }
        public decimal SaldoDisponible { get; private set; }
        public decimal SaldoBloqueado { get; private set; }

        public LibroContableAgregado(Guid idCuenta)
        {
            IdCuenta = idCuenta;
        }

        public void Retirar(decimal monto, Guid idCorrelacion)
        {
            if (monto <= 0)
                throw new DominioExcepcion("Monto inválido");

            if (Balance < monto)
                throw new DominioExcepcion("Saldos insuficientes");

            var evento = new DebitarDinero(monto, IdCuenta, idCorrelacion);

            LevantarEvento(evento);
        }

        public void BloquearSaldo(
            decimal monto,
            Guid idCorrelacion,
            TipoCorrelacionEnum tipoCorrelacion)
        {
            if (monto <= 0)
                throw new DominioExcepcion("Monto inválido");

            if (SaldoDisponible < monto)
                throw new DominioExcepcion("Saldo disponible insuficiente");

            var evento = new BloquearSaldo(monto, IdCuenta, idCorrelacion, tipoCorrelacion);

            LevantarEvento(evento);
        }

        public void Acreditar(
            decimal monto,
            Guid idCorrelacion,
            TipoCorrelacionEnum tipoCorrelacion)
        {
            if (monto <= 0)
                throw new DominioExcepcion("Monto inválido");

            var evento = new AcreditarDinero(
                monto,
                IdCuenta,
                idCorrelacion,
                tipoCorrelacion);

            LevantarEvento(evento);
        }

        public void LiquidarBloqueo(decimal monto, Guid idCorrelacion)
        {
            if (monto <= 0)
                throw new DominioExcepcion("Monto inválido");

            if (SaldoBloqueado < monto)
                throw new DominioExcepcion("No hay suficiente saldo bloqueado");

            var evento = new DebitarDinero(monto, IdCuenta, idCorrelacion);

            LevantarEvento(evento);
        }

        public void LiberarBloqueo(
            decimal monto,
            Guid idCorrelacion,
            TipoCorrelacionEnum tipoCorrelacion)
        {
            if (monto <= 0)
                throw new DominioExcepcion("Monto inválido");

            if (SaldoBloqueado < monto)
                throw new DominioExcepcion("No hay suficiente saldo bloqueado");

            var evento = new LiberarBloqueo(
                monto,
                IdCuenta,
                idCorrelacion,
                tipoCorrelacion);

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
                case BloquearSaldo e:
                    SaldoBloqueado += e.Monto;
                    SaldoDisponible -= e.Monto;
                    break;
                case AcreditarDinero e:
                    Balance += e.Monto;
                    SaldoDisponible += e.Monto;
                    break;
                case DebitarDinero e:
                    Balance -= e.Monto;
                    SaldoBloqueado -= e.Monto;
                    break;
                case LiberarBloqueo e:
                    SaldoBloqueado -= e.Monto;
                    SaldoDisponible += e.Monto;
                    break;
            }
        }

        private void LevantarEvento(IEventoDominio evento)
        {
            AplicarEvento(evento);

            AgregarEvento(evento);

            Version++;
        }

        private void AplicarEvento(IEventoDominio evento)
        {
            switch (evento)
            {
                case BloquearSaldo saldoBloqueado:
                    Aplicar(saldoBloqueado);
                    break;
                case AcreditarDinero dineroAcreditado:
                    Aplicar(dineroAcreditado);
                    break;
                case DebitarDinero dineroDebitado:
                    Aplicar(dineroDebitado);
                    break;
                case LiberarBloqueo bloqueoLiberado:
                    Aplicar(bloqueoLiberado);
                    break;
            }
        }
    }
}
