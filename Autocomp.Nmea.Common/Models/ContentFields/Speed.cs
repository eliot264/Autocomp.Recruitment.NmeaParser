using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autocomp.Nmea.Common.ContentFields
{
    public class Speed : NmeaMessageContentField<double>
    {
        private double speed;
        public override string Name { get; }
        public override double Value
        {
            get { return speed; }
            set
            {
                if (value > 0)
                {
                    speed = value;
                }
                else
                {
                    throw new ArgumentException($"Tried to add {value}. Speed takes values greater than 0");
                }
            }
        }

        public Speed(string s) : base(s) { Name = "Speed"; }

        public override string ToString()
        {
            return $"{Value:.00}";
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
