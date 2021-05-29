using Prueba.Tecnica.Application.Interfaces;
using Prueba.Tecnica.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Prueba.Tecnica.WebApi.Controllers
{
    [ApiController]
    public class EntitytwoController : Controller
    {
        private readonly IEntitytwoApplication rouletteApplication;

        public EntitytwoController(IEntitytwoApplication rouletteApplication)
        {
            this.rouletteApplication = rouletteApplication;
        }

        [HttpPost]
        [Route("pruebaT/entitytwo/create")]
        public async Task<GenericResponse> Create()
        {
            return await rouletteApplication.Create();
        }

        [HttpGet]
        [Route("pruebaT/entitytwo/all")]
        public async Task<GenericResponse> Get()
        {
            return await rouletteApplication.Get();
        }

        [HttpPut]
        [Route("pruebaT/entitytwo/{entitytwoId}/open")]
        public async Task<GenericResponse> Open([FromRoute] string rouletteId)
        {
            return await rouletteApplication.Open(rouletteId);
        }

        [HttpPut]
        [Route("pruebaT/entitytwo/{entitytwoId}/close")]
        public async Task<GenericResponse> Close([FromRoute] string rouletteId)
        {
            return await rouletteApplication.Close(rouletteId);
        }
    }
}