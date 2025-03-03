using Model.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
    public class Site
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required string City { get; set; }

        // Navigation property : un Site peut avoir plusieurs Salariés
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();

        public SiteDto ToDto()
        {
            return new SiteDto
            {
                Id = this.Id,
                City = this.City
            };
        }
    }
}
