using System;

namespace Autocomp.Nmea.Common
{
    /// <summary>
    /// Określa format komunikatów NMEA definiując wszystkie ograniczniki
    /// </summary>
    public class NmeaFormat
    {
        /// <summary>
        /// Określa ogranicznik początku komunikatu ($ - wg standardu)
        /// </summary>
        public char Prefix { get; set; }

        /// <summary>
        /// Określa ogranicznik końca danych komunikatu (* - wg standardu)
        /// </summary>
        public char Suffix { get; set; }


        /// <summary>
        /// Określa separator pól danych komunikatu (, - wg standardu)
        /// </summary>
        public char Separator { get; set; }

        /// <summary>
        /// Określa ogranicznik końca komunikatu  ([CRLF] - wg standardu)
        /// </summary>
        public string Terminator
        {
            get { return terminator; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new NotSupportedException("Terminator nie może być pustym łańcuchem");
                }
                terminator = value;
            }
        }

        public const char DefaultPrefix = '$';
        public const char DefaultSuffix = '*';
        public const char DefaultSeparator = ',';
        public const string DefaultTerminator = "\r\n";

        public static readonly NmeaFormat Default = new NmeaFormat(DefaultPrefix, DefaultSuffix, DefaultSeparator, DefaultTerminator);

        private string terminator;

        public NmeaFormat(char prefix, char suffix, char separator, string terminator)
        {
            Prefix = prefix;
            Suffix = suffix;
            Separator = separator;
            this.terminator = terminator;
        }
    }
}
