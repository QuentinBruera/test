using NegosudModel.Dto;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NegosudModel.Entities
{
    public class Family
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Le nom est obligatoire.")]
        [StringLength(100)]
        public required string Name { get; set; }

        // Relation * 1 avec l'entité Article
        [InverseProperty(nameof(Article.Family))]
        public List<Article>? Articles { get; set; }

        public FamilyDto ToDto()
        {
            return new FamilyDto
            {
                Id = this.Id,
                Name = this.Name,
                Articles = this.Articles
            };   
        }
    }
}
