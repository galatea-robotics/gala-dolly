using System;
using System.Drawing;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gala.Dolly.Test
{
    using Galatea.AI.Abstract;
    using Galatea.Imaging.IO;
    using Galatea.IO;

    [TestClass]
    [CLSCompliant(false)]
    public class ColorRecognitionTest : TestBase
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Console.WriteLine(System.String)")]
        [TestCategory("0 - Methods")]
        public void TestBrightness()
        {
            Console.WriteLine($"Red: {Color.Red.GetBrightness()}");
            Console.WriteLine($"Pink: {Color.Pink.GetBrightness()}");
            Console.WriteLine($"Maroon: {Color.Maroon.GetBrightness()}");
            Console.WriteLine($"Black: {Color.Black.GetBrightness()}");
            Console.WriteLine($"White: {Color.White.GetBrightness()}");
        }

        [TestMethod]
        [TestCategory("1 - Template")]
        public void TestColors()
        {
            bool result;
            Creator = null;

            // RED
            result = TestColorResponse(ResourcesFolderName + @"Learning\STOP.png", "Red");
            Assert.IsTrue(result);
            // YELLOW
            result = TestColorResponse(ResourcesFolderName + @"Learning\pacman.png", "Yellow");
            Assert.IsTrue(result);
            // GREEN
            result = TestColorResponse(ResourcesFolderName + @"Learning\green_circle.png", "Green");
            Assert.IsTrue(result);
            // BLUE
            result = TestColorResponse(ResourcesFolderName + @"Learning\Symbols\B.png", "Blue");
            Assert.IsTrue(result);
        }
        [TestMethod]
        [TestCategory("1 - Template")]
        public void TestColorLearning()
        {
            bool result;
            Creator = TestEngine.AI.RecognitionModel;

            string response;

            // ORANGE
            response = GetColorResponse(ResourcesFolderName + @"Learning\triangle_orange.png");
            Assert.IsFalse(response.ToUpper(CultureInfo.CurrentCulture).Contains("ORANGE"));

            TestEngine.ExecutiveFunctions.GetResponse(TestEngine.AI.LanguageModel, "The color is ORANGE!");

            // PURPLE
            response = GetColorResponse(ResourcesFolderName + @"Learning\star2.png");
            Assert.IsFalse(response.ToUpper(CultureInfo.CurrentCulture).Contains("PURPLE"));

            TestEngine.ExecutiveFunctions.GetResponse(TestEngine.AI.LanguageModel, "It's PURPLE!");

            // MIX IT UP
            TestColors();

            // NOW CHECK IF ORANGE AND PURPLE WERE LEARNED
            Creator = TestEngine.User;

            result = TestColorResponse(ResourcesFolderName + @"Learning\orange_pie.png", "orange");
            Assert.IsTrue(result);

            result = TestColorResponse(ResourcesFolderName + @"Learning\Symbols\C.png", "purple");
            Assert.IsTrue(result);
        }

        public ICreator Creator { get; set; }

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
            Assert.IsTrue(CreatorExtension.Equals(Creator, NamedTemplate.Creator));

            // Finalize
            return result;
        }
        private static bool CheckColorResponse(string response, string expectedColorName)
        {
            string expectedResponse = $"The color is {expectedColorName}.";
            return response.Equals(expectedResponse, StringComparison.CurrentCultureIgnoreCase);
        }
    }
}
