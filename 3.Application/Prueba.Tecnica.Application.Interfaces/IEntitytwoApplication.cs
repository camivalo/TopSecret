using Prueba.Tecnica.Domain.Entities;
using System.Threading.Tasks;

namespace Prueba.Tecnica.Application.Interfaces
{
    public interface IEntitytwoApplication
    {
        Task<GenericResponse> Create();

        Task<GenericResponse> Open(string entitytwoId);

        Task<GenericResponse> Close(string entitytwoId);

        Task<GenericResponse> Get();
    }
}