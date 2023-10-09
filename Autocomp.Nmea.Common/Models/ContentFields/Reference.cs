using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autocomp.Nmea.Common.ContentFields
{
    public enum DataReference
    {
        R, T
    }
    public class Reference : NmeaMessageContentField<DataReference>
    {
        public override string Name { get; }
        public override DataReference Value { get; set; }

        public Reference(string s) : base(s) { Name = "Data reference"; }
        
        public override string ToString()
        {
            switch (Value)
            {
                case DataReference.R:
                    return "Relative";
                case DataReference.T:
                    return "Theoretical";
                default:
                    throw new ArgumentException($"Cannot pase {Value} to string");
            }
        }

        protected override DataReference Parse(string s)
        {
            try
            {
                return (DataReference)Enum.Parse(typeof(DataReference), s);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
