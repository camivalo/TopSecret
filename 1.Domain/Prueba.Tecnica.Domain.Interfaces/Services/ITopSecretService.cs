using Prueba.Tecnica.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prueba.Tecnica.Domain.Interfaces.Services
{
    public interface ITopSecretService
    {
        Task<GenericResponse> GetLocation(Satellites satellites);

        string GetMessage(Satellites satellites);

    }
}