using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autocomp.Nmea.Common.ContentFields
{
    public class Longitude : NmeaMessageContentField<double>
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
        public override string Name { get; }
        public Longitude(string s) : base(s) { Name = "Longitude"; }
        protected override double Parse(string s)
        {
            try
            {
                return Convert.ToDouble(s, CultureInfo.InvariantCulture);
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

            int degree = (int)(Value / 100);
            double minute = Value - (degree * 100);

            return $"{degree} degrees, {minute:00.00} minutes";
        }
    }
}
