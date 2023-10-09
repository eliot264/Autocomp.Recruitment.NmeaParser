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
    public enum CardinalDirectionMode
    {
        OnlyNS, OnlyWE, All
    }
    internal class CardinalDirection : NmeaMessageContentField<Directions>
    {
        private readonly CardinalDirectionMode _mode;

        public override Directions Value { get; set;}

        public CardinalDirection(string s, CardinalDirectionMode mode) : base(s)
        {
            _mode = mode;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
        protected override Directions Parse(string s)
        {
            if(_mode != CardinalDirectionMode.OnlyNS)
            {
                switch (s)
                {
                    case "W":
                        return Directions.West;
                    case "E":
                        return Directions.East;
                    default:
                        break;
                }
            }

            if (_mode != CardinalDirectionMode.OnlyWE)
            {
                switch (s)
                {
                    case "N":
                        return Directions.North;
                    case "S":
                        return Directions.South;
                    default:
                        break;
                }
            }

            throw new ArgumentException($"Cannot parse {s} to on of cardinal directions.");
        }
    }
}
