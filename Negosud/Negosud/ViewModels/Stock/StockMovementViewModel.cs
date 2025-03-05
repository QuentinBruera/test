using NegosudModel.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negosud.ViewModels.Stock
{
    public class StockMovementViewModel : BaseViewModel
    {
        public DateTime Date => StockMovementDto.Date;
        public int Quantity => StockMovementDto.Quantity;
        public string ReasonName => ReasonDto?.Name ?? "N/A";
        public string ReasonColor => ReasonDto?.Color ?? "Transparent";

        private StockMovementDto StockMovementDto { get; }
        private ReasonDto ReasonDto { get; }

        public StockMovementViewModel(StockMovementDto stockMovementDto, ReasonDto reasonDto)
        {
            StockMovementDto = stockMovementDto;
            ReasonDto = reasonDto;
        }
    }
}
