using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Subasta.core.interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Subasta.core.jobs
{
    public class CerrarLotesSinPujasService : IHostedService
    {
        private Timer _timer;
        readonly IServiceProvider services;
        public CerrarLotesSinPujasService(IServiceProvider services)
        {
            this.services = services;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero,
            TimeSpan.FromMinutes(30));

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            try
            {
                using (var scope = services.CreateScope())
                {
                    var pujaService = scope.ServiceProvider.GetRequiredService<IPujaService>();
                    pujaService.ActualizarLotesHuerfanos();
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
