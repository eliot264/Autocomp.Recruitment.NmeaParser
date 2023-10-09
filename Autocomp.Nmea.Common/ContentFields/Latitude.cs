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

        private double minValue = 0.0;
        private double maxValue = 90.0;

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
                    throw new ArgumentException($"Tried to add {value}. Latitude takes values from {minValue} to {maxValue} degree.");
                }
            }
        }

        public Latitude(string s) : base(s) { }

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
            return Value.ToString();
        }
    }
}
