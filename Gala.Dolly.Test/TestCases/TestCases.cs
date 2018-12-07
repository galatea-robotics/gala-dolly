#if !NETFX_CORE
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
#else
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Galatea.Drawing;
using Galatea.Drawing.Imaging;
using Windows.Storage;
#endif

namespace Gala.Dolly.Test
{
    [TestClass]
    public class TestCases : EntityTest
    {
        [ClassInitialize]
        public new static void Initialize(TestContext context)
        {
            EntityTest.Initialize(context);
        }

        [TestMethod]
        [TestCategory("Test Cases")]
        public void  _01_03_04_07_100()
        {
            // TODO:  Load the correct Database 

#if !NETFX_CORE
            Galatea.AI.Imaging.VisualProcessor.ImagingSettings.MonochromeBlobFilterSettings.ContrastCorrectionFactor = 15;
#endif
            string response = GetEntityResponse(resourcesFolderName + @"TestCases\1.3.4.7.100\orange_test.png", false);
            Assert.IsTrue(response.ToUpper().Contains("ORANGE TRIANGULAR"));
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