using NegosudModel.Entities;

namespace NegosudModel.Dto
{
    public class SaleDto : OrderDto
    {
        public int CustomerId { get; set; }

        public override Order ToEntity()
        {
            return new Sale
            { 
                Id = this.Id,
                Date = this.Date,
                TotalWithoutTaxes = this.TotalWithoutTaxes,
                StatusId = this.StatusId,
                CustomerId = this.CustomerId
            };
        }
    }
}
