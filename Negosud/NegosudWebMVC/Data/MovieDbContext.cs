using Microsoft.EntityFrameworkCore;
using NegosudWeb.Entities;

namespace NegosudWebMVC.Data
{
    public class MovieDbContext : DbContext
    {
        public MovieDbContext (DbContextOptions<MovieDbContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movie { get; set; } = default!;
    }
}
