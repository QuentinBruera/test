using NegosudModel.Entities;

namespace NegosudModel.Dto
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string FullName => $"{Name} {FirstName}";
        public DateOnly DateOfBirth { get; set; }
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
        public string? LandlineNumber { get; set; }
        public string? CellPhoneNumber { get; set; }


        public Customer ToEntity()
        {
            return new Customer
            {
                Id = this.Id,
                Name = this.Name,
                FirstName = this.FirstName,
                DateOfBirth = this.DateOfBirth,
                Address = this.Address,
                City = this.City,
                ZipCode = this.ZipCode,
                LandlineNumber = this.LandlineNumber,
                CellPhoneNumber = this.CellPhoneNumber
            };
        }
    }
}
