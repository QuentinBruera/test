using NegosudModel.Dto;
using System.ComponentModel.DataAnnotations.Schema;

namespace NegosudModel.Entities
{
    public class Supplier : Person
    {
        public SupplierDto ToDto()
        {
            return new SupplierDto
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
