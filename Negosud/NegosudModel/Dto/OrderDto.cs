using NegosudModel.Entities;

namespace NegosudModel.Dto
{
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double TotalWithoutTaxes { get; set; }
        public double TotalWithTaxes { get; set; }
        public int StatusId { get; set; }

        public virtual Order ToEntity()
        {
            return new Order
            {
                Id = this.Id,
                Date = this.Date,
                TotalWithoutTaxes = this.TotalWithoutTaxes,
                StatusId = this.StatusId
            };
        }
    }
}
