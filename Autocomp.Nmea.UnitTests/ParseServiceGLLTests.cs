using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autocomp.Nmea.Common;
using Autocomp.Nmea.Common.ContentFields;
using Autocomp.Nmea.Common.Contents;
using Autocomp.Nmea.Common.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Autocomp.Nmea.UnitTests
{
    [TestClass]
    public class ParseServiceGLLTests
    {
        [TestMethod]
        public void CorrectMessageTest()
        {
            INmeaMessageParseService parseService = new NmeaMessageParseService();
            string message = "$GLL,3953.88008971,N,10506.75318910,W,034138.00,A,D*7A";

            GLL result = (GLL)parseService.Parse(message);


            Assert.AreEqual(result.Latitude.Value, 3953.88008971);
            Assert.AreEqual(result.LatitudeDirection.Value, Directions.North);
            Assert.AreEqual(result.Longitude.Value, 10506.75318910);
            Assert.AreEqual(result.LongitudeDirection.Value, Directions.West);
            Assert.AreEqual(result.UTCOfPosition.Value, new NmeaTimeOnly { Hour = 3, Minute = 41, Second = 38.00 });
            Assert.AreEqual(result.Status.Value, DataStatus.A);
            Assert.AreEqual(result.ModeIndicator.Value, Indicator.D);
        }
    }
}
