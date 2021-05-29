using Prueba.Tecnica.Application.Contracts.DTO;
using Prueba.Tecnica.Application.Interfaces;
using Prueba.Tecnica.Domain.Entities.Config;
using Prueba.Tecnica.Domain.Entities.Enums;
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
        [Route("topsecret/getTopSecretData")]
        public async Task<object> GetTopSecretData([FromBody] Satellites satellites)
        {
        //    if (!ValidateRequest(satellites))
        //        return BadRequest();
            var location = GetLocation(satellites);
            var message = GetMessage(satellites);
            object response;

            if (!location.Result.Success)
                response = location.Result;
            else
            {
                var shipdata = new ShipData()
                {
                    shipPosition = (ShipPosition)location.Result.Data,
                    message = message
                };
                response = shipdata;
            }

            


            return response;
        }

        [HttpPost]
        [Route("topsecret_split/{satellite_name}")]
        public async Task<object> GetTopSecretDataSplit([FromRoute] string satellitename, [FromBody] Satellite satellite)
        {
           



            return satellite;
        }

        //[HttpGet]
        //[Route("topsecret_split/getTopSecretDataSplit")]
        //public async Task<object> GetTopSecretDataSplit()
        //{




        //    return satellite;
        //}

        private async Task<GenericResponse> GetLocation(Satellites satellites)
        {
            return await topSecretApplication.GetLocation(satellites);
        }

        private  string GetMessage(Satellites satellites)
        {
            return  topSecretApplication.GetMessage(satellites);
        }

        //private bool ValidateRequest(Satellites satellites)
        //{
        //    if (!string.IsNullOrEmpty(satellites.))
        //        return false;
        //    if (!(satellites.distance == null))
        //        return false;
        //    return true;
        //}
    }
}