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

            AreDataEquals(result, 3953.88008971, Directions.North, 10506.75318910, Directions.West, new NmeaTimeOnly { Hour = 3, Minute = 41, Second = 38.00 }, DataStatus.A, Indicator.D);
        }
        [TestMethod]
        public void InvalidMessageFieldsTest()
        {
            INmeaMessageParseService parseService = new NmeaMessageParseService();
            string moreFieldsMessage = "$GLL,3953.88008971,3953.88008971,N,10506.75318910,W,034138.00,A,D*7A";
            string lessFieldsMessage = "$GLL,3953.88008971,N,10506.75318910,034138.00,A,D*7A";

            Action moreFieldsParse = () => { parseService.Parse(moreFieldsMessage); };
            Action lessFieldsParse = () => { parseService.Parse(lessFieldsMessage); };

            Assert.ThrowsException<ArgumentException>(moreFieldsParse);
            Assert.ThrowsException<ArgumentException>(lessFieldsParse);
        }
        [TestMethod]
        public void InvalidLatitude()
        {
            INmeaMessageParseService parseService = new NmeaMessageParseService();
            string twoDotsMessage = "$GLL,3953.8800.8971,N,10506.75318910,W,034138.00,A,D*7A";
            string someLettersMessage = "$GLL,3953.88ABC8971,N,10506.75318910,W,034138.00,A,D*7A";
            string lessThanRangeMessage = "$GLL,-3953.88008971,N,10506.75318910,W,034138.00,A,D*7A";
            string moreThanRangeMessage = "$GLL,399953.88008971,N,10506.75318910,W,034138.00,A,D*7A";

            Action twoDotsParse = () => { parseService.Parse(twoDotsMessage); };
            Action someLettersParse = () => { parseService.Parse(someLettersMessage); };
            Action lessThanRangeParse = () => { parseService.Parse(lessThanRangeMessage); };
            Action moreThanRangeParse = () => { parseService.Parse(moreThanRangeMessage); };

            Assert.ThrowsException<ArgumentException>(twoDotsParse);
            Assert.ThrowsException<ArgumentException>(someLettersParse);
            Assert.ThrowsException<ArgumentException>(lessThanRangeParse);
            Assert.ThrowsException<ArgumentException>(moreThanRangeParse);
        }

        private void AreDataEquals(GLL result, double latitude, Directions latitudeDirection, double longitude, Directions longitudeDirection, NmeaTimeOnly utcOfPosition, DataStatus dataStatus, Indicator modeIndicator)
        {
            Assert.AreEqual(result.Latitude.Value, latitude);
            Assert.AreEqual(result.LatitudeDirection.Value, latitudeDirection);
            Assert.AreEqual(result.Longitude.Value, longitude);
            Assert.AreEqual(result.LongitudeDirection.Value, longitudeDirection);
            Assert.AreEqual(result.UTCOfPosition.Value, utcOfPosition);
            Assert.AreEqual(result.Status.Value, dataStatus);
            Assert.AreEqual(result.ModeIndicator.Value, modeIndicator);
        }

        private void AreDataNotEquals(GLL result, double latitude, Directions latitudeDirection, double longitude, Directions longitudeDirection, NmeaTimeOnly utcOfPosition, DataStatus dataStatus, Indicator modeIndicator)
        {
            Assert.AreNotEqual(result.Latitude.Value, latitude);
            Assert.AreNotEqual(result.LatitudeDirection.Value, latitudeDirection);
            Assert.AreNotEqual(result.Longitude.Value, longitude);
            Assert.AreNotEqual(result.LongitudeDirection.Value, longitudeDirection);
            Assert.AreNotEqual(result.UTCOfPosition.Value, utcOfPosition);
            Assert.AreNotEqual(result.Status.Value, dataStatus);
            Assert.AreNotEqual(result.ModeIndicator.Value, modeIndicator);
        }
    }
}
