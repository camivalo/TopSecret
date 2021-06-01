using Prueba.Tecnica.Application.Interfaces;
using Prueba.Tecnica.Domain.Entities.Config;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System;
using Prueba.Tecnica.Domain.Entities;

namespace Prueba.Tecnica.WebApi.Controllers
{
    [ApiController]
    public class TopSecretController : Controller
    {
        private readonly ITopSecretApplication topSecretApplication;
        private readonly AppSettings appSettings;

        public TopSecretController(ITopSecretApplication topSecretApplication, IOptions<AppSettings> appSettings)
        {
            this.topSecretApplication = topSecretApplication;
            this.appSettings = appSettings.Value;
        }

        



        [HttpPost]
        [Route("topsecret")]
        public async Task<object> TopSecretData([FromBody] Satellites satellites)
        {
            var location = GetLocation(satellites);
            var message = GetMessage(satellites);
            object response;
            
            if (!location.Result.Success)
                response = location.Result;
            else if(!message.Result.Success)
                response = message.Result;
            else
            {
                var shipdata = new ShipData()
                {
                    shipPosition = (ShipPosition)location.Result.Data,
                    message = (ShipMessage)message.Result.Data
                };
                response = shipdata;
            }

            return response;
        }

        
        

        //[HttpGet]
        //[Route("topsecret_split/getTopSecretDataSplit")]
        //public async Task<object> GetTopSecretDataSplit()
        //{




        //    return satellite;
        //}

        private async Task<GenericResponse> GetLocation(Satellites satellites, string satellite_name = null)
        {
            return await topSecretApplication.GetLocation(satellites);
        }

        private async Task<GenericResponse> GetMessage(Satellites satellites)
        {
            return await topSecretApplication.GetMessage(satellites);
        }


    }
}