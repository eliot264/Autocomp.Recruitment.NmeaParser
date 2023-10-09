using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autocomp.Nmea.Common.ContentFields
{
    internal class UTCTimeOnly : NmeaMessageContentField<NmeaTimeOnly>
    {
        public override NmeaTimeOnly Value { get; set; }

        public UTCTimeOnly(string s) : base(s) { }

        public override string ToString()
        {
            return Value.ToString();
        }

        protected override NmeaTimeOnly Parse(string s)
        {
            if (s.Length != 9 && s.Length != 6 )
            {
                throw new ArgumentException($"String {s} is invalid. String must be in hhmmss.ss format.");
            }

            try
            {
                int hour = int.Parse(s.Substring(0, 2));
                int minute = int.Parse(s.Substring(2, 2));
                double second = double.Parse(s.Substring(4, 4));

                return new NmeaTimeOnly()
                {
                    Hour = hour,
                    Minute = minute,
                    Second = second
                };
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
