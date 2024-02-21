using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class TotalMixedWastageConfirm
    {
        public int TotalWastageId { get; set; }
        [Required]


        public Nullable<decimal> MixedWasteCollected { get; set; }

        [Required]
        public Nullable<decimal> TotalMixedWasteDisposed { get; set; }

        [Required]
        public Nullable<int> HousesCollected { get; set; }
        [Required]
        public Nullable<decimal> MixedWasteRecovery { get; set; }

    }
    public class TotalHHWastageConfirm
    {
        public int TotalWastageId { get; set; }
        [Required]


        public Nullable<decimal> HHWasteCollected { get; set; }

        [Required]

        public Nullable<int> HousesCollected { get; set; }
        [Required]

        public Nullable<decimal> TotalHHHSafelyDisposed { get; set; }

    }

    public class TotalDryWastageConfirm
    {
        public int TotalWastageId { get; set; }
        [Required]


        public Nullable<decimal> DryWasteCollected { get; set; }

        [Required]
        public Nullable<decimal> DryWasteProcessed { get; set; }

        [Required]
        public Nullable<int> HousesCollected { get; set; }
        [Required]
        public Nullable<decimal> DryWasteReceived { get; set; }

        [Required]
        public Nullable<decimal> DryWasteRecovery { get; set; }

    }
    public class TotalWetWastageConfirm
    {
        public int TotalWastageId { get; set; }
        [Required]


        public Nullable<decimal> WetWasteCollected { get; set; }

        [Required]
        public Nullable<decimal> WetWasteProcessed { get; set; }

        [Required]
        public Nullable<int> HousesCollected { get; set; }
        [Required]
        public Nullable<decimal> WetWasteReceived { get; set; }

        [Required]
        public Nullable<decimal> WetWasteRecovery { get; set; }

    }
}
