using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autocomp.Nmea.Common.ContentFields
{
    enum DataStatus
    {
        A, V
    }
    internal class Status : NmeaMessageContentField<DataStatus>
    {
        public override string Name { get; }
        public override DataStatus Value { get; set; }

        public Status(string s) : base(s) { Name = "Data status"; }

        public override string ToString()
        {
            switch (Value)
            {
                case DataStatus.A:
                    return "Data valid";
                case DataStatus.V:
                    return "Data not valid";
                default:
                    throw new Exception($"Cannot pase {Value} to string");
            }
        }

        protected override DataStatus Parse(string s)
        {
            try
            {
                return (DataStatus)Enum.Parse(typeof(DataStatus), s);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
