using Prueba.Tecnica.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prueba.Tecnica.Domain.Interfaces.Services
{
    public interface IEntitytwoService
    {
        Task<string> Create();

        Task<GenericResponse> Open(string entitytwoId);

        Task<GenericResponse> Close(string entitytwoId);

        Task<List<Entitytwo>> Get();
    }
}