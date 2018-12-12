using System;
using System.ComponentModel;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Galatea;
using Galatea.AI.Abstract;
using Galatea.AI.Imaging;
using Galatea.IO;
using Galatea.Imaging.IO;

namespace Gala.Dolly.Test
{
    [TestClass]
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

            result = TestShapeResponse(resourcesFolderName + @"Learning\blue_circle.png", "round");
            Assert.IsTrue(result);
        }

        [TestMethod]
        [TestCategory("1 - Template")]
        public void TestRoundShapes()
        {
            bool result;
            _creator = null;

            result = TestShapeResponse(resourcesFolderName + @"Learning\blue_circle.png", "round");
            Assert.IsTrue(result);
            result = TestShapeResponse(resourcesFolderName + @"Learning\green_circle.png", "round");
            Assert.IsTrue(result);
            result = TestShapeResponse(resourcesFolderName + @"Learning\circle_perspective.png", "round");
            Assert.IsTrue(result);
        }

        [TestMethod]
        [TestCategory("1 - Template")]
        public void TestTriangularShapes()
        {
            bool result;
            _creator = null;

            // TRIANGULAR
            result = TestShapeResponse(resourcesFolderName + @"Learning\triangle_green2.png", "triangular");
            Assert.IsTrue(result);
            result = TestShapeResponse(resourcesFolderName + @"Learning\triangle_orange.png", "triangular");
            Assert.IsTrue(result);
        }

        [TestMethod]
        [TestCategory("1 - Template")]
        public void TestYellowTriangle()
        {
            bool result;
            _creator = null;

            result = TestShapeResponse(resourcesFolderName + @"Learning\triangle_yellow.png", "triangular");
            Assert.IsTrue(result);
        }

        [TestMethod]
        [TestCategory("1 - Template")]
        public void TestQuadShapes()
        {
            bool result;
            _creator = null;

            // QUAD
            result = TestShapeResponse(resourcesFolderName + @"Learning\quad_black.png", "FOUR CORNERS");
            Assert.IsTrue(result);
            result = TestShapeResponse(resourcesFolderName + @"Learning\quad_green.png", "FOUR CORNERS");
            Assert.IsTrue(result);
        }

        [TestMethod]
        [TestCategory("1 - Template")]
        public void TestChevronShapes()
        {
            bool result;
            _creator = null;

            // CHEVRON
            result = TestShapeResponse(resourcesFolderName + @"Learning\chevron_purple.png", "Chevron");
            Assert.IsTrue(result);
            result = TestShapeResponse(resourcesFolderName + @"Learning\chevron.png", "Chevron");
            Assert.IsTrue(result);
            result = TestShapeResponse(resourcesFolderName + @"Learning\widget.png", "Chevron");
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
            response = GetShapeResponse(resourcesFolderName + @"Learning\pizza.png");
            Assert.IsFalse(response.ToUpper().Contains("PIE SHAPED"));

            TestEngine.ExecutiveFunctions.GetResponse(TestEngine.AI.LanguageModel, "The shape is PIE SHAPED!");

            // STAR
            response = GetShapeResponse(resourcesFolderName + @"Learning\star2.png");
            Assert.IsFalse(response.ToUpper().Contains("STAR"));

            TestEngine.ExecutiveFunctions.GetResponse(TestEngine.AI.LanguageModel, "The shape is STAR!");

            // MIX IT UP
            TestAllShapes();

            // NOW CHECK IF PIE AND STAR WERE LEARNED
            _creator = TestEngine.User;

            result = TestShapeResponse(resourcesFolderName + @"Learning\pacman.png", "PIE SHAPED");
            Assert.IsTrue(result);
            result = TestShapeResponse(resourcesFolderName + @"Learning\orange_pie.png", "PIE SHAPED");
            Assert.IsTrue(result);
            result = TestShapeResponse(resourcesFolderName + @"Learning\pizza.png", "PIE SHAPED");
            Assert.IsTrue(result);

            result = TestShapeResponse(resourcesFolderName + @"Learning\STAR_BLUE.png", "STAR");
            Assert.IsTrue(result);
            result = TestShapeResponse(resourcesFolderName + @"Learning\star2.png", "STAR");
            Assert.IsTrue(result);
            result = TestShapeResponse(resourcesFolderName + @"Learning\star.png", "STAR");
            Assert.IsTrue(result);

            // TEACH ADDITIONAL SHAPES
            _creator = null;
            response = GetShapeResponse(resourcesFolderName + @"Learning\pentagon.png");
            TestEngine.ExecutiveFunctions.GetResponse(TestEngine.AI.LanguageModel, "It's a Pentagon.");

            _creator = TestEngine.User;  // Evaluates to Pentagon
            response = GetShapeResponse(resourcesFolderName + @"Learning\hexagon.png");
            TestEngine.ExecutiveFunctions.GetResponse(TestEngine.AI.LanguageModel, "It's a Hexagon.");
            response = GetShapeResponse(resourcesFolderName + @"Learning\STOP.png");
            TestEngine.ExecutiveFunctions.GetResponse(TestEngine.AI.LanguageModel, "It's an Octagon.");
        }

        private bool TestShapeResponse(string filename, string expectedShapeName)
        {
            string response = GetShapeResponse(filename);
            return CheckShapeResponse(response, expectedShapeName);
        }
        internal string GetShapeResponse(string filename)
        {
            Bitmap bitmap = new Bitmap(filename);
            if (VisualProcessor.ImagingSettings.DebugRecognitionSaveImages)
            {
                bitmap.Save("bitmap.png", System.Drawing.Imaging.ImageFormat.Png);
            }
            ImagingContextStream stream = ImagingContextStream.FromImage(bitmap);

            TestEngine.ExecutiveFunctions.StreamContext(TestEngine, TestEngine.Vision.ImageAnalyzer,
                ContextType.Machine, InputType.Visual, stream, typeof(Bitmap));

            string result = TestEngine.ExecutiveFunctions.GetResponse(TestEngine.AI.LanguageModel, "What SHAPE?");
            //System.Console.WriteLine($"Result: {result}");

            // Verify Creator
            if (this.NamedTemplate.FriendlyName.Equals("I don't know", StringComparison.CurrentCultureIgnoreCase))
            {
                // Creator is Recognition Model
            }
            else
            {
                Assert.AreEqual(_creator, this.NamedTemplate.Creator);
            }

            // Finalize
            return result;
        }
        private bool CheckShapeResponse(string response, string expectedShapeName)
        {
            string expectedResponse = string.Format("The shape is {0}.", expectedShapeName);
            return response.Equals(expectedResponse, StringComparison.CurrentCultureIgnoreCase);
        }
    }
}
