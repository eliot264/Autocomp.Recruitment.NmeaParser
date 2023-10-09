using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autocomp.Nmea.Common.ContentFields
{
    public class Latitude : NmeaMessageContentField<double>
    {
        private double latitude;

        private double minValue = 0.0;
        private double maxValue = 9000.0;

        public override double Value
        {
            get { return latitude; }
            set
            {
                if(value >= minValue && value <= maxValue)
                {
                    latitude = value;
                }
                else if(value == 9100)
                {
                    latitude = value;
                }
                else
                {
                    throw new ArgumentException($"Tried to add {value}. Latitude takes values from {minValue} to {maxValue} degree.");
                }
            }
        }
        public override string Name { get; }
        public Latitude(string s) : base(s) { Name = "Latitude"; }

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
            if(Value == 9100)
            {
                return "Latitude not available";
            }

            int degree = (int)(Value / 100);
            double minute = Value - (degree * 100);

            return $"{degree} degrees, {minute:00.00} minutes";
        }
    }
}
