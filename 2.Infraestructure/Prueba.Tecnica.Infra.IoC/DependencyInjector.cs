using Prueba.Tecnica.Application.Interfaces;
using Prueba.Tecnica.Application.Services;
using Prueba.Tecnica.Domain.Interfaces.Repositories;
using Prueba.Tecnica.Domain.Interfaces.Services;
using Prueba.Tecnica.Domain.Services.Services;
using Prueba.Tecnica.Infra.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Prueba.Tecnica.Infra.IoC
{
    public class DependencyInjector
    {
        public DependencyInjector()
        {
        }

        public IServiceCollection GetServiceCollection()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddSingleton<ICacheRepository, CacheRepository>();
            services.AddSingleton<ITopSecretService, TopSecretService>();
            services.AddSingleton<IEntitytwoService, EntitytwoService>();
            services.AddSingleton<ITopSecretApplication, TopSecretApplication>();
            services.AddSingleton<IEntitytwoApplication, EntitytwoApplication>();

            return services;
        }
    }
}