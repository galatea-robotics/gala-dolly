using Microsoft.VisualStudio.TestTools.UnitTesting;


#if !NETFX_CORE
#else
#endif

namespace Gala.Dolly.Test
{
    using Galatea.AI.Abstract;
    using Gala.Data.Databases;

    [TestClass]
    public class AiLogicTest : TestBase
    {
        internal static ColorRecognitionTest colorTest;

        [ClassInitialize]
        public new static void Initialize(TestContext context)
        {
            colorTest = new ColorRecognitionTest();

            // Initialize Colors and Shapes
            SerializedDataAccessManager dataAccessManager = new SerializedDataAccessManager(ConnectionString);
            dataAccessManager.RestoreBackup(@"..\..\..\..\Data\SerializedData.1345.dat");

            TestEngine.DataAccessManager = dataAccessManager;
            TestEngine.InitializeDatabase();
        }

        [TestMethod]
        [TestCategory("3 - AI Logic")]
        public void TestHybridColors()
        {
            colorTest.Creator = TestEngine.AI.RecognitionModel;
            TestHybridColorResponse(resourcesFolderName + @"Learning\orange_yellow_crescent.png", new[] { "ORANGE", "Yellow" });
            TestHybridColorResponse(resourcesFolderName + @"Learning\green_blue_star.png", new[] { "Green", "Blue" });

            TestEngine.ExecutiveFunctions.GetResponse(TestEngine.AI.LanguageModel, "The color is Aqua");

            colorTest.Creator = TestEngine.User;
            bool result = colorTest.TestColorResponse(resourcesFolderName + @"Learning\green_blue_star.png", "Aqua");
            Assert.IsTrue(result);

            // Check Relationships
            TestColorTemplateRelationships(new[] { "Green", "Blue" }, false, TemplateRelationshipType.Contains, colorTest.NamedTemplate);
        }

        [TestMethod]
        [TestCategory("3 - AI Logic")]
        public void TestSingletonColor()
        {
            colorTest.Creator = null;
            // RED
            RegressionTestColorTemplate(resourcesFolderName + @"Learning\STOP.png", "Red");
            // YELLOW
            RegressionTestColorTemplate(resourcesFolderName + @"Learning\pacman.png", "Yellow");
            // GREEN
            RegressionTestColorTemplate(resourcesFolderName + @"Learning\green_circle.png", "Green");
            // BLUE
            RegressionTestColorTemplate(resourcesFolderName + @"Learning\Symbols\B.png", "Blue");
        }

        private void TestHybridColorResponse(string path, string[] templateTokens)
        {
            string response = colorTest.GetColorResponse(path);
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
                    result = namedTemplate.FriendlyName.Contains(token);
                    Assert.IsTrue(result, string.Format("FriendlyName does not contain the label '{0}'.", token));
                }

                result = namedTemplate.TemplateRelationships.Contains(token);
                Assert.IsTrue(result, string.Format("TemplateRelationships does not contain the ColorTemplate '{0}'.", token));
                result = namedTemplate.TemplateRelationships[token].RelationshipType == relationshipType;
                Assert.IsTrue(result, string.Format("TemplateRelationships['{0}'].RelationshipType must be '{1}'.", token, relationshipType));
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

        private void RegressionTestColorTemplate(string path, string expectedColorName)
        {
            bool result = colorTest.TestColorResponse(path, expectedColorName);
            Assert.IsTrue(result);

            // Check Relationships
            Assert.AreEqual(NamedTemplate.TemplateRelationships.Count, 0);
        }
    }
}
