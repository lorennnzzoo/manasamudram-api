using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class MicRecordingModel
    {
        public int Id { get; set; }
        public DateTime DateTimeRecorded { get; set; }
        public bool IsRead { get; set; }
        public byte[] RecordedData { get; set; }
        public int LengthOfAudio { get; set; }

    }

    public class AudioDataModel
    {
        public byte[] Audio { get; set; }
        public string Duration { get; set; }
    }

}
