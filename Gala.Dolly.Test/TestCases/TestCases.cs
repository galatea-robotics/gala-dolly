using System;
using System.Globalization;

#if !NETFX_CORE
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
#else
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
#endif

namespace Gala.Dolly.Test
{
    [TestClass]
    [CLSCompliant(false)]
    public class TestCases : EntityTest
    {
        [ClassInitialize]
        public new static void Initialize(TestContext context)
        {
            EntityTest.Initialize(context);
        }

        [TestMethod]
        [TestCategory("Test Cases")]
#pragma warning disable CA1707 // Identifiers should not contain underscores
        public void  _01_03_04_07_100()
#pragma warning restore CA1707 // Identifiers should not contain underscores
        {
            // TODO:  Load the correct Database 

#if !NETFX_CORE
            Galatea.AI.Imaging.VisualProcessor.ImagingSettings.MonochromeBlobFilterSettings.ContrastCorrectionFactor = 15;
#endif
            string response = GetEntityResponse(ResourcesFolderName + @"TestCases\1.3.4.7.100\orange_test.png", false);
            Assert.IsTrue(response.ToUpper(CultureInfo.CurrentCulture).Contains("ORANGE TRIANGULAR"));
        }

        public override void TestEntityLabelCreation()
        {
            throw new System.NotImplementedException();
        }
        public override void TestPacMan()
        {
            throw new System.NotImplementedException();
        }
        public override void TestStopSign()
        {
            throw new System.NotImplementedException();
        }
    }
}