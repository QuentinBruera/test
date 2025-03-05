using NegosudModel.Entities;

namespace NegosudModel.Dto
{
    public class ArticleDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required double TVA { get; set; }
        public required string Description { get; set; }
        public required double UnitPrice { get; set; }
        public required int Quantity { get; set; }
        public required int MinimumQuantity { get; set; }
        public required bool IsActive { get; set; }
        public int SupplierId { get; set; }
        public int FamilyId { get; set; }

        public Article ToEntity()
        {
            return new Article
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
                FamilyId = this.FamilyId
            };
        }
    }
}
