using System.ComponentModel.DataAnnotations;

namespace NegosudModel.Entities
{
    public class Person
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Le nom est obligatoire.")]
        [StringLength(100)]
        public required string Name { get; set; }
        [Required(ErrorMessage = "L'adresse est obligatoire.")]
        [StringLength(255)]
        public required string Address { get; set; }
        [Required(ErrorMessage = "La ville est obligatoire.")]
        [StringLength(100)]
        public required string City { get; set; }
        [Required(ErrorMessage = "Le code postal est obligatoire.")]
        [StringLength(100)]
        public required string ZipCode { get; set; }
        [StringLength(50)]
        public string? LandlineNumber { get; set; }
        [StringLength(50)]
        public string? CellPhoneNumber { get; set; }
    }
}
