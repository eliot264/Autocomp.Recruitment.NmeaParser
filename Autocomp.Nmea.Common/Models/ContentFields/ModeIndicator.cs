using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autocomp.Nmea.Common.ContentFields
{
    public enum Indicator
    {
        A, D, E, M, S, N
    }
    public class ModeIndicator : NmeaMessageContentField<Indicator>
    {
        public override string Name { get; }
        public override Indicator Value { get; set; }

        public ModeIndicator(string s) : base(s) { Name = "Mode indicator"; }

        public override string ToString()
        {
            switch (Value)
            {
                case Indicator.A:
                    return "Autonomous mode";
                case Indicator.D:
                    return "Differential mode";
                case Indicator.E:
                    return "Estimated (dead reckoning) mode";
                case Indicator.M:
                    return "Manual input mode";
                case Indicator.S:
                    return "Simulator mode";
                case Indicator.N:
                    return "Data not valid";
                default:
                    throw new ArgumentException($"Cannot pase {Value} to string");
            }
        }
        protected override Indicator Parse(string s)
        {
            try
            {
                return (Indicator)Enum.Parse(typeof(Indicator), s);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
