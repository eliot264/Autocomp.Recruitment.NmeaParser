using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autocomp.Nmea.Common.Services
{
    public class NmeaMessageParseService : INmeaMessageParseService
    {
        NmeaMessageContent INmeaMessageParseService.Parse(string message)
        {
            throw new NotImplementedException();
        }

        NmeaMessageContent INmeaMessageParseService.Parse(NmeaMessage message)
        {
            throw new NotImplementedException();
        }
    }
}
