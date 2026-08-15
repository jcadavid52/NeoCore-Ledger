using MassTransit;
using NeoCore.Infrastructure.EntryPoint.Mensajería.RabbitMQ.Consumidores.Comandos;
using NeoCore.Infrastructure.OutputPoint.Mensajeria.RabbitMQ.Estado;
using NeoCore.Infrastructure.OutputPoint.Mensajeria.RabbitMQ.TransferenciaEventos;
using NeoCore.SharedKernel.Transferencias;

namespace NeoCore.Infrastructure.OutputPoint.Mensajeria.RabbitMQ.MaquinasDeEstado
{
    public class TransferenciaEstadoMaquina: MassTransitStateMachine<TransferenciaEstado>
    {
        public State ValidandoRespuesta { get; private set; }
        public State BloqueandoSaldo { get; private set; }
        public State AcreditandoDestino { get; private set; }
        public State LiquidandoOrigen { get; private set; }
        public State Notificando { get; private set; }
        public State Compensando { get; private set; }
        public State Completado { get; private set; }
        public State Rechazado { get; private set; }
        public State Fallido { get; private set; }

        public Event<TransferenciaIniciada> TransferenciaIniciadaEvt { get; private set; }
        public Event<ValidacionAceptada> ValidacionAceptadaEvt { get; private set; }
        public Event<SaldoBloqueado> SaldoBloqueadoEvt { get; private set; }
        public Event<AcreditacionExitosa> AcreditacionExitosaEvt { get; private set; }
        public Event<LiquidacionExitosa> LiquidacionExitosaEvt { get; private set; }
        public Event<NotificacionCompleta> NotificacionCompletaEvt { get; private set; }

        public TransferenciaEstadoMaquina()
        {
            InstanceState(x => x.EstadoActual);

            Event(() => TransferenciaIniciadaEvt, x => x.CorrelateById(m => m.Message.IdTransferencia));
            Event(() => ValidacionAceptadaEvt, x => x.CorrelateById(m => m.Message.IdTransferencia));
            Event(() => SaldoBloqueadoEvt, x => x.CorrelateById(m => m.Message.TransferenciaId));
            Event(() => AcreditacionExitosaEvt, x => x.CorrelateById(m => m.Message.TransferenciaId));
            Event(() => LiquidacionExitosaEvt, x => x.CorrelateById(m => m.Message.TransferenciaId));
            Event(() => NotificacionCompletaEvt, x => x.CorrelateById(m => m.Message.TransferenciaId));

            Initially(
               When(TransferenciaIniciadaEvt)
                   .Then(ctx =>
                   {
                       ctx.Saga.CuentaOrigenId = ctx.Message.IdCuentaOrigen;
                       ctx.Saga.CuentaDestinoId = ctx.Message.IdCuentaDestino;
                       ctx.Saga.Monto = ctx.Message.Monto;
                       ctx.Saga.FechaInicio = DateTime.UtcNow;
                   })
                   .ThenAsync(async ctx =>
                   {
                       await ctx.Publish(new ValidacionIniciada
                       {
                           IdTransferencia = ctx.Saga.CorrelationId,
                           IdCuentaDestino = ctx.Saga.CuentaDestinoId
                       });
                   })
                   .TransitionTo(ValidandoRespuesta)
            );

            During(ValidandoRespuesta,
               When(ValidacionAceptadaEvt)
                   .PublishAsync(ctx => ctx.Init<BloquearSaldoComandoConsumidor>(new
                   {
                       IdCorrelacion = ctx.Saga.CorrelationId,
                       IdCuentaOrigen = ctx.Saga.CuentaOrigenId,
                       Monto = ctx.Saga.Monto,
                   }))
                   .TransitionTo(BloqueandoSaldo));

            During(BloqueandoSaldo,
                When(SaldoBloqueadoEvt)
                    .PublishAsync(ctx => ctx.Init<AcreditarSaldoComandoConsumidor>(new
                    {
                        IdCorrelacion = ctx.Saga.CorrelationId,
                        IdCuentaDestino = ctx.Saga.CuentaDestinoId,
                        Monto = ctx.Saga.Monto
                    }))
                    .TransitionTo(AcreditandoDestino));

            During(AcreditandoDestino,
                When(AcreditacionExitosaEvt)
                    .PublishAsync(ctx => ctx.Init<LiquidarSaldoComandoConsumidor>(new
                    {
                        IdCorrelacion = ctx.Saga.CorrelationId,
                        IdCuentaOrigen = ctx.Saga.CuentaOrigenId,
                        Monto = ctx.Saga.Monto
                    }))
                    .TransitionTo(LiquidandoOrigen));

            During(LiquidandoOrigen,
                When(LiquidacionExitosaEvt)
                    .Then(ctx =>
                    {
                        ctx.Saga.FechaFin = DateTime.UtcNow;
                    })
                    .TransitionTo(Notificando));
        }
    }
}
