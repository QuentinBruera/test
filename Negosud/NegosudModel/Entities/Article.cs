using NegosudModel.Dto;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NegosudModel.Entities
{
    public class Article
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Le nom est obligatoire.")]
        [StringLength(100)]
        public required string Name { get; set; }
        [Required(ErrorMessage = "La TVA est obligatoire.")]
        public required double TVA { get; set; }
        [Required(ErrorMessage = "La description est obligatoire.")]
        [StringLength(500)]
        public required string Description { get; set; }
        [Required(ErrorMessage = "Le prix unitaire est obligatoire.")]
        public required double UnitPrice { get; set; }
        [Required(ErrorMessage = "La quantité est obligatoire.")]
        public required int Quantity { get; set; }
        [Required(ErrorMessage = "La quantité minimum est obligatoire.")]
        public required int MinimumQuantity { get; set; }
        [Required(ErrorMessage = "L'état actif de l'article est obligatoire.")]
        public required bool IsActive { get; set; }

        // Relation 1 * avec l'entité Supplier
        [ForeignKey(nameof(Supplier))]
        public int SupplierId { get; set; }
        public Supplier? Supplier { get; set; }

        // Relation 0..1 * avec l'entité Family
        [ForeignKey(nameof(Family))]
        public int FamilyId { get; set; }
        public Family? Family { get; set; }

        public ArticleDto ToDto()
        {
            return new ArticleDto
            {
                Id = this.Id,
                Name = this.Name,
                TVA = this.TVA,
                Description = this.Description,
                UnitPrice = this.UnitPrice,
                Quantity = this.Quantity,
                MinimumQuantity = this.MinimumQuantity,
                IsActive = this.IsActive,
                SupplierId = this.SupplierId,
                FamilyId= this.FamilyId
            };
        }
    }
}
