using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace NeoCore.Infrastructure.OutputPoint.Database.SqlServer.Servicios
{
    public class BaseDatosMigracionServicio : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public BaseDatosMigracionServicio(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var alcance = _serviceProvider.CreateScope();
            var baseDatos = alcance.ServiceProvider.GetRequiredService<SqlServerContexto>();

            if (baseDatos.Database.IsRelational())
            {
                try
                {
                    await baseDatos.Database.MigrateAsync(cancellationToken);
                }
                catch (SqlException ex) when (ex.Number == 1801)
                {
                    await baseDatos.Database.MigrateAsync(cancellationToken);
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
