using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autocomp.Nmea.Common;
using Autocomp.Nmea.Common.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Autocomp.Nmea.UnitTests
{
    [TestClass]
    internal class ParseServiceGLLTests
    {
        [TestMethod]
        public void CorrectMessageTest()
        {
            INmeaMessageParseService parseService = new NmeaMessageParseService();
            string message = "$GPGLL,3953.88008971,N,10506.75318910,W,034138.00,A,D*7A";

            var result = parseService.Parse(message);

            Assert.AreEqual(message, message);
        }
    }
}
