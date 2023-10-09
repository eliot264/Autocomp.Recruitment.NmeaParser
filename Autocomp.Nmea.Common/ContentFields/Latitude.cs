using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autocomp.Nmea.Common.ContentFields
{
    internal class Latitude : NmeaMessageContentField<double>
    {
        public override double Value { get; set; }

        public override NmeaMessageContentField<double> Parse(string s)
        {
            try
            {
                NmeaMessageContentField<double> nmeaMessage = new Latitude();
                Value = double.Parse(s);
            }
            catch (Exception e)
            {

                throw e;
            }         
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }
    }
}
