using NegosudModel.Entities;

namespace NegosudModel.Dto
{
    public class ArticleInventoryDto
    {
        public int Id { get; set; }
        public int QuantityBefore { get; set; }
        public int QuantityAfter { get; set; }
        public int ArticleId { get; set; }
        public int InventoryId { get; set; }

        public ArticleInventory ToEntity()
        {
            return new ArticleInventory
            { 
                Id = this.Id,
                QuantityBefore = this.QuantityBefore,
                QuantityAfter = this.QuantityAfter,
                ArticleId = this.ArticleId,
                InventoryId = this.InventoryId
            };
        }
    }

}
