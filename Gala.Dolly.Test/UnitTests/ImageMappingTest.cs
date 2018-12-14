using System;
using System.Globalization;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gala.Dolly.Test
{
    using Galatea.AI.Abstract;

    [TestClass]
    [CLSCompliant(false)]
    public class ImageMappingTest : EntityTest
    {
        [ClassInitialize]
        public new static void Initialize(TestContext context)
        {
            EntityTest.Initialize(context);
        }

        [TestMethod]
        [TestCategory("5 - Image Mapping")]
        public void TestTwoEntities()
        {
            string response = GetEntityResponse(ResourcesFolderName + @"Learning\two_shapes1.png", false);

            // Validate Templates
            Assert.AreEqual(2, NamedEntity.TemplateRelationships.Count);
            foreach(TemplateRelationship tR in NamedEntity.TemplateRelationships)
            {
                Assert.AreEqual(TemplateRelationshipType.Contains, tR.RelationshipType);
            }

            // Validate Response
            Assert.AreEqual("a Green Round Shape and an ORANGE Triangular Shape", response);
        }

        [TestMethod]
        [TestCategory("5 - Image Mapping")]
        public void TestThreeEntities()
        {
            GetEntityResponse(ResourcesFolderName + @"Learning\three_shapes.png", false);

            Assert.AreEqual(3, NamedEntity.TemplateRelationships.Count);
            foreach (TemplateRelationship tR in NamedEntity.TemplateRelationships)
            {
                Assert.AreEqual(TemplateRelationshipType.Contains, tR.RelationshipType);
            }

            Assert.AreEqual("GREEN TRIANGULAR SHAPE", NamedEntity.TemplateRelationships[0].RelatedItem.FriendlyName.ToUpper(CultureInfo.CurrentCulture));
            Assert.AreEqual("PURPLE STAR", NamedEntity.TemplateRelationships[1].RelatedItem.FriendlyName.ToUpper(CultureInfo.CurrentCulture));
            Assert.AreEqual("RED ROUND SHAPE", NamedEntity.TemplateRelationships[2].RelatedItem.FriendlyName.ToUpper(CultureInfo.CurrentCulture));
        }

        public override void TestEntityLabelCreation()
        {
            throw new System.NotImplementedException();
        }
        public override void TestPacMan()
        {
            throw new System.NotImplementedException();
        }
        public override void TestStopSign()
        {
            throw new System.NotImplementedException();
        }
    }
}