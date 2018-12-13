using System;
using System.ComponentModel;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Galatea;
using Galatea.AI.Abstract;
using Galatea.IO;
using Galatea.Imaging.IO;

namespace Gala.Dolly.Test
{
    using Gala.Data.Databases;

    [TestClass]
    [CLSCompliant(false)]
    public class EntityTest : TestBase
    {
        ICreator creator;

        [ClassInitialize]
        public new static void Initialize(TestContext context)
        {
            // Initialize Colors and Shapes
            ColorRecognitionTest colorTest = new ColorRecognitionTest() { Creator = TestEngine.AI.RecognitionModel };
            ShapeRecognitionTest shapeTest = new ShapeRecognitionTest();

            SerializedDataAccessManager dataAccessManager = new SerializedDataAccessManager(ConnectionString);
            dataAccessManager.RestoreBackup(@"..\..\..\..\Data\SerializedData.1344.dat");

            TestEngine.DataAccessManager = dataAccessManager;
            TestEngine.InitializeDatabase();
        }

        [TestMethod]
        [TestCategory("2 - Entity")]
        public virtual void TestEntityLabelCreation()
        {
            bool result;
            creator = TestEngine.AI.RecognitionModel;

            // GREEN CIRCLE
            result = TestEntityResponse(resourcesFolderName + @"Learning\circle.png", "Green Round Shape");
            Assert.IsTrue(result);
            // ORANGE TRIANGLE
            result = TestEntityResponse(resourcesFolderName + @"Learning\triangle_orange.png", "Orange Triangular Shape");
            Assert.IsTrue(result);
        }

        //[TestMethod]
        [TestCategory("2 - Entity")]
        public void TestUnknownEntityLabel()
        {
            string response;
            creator = TestEngine.AI.RecognitionModel;

            // BLUE STAR
            response = GetEntityResponse(resourcesFolderName + @"Learning\STAR_BLUE.png");
            //Assert.IsTrue(result);
            // BOWTIE
            response = GetEntityResponse(resourcesFolderName + @"Learning\bowtie.png");
            //Assert.IsTrue(result);
            // STOP SIGN
            response = GetEntityResponse(resourcesFolderName + @"Learning\STOP.png");
            //Assert.IsTrue(result);
            // EDWIN
            response = GetEntityResponse(resourcesFolderName + @"Learning\edwin.png");
            //Assert.IsTrue(result);
        }

        #region Not Implemented

        //[TestMethod]
        [TestCategory("AI Logic"), TestCategory("Entity")]
        public void TestSymbolEntity()
        {
            bool result;
            creator = TestEngine.AI.RecognitionModel;

            result = TestEntityResponse(resourcesFolderName + @"Learning\Symbols\A.png", "Letter A");
            Assert.IsTrue(result);
            result = TestEntityResponse(resourcesFolderName + @"Learning\Symbols\K.png", "Letter K");
            Assert.IsTrue(result);
            result = TestEntityResponse(resourcesFolderName + @"Learning\Symbols\S.png", "Letter S");
            Assert.IsTrue(result);
            result = TestEntityResponse(resourcesFolderName + @"Learning\Symbols\3.png", "Number 3");
            Assert.IsTrue(result);
            result = TestEntityResponse(resourcesFolderName + @"Learning\Symbols\7.png", "Number 7");
            Assert.IsTrue(result);
            result = TestEntityResponse(resourcesFolderName + @"Learning\Symbols\$.png", "Dollar Sign");
            Assert.IsTrue(result);
        }

        #endregion

        [TestMethod]
        [TestCategory("2 - Entity")]
        public virtual void TestStopSign()
        {
            string response;
            creator = TestEngine.AI.RecognitionModel;

            // STOP SIGN
            response = GetEntityResponse(resourcesFolderName + @"Learning\STOP.png");
            Assert.IsTrue(response.ToUpper().Contains("RED OCTAGON"));

            TestEngine.ExecutiveFunctions.GetResponse(TestEngine.AI.LanguageModel, "It's a STOP SIGN!");

            // Only Red Octagon
            response = GetEntityResponse(resourcesFolderName + @"Learning\green_octagon.png");
            Assert.IsTrue(response.ToUpper().Contains("GREEN OCTAGON"));

            creator = TestEngine.User;
            response = GetEntityResponse(resourcesFolderName + @"Learning\STOP.png");
            Assert.IsTrue(response.ToUpper().Contains("STOP SIGN"));
        }

        [TestMethod]
        [TestCategory("2 - Entity")]
        public virtual void TestPacMan()
        {
            string response;
            creator = TestEngine.AI.RecognitionModel;

            // PACMAN
            response = GetEntityResponse(resourcesFolderName + @"Learning\pacman.png");
            Assert.IsTrue(response.ToUpper().Contains("YELLOW PIE"));

            TestEngine.ExecutiveFunctions.GetResponse(TestEngine.AI.LanguageModel, "It's PAC-MAN!");

            // Only yellow Pie Shape is Pac-Man
            response = GetEntityResponse(resourcesFolderName + @"Learning\pizza.png");
            Assert.IsTrue(response.ToUpper().Contains("RED PIE"));

            creator = TestEngine.User;
            response = GetEntityResponse(resourcesFolderName + @"Learning\pacman.png");
            Assert.IsTrue(response.ToUpper().Contains("PAC-MAN"));
        }

        public ICreator Creator { get { return creator; } set { creator = value; } }


        #region Private
        protected bool TestEntityResponse(string filename, string expectedColorName)
        {
            string response = GetEntityResponse(filename);
            return response.ToUpper().Contains(expectedColorName.ToUpper());
        }
        protected string GetEntityResponse(string filename, bool verifyCreator = true)
        {
            ImagingContextStream stream = GetImagingContextStream(filename);

            TestEngine.ExecutiveFunctions.StreamContext(TestEngine, TestEngine.Vision.ImageAnalyzer,
                ContextType.Machine, InputType.Visual, stream, typeof(Bitmap));

            string result = TestEngine.ExecutiveFunctions.GetResponse(TestEngine.AI.LanguageModel, "What is it?");

            if(verifyCreator)
            {
                // Verify Creator
                Assert.AreEqual(creator, this.NamedEntity.Creator);
            }

            // Finalize
            return result;
        }
        #endregion
    }
}
