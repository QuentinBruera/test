using NegosudModel.Dto;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NegosudModel.Entities
{
    public class Order
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "La date est obligatoire.")]
        public required DateTime Date { get; set; }
        [Required(ErrorMessage = "Le Total HT est obligatoire.")]
        public required double TotalWithoutTaxes { get; set; }

        // Relation 1 * avec l'entité Status
        [ForeignKey(nameof(Status))]
        public int StatusId { get; set; }
        public Status? Status { get; set; }

        public virtual OrderDto ToDto(double totalWithTaxes)
        {
            return new OrderDto
            {
                Id = this.Id,
                Date = this.Date,
                TotalWithoutTaxes = this.TotalWithoutTaxes,
                TotalWithTaxes = totalWithTaxes,
                StatusId = this.StatusId
            };
        }
    }
}
