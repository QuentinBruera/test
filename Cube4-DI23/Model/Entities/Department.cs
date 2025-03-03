using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
    public class Department
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }

        // Navigation property: a Department can have multiple Employees
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
