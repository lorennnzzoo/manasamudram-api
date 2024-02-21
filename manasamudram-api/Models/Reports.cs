using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ReportModel
    {

        public DateTime DateTime { get; set; }
        public string VehicleNumber { get; set; }
        //public string DriverName { get; set; }
        public decimal WetWasteCollected { get; set; }
        public decimal WetWasteProcessed { get; set; }
        public decimal DryWasteCollected { get; set; }
        public decimal DryWasteProcessed { get; set; }
        public decimal HHWasteCollected { get; set; }
        //public decimal HHWasteProcessed { get; set; }
        public decimal HHHSafelyDisposed { get; set; }
        public decimal MixedWasteCollected { get; set; }
        //public decimal MixedWasteProcessed { get; set; }
        public decimal MixedWasteDisposed { get; set; }
        public decimal Compost { get; set; }
        public decimal Recycled { get; set; }
        public int HousesCollected { get; set; }
        public int HousesCount { get; set; }
        public int Trips { get; set; }


    }
    public class DriverReportHeader
    {
        public string DriverName { get; set; }
        public string VehicleNumber { get; set; }
        public string State { get; set; }
        public string District { get; set; }
        public string Mandal { get; set; }
        public string Village { get; set; }
        public int PinCode { get; set; }
    }
    public class GetReportViewModel
    {
        [Required]
        public DateTime From { get; set; }
        [Required]
        public DateTime To { get; set; }

    }
    public class ReportsApiResponse
    {
        public DriverReportHeader ReportHeader { get; set; }
        public List<ReportModel> ReportList { get; set; }
    }
}
