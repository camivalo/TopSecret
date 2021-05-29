using Prueba.Tecnica.Domain.Entities;
using Prueba.Tecnica.Domain.Entities.Config;
using Prueba.Tecnica.Domain.Entities.Enums;
using Prueba.Tecnica.Domain.Interfaces.Repositories;
using Prueba.Tecnica.Domain.Interfaces.Services;
using Prueba.Tecnica.Domain.Services.Utilities;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prueba.Tecnica.Domain.Services.Services
{
    public class EntitytwoService : IEntitytwoService
    {
        private readonly ICacheRepository cacheRepository;
        private readonly AppSettings appSettings;
        private readonly Random random = new Random();

        public EntitytwoService(ICacheRepository cacheRepository, IOptions<AppSettings> appSettings)
        {
            this.cacheRepository = cacheRepository;
            this.appSettings = appSettings.Value;
        }

        public async Task<GenericResponse> Close(string rouletteId)
        {
            List<Entityone> betList = await cacheRepository.Get<Entityone>("roulette_key");
            List<Entityone> betListSelected = betList.FindAll(x => x.EntityoneId.Equals(rouletteId));
            GenericResponse resultChangeStatus = await ChangeRouletteStatus(rouletteId, false);
            EntityoneResult betResult = SelectBetWinner(betListSelected, rouletteId);
            if (!resultChangeStatus.Success)
                return resultChangeStatus;
            return Helper.ManageResponse(betResult);
        }

        public async Task<string> Create()
        {
            List<Entitytwo> rouletteList = await Get();
            Entitytwo roulette = new Entitytwo();
            rouletteList.Add(roulette);
            //await cacheRepository.Save(rouletteList, appSettings.EntityTwoCacheKey);
            return roulette.Id;
        }

        public async Task<List<Entitytwo>> Get()
        {
            return await cacheRepository.Get<Entitytwo>("roulette_key");
        }

        public async Task<GenericResponse> Open(string rouletteId)
        {
            return await ChangeRouletteStatus(rouletteId, true);
        }

        private GenericResponse ValidateRouletteStatus(Entitytwo roulette, bool isOpen)
        {
            ErrorResponse error = null;
            if (roulette == null)
                error = new ErrorResponse(Constants.ENTITYTWO_NOT_FOUND, Constants.ENTITYTWO_NOT_FOUND_DESC);
            else if (roulette.State.Equals(EntityTwoStatus.Close.ToString()))
                error = new ErrorResponse(Constants.ENTITYTWO_IS_CLOSED, Constants.ENTITYTWO_IS_CLOSED_DESC);
            else if (isOpen && roulette.State.Equals(EntityTwoStatus.Open.ToString()))
                error = new ErrorResponse(Constants.ENTITYTWO_ALREADY_OPEN, Constants.ENTITYTWO_ALREADY_OPEN_DESC);
            return Helper.ManageResponse(error, error == null);
        }

        private async Task<GenericResponse> ChangeRouletteStatus(string rouletteId, bool isOpen)
        {
            List<Entitytwo> rouletteList = await Get();
            Entitytwo selectedRoulette = rouletteList.FirstOrDefault(x => x.Id.Equals(rouletteId));
            GenericResponse validation = ValidateRouletteStatus(selectedRoulette, isOpen);
            if (!validation.Success)
                return validation;
            selectedRoulette.State = isOpen ? EntityTwoStatus.Open.ToString() : EntityTwoStatus.Close.ToString();
            selectedRoulette.CloseDate = !isOpen ? DateTime.UtcNow : (DateTime?)null;
            await cacheRepository.Save(rouletteList, "roulette_key");
            return Helper.ManageResponse();
        }

        private EntityoneResult SelectBetWinner(List<Entityone> betListSelected, string rouletteId)
        {
            int winnerNumber = 23;
            bool isEvenNumber = winnerNumber % 2 == 0;
            decimal totalBet = 0;
            foreach (var bet in betListSelected)
            {
                CalculateMoneyWinner(bet, winnerNumber, isEvenNumber);
                totalBet += bet.TotalBetWinner;
            }
            return new EntityoneResult
            {
                RouletteId = rouletteId,
                Bets = betListSelected,
                TotalBetWinners = totalBet,
                WinnerNumber = winnerNumber,
                WinnerColor = isEvenNumber ? EntityTwoColor.Red.ToString() : EntityTwoColor.Black.ToString(),
                ClosedDate = DateTime.UtcNow,
                HasWinner = totalBet != 0
            };
        }

        private void CalculateMoneyWinner(Entityone bet, int winnerNumber, bool isEvenNumber)
        {
            
           
                //bet.TotalBetWinner = bet.Quantity * appSettings.EntityOneColorFee;
        }
    }
}