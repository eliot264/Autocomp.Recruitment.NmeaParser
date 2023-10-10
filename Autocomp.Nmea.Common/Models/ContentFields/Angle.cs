using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autocomp.Nmea.Common.ContentFields
{
    public class Angle : NmeaMessageContentField<double>
    {
        private double angle;

        private double minValue = 0.0;
        private double maxValue = 359.0;

        public override string Name { get; }
        public override double Value
        {
            get { return angle; }
            set
            {
                if(value >= minValue && value <= maxValue)
                {
                    angle = value;
                }
                else
                {
                    throw new ArgumentException($"Tried to add {value}. Angle takes values from {minValue} to {maxValue} degree.");
                }
            }
        }

        public Angle(string s) : base(s) { Name = "Angle"; }

        public override string ToString()
        {
            return $"{Value:.00} degrees";
        }

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
    }
}
