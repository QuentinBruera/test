using System.ComponentModel.DataAnnotations;

namespace NegosudModel.Request
{
    public class CreateUpdateCustomerRequest
    {
        [Required(ErrorMessage = "Le nom est obligatoire.")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Le prénom est obligatoire.")]
        public required string FirstName { get; set; }

        [Required(ErrorMessage = "La date de naissance est obligatoire.")]
        public required DateOnly DateOfBirth { get; set; }

        [Required(ErrorMessage = "L'adresse est obligatoire.")]
        public required string Address { get; set; }

        [Required(ErrorMessage = "La ville est obligatoire.")]
        public required string City { get; set; }

        [Required(ErrorMessage = "Le code postal est obligatoire.")]
        [StringLength(5, ErrorMessage = "Le code postal doit contenir 5 chiffres.")]
        public required string ZipCode { get; set; }

        public string? LandlineNumber { get; set; }
        public string? CellPhoneNumber { get; set; }
    }
}
