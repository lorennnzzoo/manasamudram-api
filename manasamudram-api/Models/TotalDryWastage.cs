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
    
    public partial class TotalDryWastage
    {
        public int DryWastageId { get; set; }
        public Nullable<System.DateTime> DateTime { get; set; }
        public Nullable<decimal> DryWasteCollected { get; set; }
        public Nullable<decimal> DryWasteReceived { get; set; }
        public Nullable<decimal> DryWasteProcessed { get; set; }
        public Nullable<decimal> DryWasteRecovery { get; set; }
        public Nullable<int> HousesCollected { get; set; }
    }
}
