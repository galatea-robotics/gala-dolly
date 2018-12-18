using System;
using System.Drawing;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gala.Dolly.Test
{
    using Galatea.AI.Abstract;
    using Galatea.AI.Imaging;
    using Galatea.Imaging.IO;
    using Galatea.IO;

    [TestClass]
    [CLSCompliant(false)]
    public class ShapeRecognitionTest : TestBase
    {
        ICreator _creator;

        public ICreator Creator { get { return _creator; } set { _creator = value; } }

        //[TestMethod]
        //[TestCategory("1 - Template")]
        public void TestAllShapes()
        {
            TestRoundShapes();
            TestTriangularShapes();
            TestQuadShapes();
            TestChevronShapes();
        }

        [TestMethod]
        [TestCategory("1 - Template")]
        public void TestBlueCircle()
        {
            bool result;
            _creator = null;

            result = TestShapeResponse(ResourcesFolderName + @"Learning\blue_circle.png", "round");
            Assert.IsTrue(result);
        }

        [TestMethod]
        [TestCategory("1 - Template")]
        public void TestRoundShapes()
        {
            bool result;
            _creator = null;

            result = TestShapeResponse(ResourcesFolderName + @"Learning\blue_circle.png", "round");
            Assert.IsTrue(result);
            result = TestShapeResponse(ResourcesFolderName + @"Learning\green_circle.png", "round");
            Assert.IsTrue(result);
            result = TestShapeResponse(ResourcesFolderName + @"Learning\circle_perspective.png", "round");
            Assert.IsTrue(result);
        }

        [TestMethod]
        [TestCategory("1 - Template")]
        public void TestTriangularShapes()
        {
            bool result;
            _creator = null;

            // TRIANGULAR
            result = TestShapeResponse(ResourcesFolderName + @"Learning\triangle_green2.png", "triangular");
            Assert.IsTrue(result);
            result = TestShapeResponse(ResourcesFolderName + @"Learning\triangle_orange.png", "triangular");
            Assert.IsTrue(result);
        }

        [TestMethod]
        [TestCategory("1 - Template")]
        public void TestYellowTriangle()
        {
            bool result;
            _creator = null;

            result = TestShapeResponse(ResourcesFolderName + @"Learning\triangle_yellow.png", "triangular");
            Assert.IsTrue(result);
        }

        [TestMethod]
        [TestCategory("1 - Template")]
        public void TestQuadShapes()
        {
            bool result;
            _creator = null;

            // QUAD
            result = TestShapeResponse(ResourcesFolderName + @"Learning\quad_black.png", "FOUR CORNERS");
            Assert.IsTrue(result);
            result = TestShapeResponse(ResourcesFolderName + @"Learning\quad_green.png", "FOUR CORNERS");
            Assert.IsTrue(result);
        }

        [TestMethod]
        [TestCategory("1 - Template")]
        public void TestChevronShapes()
        {
            bool result;
            _creator = null;

            // CHEVRON
            result = TestShapeResponse(ResourcesFolderName + @"Learning\chevron_purple.png", "Chevron");
            Assert.IsTrue(result);
            result = TestShapeResponse(ResourcesFolderName + @"Learning\chevron.png", "Chevron");
            Assert.IsTrue(result);
            result = TestShapeResponse(ResourcesFolderName + @"Learning\widget.png", "Chevron");
            Assert.IsTrue(result);
        }

        [TestMethod]
        [TestCategory("1 - Template")]
        public void TestShapeLearning()
        {
            bool result;
            _creator = null;

            string response;

            // PIE
            response = GetShapeResponse(ResourcesFolderName + @"Learning\pizza.png");
            Assert.IsFalse(response.Contains("PIE SHAPED", StringComparison.CurrentCultureIgnoreCase));

            TestEngine.ExecutiveFunctions.GetResponse(TestEngine.AI.LanguageModel, "The shape is PIE SHAPED!");

            // STAR
            response = GetShapeResponse(ResourcesFolderName + @"Learning\star2.png");
            Assert.IsFalse(response.Contains("STAR", StringComparison.CurrentCultureIgnoreCase));

            TestEngine.ExecutiveFunctions.GetResponse(TestEngine.AI.LanguageModel, "The shape is STAR!");

            // MIX IT UP
            TestAllShapes();

            // NOW CHECK IF PIE AND STAR WERE LEARNED
            _creator = TestEngine.User;

            result = TestShapeResponse(ResourcesFolderName + @"Learning\pacman.png", "PIE SHAPED");
            Assert.IsTrue(result);
            result = TestShapeResponse(ResourcesFolderName + @"Learning\orange_pie.png", "PIE SHAPED");
            Assert.IsTrue(result);
            result = TestShapeResponse(ResourcesFolderName + @"Learning\pizza.png", "PIE SHAPED");
            Assert.IsTrue(result);

            result = TestShapeResponse(ResourcesFolderName + @"Learning\STAR_BLUE.png", "STAR");
            Assert.IsTrue(result);
            result = TestShapeResponse(ResourcesFolderName + @"Learning\star2.png", "STAR");
            Assert.IsTrue(result);
            result = TestShapeResponse(ResourcesFolderName + @"Learning\star.png", "STAR");
            Assert.IsTrue(result);

            // TEACH ADDITIONAL SHAPES
            _creator = null;
            response = GetShapeResponse(ResourcesFolderName + @"Learning\pentagon.png");
            TestEngine.ExecutiveFunctions.GetResponse(TestEngine.AI.LanguageModel, "It's a Pentagon.");

            _creator = TestEngine.User;  // Evaluates to Pentagon
            response = GetShapeResponse(ResourcesFolderName + @"Learning\hexagon.png");
            TestEngine.ExecutiveFunctions.GetResponse(TestEngine.AI.LanguageModel, "It's a Hexagon.");
            response = GetShapeResponse(ResourcesFolderName + @"Learning\STOP.png");
            TestEngine.ExecutiveFunctions.GetResponse(TestEngine.AI.LanguageModel, "It's an Octagon.");
        }

        private bool TestShapeResponse(string filename, string expectedShapeName)
        {
            string response = GetShapeResponse(filename);
            return CheckShapeResponse(response, expectedShapeName);
        }
        internal string GetShapeResponse(string filename)
        {
            ImagingContextStream stream;

            using (Bitmap bitmap = new Bitmap(filename))
            {
                if (VisualProcessor.ImagingSettings.DebugRecognitionSaveImages)
                {
                    bitmap.Save("bitmap.png", System.Drawing.Imaging.ImageFormat.Png);
                }
                stream = ImagingContextStream.FromImage(bitmap);
            }

             TestEngine.ExecutiveFunctions.StreamContext(TestEngine, TestEngine.Vision.ImageAnalyzer,
                ContextType.Machine, InputType.Visual, stream, typeof(Bitmap));

            string result = TestEngine.ExecutiveFunctions.GetResponse(TestEngine.AI.LanguageModel, "What SHAPE?");

            // Verify Creator
            if (NamedTemplate.FriendlyName.Equals("I don't know", StringComparison.CurrentCultureIgnoreCase))
            {
                // Creator is Recognition Model
            }
            else
            {
                Assert.AreEqual(_creator, NamedTemplate.Creator);
            }

            // Finalize
            return result;
        }
        private static bool CheckShapeResponse(string response, string expectedShapeName)
        {
            string expectedResponse = $"The shape is {expectedShapeName}.";
            return response.Equals(expectedResponse, StringComparison.CurrentCultureIgnoreCase);
        }
    }
}
