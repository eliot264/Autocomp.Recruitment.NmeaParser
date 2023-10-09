using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autocomp.Nmea.Common
{
    public class NmeaTimeOnly
    {
        private int hour;
        private int minute;
        private double second;
        public int Hour
        {
            get { return hour; }
            set
            {
                if (value >=0 && value < 24)
                {
                    hour = value;
                }
                else
                {
                    throw new ArgumentException($"Tried to add {value}. Hour takes values from 0 to 23");
                }
            }
        }
        public int Minute
        {
            get { return minute; }
            set
            {
                if (value >= 0 || value < 60)
                {
                    hour = value;
                }
                else
                {
                    throw new ArgumentException($"Tried to add {value}. Minute takes values from 0 to 59");
                }
            }
        }
        public double Second
        {
            get { return second; }
            set
            {
                if (value >= 0 && value < 60.0)
                {
                    second = value;
                }
                else if(value == 60 || value == 61 || value == 62 || value == 63)
                {
                    second = value;
                }
                else
                {
                    throw new ArgumentException($"Tried to add {value}. Second takes values from 0 to 59");
                }
            }
        }

        public override string ToString()
        {
            switch (Second)
            {
                case 60:
                    return "Time stamp is not available";
                case 61:
                    return "Positioning system is in manual input mode";
                case 62:
                    return "Electronic Position Fixing System operates in estimated (dead reckoning) mode";
                case 63:
                    return "Positioning system is inoperative";
                default:
                    return $"{hour:00}:{minute:00}:{second:00.00}";
            }
        }
    }
}
