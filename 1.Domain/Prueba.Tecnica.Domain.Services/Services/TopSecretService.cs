using Prueba.Tecnica.Domain.Entities;
using Prueba.Tecnica.Domain.Entities.Config;
using Prueba.Tecnica.Domain.Interfaces.Repositories;
using Prueba.Tecnica.Domain.Interfaces.Services;
using Prueba.Tecnica.Domain.Services.Utilities;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using Numpy;

namespace Prueba.Tecnica.Domain.Services.Services
{
    public class TopSecretService : ITopSecretService
    {
        private readonly ICacheRepository cacheRepository;
        private readonly AppSettings appSettings;

        public TopSecretService(ICacheRepository cacheRepository, IOptions<AppSettings> appSettings)
        {
            this.cacheRepository = cacheRepository;
            this.appSettings = appSettings.Value;
        }

       

        public async Task<GenericResponse> GetLocation(Satellites satellites)
        {

            double distanceKenobi = 0;
            double latitudeKenobi = 0;
            double lengthKenobi = 0;

            double distanceSkywalker = 0;
            double latitudeSkywalker = 0;
            double lengthSkywalker = 0;

            double distanceSato = 0;
            double latitudeSato = 0;
            double lengthSato = 0;

            double distanceKenobiSkywalker = 0;

            double latitudeCargoShip = 0;
            double lengthCargoShip = 0;


            foreach (Satellite satellite in satellites.satellites)
            {

                if (satellite.name == "kenobi")
                {
                    distanceKenobi = satellite.distance;
                    latitudeKenobi = appSettings.LatitudeKenobi;
                    lengthKenobi = appSettings.LengthKenobi;
                }
                else if (satellite.name == "skywalker")
                {
                    distanceSkywalker = satellite.distance;
                    latitudeSkywalker = appSettings.LatitudeSkywalker;
                    lengthSkywalker = appSettings.LengthSkywalker;
                }
                else
                {
                    distanceSato = satellite.distance;
                    latitudeSato = appSettings.LatitudeSato;
                    lengthSato = appSettings.LengthSato;
                }



            }

            // Triangular scale validation
            //distanceKenobiSkywalker = Math.Sqrt(Math.Pow((latitudeSkywalker - latitudeKenobi), 2) + Math.Pow((lengthSkywalker - lengthKenobi), 2));

            //if (distanceKenobiSkywalker >= (distanceKenobi + distanceSkywalker) || distanceKenobiSkywalker <= Math.Abs(distanceKenobi - distanceSkywalker))
            //{
            //    ErrorResponse error = null;
            //    error = new ErrorResponse("404", Constants.SHIP_POSITION_NOT_DETERMINED);
            //    return Helper.ManageResponse(error, false);

            //}

            // Trilateration process

            double calculation1 = ((Math.Pow(distanceSkywalker, 2)) + ((Math.Pow(latitudeSkywalker, 2)) * (-1)) + ((Math.Pow(lengthSkywalker, 2)) * (-1)))
                            - ((Math.Pow(distanceKenobi, 2)) + ((Math.Pow(latitudeKenobi, 2)) * (-1)) + ((Math.Pow(lengthKenobi, 2)) * (-1)))
                            / (latitudeSkywalker * 2 - latitudeKenobi * 2) * (-1);
            double v12 = ((lengthSkywalker * 2 - lengthKenobi * 2) * (-1) / (latitudeSkywalker * 2 - latitudeKenobi * 2) * (-1)) * (-1);
            double numeroSoloC = Math.Pow(calculation1, 2) + latitudeKenobi * 2 * (-1) * calculation1 + (((Math.Pow(distanceKenobi, 2)) + ((Math.Pow(latitudeKenobi, 2)) * (-1)) + ((Math.Pow(lengthKenobi, 2)) * (-1))) * (-1));
            double numerosConYB = (2 * v12 * calculation1) + latitudeKenobi * 2 * (-1) * v12 + 2 * lengthKenobi * (-1);
            double numerosConYCuadradoA = Math.Pow(v12, 2) + 1;

            double y1 = (((-1) * (numerosConYB) - (Math.Sqrt(Math.Pow(numerosConYB, 2) - (4 * numerosConYCuadradoA * numeroSoloC)))) / (2 * numerosConYCuadradoA));
            double y2 = (((-1) * (numerosConYB) + (Math.Sqrt(Math.Pow(numerosConYB, 2) - (4 * numerosConYCuadradoA * numeroSoloC)))) / (2 * numerosConYCuadradoA));

            // When y1
            var x1 = calculation1 + v12 * y1;

            // When y2
            var x2 = calculation1 + v12 * y2;

            double resp1 = Math.Pow(x1, 2) - (2 * x1 * latitudeSato) + Math.Pow(latitudeSato, 2) + (Math.Pow(y1, 2)) - (2 * y1 * lengthSato) + Math.Pow(lengthSato, 2);
            double resp2 = Math.Pow(x2, 2) - (2 * x2 * latitudeSato) + Math.Pow(latitudeSato, 2) + (Math.Pow(y2, 2)) - (2 * y2 * lengthSato) + Math.Pow(lengthSato, 2);

            List<double> list = new List<double> { resp1, resp2 };
            double number = Math.Pow(distanceSato, 2);

            double closest = list.Aggregate((x, y) => Math.Abs(x - number) < Math.Abs(y - number) ? x : y);

            if (closest == resp1)
            {
                latitudeCargoShip = x1;
                lengthCargoShip = y1;
            }
            else
            {
                latitudeCargoShip = x2;
                lengthCargoShip = y2;
            }
            //if (Double.IsNaN(latitudeCargoShip) || Double.IsNaN(latitudeCargoShip))
            //{
            //    ErrorResponse error = null;
            //    error = new ErrorResponse("404", Constants.SHIP_POSITION_NOT_DETERMINED);
            //    return Helper.ManageResponse(error, false);
            //}

            var shipPosition = new ShipPosition()
            {
                shipPositionX = latitudeCargoShip,
                shipPositionY = lengthCargoShip
            };


            return Helper.ManageResponse(shipPosition , true);
        }



        public async Task<GenericResponse> GetMessage(Satellites satellites)
        {
            string spaceShipMessage = "";
            var message = new List<string>();

            foreach (Satellite satellite in satellites.satellites)
            {
                var cont = 0;
                foreach (string finalMessage in satellite.message)
                {
                    if (message.Count == cont)
                    {
                        message.Add(finalMessage);
                    }
                        
                    if (!string.IsNullOrWhiteSpace(finalMessage))
                        message[cont] = finalMessage;

                    cont++;
                }
            }

            foreach (string m in message)
            {
                if (string.IsNullOrWhiteSpace(m))
                    return Helper.ManageResponse(Constants.MESSAGE_INCOMPLETE, false);
                spaceShipMessage = spaceShipMessage + m + " ";
            }

            var shipMessage = new ShipMessage()
            {
                message = spaceShipMessage
            };

            return Helper.ManageResponse(shipMessage, true);
        }

        public async Task<Satellite> SatelliteData(Satellite satellite)
        {
            

           

            

            return satellite;
        }



    }
}