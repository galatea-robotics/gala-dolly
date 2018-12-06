using System.Drawing;
using System.Drawing.Imaging;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gala.Dolly.Test
{
    using Galatea.AI.Abstract;
    using Galatea.AI.Imaging;
    using Gala.Data.Databases;

    [TestClass]
    public class TemplateRelationshipTest : TestBase
    {
        private static ColorRecognitionTest colorTest;
        private static ShapeRecognitionTest shapeTest;

        [ClassInitialize]
        public new static void Initialize(TestContext context)
        {
            colorTest = new ColorRecognitionTest();
            shapeTest = new ShapeRecognitionTest();

            SerializedDataAccessManager dataAccessManager = new SerializedDataAccessManager(Properties.Settings.Default.DataAccessManagerConnectionString);
            dataAccessManager.RestoreBackup(@"..\..\..\Data\SerializedData.1346.dat");

            TestEngine.DataAccessManager = dataAccessManager;
            TestEngine.InitializeDatabase();
        }


        [TestMethod]
        [TestCategory("4 - Template Relationships")]
        public void TestOblongCircle()
        {
            shapeTest.Creator = TestEngine.AI.RecognitionModel;

            string result = shapeTest.GetShapeResponse(@"..\..\..\Resources\Learning\fat_circle.png");

            #region Save Bitmaps for Debugging

            // Save images
            ShapeTemplate namedTemplate = (ShapeTemplate)NamedTemplate;
            Color fillColor = Color.FromArgb(128, 128, 255);

            Bitmap temp = new Bitmap(@"..\..\..\Resources\Learning\fat_circle.png");
            if (VisualProcessor.ImagingSettings.DebugRecognitionSaveImages)
            {
                temp.Save("bitmap.png", ImageFormat.Png);
            }

            // Get Initial Points
            GuiPointsGraphics.DrawInitialPoints(Color.Blue, Color.DarkBlue, Color.LightBlue, GuiPointShape.Cross,
                namedTemplate.BlobPoints.InitialPoints, temp);
            if (VisualProcessor.ImagingSettings.DebugRecognitionSaveImages)
            {
                temp.Save("bitmapPoints.png", ImageFormat.Png);
            }

            // Get final Blob Points
            GuiPointsGraphics.DrawBlobPoints(temp, namedTemplate.BlobPoints, Color.Orange, Color.Red, Color.Pink, Color.Yellow, Color.Green);
            if (VisualProcessor.ImagingSettings.DebugRecognitionSaveImages)
            {
                temp.Save("bitmapBlobPoints.png", ImageFormat.Png);
            }

            #endregion

            Assert.AreEqual("Oblong Round", NamedTemplate.FriendlyName);

            TestTemplateRelationShips(NamedTemplate.TemplateRelationships, TemplateRelationshipType.Comparison, Memory.Default[TemplateType.Shape]["Circle"]);
        }
        [TestMethod]
        [TestCategory("4 - Template Relationships")]
        public void TestOblongTriangle()
        {
            shapeTest.Creator = TestEngine.AI.RecognitionModel;

            string result = shapeTest.GetShapeResponse(@"..\..\..\Resources\Learning\tall_triangle.png");
            Assert.AreEqual("Oblong Triangular", NamedTemplate.FriendlyName);

            TestTemplateRelationShips(NamedTemplate.TemplateRelationships, TemplateRelationshipType.Comparison, Memory.Default[TemplateType.Shape]["Triangle"]);
        }

        [TestMethod]
        [TestCategory("4 - Template Relationships")]
        public void TestOblongDiagonal()
        {
            shapeTest.Creator = TestEngine.AI.RecognitionModel;

            string result = shapeTest.GetShapeResponse(@"..\..\..\Resources\Learning\diagonal.png");
            Assert.AreEqual("Oblong Four Corners", NamedTemplate.FriendlyName);

            TestTemplateRelationShips(NamedTemplate.TemplateRelationships, TemplateRelationshipType.Comparison, Memory.Default[TemplateType.Shape]["Quadrilateral"]);
        }

        [TestMethod]
        [TestCategory("4 - Template Relationships")]
        public void TestOblongDiagonalStar()
        {
            shapeTest.Creator = TestEngine.AI.RecognitionModel;

            string result = shapeTest.GetShapeResponse(@"..\..\..\Resources\Learning\diagonal_star1.png");
            Assert.AreEqual("Oblong Star", NamedTemplate.FriendlyName);

            TestTemplateRelationShips(NamedTemplate.TemplateRelationships, TemplateRelationshipType.Comparison, Memory.Default[TemplateType.Shape]["Star"]);
        }

        [TestMethod]
        [TestCategory("4 - Template Relationships")]
        public void TestFourPointedStar()
        {
            shapeTest.Creator = TestEngine.AI.RecognitionModel;

            string result = shapeTest.GetShapeResponse(@"..\..\..\Resources\Learning\pink_star.png");
            Assert.AreEqual("Star", NamedTemplate.FriendlyName);

            TestTemplateRelationShips(NamedTemplate.TemplateRelationships, TemplateRelationshipType.Comparison, Memory.Default[TemplateType.Shape]["Star"]);
        }

        /*
         * This is already tested as AiLogicTest.TestHybridColors
         * 
        [TestMethod]
        [TestCategory("4 - Template Relationships")]
        public void TestAqua()
        {
            bool result;
            colorTest.Creator = TestEngine.AI.RecognitionModel;

            // Check AI Logic
            result = colorTest.TestColorResponse(@"..\..\..\Resources\Learning\green_blue_star.png", "Green Blue");
            Assert.IsTrue(result);

            // Check Relationships
            AiLogicTest.TestColorTemplateRelationships(new[] { "Green", "Blue" }, false, TemplateRelationshipType.Contains, colorTest.NamedTemplate);

            // Learning Feedback
            colorTest.Creator = TestEngine.User;
            TestEngine.ExecutiveFunctions.GetResponse(TestEngine.AI.LanguageModel, User, "The color is AQUA");

            result = colorTest.TestColorResponse(@"..\..\..\Resources\Learning\green_blue_star.png", "AQUA");
            Assert.IsTrue(result);

            // Check Relationships
            AiLogicTest.TestColorTemplateRelationships(new[] { "Green", "Blue" }, false, TemplateRelationshipType.Contains, colorTest.NamedTemplate);
        }
         */

        [TestMethod]
        [TestCategory("4 - Template Relationships")]
        public void TestColorComparison()
        {
            // Pink
            TestColorComparison(@"..\..\..\Resources\Learning\pink_star.png", "LIGHT RED", new[] { Memory.Default[TemplateType.Color]["Red"] });
            TestEngine.ExecutiveFunctions.GetResponse(TestEngine.AI.LanguageModel, User, "The color is Pink");

            // Navy
            TestColorComparison(@"..\..\..\Resources\Learning\triangle_navy.png", "DARK BLUE", new[] { Memory.Default[TemplateType.Color]["Blue"] });
            TestEngine.ExecutiveFunctions.GetResponse(TestEngine.AI.LanguageModel, User, "The color is Navy Blue");

            // Maroon
            TestColorComparison(@"..\..\..\Resources\Learning\pentagon_maroon.png", "DARK RED", new[] { Memory.Default[TemplateType.Color]["Red"] });
            TestEngine.ExecutiveFunctions.GetResponse(TestEngine.AI.LanguageModel, User, "The color is Maroon");

            // Regression Testing
            AiLogicTest.colorTest = colorTest;
            AiLogicTest aiLogicTest = new AiLogicTest();
            aiLogicTest.TestSingletonColor();

            // Test User Labels
            bool result;
            colorTest.Creator = TestEngine.User;

            result = colorTest.TestColorResponse(@"..\..\..\Resources\Learning\pink_star.png", "Pink");
            Assert.IsTrue(result);
            result = colorTest.TestColorResponse(@"..\..\..\Resources\Learning\triangle_navy.png", "Navy Blue");
            Assert.IsTrue(result);
            result = colorTest.TestColorResponse(@"..\..\..\Resources\Learning\pentagon_maroon.png", "Maroon");
            Assert.IsTrue(result);
        }

        private void TestColorComparison(string path, string expectedColorName, params BaseTemplate[] relatedTemplates)
        {
            colorTest.Creator = TestEngine.AI.RecognitionModel;
            bool result = colorTest.TestColorResponse(path, expectedColorName);
            Assert.IsTrue(result);

            TestTemplateRelationShips(colorTest.NamedTemplate.TemplateRelationships, TemplateRelationshipType.Comparison, relatedTemplates);
        }

        private void TestTemplateRelationShips(TemplateRelationshipCollection templateRelationships, TemplateRelationshipType relationshipType, params BaseTemplate[] relatedTemplates)
        {
            switch(relationshipType)
            {
                case TemplateRelationshipType.Comparison:
                    Assert.AreEqual(1, templateRelationships.Count);
                    Assert.AreEqual(relatedTemplates[0], templateRelationships[0].RelatedItem);
                    Assert.AreEqual(TemplateRelationshipType.Comparison, templateRelationships[0].RelationshipType);
                    break;

                default: throw new System.NotImplementedException();                
            }
        }
    }
}
