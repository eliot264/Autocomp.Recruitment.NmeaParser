using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autocomp.Nmea.Common.ContentFields
{
    internal class Longitude : NmeaMessageContentField<double>
    {
        private double longitude;

        private double minValue = 0.0;
        private double maxValue = 180.0;
        public override double Value
        {
            get { return longitude; }
            set
            {
                if (value >= minValue && value <= maxValue)
                {
                    longitude = value;
                }
                else
                {
                    throw new ArgumentException($"Tried to add {value}. Latitude takes values from {minValue} to {maxValue} degree.");
                }
            }
        }
        public override NmeaMessageContentField<double> Parse(string s)
        {
            try
            {
                NmeaMessageContentField<double> nmeaMessage = new Longitude();
                Value = double.Parse(s);
                return nmeaMessage;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }
    }
}
