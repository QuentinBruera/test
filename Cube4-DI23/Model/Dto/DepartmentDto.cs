using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dto
{
    public class DepartmentDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public Department ToEntity()
        {
            return new Department
            {
                Id = this.Id,
                Name = this.Name
            };
        }
    }
}
