using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NegosudModel.Entities
{
    public class Customer : Person
    {
        [Required(ErrorMessage = "Le prénom est obligatoire.")]
        [StringLength(100)]
        public required string FirstName { get; set; }
        [Required(ErrorMessage = "La date de naissance est obligatoire.")]
        public required DateOnly DateOfBirth { get; set; }
    }
}
