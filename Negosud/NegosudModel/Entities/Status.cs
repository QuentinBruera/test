using NegosudModel.Dto;
using System.ComponentModel.DataAnnotations.Schema;

namespace NegosudModel.Entities
{
    public class Status : Badge 
    {
        public StatusDto ToDto()
        {
            return new StatusDto
            {
                Id = this.Id,
                Name = this.Name,
                Color = this.Color
            };
        }
    }
}
