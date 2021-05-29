using Prueba.Tecnica.Application.Contracts.DTO;
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

        public  string GetMessage(Satellites satellites)
        {
            return topSecretService.GetMessage(satellites);
        }
    }
}