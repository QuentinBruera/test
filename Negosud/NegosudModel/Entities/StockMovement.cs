using NegosudModel.Dto;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NegosudModel.Entities
{
    public class StockMovement
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "La date est obligatoire.")]
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "La quantité est obligatoire.")]
        public required int Quantity { get; set; }

        // Relation 1 * avec l'entité Article
        [ForeignKey(nameof(Article))]
        public int ArticleId { get; set; }
        public Article? Article { get; set; }

        // Relation 1 * avec l'entité Reason
        [ForeignKey(nameof(Reason))]
        public int ReasonId { get; set; }
        public Reason? Reasons { get; set; }

        public StockMovementDto ToDto()
        {
            return new StockMovementDto
            {
                Id = this.Id,
                Date = this.Date,
                Quantity = this.Quantity,
                ArticleId = this.ArticleId,
                ReasonId = this.ReasonId
            };
        }
    }
}
