using System.ComponentModel.DataAnnotations;

namespace Prueba.Tecnica.Application.Contracts.DTO
{
    public class EntityoneDTO
    {
        [Required]
        public string EntityoneId { get; set; }

        public int? Number { get; set; }
        public string Color { get; set; }

        [Required]
        public decimal Quantity { get; set; }
    }
}