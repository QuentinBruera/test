using NegosudModel.Entities;

namespace NegosudModel.Dto
{
    public class FamilyDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<Article>? Articles { get; set; }

        public Family ToEntity()
        {
            return new Family
            {
                Id = this.Id,
                Name = this.Name,
                Articles = this.Articles
            };
        }
    }

}
