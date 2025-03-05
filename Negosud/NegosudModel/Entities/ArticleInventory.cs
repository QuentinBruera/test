using NegosudModel.Dto;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NegosudModel.Entities
{
    public class ArticleInventory
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "La quantité d'avant est obligatoire.")]
        public required int QuantityBefore { get; set; }
        [Required(ErrorMessage = "La quantité d'après est obligatoire.")]
        public required int QuantityAfter { get; set; }

        // Relation 1 * avec l'entité Article
        [ForeignKey(nameof(Article))]
        public int ArticleId { get; set; }
        public Article? Article { get; set; }

        // Relation 1 * avec l'entité Inventory
        [ForeignKey(nameof(Inventory))]
        public int InventoryId { get; set; }
        public Inventory? Inventory { get; set; }

        public ArticleInventoryDto ToDto()
        {
            return new ArticleInventoryDto
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
