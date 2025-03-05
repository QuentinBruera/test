using System.ComponentModel.DataAnnotations.Schema;

namespace NegosudModel.Entities
{
    public class Reason : Badge
    {
        // Relation * 1 avec l'entité StockMovement
        [InverseProperty(nameof(StockMovement.Reasons))]
        public List<StockMovement> StockMovements { get; set; } = new List<StockMovement>();
    }
}
