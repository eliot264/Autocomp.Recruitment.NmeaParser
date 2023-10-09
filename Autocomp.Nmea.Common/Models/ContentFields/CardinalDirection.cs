using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autocomp.Nmea.Common.ContentFields
{
    public enum Directions
    {
        North, South, East, West
    }
    public class CardinalDirection : NmeaMessageContentField<Directions>
    {
        public override Directions Value { get; set;}
        public override string Name { get; }

        public CardinalDirection(string s) : base(s)
        {
            Name = "Cardinal direction";
        }

        public override string ToString()
        {
            return Value.ToString();
        }
        protected override Directions Parse(string s)
        {
            switch (s)
            {
                case "W":
                    return Directions.West;
                case "E":
                    return Directions.East;
                case "N":
                    return Directions.North;
                case "S":
                    return Directions.South;
                default:
                    throw new ArgumentException($"Cannot parse {s} to on of cardinal directions.");
            }
        }
    }
}
