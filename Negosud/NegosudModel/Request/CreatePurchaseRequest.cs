using NegosudModel.Dto;
using System.ComponentModel.DataAnnotations;

namespace NegosudModel.Request
{
    public class CreatePurchaseRequest
    {
        public required int SupplierId { get; set; }
        [Required(ErrorMessage = "The date is required.")]
        public required DateTime Date { get; set; }
        [Required]
        public required List<ArticleQuantity> ArticleQuantities { get; set; } = new();
    }

    public class ArticleQuantity
    {
        public required ArticleDto? Article { get; set; }
        public required int Quantity { get; set; }
    }
}