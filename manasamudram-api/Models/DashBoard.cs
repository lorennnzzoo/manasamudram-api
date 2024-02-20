using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class DashBoardViewModel
    {
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
    }
    public class DashBoard
    {
        public decimal PerCapitaWasteGeneration { get; set; }
        public decimal ServiceEfficiency { get; set; }
        public decimal WetWasteProcessingEfficiency { get; set; }
        public decimal DryWasteProcessingEfficiency { get; set; }
        public decimal HHWasteProcessingEfficiency { get; set; }
        public decimal MixedWasteProcessingEfficiency { get; set; }
        public decimal TotalWetWasteCollected { get; set; }
        public decimal TotalDryWasteCollected { get; set; }
        public decimal TotalHHWasteCollected { get; set; }
        public decimal TotalMixedWasteCollected { get; set; }
        public decimal TotalWetWasteProcessed { get; set; }
        public decimal TotalDryWasteProcessed { get; set; }
        public decimal TotalHHWasteProcessed { get; set; }
        public decimal TotalMixedWasteProcessed { get; set; }
        public decimal CompostProduced { get; set; }
        public decimal SentForRecycling { get; set; }
    }
}
