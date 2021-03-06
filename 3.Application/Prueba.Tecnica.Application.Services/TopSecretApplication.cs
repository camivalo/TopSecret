using Prueba.Tecnica.Application.Interfaces;
using Prueba.Tecnica.Domain.Entities;
using Prueba.Tecnica.Domain.Interfaces.Services;
using System.Threading.Tasks;

namespace Prueba.Tecnica.Application.Services
{
    public class TopSecretApplication : ITopSecretApplication
    {
        private readonly ITopSecretService topSecretService;

        public TopSecretApplication(ITopSecretService topSecretService)
        {
            this.topSecretService = topSecretService;
        }

        public async Task<GenericResponse> GetLocation(Satellites satellites)
        {
            return await topSecretService.GetLocation(satellites);
        }

        public async Task<GenericResponse> GetMessage(Satellites satellites)
        {
            return await topSecretService.GetMessage(satellites);
        }
    }
}