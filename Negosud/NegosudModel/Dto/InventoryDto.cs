using NegosudModel.Entities;
using System.ComponentModel.DataAnnotations;

namespace NegosudModel.Dto
{
    public class InventoryDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }

        public Inventory ToEntity()
        {
            return new Inventory
            {
                Id = this.Id,
                Date = this.Date
            };
        }
    }
}
