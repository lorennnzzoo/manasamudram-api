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
    
    public partial class WastageInfo
    {
        public int wastageid { get; set; }
        public string DriverName { get; set; }
        public Nullable<decimal> WetWasteCollected { get; set; }
        public Nullable<decimal> DryWasteCollected { get; set; }
        public Nullable<decimal> HHWasteCollected { get; set; }
        public Nullable<decimal> MixedWasteCollected { get; set; }
        public Nullable<System.DateTime> DateTimeWasteLogged { get; set; }
    }
}