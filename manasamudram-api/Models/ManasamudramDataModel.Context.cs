﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ManasamudramEntities : DbContext
    {
        public ManasamudramEntities()
            : base("name=ManasamudramEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<App_Users> App_Users { get; set; }
        public virtual DbSet<HouseHold> HouseHolds { get; set; }
        public virtual DbSet<MicRecording> MicRecordings { get; set; }
        public virtual DbSet<QR> QRs { get; set; }
        public virtual DbSet<TotalDryWastage> TotalDryWastages { get; set; }
        public virtual DbSet<TotalHHwastage> TotalHHwastages { get; set; }
        public virtual DbSet<TotalMixedWastage> TotalMixedWastages { get; set; }
        public virtual DbSet<TotalWetWastage> TotalWetWastages { get; set; }
        public virtual DbSet<VehiclesForMic> VehiclesForMics { get; set; }
        public virtual DbSet<WastageConfirmation> WastageConfirmations { get; set; }
        public virtual DbSet<WastageInfo> WastageInfoes { get; set; }
        public virtual DbSet<Trip> Trips { get; set; }
    }
}