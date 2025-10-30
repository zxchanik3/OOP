using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab
{
    public class Track
    {
        public string Name { get; set; }
        public int RequiredLapCount { get; set; }
        public double TotalLength { get; set; }

        public double StartLineX { get; set; }
        public double StartLineY { get; set; }

        public Track(string name, int lapCount, double length, double startX, double startY)
        {
            Name = name;
            RequiredLapCount = lapCount;
            TotalLength = length;
            StartLineX = startX;
            StartLineY = startY;
        }
    }
}
