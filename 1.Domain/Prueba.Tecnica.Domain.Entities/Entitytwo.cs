using Prueba.Tecnica.Domain.Entities.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Prueba.Tecnica.Domain.Entities
{
    public class Entitytwo
    {
        [Required]
        public string Id { get; set; } = Guid.NewGuid().ToString("N");

        public string State { get; set; } = EntityTwoStatus.Pending.ToString();

        public DateTime CreationDate { get; set; } = DateTime.UtcNow;

        public DateTime? CloseDate { get; set; }
    }
}