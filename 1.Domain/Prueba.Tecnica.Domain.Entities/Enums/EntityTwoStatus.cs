using System.ComponentModel.DataAnnotations;

namespace Prueba.Tecnica.Domain.Entities.Enums
{
    public enum EntityTwoStatus
    {
        [Display(Name = "Open", Description = "EntityTwo is Open")]
        Open = 0,

        [Display(Name = "Close", Description = "EntityTwo is Closed")]
        Close = 1,

        [Display(Name = "Pending", Description = "EntityTwo< is Pending")]
        Pending = 2
    }
}