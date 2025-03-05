using NegosudModel.Entities;

namespace NegosudModel.Dto
{
    public class StatusDto
    {
        public int Id { get; set; }
        public required string Name { get; set; } = string.Empty;
        public required string Color { get; set; } = string.Empty;

        public Status ToEntity()
        {
            return new Status
            {
                Id = this.Id,
                Name = this.Name,
                Color = this.Color
            };
        }
    }
}
