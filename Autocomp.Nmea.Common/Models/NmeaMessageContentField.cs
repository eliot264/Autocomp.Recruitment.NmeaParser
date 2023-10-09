using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autocomp.Nmea.Common
{
    internal abstract class NmeaMessageContentField<T>
    {
        public abstract string Name { get; }
        public abstract T Value { get; set; }
        public NmeaMessageContentField(string s)
        {
            Value = Parse(s);
        }
        protected abstract T Parse(string s);
        public abstract override string ToString();
    }
}
