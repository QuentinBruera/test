using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required  string LastName { get; set; }

        [Required]
        public required string FirstName { get; set; }

        public required string Phone { get; set; }
        public required string MobilePhone { get; set; }
        public required string Email { get; set; }

        // Foreign key to Department
        public int DepartmentId { get; set; }
        public Department? Department { get; set; }

        // Foreign key to Site
        public int SiteId { get; set; }
        public Site? Site { get; set; }
    }
}
