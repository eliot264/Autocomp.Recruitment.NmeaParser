using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autocomp.Nmea.Common
{
    public abstract class NmeaMessageContent
    {
        public abstract string Name { get; }
        public NmeaMessageContent(string message) : this(NmeaMessage.FromString(message)) { }
        public NmeaMessageContent(NmeaMessage message)
        {
            try
            {
                Parse(message);
            }
            catch (Exception e)
            {
                throw new Exception("Cannot parse to NmeaMessageContent", e);
            }
        }
        protected abstract void Parse(NmeaMessage message);
        public abstract Dictionary<string, string> ToDictionary();
    }
}
