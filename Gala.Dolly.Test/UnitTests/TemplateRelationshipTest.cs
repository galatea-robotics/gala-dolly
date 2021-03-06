﻿using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gala.Dolly.Test
{
    using Galatea.AI.Abstract;
    using Galatea.AI.Imaging;
    using Gala.Data.Databases;

    // TODO:  Modifiers such as "Oblong", "Light", "Dark" need to be ITemplate
    //          so that they can be included in 'similar' or 'different' 
    //          logical comparisons.      
    [TestClass]
    [CLSCompliant(false)]
    public class TemplateRelationshipTest : TestBase
    {
        private static ColorRecognitionTest colorTest;
        private static ShapeRecognitionTest shapeTest;

        [ClassInitialize]
#pragma warning disable CA1801 // Review unused parameters
        public new static void Initialize(TestContext context)
        {
#if NETFX_CORE
            VisualProcessor.ImagingSettings.TemplateRecognitionSettings.ShapeOblongRecognitionLevel = 2;
#endif
            colorTest = new ColorRecognitionTest();
            shapeTest = new ShapeRecognitionTest();

            SerializedDataAccessManager dataAccessManager = null;
            try
            {
                dataAccessManager = new SerializedDataAccessManager(ConnectionString);
                dataAccessManager.RestoreBackup(@"..\..\..\..\Data\SerializedData.1346.dat");

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
        [TestCategory("4 - Template Relationships")]
        public void TestOblongCircle()
        {
            shapeTest.Creator = TestEngine.AI.RecognitionModel;

            shapeTest.GetShapeResponse(ResourcesFolderName + @"Learning\fat_circle.png");

            #region Save Bitmaps for Debugging
            /*
            // Save images
            ShapeTemplate namedTemplate = (ShapeTemplate)NamedTemplate;
            Color fillColor = Color.FromArgb(128, 128, 255);

            Bitmap temp = new Bitmap(ResourcesFolderName + @"Learning\fat_circle.png");
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
             */          
            #endregion

            Assert.AreEqual("Oblong Round", NamedTemplate.FriendlyName);

            TestTemplateRelationships(NamedTemplate.TemplateRelationships, TemplateRelationshipType.Comparison, Memory.Default[TemplateType.Shape]["Circle"]);
        }
        [TestMethod]
        [TestCategory("4 - Template Relationships")]
        public void TestOblongTriangle()
        {
            shapeTest.Creator = TestEngine.AI.RecognitionModel;

            shapeTest.GetShapeResponse(ResourcesFolderName + @"Learning\tall_triangle.png");
            Assert.AreEqual("Oblong Triangular", NamedTemplate.FriendlyName);

            TestTemplateRelationships(NamedTemplate.TemplateRelationships, TemplateRelationshipType.Comparison, Memory.Default[TemplateType.Shape]["Triangle"]);
        }

        [TestMethod]
        [TestCategory("4 - Template Relationships")]
        public void TestOblongDiagonal()
        {
            shapeTest.Creator = TestEngine.AI.RecognitionModel;

            shapeTest.GetShapeResponse(ResourcesFolderName + @"Learning\diagonal2.png");
            Assert.AreEqual("Oblong Four Corners", NamedTemplate.FriendlyName);

            TestTemplateRelationships(NamedTemplate.TemplateRelationships, TemplateRelationshipType.Comparison, Memory.Default[TemplateType.Shape]["Quadrilateral"]);
        }

        //[TestMethod]
        //[TestCategory("4 - Template Relationships")]
#pragma warning disable CA1822 // Mark members as static
        public void TestOblongDiagonalStar()
        {
            shapeTest.Creator = TestEngine.AI.RecognitionModel;

            shapeTest.GetShapeResponse(ResourcesFolderName + @"Learning\diagonal_star1.png");
            Assert.AreEqual("Oblong STAR", NamedTemplate.FriendlyName);

            TestTemplateRelationships(NamedTemplate.TemplateRelationships, TemplateRelationshipType.Comparison, Memory.Default[TemplateType.Shape]["STAR"]);
        }
#pragma warning restore CA1822 // Mark members as static

        [TestMethod]
        [TestCategory("4 - Template Relationships")]
        public void TestFourPointedStar()
        {
            shapeTest.Creator = null;
            string response = shapeTest.GetShapeResponse(ResourcesFolderName + @"Learning\pink_star.png");

            shapeTest.Creator = TestEngine.User;
            response = TestEngine.ExecutiveFunctions.GetResponse(TestEngine.AI.LanguageModel, "It's a STAR");
            Assert.AreEqual("It's a type of STAR.", response);

            response = shapeTest.GetShapeResponse(ResourcesFolderName + @"Learning\pink_star.png");
            Assert.AreEqual("The Shape is STAR.", response);

            TestTemplateRelationships(NamedTemplate.TemplateRelationships, TemplateRelationshipType.TypeOf, Memory.Default.ShapeTemplates["STAR"]);

            // Now do the Oblong Diagonal Star
            TestOblongDiagonalStar();
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
            result = colorTest.TestColorResponse(ResourcesFolderName + @"Learning\green_blue_star.png", "Green Blue");
            Assert.IsTrue(result);

            // Check Relationships
            AiLogicTest.TestColorTemplateRelationships(new[] { "Green", "Blue" }, false, TemplateRelationshipType.Contains, colorTest.NamedTemplate);

            // Learning Feedback
            colorTest.Creator = TestEngine.User;
            TestEngine.ExecutiveFunctions.GetResponse(TestEngine.AI.LanguageModel, "The color is AQUA");

            result = colorTest.TestColorResponse(ResourcesFolderName + @"Learning\green_blue_star.png", "AQUA");
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
            TestColorComparison(ResourcesFolderName + @"Learning\pink_star.png", "LIGHT RED", new[] { Memory.Default[TemplateType.Color]["Red"] });
            TestEngine.ExecutiveFunctions.GetResponse(TestEngine.AI.LanguageModel, "The color is Pink");

            // Navy
            TestColorComparison(ResourcesFolderName + @"Learning\triangle_navy.png", "DARK BLUE", new[] { Memory.Default[TemplateType.Color]["Blue"] });
            TestEngine.ExecutiveFunctions.GetResponse(TestEngine.AI.LanguageModel, "The color is Navy Blue");

            // Maroon
            TestColorComparison(ResourcesFolderName + @"Learning\pentagon_maroon.png", "DARK RED", new[] { Memory.Default[TemplateType.Color]["Red"] });
            TestEngine.ExecutiveFunctions.GetResponse(TestEngine.AI.LanguageModel, "The color is Maroon");

            // Regression Testing
            AILogicTest.colorTest = colorTest;
            using (AILogicTest aiLogicTest = new AILogicTest())
            {
                aiLogicTest.TestSingletonColor();
            }

            // Test User Labels
            bool result;
            colorTest.Creator = TestEngine.User;

            result = colorTest.TestColorResponse(ResourcesFolderName + @"Learning\pink_star.png", "Pink");
            Assert.IsTrue(result);
            result = colorTest.TestColorResponse(ResourcesFolderName + @"Learning\triangle_navy.png", "Navy Blue");
            Assert.IsTrue(result);
            result = colorTest.TestColorResponse(ResourcesFolderName + @"Learning\pentagon_maroon.png", "Maroon");
            Assert.IsTrue(result);
        }

        private static void TestColorComparison(string path, string expectedColorName, params BaseTemplate[] relatedTemplates)
        {
            colorTest.Creator = TestEngine.AI.RecognitionModel;
            bool result = colorTest.TestColorResponse(path, expectedColorName);
            Assert.IsTrue(result);

            TestTemplateRelationships(ColorRecognitionTest.NamedTemplate.TemplateRelationships, TemplateRelationshipType.Comparison, relatedTemplates);
        }

        private static void TestTemplateRelationships(TemplateRelationshipCollection templateRelationships, TemplateRelationshipType relationshipType, params BaseTemplate[] relatedTemplates)
        {
            switch(relationshipType)
            {
                case TemplateRelationshipType.Comparison:
                    Assert.AreEqual(1, templateRelationships.Count);
                    Assert.AreEqual(relatedTemplates[0].FriendlyName, templateRelationships[0].RelatedItem.FriendlyName);
                    Assert.AreEqual(TemplateRelationshipType.Comparison, templateRelationships[0].RelationshipType);
                    break;
                case TemplateRelationshipType.TypeOf:
                    var parentRelationships = templateRelationships.Where(tr => tr.RelationshipType == TemplateRelationshipType.TypeOf);
                    var parentRelation = parentRelationships.FirstOrDefault();
                    Assert.AreEqual(1, parentRelationships.ToArray().Length);
                    Assert.AreEqual(1, relatedTemplates.Length);
                    Assert.AreEqual(relatedTemplates[0], parentRelation.RelatedItem);
                    break;

                default: throw new System.NotImplementedException();                
            }
        }
    }
}
