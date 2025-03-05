using NegosudModel.Entities;

namespace NegosudModel.Dto
{
    public class ArticleOrderDto
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double TVA { get; set; }
        public int ArticleId { get; set; }
        public int OrderId { get; set; }
        public int? PersonId { get; set; }

        public ArticleOrder ToEntity()
        {
            return new ArticleOrder
            {
                Id = this.Id,
                Quantity = this.Quantity,
                UnitPrice = this.UnitPrice,
                TVA = this.TVA,
                ArticleId = this.ArticleId,
                OrderId = this.OrderId,
                PersonId = this.PersonId
            };
        }
    }
}
