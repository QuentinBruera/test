using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NegosudWeb.Entities
{
    public class Movie
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Le titre est requis")]
        [Display(Name = "Titre")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "Le titre doit être compris entre 3 et 60 caractères")]
        public required string Title { get; set; }

        [Display(Name = "Date de sortie")]
        [DataType(DataType.Date)]
        public DateTime? ReleaseDate { get; set; }

        [Required(ErrorMessage = "Le genre est requis")]
        [Display(Name = "Genre")]
        [StringLength(40, ErrorMessage = "Le genre doit être inférieur à 40 caractères")]
        [RegularExpression(@"^[A-ZÀ-Ÿ]+[a-zA-ZÀ-ÿ\s]*$")]
        public required string Genre { get; set; }

        [Required(ErrorMessage = "Le prix est requis")]
        [Display(Name = "Prix")]
        [Range(1, 100, ErrorMessage = "Le prix doit être compris entre 1 et 100")]
        [DataType(DataType.Currency)]
        public required decimal Price { get; set; }

        [Display(Name = "Note")]
        [Range(0, 5, ErrorMessage = "La note doit être compris entre 0 et 5")]
        public int? Rating { get; set; }
    }
}
