using NegosudModel.Entities;

namespace NegosudModel.Dto
{
    public class SupplierDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
        public string? LandlineNumber { get; set; }
        public string? CellPhoneNumber { get; set; }

        public Supplier ToEntity()
        {
            return new Supplier
            {
                Id = this.Id,
                Name = this.Name,
                Address = this.Address,
                City = this.City,
                ZipCode = this.ZipCode,
                LandlineNumber = this.LandlineNumber,
                CellPhoneNumber = this.CellPhoneNumber
            };
        }
    }
}
