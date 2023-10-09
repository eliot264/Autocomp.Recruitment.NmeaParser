using Autocomp.Nmea.Common.Contents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autocomp.Nmea.Common.Services
{
    public class NmeaMessageParseService : INmeaMessageParseService
    {
        private Dictionary<string, Type> messageTypes;

        public NmeaMessageParseService()
        {
            messageTypes = new Dictionary<string, Type>
            {
                { "GLL", typeof(GLL) },
                { "MWV", typeof(MWV) }
            };
        }

        public NmeaMessageContent Parse(string message)
        {
            return Parse(NmeaMessage.FromString(message));
        }
        public NmeaMessageContent Parse(NmeaMessage message)
        {
            try
            {
                Type contentType = messageTypes.Single(m => m.Key == message.Header).Value;

                return (NmeaMessageContent)Activator.CreateInstance(contentType, new object[] { message });
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
