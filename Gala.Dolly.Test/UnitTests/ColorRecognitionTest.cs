using System;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Galatea.AI.Abstract;
using Galatea.IO;
using Galatea.Imaging.IO;

namespace Gala.Dolly.Test
{
    [TestClass]
    public class ColorRecognitionTest : TestBase
    {
        ICreator _creator;

        [TestMethod]
        [TestCategory("1 - Template")]
        public void TestColors()
        {
            bool result;
            _creator = null;

            // RED
            result = TestColorResponse(resourcesFolderName + @"Learning\STOP.png", "Red");
            Assert.IsTrue(result);
            // YELLOW
            result = TestColorResponse(resourcesFolderName + @"Learning\pacman.png", "Yellow");
            Assert.IsTrue(result);
            // GREEN
            result = TestColorResponse(resourcesFolderName + @"Learning\green_circle.png", "Green");
            Assert.IsTrue(result);
            // BLUE
            result = TestColorResponse(resourcesFolderName + @"Learning\Symbols\B.png", "Blue");
            Assert.IsTrue(result);
        }
        [TestMethod]
        [TestCategory("1 - Template")]
        public void TestColorLearning()
        {
            bool result;
            _creator = TestEngine.AI.RecognitionModel;

            string response;

            // ORANGE
            response = GetColorResponse(resourcesFolderName + @"Learning\triangle_orange.png");
            Assert.IsFalse(response.ToUpper().Contains("ORANGE"));

            TestEngine.ExecutiveFunctions.GetResponse(TestEngine.AI.LanguageModel, "The color is ORANGE!");

            // PURPLE
            response = GetColorResponse(resourcesFolderName + @"Learning\star2.png");
            Assert.IsFalse(response.ToUpper().Contains("PURPLE"));

            TestEngine.ExecutiveFunctions.GetResponse(TestEngine.AI.LanguageModel, "It's PURPLE!");

            // MIX IT UP
            TestColors();

            // NOW CHECK IF ORANGE AND PURPLE WERE LEARNED
            _creator = TestEngine.User;

            result = TestColorResponse(resourcesFolderName + @"Learning\orange_pie.png", "orange");
            Assert.IsTrue(result);

            result = TestColorResponse(resourcesFolderName + @"Learning\Symbols\C.png", "purple");
            Assert.IsTrue(result);
        }

        public ICreator Creator { get { return _creator; } set { _creator = value; } }

        internal bool TestColorResponse(string path, string expectedColorName)
        {
            string response = GetColorResponse(path);
            return CheckColorResponse(response, expectedColorName);
        }

        internal string GetColorResponse(string path)
        {
            ImagingContextStream stream = GetImagingContextStream(path);

            TestEngine.ExecutiveFunctions.StreamContext(TestEngine, TestEngine.Vision.ImageAnalyzer,
                ContextType.Machine, InputType.Visual, stream, typeof(Bitmap));

            string result = TestEngine.ExecutiveFunctions.GetResponse(TestEngine.AI.LanguageModel, "What color?");

            // Verify Creator
            Assert.IsTrue(CreatorExtension.Equals(_creator, NamedTemplate.Creator));

            // Finalize
            return result;
        }
        private bool CheckColorResponse(string response, string expectedColorName)
        {
            string expectedResponse = string.Format("The color is {0}.", expectedColorName);
            return response.Equals(expectedResponse, StringComparison.CurrentCultureIgnoreCase);
        }
    }
}
