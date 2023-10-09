using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autocomp.Nmea.Common
{
    public abstract class NmeaMessageContent
    {
        public NmeaMessageContent(string message) : this(NmeaMessage.FromString(message)) { }
        public NmeaMessageContent(NmeaMessage message)
        {
            Parse(message);
        }
        protected abstract void Parse(NmeaMessage message);
        public abstract Dictionary<string, string> ToDictionary();
    }
}
