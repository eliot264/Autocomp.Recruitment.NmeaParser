using Autocomp.Nmea.Common.ContentFields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autocomp.Nmea.Common.Models.ContentFields
{
    public class WEOnlyCardinalDirection : CardinalDirection
    {
        public WEOnlyCardinalDirection(string s) : base(s) { }

        protected override Directions Parse(string s)
        {
            Directions result;

            try
            {
                result = base.Parse(s);
            }
            catch (Exception)
            {
                throw;
            }

            if (result == Directions.North || result == Directions.South)
            {
                throw new ArgumentException("This Cardinal direction can be only East or West");
            }

            return result;
        }
    }
}
