using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autocomp.Nmea.Common
{
    public interface INmeaMessageParseService
    {
        NmeaMessageContent Parse(string message);
        NmeaMessageContent Parse(NmeaMessage message);
    }
}
