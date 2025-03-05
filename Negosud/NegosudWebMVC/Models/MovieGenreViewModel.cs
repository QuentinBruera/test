using Microsoft.AspNetCore.Mvc.Rendering;
using NegosudWeb.Entities;

namespace NegosudWeb.Models
{
    public class MovieGenreViewModel
    {
        public List<Movie>? Movies { get; set; }
        public SelectList? Genres { get; set; }
        public string? MovieGenre { get; set; }
        public string? SearchString { get; set; }
        public SelectList? Tris { get; set; }
        public string? IndexTri { get; set; }
    }
}
