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
        private double maxValue = 18000.0;
        public override double Value
        {
            get { return longitude; }
            set
            {
                if (value >= minValue && value <= maxValue)
                {
                    longitude = value;
                }
                else if(value == 18100)
                {
                    longitude = value;
                }
                else
                {
                    throw new ArgumentException($"Tried to add {value}. Latitude takes values from {minValue} to {maxValue} degree.");
                }
            }
        }
        public Longitude(string s) : base(s) { }
        protected override double Parse(string s)
        {
            try
            {
                return double.Parse(s);
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public override string ToString()
        {
            if(Value == 18100)
            {
                return "Longitude not available";
            }

            int degree = (int)(Value / 1000);
            double minute = Value - degree;

            return $"{degree} degrees, {minute:00.00} minutes";
        }
    }
}
