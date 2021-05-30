using Prueba.Tecnica.Application.Contracts.DTO;
using Prueba.Tecnica.Domain.Entities;
using System.Threading.Tasks;

namespace Prueba.Tecnica.Application.Interfaces
{
    public interface ITopSecretApplication
    {
        Task<GenericResponse> GetLocation(Satellites satellites);
        Task<GenericResponse> GetMessage(Satellites satellites);
    }
}