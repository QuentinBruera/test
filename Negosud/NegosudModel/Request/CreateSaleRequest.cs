using System.ComponentModel.DataAnnotations;

namespace NegosudModel.Request
{
    public class CreateSaleRequest
    {
        public required int CustomerId { get; set; }
        [Required(ErrorMessage = "The date is required.")]
        public required DateTime Date { get; set; }
        [Required]
        public required List<ArticleQuantity> ArticleQuantities { get; set; } = new();
    }
}
