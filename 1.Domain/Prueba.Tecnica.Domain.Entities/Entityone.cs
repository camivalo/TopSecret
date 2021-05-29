using System;

namespace Prueba.Tecnica.Domain.Entities
{
    public class Entityone
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("N");
        public string EntityoneId { get; set; }
        public string UserId { get; set; }
        public int? Number { get; set; }
        public string Color { get; set; }
        public decimal Quantity { get; set; }
        public decimal TotalBetWinner { get; set; }
    }
}