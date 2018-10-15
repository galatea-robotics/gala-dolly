using System;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Galatea.IO;
using Galatea.Imaging.IO;

namespace Gala.Dolly.Test
{
    using Galatea.AI.Abstract;

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
            result = TestColorResponse(@"..\..\..\Resources\Learning\STOP.png", "Red");
            Assert.IsTrue(result);
            // YELLOW
            result = TestColorResponse(@"..\..\..\Resources\Learning\pacman.png", "Yellow");
            Assert.IsTrue(result);
            // GREEN
            result = TestColorResponse(@"..\..\..\Resources\Learning\green_circle.png", "Green");
            Assert.IsTrue(result);
            // BLUE
            result = TestColorResponse(@"..\..\..\Resources\Learning\Symbols\B.png", "Blue");
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
            response = GetColorResponse(@"..\..\..\Resources\Learning\triangle_orange.png");
            Assert.IsFalse(response.ToUpper().Contains("ORANGE"));

            TestEngine.ExecutiveFunctions.GetResponse(TestEngine.AI.LanguageModel, User, "The color is ORANGE!");

            // PURPLE
            response = GetColorResponse(@"..\..\..\Resources\Learning\star2.png");
            Assert.IsFalse(response.ToUpper().Contains("PURPLE"));

            TestEngine.ExecutiveFunctions.GetResponse(TestEngine.AI.LanguageModel, User, "It's PURPLE!");

            // MIX IT UP
            TestColors();

            // NOW CHECK IF ORANGE AND PURPLE WERE LEARNED
            _creator = TestEngine.User;

            result = TestColorResponse(@"..\..\..\Resources\Learning\orange_pie.png", "orange");
            Assert.IsTrue(result);

            result = TestColorResponse(@"..\..\..\Resources\Learning\Symbols\C.png", "purple");
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
            Bitmap bitmap = new Bitmap(path);
            ImagingContextStream stream = ImagingContextStream.FromBitmap(bitmap);

            TestEngine.ExecutiveFunctions.StreamContext(TestEngine, TestEngine.Vision.ImageAnalyzer,
                ContextType.Machine, InputType.Visual, stream, typeof(Bitmap));

            string result = TestEngine.ExecutiveFunctions.GetResponse(TestEngine.AI.LanguageModel, User, "What color?");

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
