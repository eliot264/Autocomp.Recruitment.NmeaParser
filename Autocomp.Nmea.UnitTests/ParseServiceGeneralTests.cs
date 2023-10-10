using Autocomp.Nmea.Common.Services;
using Autocomp.Nmea.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autocomp.Nmea.UnitTests
{
    [TestClass]
    public class ParseServiceGeneralTests
    {
        [TestMethod]
        public void UnsuportedHeader()
        {
            INmeaMessageParseService parseService = new NmeaMessageParseService();
            string message = "$HDD,3953.88008971,N,10506.75318910,W,034138.00,A,D*7A";

            Action parseAction = () => parseService.Parse(message);

            Assert.ThrowsException<ArgumentException>(parseAction);
        }
        [TestMethod]
        public void InvalidMessageFormat()
        {
            INmeaMessageParseService parseService = new NmeaMessageParseService();
            string invalidPrefixMessage = "#HDD,3953.88008971N,10506.75318910,W,034138.00,A,D*7A";
            string invalidSuffixMessage = "$HDD,3953.88008971N,10506.75318910,W,034138.00,A,D#7A";
            string invalidSeparatorMessage = "$HDD'3953.88008971N'10506.75318910'W,034138.00'A'D*7A";
            string invalidTerminatorMessage = "$HDD,3953.88008971N,10506.75318910,W,034138.00,A,D*7A\n\n\n";

            Action invalidPrefixAction = () => parseService.Parse(invalidPrefixMessage);
            Action invalidSuffixAction = () => parseService.Parse(invalidSuffixMessage);
            Action invalidSeparatorAction = () => parseService.Parse(invalidSeparatorMessage);
            Action invalidTerminatorAction = () => parseService.Parse(invalidTerminatorMessage);

            Assert.ThrowsException<ArgumentException>(invalidPrefixAction);
            Assert.ThrowsException<ArgumentException>(invalidSuffixAction);
            Assert.ThrowsException<ArgumentException>(invalidSeparatorAction);
            Assert.ThrowsException<ArgumentException>(invalidTerminatorAction);
        }
    }
}
