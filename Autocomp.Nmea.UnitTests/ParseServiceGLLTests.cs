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
            Dictionary<string, string> resultDictionary = result.ToDictionary();

            AreDataEquals(result, 3953.88008971, Directions.North, 10506.75318910, Directions.West, new NmeaTimeOnly { Hour = 3, Minute = 41, Second = 38.00 }, DataStatus.A, Indicator.D);

            Assert.AreEqual(5, resultDictionary.Count);

            Assert.AreEqual("Latitude", resultDictionary.ElementAt(0).Key);
            Assert.AreEqual("39 degrees, 53,88 minutes North", resultDictionary.ElementAt(0).Value);

            Assert.AreEqual("Longitude", resultDictionary.ElementAt(1).Key);
            Assert.AreEqual("105 degrees, 06,75 minutes West", resultDictionary.ElementAt(1).Value);

            Assert.AreEqual("Time stamp UTC", resultDictionary.ElementAt(2).Key);
            Assert.AreEqual("03:41:38,00", resultDictionary.ElementAt(2).Value);

            Assert.AreEqual("Data status", resultDictionary.ElementAt(3).Key);
            Assert.AreEqual("Data valid", resultDictionary.ElementAt(3).Value);

            Assert.AreEqual("Mode indicator", resultDictionary.ElementAt(4).Key);
            Assert.AreEqual("Differential mode", resultDictionary.ElementAt(4).Value);
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

            Assert.AreEqual(3953.88008971, normalLatitudeResult.Latitude.Value);
            Assert.AreEqual("39 degrees, 53,88 minutes", normalLatitudeResult.Latitude.ToString());

            Assert.AreEqual(9100, specialLatitudeResult.Latitude.Value);
            Assert.AreEqual("Latitude not available", specialLatitudeResult.Latitude.ToString());
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

            Assert.AreEqual(Directions.North, latitudeDirectionNResult.LatitudeDirection.Value);
            Assert.AreEqual("North", latitudeDirectionNResult.LatitudeDirection.ToString());

            Assert.AreEqual(Directions.South, latitudeDirectionSResult.LatitudeDirection.Value);
            Assert.AreEqual("South", latitudeDirectionSResult.LatitudeDirection.ToString());
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

            Assert.AreEqual(10506.75318910, normalLatitudeResult.Longitude.Value);
            Assert.AreEqual("105 degrees, 06,75 minutes", normalLatitudeResult.Longitude.ToString());

            Assert.AreEqual(18100, specialLatitudeResult.Longitude.Value);
            Assert.AreEqual("Longitude not available", specialLatitudeResult.Longitude.ToString());
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

            Assert.AreEqual(Directions.West, longitudeDirectionWResult.LongitudeDirection.Value);
            Assert.AreEqual("West", longitudeDirectionWResult.LongitudeDirection.ToString());

            Assert.AreEqual(Directions.East, longitudeDirectionEResult.LongitudeDirection.Value);
            Assert.AreEqual("East", longitudeDirectionEResult.LongitudeDirection.ToString());
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

            Assert.AreEqual(new NmeaTimeOnly { Hour = 3, Minute = 41, Second = 38 }, correctResult.UTCOfPosition.Value);
            Assert.AreEqual("03:41:38,00", correctResult.UTCOfPosition.ToString());

            Assert.AreEqual(new NmeaTimeOnly { Hour = 3, Minute = 41, Second = 60 }, specialMessage60secondResult.UTCOfPosition.Value);
            Assert.AreEqual("Time stamp is not available", specialMessage60secondResult.UTCOfPosition.ToString());

            Assert.AreEqual(new NmeaTimeOnly { Hour = 3, Minute = 41, Second = 61 }, specialMessage61secondResult.UTCOfPosition.Value);
            Assert.AreEqual("Positioning system is in manual input mode", specialMessage61secondResult.UTCOfPosition.ToString());

            Assert.AreEqual(new NmeaTimeOnly { Hour = 3, Minute = 41, Second = 62 }, specialMessage62secondResult.UTCOfPosition.Value);
            Assert.AreEqual("Electronic Position Fixing System operates in estimated (dead reckoning) mode", specialMessage62secondResult.UTCOfPosition.ToString());

            Assert.AreEqual(new NmeaTimeOnly { Hour = 3, Minute = 41, Second = 63 }, specialMessage63secondResult.UTCOfPosition.Value);
            Assert.AreEqual("Positioning system is inoperative", specialMessage63secondResult.UTCOfPosition.ToString());
        }
        [TestMethod]
        public void InvalidStatus()
        {
            INmeaMessageParseService parseService = new NmeaMessageParseService();
            string invalidStatusMessage = "$GLL,3953.88008971,N,10506.75318910,W,034138.00,G,D*7A";

            Action invalidStatusAction = () => { parseService.Parse(invalidStatusMessage); };

            Assert.ThrowsException<ArgumentException>(invalidStatusAction);
        }
        [TestMethod]
        public void ValidStatus()
        {
            INmeaMessageParseService parseService = new NmeaMessageParseService();
            string statusAMessage = "$GLL,3953.88008971,N,10506.75318910,W,034138.00,A,D*7A";
            string statusVMessage = "$GLL,3953.88008971,N,10506.75318910,W,034138.00,V,D*7A";

            GLL statusAResult = (GLL)parseService.Parse(statusAMessage);
            GLL statusVResult = (GLL)parseService.Parse(statusVMessage);

            Assert.AreEqual(DataStatus.A, statusAResult.Status.Value);
            Assert.AreEqual("Data valid", statusAResult.Status.ToString());

            Assert.AreEqual(DataStatus.V, statusVResult.Status.Value);
            Assert.AreEqual("Data not valid", statusVResult.Status.ToString());
        }
        [TestMethod]
        public void InvalidModeIndicator()
        {
            INmeaMessageParseService parseService = new NmeaMessageParseService();
            string invalidModeIndicatorMessage = "$GLL,3953.88008971,N,10506.75318910,W,034138.00,A,Z*7A";

            Action invalidModeIndicatorAction = () => { parseService.Parse(invalidModeIndicatorMessage); };

            Assert.ThrowsException<ArgumentException>(invalidModeIndicatorAction);
        }
        [TestMethod]
        public void ValidModeIndicator()
        {
            INmeaMessageParseService parseService = new NmeaMessageParseService();
            string modeIndicatorAMessage = "$GLL,3953.88008971,N,10506.75318910,W,034138.00,A,A*7A";
            string modeIndicatorDMessage = "$GLL,3953.88008971,N,10506.75318910,W,034138.00,A,D*7A";
            string modeIndicatorEMessage = "$GLL,3953.88008971,N,10506.75318910,W,034138.00,A,E*7A";
            string modeIndicatorMMessage = "$GLL,3953.88008971,N,10506.75318910,W,034138.00,A,M*7A";
            string modeIndicatorSMessage = "$GLL,3953.88008971,N,10506.75318910,W,034138.00,A,S*7A";
            string modeIndicatorNMessage = "$GLL,3953.88008971,N,10506.75318910,W,034138.00,A,N*7A";

            GLL modeIndicatorAResult = (GLL)parseService.Parse(modeIndicatorAMessage);
            GLL modeIndicatorDResult = (GLL)parseService.Parse(modeIndicatorDMessage);
            GLL modeIndicatorEResult = (GLL)parseService.Parse(modeIndicatorEMessage);
            GLL modeIndicatorMResult = (GLL)parseService.Parse(modeIndicatorMMessage);
            GLL modeIndicatorSResult = (GLL)parseService.Parse(modeIndicatorSMessage);
            GLL modeIndicatorNResult = (GLL)parseService.Parse(modeIndicatorNMessage);

            Assert.AreEqual(Indicator.A, modeIndicatorAResult.ModeIndicator.Value);
            Assert.AreEqual("Autonomous mode", modeIndicatorAResult.ModeIndicator.ToString());

            Assert.AreEqual(Indicator.D, modeIndicatorDResult.ModeIndicator.Value);
            Assert.AreEqual("Differential mode", modeIndicatorDResult.ModeIndicator.ToString());

            Assert.AreEqual(Indicator.E, modeIndicatorEResult.ModeIndicator.Value);
            Assert.AreEqual("Estimated (dead reckoning) mode", modeIndicatorEResult.ModeIndicator.ToString());

            Assert.AreEqual(Indicator.M, modeIndicatorMResult.ModeIndicator.Value);
            Assert.AreEqual("Manual input mode", modeIndicatorMResult.ModeIndicator.ToString());

            Assert.AreEqual(Indicator.S, modeIndicatorSResult.ModeIndicator.Value);
            Assert.AreEqual("Simulator mode", modeIndicatorSResult.ModeIndicator.ToString());

            Assert.AreEqual(Indicator.N, modeIndicatorNResult.ModeIndicator.Value);
            Assert.AreEqual("Data not valid", modeIndicatorNResult.ModeIndicator.ToString());
        }
        private void AreDataEquals(GLL result, double latitude, Directions latitudeDirection, double longitude, Directions longitudeDirection, NmeaTimeOnly utcOfPosition, DataStatus dataStatus, Indicator modeIndicator)
        {
            Assert.AreEqual(latitude, result.Latitude.Value);
            Assert.AreEqual(latitudeDirection, result.LatitudeDirection.Value);
            Assert.AreEqual(longitude, result.Longitude.Value);
            Assert.AreEqual(longitudeDirection, result.LongitudeDirection.Value);
            Assert.AreEqual(utcOfPosition, result.UTCOfPosition.Value);
            Assert.AreEqual(dataStatus, result.Status.Value);
            Assert.AreEqual(modeIndicator, result.ModeIndicator.Value);
        }

        private void AreDataNotEquals(GLL result, double latitude, Directions latitudeDirection, double longitude, Directions longitudeDirection, NmeaTimeOnly utcOfPosition, DataStatus dataStatus, Indicator modeIndicator)
        {
            Assert.AreNotEqual(latitude, result.Latitude.Value);
            Assert.AreNotEqual(latitudeDirection, result.LatitudeDirection.Value);
            Assert.AreNotEqual(longitude, result.Longitude.Value);
            Assert.AreNotEqual(longitudeDirection, result.LongitudeDirection.Value);
            Assert.AreNotEqual(utcOfPosition, result.UTCOfPosition.Value);
            Assert.AreNotEqual(dataStatus, result.Status.Value);
            Assert.AreNotEqual(modeIndicator, result.ModeIndicator.Value);
        }
    }
}
