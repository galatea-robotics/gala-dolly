﻿using System.IO;
#if !NETFX_CORE
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
#else
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Windows.Storage;
using Gala.Data.Databases;
using Galatea.Drawing;
using Galatea.Drawing.Imaging;
#endif

namespace Gala.Dolly.Test
{
    using Galatea.AI.Abstract;

    [TestClass]
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
            string response = GetEntityResponse(@"..\..\..\Resources\Learning\two_shapes1.png", false);

            // Validate Templates
            Assert.AreEqual(2, this.NamedEntity.TemplateRelationships.Count);
            foreach(TemplateRelationship tR in this.NamedEntity.TemplateRelationships)
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
            string response = GetEntityResponse(@"..\..\..\Resources\Learning\three_shapes.png", false);

            Assert.AreEqual(3, this.NamedEntity.TemplateRelationships.Count);
            foreach (TemplateRelationship tR in this.NamedEntity.TemplateRelationships)
            {
                Assert.AreEqual(TemplateRelationshipType.Contains, tR.RelationshipType);
            }

            Assert.AreEqual("GREEN TRIANGULAR SHAPE", NamedEntity.TemplateRelationships[0].RelatedItem.FriendlyName.ToUpper());
            Assert.AreEqual("PURPLE STAR", NamedEntity.TemplateRelationships[1].RelatedItem.FriendlyName.ToUpper());
            Assert.AreEqual("RED ROUND SHAPE", NamedEntity.TemplateRelationships[2].RelatedItem.FriendlyName.ToUpper());
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