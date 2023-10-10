using Autocomp.Nmea.Common.ContentFields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autocomp.Nmea.Common.Contents
{
    public class MWV : NmeaMessageContent
    {
        public override string Name { get; }
        public Angle Angle { get; set; }
        public Reference Reference { get; set; }
        public Speed Speed { get; set; }
        public SpeedUnit SpeedUnit { get; set; }
        public Status Status { get; set; }

        public MWV(string message) : base(message) { Name = "Wind Speed and Angle"; }
        public MWV(NmeaMessage message) : base(message) { Name = "Wind Speed and Angle"; }

        public override Dictionary<string, string> ToDictionary()
        {
            return new Dictionary<string, string>
            {
                {Angle.Name, Angle.ToString() },
                {Reference.Name, Reference.ToString()},
                {Speed.Name, $"{Speed} {SpeedUnit}"},
                {Status.Name, Status.ToString()}
            };
        }

        protected override void Parse(NmeaMessage message)
        {
            if(message.Fields.Length != 5)
            {
                throw new ArgumentException($"MWV message should have 5 fields instead of {message.Fields.Length}");
            }

            try
            {
                Angle = new Angle(message.Fields[0]);
                Reference = new Reference(message.Fields[1]);
                Speed = new Speed(message.Fields[2]);
                SpeedUnit = new SpeedUnit(message.Fields[3]);
                Status = new Status(message.Fields[4]);
            }
            catch (Exception e)
            {
                throw new ArgumentException("Cannot parse message to MWV", e);
            }
        }
    }
}
