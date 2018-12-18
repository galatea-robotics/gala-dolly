
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gala.Dolly.Test
{
    using Galatea.AI.Abstract;
    using Gala.Data.Databases;

    [TestClass]
    [CLSCompliant(false)]
    public class AILogicTest : TestBase
    {
        internal static ColorRecognitionTest colorTest;

        [ClassInitialize]
#pragma warning disable CA1801 // Review unused parameters
        public new static void Initialize(TestContext context)
        {
            colorTest = new ColorRecognitionTest();

            // Initialize Colors and Shapes
            SerializedDataAccessManager dataAccessManager = null;
            try
            {
                dataAccessManager = new SerializedDataAccessManager(ConnectionString);
                dataAccessManager.RestoreBackup(@"..\..\..\..\Data\SerializedData.1345.dat");

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
        [TestCategory("3 - AI Logic")]
        public void TestHybridColors()
        {
            colorTest.Creator = TestEngine.AI.RecognitionModel;
            TestHybridColorResponse(ResourcesFolderName + @"Learning\orange_yellow_crescent.png", new[] { "ORANGE", "Yellow" });
            TestHybridColorResponse(ResourcesFolderName + @"Learning\green_blue_star.png", new[] { "Green", "Blue" });

            TestEngine.ExecutiveFunctions.GetResponse(TestEngine.AI.LanguageModel, "The color is Aqua");

            colorTest.Creator = TestEngine.User;
            bool result = colorTest.TestColorResponse(ResourcesFolderName + @"Learning\green_blue_star.png", "Aqua");
            Assert.IsTrue(result);

            // Check Relationships
            TestColorTemplateRelationships(new[] { "Green", "Blue" }, false, TemplateRelationshipType.Contains, NamedTemplate);
        }

        [TestMethod]
        [TestCategory("3 - AI Logic")]
        public void TestSingletonColor()
        {
            colorTest.Creator = null;
            // RED
            RegressionTestColorTemplate(ResourcesFolderName + @"Learning\STOP.png", "Red");
            // YELLOW
            RegressionTestColorTemplate(ResourcesFolderName + @"Learning\pacman.png", "Yellow");
            // GREEN
            RegressionTestColorTemplate(ResourcesFolderName + @"Learning\green_circle.png", "Green");
            // BLUE
            RegressionTestColorTemplate(ResourcesFolderName + @"Learning\Symbols\B.png", "Blue");
        }

        private static void TestHybridColorResponse(string path, string[] templateTokens)
        {
            colorTest.GetColorResponse(path);
            TestColorTemplateRelationships(templateTokens, true, TemplateRelationshipType.Contains, NamedTemplate);
        }

        internal static void TestColorTemplateRelationships(string[] templateTokens, bool checkHybridName, 
            TemplateRelationshipType relationshipType, BaseTemplate namedTemplate)
        {
            bool result;

            // Validate Test Results
            foreach (string token in templateTokens)
            {
                if(checkHybridName)
                {
                    result = namedTemplate.FriendlyName.Contains(token, StringComparison.CurrentCultureIgnoreCase);
                    Assert.IsTrue(result, $"FriendlyName does not contain the label '{token}'.");
                }

                result = namedTemplate.TemplateRelationships.Contains(token);
                Assert.IsTrue(result, $"TemplateRelationships does not contain the ColorTemplate '{token}'.");
                result = namedTemplate.TemplateRelationships[token].RelationshipType == relationshipType;
                Assert.IsTrue(result, $"TemplateRelationships['{token}'].RelationshipType must be '{relationshipType}'.");
            }

            // Check for Invalid Template Relationships
            System.Collections.Generic.List<string> tokens = new System.Collections.Generic.List<string>(templateTokens);

            foreach (TemplateRelationship r in namedTemplate.TemplateRelationships)
            {
                //Assert.Equals(r.RelationshipType, relationshipType);
                Assert.IsTrue(r.RelationshipType == relationshipType);
                Assert.IsTrue(tokens.Contains(r.RelatedItem.Name));
            }
        }

        private static void RegressionTestColorTemplate(string path, string expectedColorName)
        {
            bool result = colorTest.TestColorResponse(path, expectedColorName);
            Assert.IsTrue(result);

            // Check Relationships
            Assert.AreEqual(NamedTemplate.TemplateRelationships.Count, 0);
        }
    }
}
