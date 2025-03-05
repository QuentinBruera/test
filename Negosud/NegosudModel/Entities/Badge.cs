using System.ComponentModel.DataAnnotations;

namespace NegosudModel.Entities
{
    public class Badge
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Le nom est obligatoire.")]
        [StringLength(100)]
        public required string Name { get; set; }
        [Required(ErrorMessage = "La couleur est obligatoire.")]
        [StringLength(10)]
        public required string Color { get; set; }

    }
}
