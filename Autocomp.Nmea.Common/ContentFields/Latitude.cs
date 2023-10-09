using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autocomp.Nmea.Common.ContentFields
{
    internal class Latitude : NmeaMessageContentField
    {
        public double Value { get; set; }
        public override NmeaMessageContentField Parse(string s)
        {
            try
            {
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
