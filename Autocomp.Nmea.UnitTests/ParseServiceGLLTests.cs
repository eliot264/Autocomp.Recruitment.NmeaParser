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
        public void InvalidMessageNumberOfFieldsTest()
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
        [TestMethod]
        public void ValidLatitude()
        {
            INmeaMessageParseService parseService = new NmeaMessageParseService();
            string normalLatitudeMessage = "$GLL,3953.88008971,N,10506.75318910,W,034138.00,A,D*7A";
            string specialLatitudeMessage = "$GLL,9100,N,10506.75318910,W,034138.00,A,D*7A";

            GLL normalLatitudeResult = (GLL)parseService.Parse(normalLatitudeMessage);
            GLL specialLatitudeResult = (GLL)parseService.Parse(specialLatitudeMessage);

            Assert.AreEqual(normalLatitudeResult.Latitude.Value, 3953.88008971);
            Assert.AreEqual(normalLatitudeResult.Latitude.ToString(), "39 degrees, 53,88 minutes");

            Assert.AreEqual(specialLatitudeResult.Latitude.Value, 9100);
            Assert.AreEqual(specialLatitudeResult.Latitude.ToString(), "Latitude not available");
        }
        [TestMethod]
        public void InvalidLatitudeDirection()
        {
            INmeaMessageParseService parseService = new NmeaMessageParseService();
            string latitudeDirectionWMessage = "$GLL,3953.88008971,W,10506.75318910,W,034138.00,A,D*7A";
            string latitudeDirectionEMessage = "$GLL,3953.88008971,E,10506.75318910,W,034138.00,A,D*7A";

            Action latitudeDirectionWParse = () => { parseService.Parse(latitudeDirectionWMessage); };
            Action latitudeDirectionEParse = () => { parseService.Parse(latitudeDirectionEMessage); };

            Assert.ThrowsException<ArgumentException>(latitudeDirectionWParse);
            Assert.ThrowsException<ArgumentException>(latitudeDirectionEParse);
        }
        [TestMethod]
        public void ValidLatitudeDirection()
        {
            INmeaMessageParseService parseService = new NmeaMessageParseService();
            string latitudeDirectionNMessage = "$GLL,3953.88008971,N,10506.75318910,W,034138.00,A,D*7A";
            string latitudeDirectionSMessage = "$GLL,3953.88008971,S,10506.75318910,W,034138.00,A,D*7A";

            GLL latitudeDirectionNResult = (GLL)parseService.Parse(latitudeDirectionNMessage);
            GLL latitudeDirectionSResult = (GLL)parseService.Parse(latitudeDirectionSMessage);

            Assert.AreEqual(latitudeDirectionNResult.LatitudeDirection.Value, Directions.North);
            Assert.AreEqual(latitudeDirectionNResult.LatitudeDirection.ToString(), "North");

            Assert.AreEqual(latitudeDirectionSResult.LatitudeDirection.Value, Directions.South);
            Assert.AreEqual(latitudeDirectionSResult.LatitudeDirection.ToString(), "South");
        }
        [TestMethod]
        public void InvalidLongitude()
        {
            INmeaMessageParseService parseService = new NmeaMessageParseService();
            string twoDotsMessage = "$GLL,3953.88008971,N,10506.7531.8910,W,034138.00,A,D*7A";
            string someLettersMessage = "$GLL,3953.888971,N,10506.753SMC18910,W,034138.00,A,D*7A";
            string lessThanRangeMessage = "$GLL,3953.88008971,N,-10506.75318910,W,034138.00,A,D*7A";
            string moreThanRangeMessage = "$GLL,3953.88008971,N,1099506.75318910,W,034138.00,A,D*7A";

            Action twoDotsParse = () => { parseService.Parse(twoDotsMessage); };
            Action someLettersParse = () => { parseService.Parse(someLettersMessage); };
            Action lessThanRangeParse = () => { parseService.Parse(lessThanRangeMessage); };
            Action moreThanRangeParse = () => { parseService.Parse(moreThanRangeMessage); };

            Assert.ThrowsException<ArgumentException>(twoDotsParse);
            Assert.ThrowsException<ArgumentException>(someLettersParse);
            Assert.ThrowsException<ArgumentException>(lessThanRangeParse);
            Assert.ThrowsException<ArgumentException>(moreThanRangeParse);
        }
        [TestMethod]
        public void ValidLongitude()
        {
            INmeaMessageParseService parseService = new NmeaMessageParseService();
            string normalLatitudeMessage = "$GLL,3953.88008971,N,10506.75318910,W,034138.00,A,D*7A";
            string specialLatitudeMessage = "$GLL,3953.88008971,N,18100.00,W,034138.00,A,D*7A";

            GLL normalLatitudeResult = (GLL)parseService.Parse(normalLatitudeMessage);
            GLL specialLatitudeResult = (GLL)parseService.Parse(specialLatitudeMessage);

            Assert.AreEqual(normalLatitudeResult.Longitude.Value, 10506.75318910);
            Assert.AreEqual(normalLatitudeResult.Longitude.ToString(), "105 degrees, 06,75 minutes");

            Assert.AreEqual(specialLatitudeResult.Longitude.Value, 18100);
            Assert.AreEqual(specialLatitudeResult.Longitude.ToString(), "Longitude not available");
        }
        [TestMethod]
        public void InvalidLongitudeDirection()
        {
            INmeaMessageParseService parseService = new NmeaMessageParseService();
            string logitudeDirectionNMessage = "$GLL,3953.88008971,N,10506.75318910,N,034138.00,A,D*7A";
            string longitudeDirectionSMessage = "$GLL,3953.88008971,N,10506.75318910,S,034138.00,A,D*7A";

            Action longitudeDirectionNParse = () => { parseService.Parse(logitudeDirectionNMessage); };
            Action longitudeDirectionSParse = () => { parseService.Parse(longitudeDirectionSMessage); };

            Assert.ThrowsException<ArgumentException>(longitudeDirectionNParse);
            Assert.ThrowsException<ArgumentException>(longitudeDirectionSParse);
        }
        [TestMethod]
        public void ValidLongitudeDirection()
        {
            INmeaMessageParseService parseService = new NmeaMessageParseService();
            string longitudeDirectionWMessage = "$GLL,3953.88008971,N,10506.75318910,W,034138.00,A,D*7A";
            string longitudeDirectionEMessage = "$GLL,3953.88008971,S,10506.75318910,E,034138.00,A,D*7A";

            GLL longitudeDirectionWResult = (GLL)parseService.Parse(longitudeDirectionWMessage);
            GLL longitudeDirectionEResult = (GLL)parseService.Parse(longitudeDirectionEMessage);

            Assert.AreEqual(longitudeDirectionWResult.LongitudeDirection.Value, Directions.West);
            Assert.AreEqual(longitudeDirectionWResult.LongitudeDirection.ToString(), "West");

            Assert.AreEqual(longitudeDirectionEResult.LongitudeDirection.Value, Directions.East);
            Assert.AreEqual(longitudeDirectionEResult.LongitudeDirection.ToString(), "East");
        }
        [TestMethod]
        public void InvalidUTCOfPosition()
        {
            INmeaMessageParseService parseService = new NmeaMessageParseService();
            string someLettersMessage = "$GLL,3953.88008971,N,10506.75318910,W,03ABC8.00,A,D*7A";
            string tooShortMessage = "$GLL,3953.88008971,N,10506.75318910,W,0338.00,A,D*7A";
            string tooLongMessage = "$GLL,3953.88008971,N,10506.75318910,W,03400138.00,A,D*7A";
            string hourLessThanRangeMessage = "$GLL,3953.88008971,N,10506.75318910,W,-34138.00,A,D*7A";
            string hourMoreThanRangeMessage = "$GLL,3953.88008971,N,10506.75318910,W,934138.00,A,D*7A";
            string minuteLessThanRangeMessage = "$GLL,3953.88008971,N,10506.75318910,W,03-138.00,A,D*7A";
            string minuteMoreThanRangeMessage = "$GLL,3953.88008971,N,10506.75318910,W,039938.00,A,D*7A";
            string secondLessThanRangeMessage = "$GLL,3953.88008971,N,10506.75318910,W,0341-8.00,A,D*7A";
            string secondMoreThanRangeMessage = "$GLL,3953.88008971,N,10506.75318910,W,034198.00,A,D*7A";

            Action someLettersAction = () => { parseService.Parse(someLettersMessage); };
            Action tooShortAction = () => { parseService.Parse(tooShortMessage); };
            Action tooLongAction = () => { parseService.Parse(tooLongMessage); };
            Action hourLessThanRangeAction = () => { parseService.Parse(hourLessThanRangeMessage); };
            Action hourMoreThanRangeAction = () => { parseService.Parse(hourMoreThanRangeMessage); };
            Action minuteLessThanRangeAction = () => { parseService.Parse(minuteLessThanRangeMessage); };
            Action minuteMoreThanRangeAction = () => { parseService.Parse(minuteMoreThanRangeMessage); };
            Action secondLessThanRangeAction = () => { parseService.Parse(secondLessThanRangeMessage); };
            Action secondMoreThanRangeAction = () => { parseService.Parse(secondMoreThanRangeMessage); };

            Assert.ThrowsException<ArgumentException>(someLettersAction);
            Assert.ThrowsException<ArgumentException>(tooShortAction);
            Assert.ThrowsException<ArgumentException>(tooLongAction);
            Assert.ThrowsException<ArgumentException>(hourLessThanRangeAction);
            Assert.ThrowsException<ArgumentException>(hourMoreThanRangeAction);
            Assert.ThrowsException<ArgumentException>(minuteLessThanRangeAction);
            Assert.ThrowsException<ArgumentException>(minuteMoreThanRangeAction);
            Assert.ThrowsException<ArgumentException>(secondLessThanRangeAction);
            Assert.ThrowsException<ArgumentException>(secondMoreThanRangeAction);
        }
        [TestMethod]
        public void ValidUTCOfPosition()
        {
            INmeaMessageParseService parseService = new NmeaMessageParseService();
            string correctMessage = "$GLL,3953.88008971,N,10506.75318910,W,034138.00,A,D*7A";
            string specialMessage60second = "$GLL,3953.88008971,N,10506.75318910,W,034160.00,A,D*7A";
            string specialMessage61second = "$GLL,3953.88008971,N,10506.75318910,W,034161.00,A,D*7A";
            string specialMessage62second = "$GLL,3953.88008971,N,10506.75318910,W,034162.00,A,D*7A";
            string specialMessage63second = "$GLL,3953.88008971,N,10506.75318910,W,034163.00,A,D*7A";

            GLL correctResult = (GLL)parseService.Parse(correctMessage);
            GLL specialMessage60secondResult = (GLL)parseService.Parse(specialMessage60second);
            GLL specialMessage61secondResult = (GLL)parseService.Parse(specialMessage61second);
            GLL specialMessage62secondResult = (GLL)parseService.Parse(specialMessage62second);
            GLL specialMessage63secondResult = (GLL)parseService.Parse(specialMessage63second);

            Assert.AreEqual(correctResult.UTCOfPosition.Value, new NmeaTimeOnly { Hour = 3, Minute = 41, Second = 38});
            Assert.AreEqual(correctResult.UTCOfPosition.ToString(), "03:41:38,00");

            Assert.AreEqual(specialMessage60secondResult.UTCOfPosition.Value, new NmeaTimeOnly { Hour = 3, Minute = 41, Second = 60 });
            Assert.AreEqual(specialMessage60secondResult.UTCOfPosition.ToString(), "Time stamp is not available");

            Assert.AreEqual(specialMessage61secondResult.UTCOfPosition.Value, new NmeaTimeOnly { Hour = 3, Minute = 41, Second = 61 });
            Assert.AreEqual(specialMessage61secondResult.UTCOfPosition.ToString(), "Positioning system is in manual input mode");

            Assert.AreEqual(specialMessage62secondResult.UTCOfPosition.Value, new NmeaTimeOnly { Hour = 3, Minute = 41, Second = 62 });
            Assert.AreEqual(specialMessage62secondResult.UTCOfPosition.ToString(), "Electronic Position Fixing System operates in estimated (dead reckoning) mode");

            Assert.AreEqual(specialMessage63secondResult.UTCOfPosition.Value, new NmeaTimeOnly { Hour = 3, Minute = 41, Second = 63 });
            Assert.AreEqual(specialMessage63secondResult.UTCOfPosition.ToString(), "Positioning system is inoperative");
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
