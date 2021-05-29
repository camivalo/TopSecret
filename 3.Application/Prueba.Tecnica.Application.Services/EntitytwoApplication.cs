using Prueba.Tecnica.Application.Interfaces;
using Prueba.Tecnica.Domain.Entities;
using Prueba.Tecnica.Domain.Interfaces.Services;
using Prueba.Tecnica.Domain.Services.Utilities;
using System.Threading.Tasks;

namespace Prueba.Tecnica.Application.Services
{
    public class EntitytwoApplication : IEntitytwoApplication
    {
        private readonly IEntitytwoService rouletteService;

        public EntitytwoApplication(IEntitytwoService rouletteService)
        {
            this.rouletteService = rouletteService;
        }

        public async Task<GenericResponse> Close(string rouletteId)
        {
            return await rouletteService.Close(rouletteId);
        }

        public async Task<GenericResponse> Create()
        {
            var rouletteId = await rouletteService.Create();
            return Helper.ManageResponse(rouletteId);
        }

        public async Task<GenericResponse> Get()
        {
            var rouletteList = await rouletteService.Get();
            return Helper.ManageResponse(rouletteList);
        }

        public async Task<GenericResponse> Open(string rouletteId)
        {
            return await rouletteService.Open(rouletteId);
        }
    }
}