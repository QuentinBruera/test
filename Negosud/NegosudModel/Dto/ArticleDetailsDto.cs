using NegosudModel.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NegosudModel.Dto
{
    public class ArticleDetailsDto : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required double TVA { get; set; }
        public required string Description { get; set; }
        public required double UnitPrice { get; set; }
        //public required int Quantity { get; set; }
        private int _quantity;
        public int Quantity
        {
            get => _quantity;
            set
            {
                if (_quantity != value)
                {
                    _quantity = value;
                    OnPropertyChanged();
                }
            }
        }

        public required int MinimumQuantity { get; set; }
        public required bool IsActive { get; set; }
        public int SupplierId { get; set; }
        public Supplier? Supplier { get; set; }
        public int FamilyId { get; set; }
        public Family? Family { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Article ToEntity()
        {
            return new Article
            {
                Id = this.Id,
                Name = this.Name,
                TVA = this.TVA,
                Description = this.Description,
                UnitPrice = this.UnitPrice,
                Quantity = this.Quantity,
                MinimumQuantity = this.MinimumQuantity,
                IsActive = this.IsActive,
                SupplierId = this.SupplierId,
                Supplier = this.Supplier,
                FamilyId = this.FamilyId,
                Family = this.Family
            };
        }
    }
}
