using NeoCore.Infrastructure.OutputPoint.Database.SqlServer.Entidades;

namespace NeoCore.Infrastructure.OutputPoint.Database
{
    public static class SeedDatabase
    {
        private static readonly List<EventoAlmacenEntidad> _eventos = new()
        {
            new EventoAlmacenEntidad
            {
                Dato = "{\"Monto\": 150.00, \"IdCuenta\": \"c4770d84-e0e0-49a7-9dab-db9e4ae7bb80\"}",
                IdAgregado = Guid.Parse("c4770d84-e0e0-49a7-9dab-db9e4ae7bb80"),
                TipoMensaje = "RetirarDinero",
                Version = 1
            },
            new EventoAlmacenEntidad
            {
                Dato = "{\"Monto\": 50.00, \"IdCuenta\": \"c4770d84-e0e0-49a7-9dab-db9e4ae7bb80\"}",
                IdAgregado = Guid.Parse("c4770d84-e0e0-49a7-9dab-db9e4ae7bb80"),
                TipoMensaje = "RetirarDinero",
                Version = 2
            },
            new EventoAlmacenEntidad
            {
                Dato = "{\"Monto\": 300.00, \"IdCuentaDestino\": \"c4770d84-e0e0-49a7-9dab-db9e4ae7bb80\",\"IdCuentaOrigen\":\"48eda670-3188-4f8d-be16-f68cc77ecacd\"}",
                IdAgregado = Guid.Parse("c4770d84-e0e0-49a7-9dab-db9e4ae7bb80"),
                TipoMensaje = "DepositarDinero",
                Version = 3
            }
        };

        public static IEnumerable<EventoAlmacenEntidad> ObtenerEventos() => _eventos;

        public static void AgregarEvento(EventoAlmacenEntidad evento)
        {
            _eventos.Add(evento);
        }
    }
}
