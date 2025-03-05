using NegosudModel.Entities;

namespace NegosudModel.Dto
{
    public class PurchaseDto : OrderDto
    {
        public int SupplierId { get; set; }

        public override Purchase ToEntity()
        {
            return new Purchase
            {
                Id = this.Id,
                Date = this.Date,
                TotalWithoutTaxes = this.TotalWithoutTaxes,
                StatusId = this.StatusId,
                SupplierId = this.SupplierId
            };
        }
    }
}
