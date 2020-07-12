using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gala.Dolly.Test
{
    using Gala.Data.Databases;
    using Galatea;
    using Galatea.AI.Abstract;
    using Galatea.Imaging.IO;
    using Galatea.IO;

    [TestClass]
    [CLSCompliant(false)]
    public class EntityTest : TestBase
    {
        ICreator creator;

#pragma warning disable CA1801 // Review unused parameters
        [ClassInitialize]
        public new static void Initialize(TestContext context)
        {
            // Initialize Colors and Shapes
            //ColorRecognitionTest colorTest = new ColorRecognitionTest() { Creator = TestEngine.AI.RecognitionModel };
            //ShapeRecognitionTest shapeTest = new ShapeRecognitionTest();

            SerializedDataAccessManager dataAccessManager = null;
            try
            {
                dataAccessManager = new SerializedDataAccessManager(ConnectionString);
                dataAccessManager.RestoreBackup("SerializedData.1344.dat");

                TestEngine.DataAccessManager = dataAccessManager;
                TestEngine.InitializeDatabase();

                dataAccessManager = null;
            }
            finally
            {
                dataAccessManager?.Dispose();
            }
        }
#pragma warning restore CA1801 // Review unused parameters

        [TestMethod]
        [TestCategory("2 - Entity")]
        public virtual void TestEntityLabelCreation()
        {
            bool result;
            creator = TestEngine.AI.RecognitionModel;

            // GREEN CIRCLE
            result = TestEntityResponse(ResourcesFolderName + @"Learning\circle.png", "Green Round Shape");
            Assert.IsTrue(result);
            // ORANGE TRIANGLE
            result = TestEntityResponse(ResourcesFolderName + @"Learning\triangle_orange.png", "Orange Triangular Shape");
            Assert.IsTrue(result);
        }

        //[TestMethod]
        [TestCategory("2 - Entity")]
        public void TestUnknownEntityLabel()
        {
            creator = TestEngine.AI.RecognitionModel;

            // BLUE STAR
            GetEntityResponse(ResourcesFolderName + @"Learning\STAR_BLUE.png");
            // BOWTIE
            GetEntityResponse(ResourcesFolderName + @"Learning\bowtie.png");
            // STOP SIGN
            GetEntityResponse(ResourcesFolderName + @"Learning\STOP.png");
            // EDWIN
            GetEntityResponse(ResourcesFolderName + @"Learning\edwin.png");
        }

        #region Not Implemented

        //[TestMethod]
        [TestCategory("AI Logic"), TestCategory("Entity")]
        public void TestSymbolEntity()
        {
            bool result;
            creator = TestEngine.AI.RecognitionModel;

            result = TestEntityResponse(ResourcesFolderName + @"Learning\Symbols\A.png", "Letter A");
            Assert.IsTrue(result);
            result = TestEntityResponse(ResourcesFolderName + @"Learning\Symbols\K.png", "Letter K");
            Assert.IsTrue(result);
            result = TestEntityResponse(ResourcesFolderName + @"Learning\Symbols\S.png", "Letter S");
            Assert.IsTrue(result);
            result = TestEntityResponse(ResourcesFolderName + @"Learning\Symbols\3.png", "Number 3");
            Assert.IsTrue(result);
            result = TestEntityResponse(ResourcesFolderName + @"Learning\Symbols\7.png", "Number 7");
            Assert.IsTrue(result);
            result = TestEntityResponse(ResourcesFolderName + @"Learning\Symbols\$.png", "Dollar Sign");
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
            response = GetEntityResponse(ResourcesFolderName + @"Learning\STOP.png");
            Assert.IsTrue(response.ContainsCurrentCulture("RED OCTAGON"));

            TestEngine.ExecutiveFunctions.GetResponse(TestEngine.AI.LanguageModel, "It's a STOP SIGN!");

            // Only Red Octagon
            response = GetEntityResponse(ResourcesFolderName + @"Learning\green_octagon.png");
            Assert.IsTrue(response.ContainsCurrentCulture("GREEN OCTAGON"));

            creator = TestEngine.User;
            response = GetEntityResponse(ResourcesFolderName + @"Learning\STOP.png");
            Assert.IsTrue(response.ContainsCurrentCulture("STOP SIGN"));
        }

        [TestMethod]
        [TestCategory("2 - Entity")]
        public virtual void TestPacMan()
        {
            string response;
            creator = TestEngine.AI.RecognitionModel;

            // PACMAN
            response = GetEntityResponse(ResourcesFolderName + @"Learning\pacman.png");
            Assert.IsTrue(response.ContainsCurrentCulture("YELLOW PIE"));

            TestEngine.ExecutiveFunctions.GetResponse(TestEngine.AI.LanguageModel, "It's PAC-MAN!");

            // Only yellow Pie Shape is Pac-Man
            response = GetEntityResponse(ResourcesFolderName + @"Learning\pizza.png");
            Assert.IsTrue(response.ContainsCurrentCulture("RED PIE"));

            creator = TestEngine.User;
            response = GetEntityResponse(ResourcesFolderName + @"Learning\pacman.png");
            Assert.IsTrue(response.ContainsCurrentCulture("PAC-MAN"));
        }

        public ICreator Creator { get { return creator; } set { creator = value; } }


        #region Private
        protected bool TestEntityResponse(string fileName, string expectedColorName)
        {
            if (expectedColorName == null) throw new ArgumentNullException(nameof(expectedColorName));

            string response = GetEntityResponse(fileName);
            return response.ContainsCurrentCulture(expectedColorName);
        }
        protected string GetEntityResponse(string fileName)
        {
            return GetEntityResponse(fileName, false);
        }
        protected string GetEntityResponse(string fileName, bool verifyCreator)
        {
            ImagingContextStream stream = GetImagingContextStream(fileName);

            TestEngine.ExecutiveFunctions.StreamContext(TestEngine, TestEngine.Vision.ImageAnalyzer,
                ContextType.Machine, InputType.Visual, stream, typeof(Bitmap));

            string result = TestEngine.ExecutiveFunctions.GetResponse(TestEngine.AI.LanguageModel, "What is it?");

            if (verifyCreator)
            {
                // Verify Creator
                Assert.AreEqual(creator, NamedEntity.Creator);
            }

            // Finalize
            return result;
        }
        #endregion
    }
}