using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autocomp.Nmea.Common.ContentFields
{
    enum Unit
    {
        K, M, N, S
    }
    public class SpeedUnit : NmeaMessageContentField<Unit>
    {
        public override string Name { get; }
        public override Unit Value { get; set; }

        public SpeedUnit(string s) : base(s) { Name = "Speed unit"; }

        public override string ToString()
        {
            switch (Value)
            {
                case Unit.K:
                    return "km/h";
                case Unit.M:
                    return "m/s";
                case Unit.N:
                    return "knots";
                case Unit.S:
                    return "statute miles/h";
                default:
                    throw new ArgumentException($"Cannot pase {Value} to string");
            }
        }

        protected override Unit Parse(string s)
        {
            try
            {
                return (Unit)Enum.Parse(typeof(Unit), s);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
