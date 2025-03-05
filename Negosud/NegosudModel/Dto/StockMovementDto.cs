using NegosudModel.Entities;

namespace NegosudModel.Dto
{
    public class StockMovementDto
    {
        public int? Id { get; set; }
        public DateTime Date { get; set; }
        public int Quantity { get; set; }
        public int ArticleId { get; set; }
        public int ReasonId { get; set; }
        public ReasonDto Reason { get; set; }

        public StockMovement ToEntity(StockMovement stockMovement)
        {
            return new StockMovement
            {
                Id = (int)this.Id,
                Date = this.Date,
                Quantity = this.Quantity,
                ArticleId = this.ArticleId,
                ReasonId = this.ReasonId
            };
        }
    }
}
