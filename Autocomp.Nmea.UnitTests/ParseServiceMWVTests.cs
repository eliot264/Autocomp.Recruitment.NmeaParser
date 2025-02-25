﻿using Autocomp.Nmea.Common.Services;
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

            Assert.AreEqual("Wind Speed and Angle", result.Name);

            Assert.AreEqual(4, resultDictionary.Count);

            Assert.AreEqual("Angle", resultDictionary.ElementAt(0).Key);
            Assert.AreEqual("320,00 degrees", resultDictionary.ElementAt(0).Value);

            Assert.AreEqual("Data reference", resultDictionary.ElementAt(1).Key);
            Assert.AreEqual("Relative", resultDictionary.ElementAt(1).Value);

            Assert.AreEqual("Speed", resultDictionary.ElementAt(2).Key);
            Assert.AreEqual("15,00 m/s", resultDictionary.ElementAt(2).Value);

            Assert.AreEqual("Data status", resultDictionary.ElementAt(3).Key);
            Assert.AreEqual("Data valid", resultDictionary.ElementAt(3).Value);
        }
        [TestMethod]
        public void InvalidMessageNumberOfFieldsTest()
        {
            INmeaMessageParseService parseService = new NmeaMessageParseService();
            string moreFieldsMessage = "$MWV,320,320,R,15.0,M,A*0B";
            string lessFieldsMessage = "$MWV,R,15.0,M,A*0B";

            Action moreFieldsParse = () => { parseService.Parse(moreFieldsMessage); };
            Action lessFieldsParse = () => { parseService.Parse(lessFieldsMessage); };

            Assert.ThrowsException<ArgumentException>(moreFieldsParse);
            Assert.ThrowsException<ArgumentException>(lessFieldsParse);
        }
        [TestMethod]
        public void InvalidAngle()
        {
            INmeaMessageParseService parseService = new NmeaMessageParseService();
            string twoDotsMessage = "$MWV,320.0.0,R,15.0,M,A*0B";
            string someLettersMessage = "$MWV,32ABC0,R,15.0,M,A*0B";
            string lessThanRangeMessage = "$MWV,-320,R,15.0,M,A*0B";
            string moreThanRangeMessage = "$MWV,520,R,15.0,M,A*0B";

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
        public void ValidAngle()
        {
            INmeaMessageParseService parseService = new NmeaMessageParseService();
            string correctAngleMessage = "$MWV,320,R,15.0,M,A*0B";

            MWV correctAngleResult = (MWV)parseService.Parse(correctAngleMessage);

            Assert.AreEqual(320.0, correctAngleResult.Angle.Value);
            Assert.AreEqual("320,00 degrees", correctAngleResult.Angle.ToString());
        }
        [TestMethod]
        public void InvalidReference()
        {
            INmeaMessageParseService parseService = new NmeaMessageParseService();
            string invalidReferenceMessage = "$MWV,320,Z,15.0,M,A*0B";

            Action invalidReferenceAction = () => { parseService.Parse(invalidReferenceMessage); };

            Assert.ThrowsException<ArgumentException>(invalidReferenceAction);
        }
        [TestMethod]
        public void ValidReference()
        {
            INmeaMessageParseService parseService = new NmeaMessageParseService();
            string referenceRMessage = "$MWV,320,R,15.0,M,A*0B";
            string referenceTMessage = "$MWV,320,T,15.0,M,A*0B";

            MWV referenceRResult = (MWV)parseService.Parse(referenceRMessage);
            MWV referenceTResult = (MWV)parseService.Parse(referenceTMessage);

            Assert.AreEqual(DataReference.R, referenceRResult.Reference.Value);
            Assert.AreEqual("Relative", referenceRResult.Reference.ToString());

            Assert.AreEqual(DataReference.T, referenceTResult.Reference.Value);
            Assert.AreEqual("Theoretical", referenceTResult.Reference.ToString());
        }
        [TestMethod]
        public void InvalidSpeed()
        {
            INmeaMessageParseService parseService = new NmeaMessageParseService();
            string twoDotsMessage = "$MWV,320,R,15.1.0,M,A*0B";
            string someLettersMessage = "$MWV,320,R,1ABC5.0,M,A*0B";
            string lessThanRangeMessage = "$MWV,320,R,-15.0,M,A*0B";

            Action twoDotsParse = () => { parseService.Parse(twoDotsMessage); };
            Action someLettersParse = () => { parseService.Parse(someLettersMessage); };
            Action lessThanRangeParse = () => { parseService.Parse(lessThanRangeMessage); };

            Assert.ThrowsException<ArgumentException>(twoDotsParse);
            Assert.ThrowsException<ArgumentException>(someLettersParse);
            Assert.ThrowsException<ArgumentException>(lessThanRangeParse);
        }
        [TestMethod]
        public void ValidSpeed()
        {
            INmeaMessageParseService parseService = new NmeaMessageParseService();
            string correctAngleMessage = "$MWV,320,R,15.0,M,A*0B";

            MWV correctAngleResult = (MWV)parseService.Parse(correctAngleMessage);

            Assert.AreEqual(15.0, correctAngleResult.Speed.Value);
            Assert.AreEqual("15,00", correctAngleResult.Speed.ToString());
        }
        [TestMethod]
        public void InvalidSpeedUnit()
        {
            INmeaMessageParseService parseService = new NmeaMessageParseService();
            string invalidReferenceMessage = "$MWV,320,R,15.0,Z,A*0B";

            Action invalidReferenceAction = () => { parseService.Parse(invalidReferenceMessage); };

            Assert.ThrowsException<ArgumentException>(invalidReferenceAction);
        }
        [TestMethod]
        public void ValidSpeedUnit()
        {
            INmeaMessageParseService parseService = new NmeaMessageParseService();
            string speedUnitKMessage = "$MWV,320,R,15.0,K,A*0B";
            string speedUnitMMessage = "$MWV,320,R,15.0,M,A*0B";
            string speedUnitNMessage = "$MWV,320,R,15.0,N,A*0B";
            string speedUnitSMessage = "$MWV,320,R,15.0,S,A*0B";

            MWV speedUnitKResult = (MWV)parseService.Parse(speedUnitKMessage);
            MWV speedUnitMResult = (MWV)parseService.Parse(speedUnitMMessage);
            MWV speedUnitNResult = (MWV)parseService.Parse(speedUnitNMessage);
            MWV speedUnitSResult = (MWV)parseService.Parse(speedUnitSMessage);

            Assert.AreEqual(Unit.K, speedUnitKResult.SpeedUnit.Value);
            Assert.AreEqual("km/h", speedUnitKResult.SpeedUnit.ToString());

            Assert.AreEqual(Unit.M, speedUnitMResult.SpeedUnit.Value);
            Assert.AreEqual("m/s", speedUnitMResult.SpeedUnit.ToString());

            Assert.AreEqual(Unit.N, speedUnitNResult.SpeedUnit.Value);
            Assert.AreEqual("knots", speedUnitNResult.SpeedUnit.ToString());

            Assert.AreEqual(Unit.S, speedUnitSResult.SpeedUnit.Value);
            Assert.AreEqual("statute miles/h", speedUnitSResult.SpeedUnit.ToString());
        }
        [TestMethod]
        public void InvalidStatus()
        {
            INmeaMessageParseService parseService = new NmeaMessageParseService();
            string invalidStatusMessage = "$MWV,320,R,15.0,K,Z*0B";

            Action invalidStatusAction = () => { parseService.Parse(invalidStatusMessage); };

            Assert.ThrowsException<ArgumentException>(invalidStatusAction);
        }
        [TestMethod]
        public void ValidStatus()
        {
            INmeaMessageParseService parseService = new NmeaMessageParseService();
            string statusAMessage = "$MWV,320,R,15.0,K,A*0B";
            string statusVMessage = "$MWV,320,R,15.0,K,V*0B";

            MWV statusAResult = (MWV)parseService.Parse(statusAMessage);
            MWV statusVResult = (MWV)parseService.Parse(statusVMessage);

            Assert.AreEqual(DataStatus.A, statusAResult.Status.Value);
            Assert.AreEqual("Data valid", statusAResult.Status.ToString());

            Assert.AreEqual(DataStatus.V, statusVResult.Status.Value);
            Assert.AreEqual("Data not valid", statusVResult.Status.ToString());
        }
    }
}
