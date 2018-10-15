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
    public class EntityTest : TestBase
    {
        ICreator creator;

        [ClassInitialize]
        public new static void Initialize(TestContext context)
        {
            // Initialize Colors and Shapes
            ColorRecognitionTest colorTest = new ColorRecognitionTest() { Creator = TestEngine.AI.RecognitionModel };
            colorTest.TestColorLearning();

            ShapeRecognitionTest shapeTest = new ShapeRecognitionTest();
            shapeTest.TestShapeLearning();
        }

        [TestMethod]
        [TestCategory("2 - Entity")]
        public void TestEntityLabelCreation()
        {
            bool result;
            creator = TestEngine.AI.RecognitionModel;

            // GREEN CIRCLE
            result = TestEntityResponse(@"..\..\..\Resources\Learning\circle.png", "Green Round Shape");
            Assert.IsTrue(result);
            // ORANGE TRIANGLE
            result = TestEntityResponse(@"..\..\..\Resources\Learning\triangle_orange.png", "Orange Triangular Shape");
            Assert.IsTrue(result);
        }

        //[TestMethod]
        [TestCategory("2 - Entity")]
        public void TestUnknownEntityLabel()
        {
            string response;
            creator = TestEngine.AI.RecognitionModel;

            // BLUE STAR
            response = GetEntityResponse(@"..\..\..\Resources\Learning\STAR_BLUE.png");
            //Assert.IsTrue(result);
            // BOWTIE
            response = GetEntityResponse(@"..\..\..\Resources\Learning\bowtie.png");
            //Assert.IsTrue(result);
            // STOP SIGN
            response = GetEntityResponse(@"..\..\..\Resources\Learning\STOP.png");
            //Assert.IsTrue(result);
            // EDWIN
            response = GetEntityResponse(@"..\..\..\Resources\Learning\edwin.png");
            //Assert.IsTrue(result);
        }

        #region Not Implemented

        //[TestMethod]
        [TestCategory("AI Logic"), TestCategory("Entity")]
        public void TestSymbolEntity()
        {
            bool result;
            creator = TestEngine.AI.RecognitionModel;

            result = TestEntityResponse(@"..\..\..\Resources\Learning\Symbols\A.png", "Letter A");
            Assert.IsTrue(result);
            result = TestEntityResponse(@"..\..\..\Resources\Learning\Symbols\K.png", "Letter K");
            Assert.IsTrue(result);
            result = TestEntityResponse(@"..\..\..\Resources\Learning\Symbols\S.png", "Letter S");
            Assert.IsTrue(result);
            result = TestEntityResponse(@"..\..\..\Resources\Learning\Symbols\3.png", "Number 3");
            Assert.IsTrue(result);
            result = TestEntityResponse(@"..\..\..\Resources\Learning\Symbols\7.png", "Number 7");
            Assert.IsTrue(result);
            result = TestEntityResponse(@"..\..\..\Resources\Learning\Symbols\$.png", "Dollar Sign");
            Assert.IsTrue(result);
        }

        #endregion

        [TestMethod]
        [TestCategory("2 - Entity")]
        public void TestStopSign()
        {
            string response;
            creator = TestEngine.AI.RecognitionModel;

            // STOP SIGN
            response = GetEntityResponse(@"..\..\..\Resources\Learning\STOP.png");
            Assert.IsTrue(response.ToUpper().Contains("RED OCTAGON"));

            TestEngine.ExecutiveFunctions.GetResponse(TestEngine.AI.LanguageModel, User, "It's a STOP SIGN!");

            // Only Red Octagon
            response = GetEntityResponse(@"..\..\..\Resources\Learning\green_octagon.png");
            Assert.IsTrue(response.ToUpper().Contains("GREEN OCTAGON"));

            creator = TestEngine.User;
            response = GetEntityResponse(@"..\..\..\Resources\Learning\STOP.png");
            Assert.IsTrue(response.ToUpper().Contains("STOP SIGN"));
        }

        [TestMethod]
        [TestCategory("2 - Entity")]
        public void TestPacMan()
        {
            string response;
            creator = TestEngine.AI.RecognitionModel;

            // PACMAN
            response = GetEntityResponse(@"..\..\..\Resources\Learning\pacman.png");
            Assert.IsTrue(response.ToUpper().Contains("YELLOW PIE"));

            TestEngine.ExecutiveFunctions.GetResponse(TestEngine.AI.LanguageModel, User, "It's PAC-MAN!");

            // Only yellow Pie Shape is Pac-Man
            response = GetEntityResponse(@"..\..\..\Resources\Learning\pizza.png");
            Assert.IsTrue(response.ToUpper().Contains("RED PIE"));

            creator = TestEngine.User;
            response = GetEntityResponse(@"..\..\..\Resources\Learning\pacman.png");
            Assert.IsTrue(response.ToUpper().Contains("PAC-MAN"));
        }


        #region Private
        private bool TestEntityResponse(string fileName, string expectedColorName)
        {
            string response = GetEntityResponse(fileName);
            return response.ToUpper().Contains(expectedColorName.ToUpper());
        }
        private string GetEntityResponse(string fileName)
        {
            Bitmap bitmap = new Bitmap(fileName);
            ImagingContextStream stream = ImagingContextStream.FromBitmap(bitmap);

            TestEngine.ExecutiveFunctions.StreamContext(TestEngine, TestEngine.Vision.ImageAnalyzer,
                ContextType.Machine, InputType.Visual, stream, typeof(Bitmap));

            string result = TestEngine.ExecutiveFunctions.GetResponse(TestEngine.AI.LanguageModel, User, "What is it?");

            // Verify Creator
            Assert.AreEqual(creator, this.NamedEntity.Creator);

            // Finalize
            return result;
        }
        #endregion
    }
}
