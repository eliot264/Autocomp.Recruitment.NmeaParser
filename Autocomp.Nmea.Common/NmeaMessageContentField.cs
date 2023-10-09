using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autocomp.Nmea.Common
{
    internal abstract class NmeaMessageContentField
    {
        public abstract NmeaMessageContentField Parse(string s);
        public abstract override string ToString();
    }
}
