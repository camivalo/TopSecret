using System;
using System.Collections.Generic;

namespace Prueba.Tecnica.Domain.Entities
{
    public class EntityoneResult
    {
        public string RouletteId { get; set; }
        public List<Entityone> Bets { get; set; }
        public int? WinnerNumber { get; set; }
        public string WinnerColor { get; set; }
        public bool HasWinner { get; set; }
        public decimal TotalBetWinners { get; set; }
        public DateTime ClosedDate { get; set; }
    }
}