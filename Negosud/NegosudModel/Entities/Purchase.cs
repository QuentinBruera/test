using NegosudModel.Dto;
using System.ComponentModel.DataAnnotations.Schema;

namespace NegosudModel.Entities
{
    public class Purchase : Order
    {
        // Relation 1 * avec l'entité Supplier
        [ForeignKey(nameof(Supplier))]
        public int SupplierId { get; set; }
        public Supplier? Supplier { get; set; }

        public override PurchaseDto ToDto(double totalWithTaxes)
        {
            return new PurchaseDto
            {
                Id = this.Id,
                Date = this.Date,
                TotalWithoutTaxes = this.TotalWithoutTaxes,
                TotalWithTaxes = totalWithTaxes,
                StatusId = this.StatusId,
                SupplierId = this.SupplierId
            };
        }
    }
}
