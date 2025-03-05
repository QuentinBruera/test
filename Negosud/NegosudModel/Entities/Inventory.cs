using NegosudModel.Dto;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NegosudModel.Entities
{
    public class Inventory
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "La date est obligatoire.")]
        public DateTime Date { get; set; }

        public InventoryDto ToDto()
        {
            return new InventoryDto
            {
                Id = this.Id,
                Date = this.Date
            };
        }
    }
}
