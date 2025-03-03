using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dto
{
    public class SiteDto
    {
        public int Id { get; set; }
        public string City { get; set; } = null!;

        public Site ToEntity()
        {
            return new Site
            {
                Id = this.Id,
                City = this.City
            };
        }
    }
}
