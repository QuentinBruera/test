using NegosudModel.Dto;
using System.ComponentModel.DataAnnotations.Schema;

namespace NegosudModel.Entities
{
    public class Sale : Order
    {
        // Relation 1 * avec l'entité Customer
        [ForeignKey(nameof(Customer))]
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }

        public override SaleDto ToDto(double totalWithTaxes)
        {
            return new SaleDto
            {
                Id = this.Id,
                Date = this.Date,
                TotalWithoutTaxes = this.TotalWithoutTaxes,
                TotalWithTaxes = totalWithTaxes,
                StatusId = this.StatusId,
                CustomerId = this.CustomerId
            };
        }
    }
}
