using System;

namespace Autocomp.Nmea.Common
{
    public static class NmeaCrcCalculator
    {
        public static byte CRC(NmeaMessage msg)
        {
            return CRC(msg, msg.Format);
        }

        public static byte CRC(NmeaMessage msg, NmeaFormat format)
        {
            byte crc = 0;

            if (!string.IsNullOrEmpty(msg.Header))
            {
                for (int i = 0; i < msg.Header.Length; i++)
                {
                    crc ^= (byte)msg.Header[i];
                }
            }

            if (msg.Fields != null)
            {
                foreach (string field in msg.Fields)
                {
                    crc ^= Convert.ToByte(format.Separator);
                    if (!string.IsNullOrEmpty(field))
                    {
                        for (int i = 0; i < field.Length; i++)
                        {
                            crc ^= (byte)field[i];
                        }
                    }
                }
            }

            return crc;
        }
    }
}
