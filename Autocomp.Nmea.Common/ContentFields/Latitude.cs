using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autocomp.Nmea.Common.ContentFields
{
    internal class Latitude : NmeaMessageContentField<double>
    {
        private double latitude;
        public override double Value
        {
            get { return latitude; }
            set
            {
                if(value >= 0.0 && value <= 90.0)
                {
                    latitude = value;
                }
                else
                {
                    throw new ArgumentException($"Tried to add {value}. Latitude takes values from 0 to 90 degree.");
                }
            }
        }

        public override NmeaMessageContentField<double> Parse(string s)
        {
            try
            {
                NmeaMessageContentField<double> nmeaMessage = new Latitude();
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
            return Value.ToString();
        }
    }
}
