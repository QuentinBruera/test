using NegosudModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NegosudModel.Dto
{
    public class ReasonDto
    {
        public int Id { get; set; }
        public required string Name { get; set; } = string.Empty;
        public required string Color { get; set; } = string.Empty;

        public Reason ToEntity()
        {
            return new Reason
            {
                Id = this.Id,
                Name = this.Name,
                Color = this.Color
            };
        }  
    }
}
