//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TotalWetWastage
    {
        public int WetWastageId { get; set; }
        public Nullable<System.DateTime> DateTime { get; set; }
        public Nullable<decimal> WetWasteCollected { get; set; }
        public Nullable<decimal> WetWasteReceived { get; set; }
        public Nullable<decimal> WetWasteProcessed { get; set; }
        public Nullable<decimal> WetWasteRecovery { get; set; }
        public Nullable<int> HousesCollected { get; set; }
    }
}
