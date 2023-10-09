using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autocomp.Nmea.Common
{
    internal abstract class NmeaMessageContentField<T>  where T : class
    {
        public T Value { get; set; }
        protected NmeaMessageContentField() { }
        public abstract NmeaMessageContentField<T> Parse(string s);
        public abstract override string ToString();
    }
}
