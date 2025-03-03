using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dto
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public required string LastName { get; set; }
        public required string FirstName { get; set; }
        public required string Phone { get; set; }
        public required string MobilePhone { get; set; }
        public required string Email { get; set; }

        // We can expose either IDs or nested DTOs (depending on your needs)
        public int DepartmentId { get; set; }
        public int SiteId { get; set; }

        public Employee ToEntity()
        {
            return new Employee
            {
                Id = this.Id,
                LastName = this.LastName,
                FirstName = this.FirstName,
                Phone = this.Phone,
                MobilePhone = this.MobilePhone,
                Email = this.Email,
                DepartmentId = this.DepartmentId,
                SiteId = this.SiteId
            };
        }
    }
}
