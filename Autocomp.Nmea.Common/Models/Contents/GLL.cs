using Autocomp.Nmea.Common.ContentFields;
using Autocomp.Nmea.Common.Models.ContentFields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autocomp.Nmea.Common.Contents
{
    public class GLL : NmeaMessageContent
    {
        public override string Name { get; }
        public Latitude Latitude { get; set; }
        public NSOnlyCardinalDirection LatitudeDirection { get; set; }
        public Longitude Longitude { get; set; }
        public WEOnlyCardinalDirection LongitudeDirection { get; set; }
        public UTCTimeOnly UTCOfPosition { get; set; }
        public Status Status { get; set; }
        public ModeIndicator ModeIndicator { get; set; }

        public GLL(string message) : base(message) { Name = "Geographic Position - Latitude/Longitude"; }
        public GLL(NmeaMessage message) : base(message) { Name = "Geographic Position - Latitude/Longitude"; }

        public override Dictionary<string, string> ToDictionary()
        {
            Dictionary<string, string> result = new Dictionary<string, string>
            {
                { Latitude.Name, $"{Latitude} {LatitudeDirection}" },
                { Longitude.Name, $"{Longitude} {LongitudeDirection}" },
                { UTCOfPosition.Name, UTCOfPosition.ToString() },
                { Status.Name, Status.ToString() },
                { ModeIndicator.Name, ModeIndicator.ToString() }
            };

            return result;
        }

        protected override void Parse(NmeaMessage message)
        {
            if(message.Fields.Length != 7)
            {
                throw new ArgumentException($"GLL message should have 7 fields instead of {message.Fields.Length}");
            }

            try
            {
                Latitude = new Latitude(message.Fields[0]);
                LatitudeDirection = new NSOnlyCardinalDirection(message.Fields[1]);
                Longitude = new Longitude(message.Fields[2]);
                LongitudeDirection = new WEOnlyCardinalDirection(message.Fields[3]);
                UTCOfPosition = new UTCTimeOnly(message.Fields[4]);
                Status = new Status(message.Fields[5]);
                ModeIndicator = new ModeIndicator(message.Fields[6]);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
