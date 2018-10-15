using System;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Galatea.IO;
using Galatea.Imaging.IO;
using Galatea;
using System.ComponentModel;

namespace Gala.Dolly.Test
{
    using Galatea.AI.Abstract;

    [TestClass]
    public class ShapeRecognitionTest : TestBase
    {
        ICreator _creator;

        public ICreator Creator { get { return _creator; } set { _creator = value; } }

        [TestMethod]
        [TestCategory("1 - Template")]
        public void TestShapes()
        {
            bool result;
            _creator = null;

            // ROUND
            result = TestShapeResponse(@"..\..\..\Resources\Learning\green_circle.png", "round");
            Assert.IsTrue(result);
            result = TestShapeResponse(@"..\..\..\Resources\Learning\circle_perspective.png", "round");
            Assert.IsTrue(result);
            // TRIANGULAR
            result = TestShapeResponse(@"..\..\..\Resources\Learning\triangle_green2.png", "triangular");
            Assert.IsTrue(result);
            result = TestShapeResponse(@"..\..\..\Resources\Learning\triangle_orange.png", "triangular");
            Assert.IsTrue(result);
            result = TestShapeResponse(@"..\..\..\Resources\Learning\triangle_yellow.png", "triangular");
            Assert.IsTrue(result);
            // QUAD
            result = TestShapeResponse(@"..\..\..\Resources\Learning\quad_black.png", "FOUR CORNERS");
            Assert.IsTrue(result);
            result = TestShapeResponse(@"..\..\..\Resources\Learning\quad_green.png", "FOUR CORNERS");
            Assert.IsTrue(result);
            // CHEVRON
            result = TestShapeResponse(@"..\..\..\Resources\Learning\chevron_purple.png", "Chevron");
            Assert.IsTrue(result);
            result = TestShapeResponse(@"..\..\..\Resources\Learning\chevron.png", "Chevron");
            Assert.IsTrue(result);
            result = TestShapeResponse(@"..\..\..\Resources\Learning\widget.png", "Chevron");
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
            response = GetShapeResponse(@"..\..\..\Resources\Learning\pizza.png");
            Assert.IsFalse(response.ToUpper().Contains("PIE SHAPED"));

            TestEngine.ExecutiveFunctions.GetResponse(TestEngine.AI.LanguageModel, User, "The shape is PIE SHAPED!");

            // STAR
            response = GetShapeResponse(@"..\..\..\Resources\Learning\star2.png");
            Assert.IsFalse(response.ToUpper().Contains("STAR"));

            TestEngine.ExecutiveFunctions.GetResponse(TestEngine.AI.LanguageModel, User, "The shape is STAR!");

            // MIX IT UP
            TestShapes();

            // NOW CHECK IF PIE AND STAR WERE LEARNED
            _creator = TestEngine.User;

            result = TestShapeResponse(@"..\..\..\Resources\Learning\pacman.png", "PIE SHAPED");
            Assert.IsTrue(result);
            //result = TestShapeResponse(@"..\..\..\Resources\Learning\orange_pie.png", "PIE SHAPED");
            //Assert.IsTrue(result);

            //result = TestShapeResponse(@"..\..\..\Resources\Learning\STAR_BLUE.png", "STAR");
            //Assert.IsTrue(result);
            //result = TestShapeResponse(@"..\..\..\Resources\Learning\star.png", "STAR");
            //Assert.IsTrue(result);

            //// TEACH ADDITIONAL SHAPES
            //_creator = null;
            //response = GetShapeResponse(@"..\..\..\Resources\Learning\pentagon.png");
            //TestEngine.ExecutiveFunctions.GetResponse(TestEngine.AI.LanguageModel, User, "It's a Pentagon.");

            //_creator = TestEngine.User;  // Evaluates to Pentagon
            //response = GetShapeResponse(@"..\..\..\Resources\Learning\hexagon.png");
            //TestEngine.ExecutiveFunctions.GetResponse(TestEngine.AI.LanguageModel, User, "It's a Hexagon.");
            //response = GetShapeResponse(@"..\..\..\Resources\Learning\STOP.png");
            //TestEngine.ExecutiveFunctions.GetResponse(TestEngine.AI.LanguageModel, User, "It's an Octagon.");
        }

        private bool TestShapeResponse(string fileName, string expectedShapeName)
        {
            string response = GetShapeResponse(fileName);
            return CheckShapeResponse(response, expectedShapeName);
        }
        internal string GetShapeResponse(string fileName)
        {
            Bitmap bitmap = new Bitmap(fileName);
            bitmap.Save("bitmap.png", System.Drawing.Imaging.ImageFormat.Png);

            ImagingContextStream stream = ImagingContextStream.FromBitmap(bitmap);

            TestEngine.ExecutiveFunctions.StreamContext(TestEngine, TestEngine.Vision.ImageAnalyzer,
                ContextType.Machine, InputType.Visual, stream, typeof(Bitmap));

            string result = TestEngine.ExecutiveFunctions.GetResponse(TestEngine.AI.LanguageModel, User, "What SHAPE?");
            System.Console.WriteLine($"Result: {result}");

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
