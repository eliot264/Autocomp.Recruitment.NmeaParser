﻿using Autocomp.Nmea.Common.ContentFields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autocomp.Nmea.Common.Contents
{
    internal class GLL : NmeaMessageContent
    {
        Latitude Latitude { get; set; }
        CardinalDirection LatitudeDirection { get; set; }
        Longitude Longitude { get; set; }
        CardinalDirection LongitudeDirection { get; set; }
        UTCTimeOnly UTCOfPosition { get; set; }
        public GLL(string message) : base(message) { }

        public GLL(NmeaMessage message) : base(message) { }

        public override Dictionary<string, string> ToDictionary()
        {
            throw new NotImplementedException();
        }

        protected override void Parse(NmeaMessage message)
        {
            if(message.Fields.Length != 7)
            {
                throw new ArgumentException($"GLL Message should have 7 fields instead of {message.Fields.Length}");
            }

            try
            {
                Latitude = new Latitude(message.Fields[0]);
                LatitudeDirection = new CardinalDirection(message.Fields[1], CardinalDirectionMode.OnlyNS);
                Longitude = new Longitude(message.Fields[2]);
                LongitudeDirection = new CardinalDirection(message.Fields[3], CardinalDirectionMode.OnlyWE);
                UTCOfPosition = new UTCTimeOnly(message.Fields[4]);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}