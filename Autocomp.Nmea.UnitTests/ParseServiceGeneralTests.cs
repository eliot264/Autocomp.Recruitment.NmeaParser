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
    }
}
