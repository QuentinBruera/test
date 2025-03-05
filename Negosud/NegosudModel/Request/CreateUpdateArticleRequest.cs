using System.ComponentModel.DataAnnotations;

namespace NegosudModel.Request
{
    public class CreateUpdateArticleRequest
    {
        [Required(ErrorMessage = "Le nom est obligatoire.")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "La TVA est obligatoire.")]
        public required double TVA { get; set; }

        [Required(ErrorMessage = "La description est obligatoire.")]
        public required string Description { get; set; }

        [Required(ErrorMessage = "Le prix unitaire est obligatoire.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Le prix unitaire doit être supérieur à 0.")]
        public required double UnitPrice { get; set; }

        [Required(ErrorMessage = "La quantité est obligatoire.")]
        [Range(0, int.MaxValue, ErrorMessage = "La quantité ne peut pas être négative.")]
        public required int Quantity { get; set; }

        [Required(ErrorMessage = "La quantité minimum est obligatoire.")]
        [Range(0, int.MaxValue, ErrorMessage = "La quantité minimum ne peut pas être négative.")]
        public required int MinimumQuantity { get; set; }

        [Required(ErrorMessage = "L'état actif de l'article est obligatoire.")]
        public required bool IsActive { get; set; }

        [Required(ErrorMessage = "L'identifiant du fournisseur est obligatoire.")]
        public required int SupplierId { get; set; }

        [Required(ErrorMessage = "L'identifiant de la famille est obligatoire.")]
        public required int FamilyId { get; set; }
    }
}
