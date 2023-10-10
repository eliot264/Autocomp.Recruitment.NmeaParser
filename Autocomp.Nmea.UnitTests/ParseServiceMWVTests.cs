using Autocomp.Nmea.Common.Services;
using Autocomp.Nmea.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autocomp.Nmea.Common.Contents;
using Autocomp.Nmea.Common.ContentFields;

namespace Autocomp.Nmea.UnitTests
{
    [TestClass]
    public class ParseServiceMWVTests
    {
        [TestMethod]
        public void CorrectMessageTest()
        {
            INmeaMessageParseService parseService = new NmeaMessageParseService();
            string message = "$MWV,320,R,15.0,M,A*0B";

            MWV result = (MWV)parseService.Parse(message);
            Dictionary<string, string> resultDictionary = result.ToDictionary();

            Assert.AreEqual(320.0, result.Angle.Value);
            Assert.AreEqual(DataReference.R, result.Reference.Value);
            Assert.AreEqual(15.0, result.Speed.Value);
            Assert.AreEqual(Unit.M, result.SpeedUnit.Value);
            Assert.AreEqual(DataStatus.A, result.Status.Value);

            Assert.AreEqual(4, resultDictionary.Count);

            Assert.AreEqual("Angle", resultDictionary.ElementAt(0).Key);
            Assert.AreEqual("320,00 degrees", resultDictionary.ElementAt(0).Value);

            Assert.AreEqual("Reference", resultDictionary.ElementAt(1).Key);
            Assert.AreEqual("Relative", resultDictionary.ElementAt(1).Value);

            Assert.AreEqual("Speed", resultDictionary.ElementAt(2).Key);
            Assert.AreEqual("320,00 m/s", resultDictionary.ElementAt(2).Value);

            Assert.AreEqual("Status", resultDictionary.ElementAt(3).Key);
            Assert.AreEqual("Data valid", resultDictionary.ElementAt(3).Value);
        }
    }
}
