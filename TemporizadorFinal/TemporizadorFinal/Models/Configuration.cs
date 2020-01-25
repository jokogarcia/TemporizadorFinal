using System;
using System.Collections.Generic;
using System.Text;

namespace TemporizadorFinal.Models
{
    public class Configuration
    {
        public int NumberOfIntervals { get; set; }
        public TimeSpan ExamDuration { get; set; }
        public TimeSpan PauseDuration { get; set; }
        public Configuration()
        {
            ExamDuration = new TimeSpan(5L * TimeSpan.TicksPerMinute);
            NumberOfIntervals = 12;
            PauseDuration = new TimeSpan(30L * TimeSpan.TicksPerSecond);
        }
    }
   
}
