using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class tripdata
    {
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        //public DateTime DateTimeWasteLogged { get; set; } = DateTime.Now;
        public string DriverName { get; set; }
        [Required]
        public System.DateTime Datetime { get; set; }
        [Required]
        public Nullable<decimal> WetWasteCollected { get; set; }
        [Required]
        public Nullable<decimal> DryWasteCollected { get; set; }
        [Required]
        public Nullable<decimal> HHWasteCollected { get; set; }
        [Required]
        public Nullable<decimal> MixedWasteCollected { get; set; }
    }
}
