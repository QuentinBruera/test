using NegosudModel.Dto;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NegosudModel.Entities
{
    public class ArticleOrder
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "La quantité est obligatoire.")]
        public required int Quantity { get; set; }
        [Required(ErrorMessage = "Le prix unitaire est obligatoire.")]
        public required double UnitPrice { get; set; }
        public required double TVA { get; set; }

        // Relation 1 * avec l'entité Article
        [ForeignKey(nameof(Article))]
        public int ArticleId { get; set; }
        public Article? Article { get; set; }

        // Relation 1 * avec l'entité Order
        [ForeignKey(nameof(Order))]
        public int OrderId { get; set; }
        public Order? Order { get; set; }

        [ForeignKey(nameof(Person))]
        public int? PersonId { get; set; }
        public Person? Person { get; set; }

        public ArticleOrderDto ToDto()
        {
            return new ArticleOrderDto
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

        public double GetTotalWithoutTaxes()
        {
            return this.UnitPrice * this.Quantity;
        }

        public double GetTotalWithTaxes()
        {
            return GetUnitPriceWithTaxes() * this.Quantity;
        }

        public double GetTaxesCost()
        {
            return this.UnitPrice * this.TVA;
        }

        public double GetUnitPriceWithTaxes()
        {
            return this.UnitPrice + GetTaxesCost();
        }
    }
}
