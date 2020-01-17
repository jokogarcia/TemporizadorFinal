using System;
using System.Collections.Generic;
using System.Text;

namespace TemporizadorFinal.Models
{
    class Configuration
    {
        public int NumberOfIntervals { get; set; }
        public TimeSpan ExamDuration { get; set; }
        public TimeSpan PauseDuration { get; set; }
    }
}
