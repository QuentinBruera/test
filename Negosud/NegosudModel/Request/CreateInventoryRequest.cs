using System.ComponentModel.DataAnnotations;

namespace NegosudModel.Request
{
    public class CreateInventoryRequest
    {
        [Required(ErrorMessage = "The date is required.")]
        public required DateTime Date { get; set; }

        [Required(ErrorMessage = "The article inventories are required.")]
        public required List<ArticleInventoryRequest> ArticleInventories { get; set; } = new();
    }

    public class ArticleInventoryRequest
    {
        [Required(ErrorMessage = "The ArticleId is required.")]
        public required int ArticleId { get; set; }

        [Required(ErrorMessage = "The QuantityBefore is required.")]
        public required int QuantityBefore { get; set; }

        [Required(ErrorMessage = "The QuantityAfter is required.")]
        public required int QuantityAfter { get; set; }
    }
}
