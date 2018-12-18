using System;
using System.Drawing;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gala.Dolly.Test
{
    using Galatea.AI.Abstract;
    using Galatea.Imaging.IO;
    using Galatea.IO;

    [TestClass]
    [CLSCompliant(false)]
    public class SymbolRecognitionTest : TestBase
    {
        ICreator creator;

        [TestMethod]
        [TestCategory("1 - Template")]
        public void TestSymbols()
        {
            bool result;
            creator = null;

            result = TestSymbolResponse(ResourcesFolderName + @"Learning\Symbols\A.png", "Letter A"); Assert.IsTrue(result);
            result = TestSymbolResponse(ResourcesFolderName + @"Learning\Symbols\B.png", "Letter B"); Assert.IsTrue(result);
            result = TestSymbolResponse(ResourcesFolderName + @"Learning\Symbols\C.png", "Letter C"); Assert.IsTrue(result);
            result = TestSymbolResponse(ResourcesFolderName + @"Learning\Symbols\D.png", "Letter D"); Assert.IsTrue(result);
            result = TestSymbolResponse(ResourcesFolderName + @"Learning\Symbols\E.png", "Letter E"); Assert.IsTrue(result);
            result = TestSymbolResponse(ResourcesFolderName + @"Learning\Symbols\F.png", "Letter F"); Assert.IsTrue(result);
            //result = TestSymbolResponse(resourcesFolderName + @"Learning\Symbols\G.png", "Letter G"); Assert.IsTrue(result);
            result = TestSymbolResponse(ResourcesFolderName + @"Learning\Symbols\H.png", "Letter H"); Assert.IsTrue(result);
            result = TestSymbolResponse(ResourcesFolderName + @"Learning\Symbols\I.png", "Letter I"); Assert.IsTrue(result);
            result = TestSymbolResponse(ResourcesFolderName + @"Learning\Symbols\J.png", "Letter J"); Assert.IsTrue(result);
            result = TestSymbolResponse(ResourcesFolderName + @"Learning\Symbols\K.png", "Letter K"); Assert.IsTrue(result);
            result = TestSymbolResponse(ResourcesFolderName + @"Learning\Symbols\L.png", "Letter L"); Assert.IsTrue(result);
            result = TestSymbolResponse(ResourcesFolderName + @"Learning\Symbols\M.png", "Letter M"); Assert.IsTrue(result);
            result = TestSymbolResponse(ResourcesFolderName + @"Learning\Symbols\N.png", "Letter N"); Assert.IsTrue(result);
            result = TestSymbolResponse(ResourcesFolderName + @"Learning\Symbols\O.png", "Letter O"); Assert.IsTrue(result);
            result = TestSymbolResponse(ResourcesFolderName + @"Learning\Symbols\P.png", "Letter P"); Assert.IsTrue(result);
            result = TestSymbolResponse(ResourcesFolderName + @"Learning\Symbols\Q.png", "Letter Q"); Assert.IsTrue(result);
            result = TestSymbolResponse(ResourcesFolderName + @"Learning\Symbols\R.png", "Letter R"); Assert.IsTrue(result);
            //result = TestSymbolResponse(resourcesFolderName + @"Learning\Symbols\S.png", "Letter S"); Assert.IsTrue(result);
            result = TestSymbolResponse(ResourcesFolderName + @"Learning\Symbols\T.png", "Letter T"); Assert.IsTrue(result);
            result = TestSymbolResponse(ResourcesFolderName + @"Learning\Symbols\U.png", "Letter U"); Assert.IsTrue(result);
            result = TestSymbolResponse(ResourcesFolderName + @"Learning\Symbols\V.png", "Letter V"); Assert.IsTrue(result);
            result = TestSymbolResponse(ResourcesFolderName + @"Learning\Symbols\W.png", "Letter W"); Assert.IsTrue(result);
            result = TestSymbolResponse(ResourcesFolderName + @"Learning\Symbols\X.png", "Letter X"); Assert.IsTrue(result);
            result = TestSymbolResponse(ResourcesFolderName + @"Learning\Symbols\Y.png", "Letter Y"); Assert.IsTrue(result);
            result = TestSymbolResponse(ResourcesFolderName + @"Learning\Symbols\Z.png", "Letter Z"); Assert.IsTrue(result);
            result = TestSymbolResponse(ResourcesFolderName + @"Learning\Symbols\1.png", "Number 1"); Assert.IsTrue(result);
            result = TestSymbolResponse(ResourcesFolderName + @"Learning\Symbols\2.png", "Number 2"); Assert.IsTrue(result);
            //result = TestSymbolResponse(resourcesFolderName + @"Learning\Symbols\3.png", "Number 3"); Assert.IsTrue(result);
            result = TestSymbolResponse(ResourcesFolderName + @"Learning\Symbols\4.png", "Number 4"); Assert.IsTrue(result);
            //result = TestSymbolResponse(resourcesFolderName + @"Learning\Symbols\5.png", "Number 5"); Assert.IsTrue(result);
            result = TestSymbolResponse(ResourcesFolderName + @"Learning\Symbols\6.png", "Number 6"); Assert.IsTrue(result);
            result = TestSymbolResponse(ResourcesFolderName + @"Learning\Symbols\7.png", "Number 7"); Assert.IsTrue(result);
            result = TestSymbolResponse(ResourcesFolderName + @"Learning\Symbols\8.png", "Number 8"); Assert.IsTrue(result);
            //result = TestSymbolResponse(resourcesFolderName + @"Learning\Symbols\9.png", "Number 9"); Assert.IsTrue(result);
        }

        [TestMethod]
        [TestCategory("1 - Template")]
        public void TestSymbolLetterG()
        {
            bool result;
            creator = null;

            result = TestSymbolResponse(ResourcesFolderName + @"Learning\Symbols\G.png", "Letter G"); Assert.IsTrue(result);
        }
        [TestMethod]
        [TestCategory("1 - Template")]
        public void TestSymbolLetterS()
        {
            bool result;
            creator = null;

            result = TestSymbolResponse(ResourcesFolderName + @"Learning\Symbols\S.png", "Letter S"); Assert.IsTrue(result);
        }
        [TestMethod]
        [TestCategory("1 - Template")]
        public void TestSymbolNumber3()
        {
            bool result;
            creator = null;

            result = TestSymbolResponse(ResourcesFolderName + @"Learning\Symbols\3.png", "Number 3"); Assert.IsTrue(result);
        }
        [TestMethod]
        [TestCategory("1 - Template")]
        public void TestSymbolNumber5()
        {
            bool result;
            creator = null;

            result = TestSymbolResponse(ResourcesFolderName + @"Learning\Symbols\5.png", "Number 5"); Assert.IsTrue(result);
        }
        [TestMethod]
        [TestCategory("1 - Template")]
        public void TestSymbolNumber9()
        {
            bool result;
            creator = null;

            result = TestSymbolResponse(ResourcesFolderName + @"Learning\Symbols\9.png", "Number 9"); Assert.IsTrue(result);
        }

        /*
        [TestMethod]
        [TestCategory("1 - Template")]
        public void TestSymbolLearning()
        {
            bool result;
            creator = null;

            string response;

            // Dollar Sign
            response = GetSymbolResponse(resourcesFolderName + @"Learning\Symbols\$.png");
            Assert.IsFalse(response.ToUpper().Contains("DOLLAR SIGN"));

            TestEngine.ExecutiveFunctions.GetResponse(TestEngine.AI.LanguageModel, "The Symbol is DOLLAR SIGN");

            // NOW CHECK IF SYMBOLS WERE LEARNED
            creator = TestEngine.User;

            result = TestSymbolResponse(resourcesFolderName + @"Learning\Symbols\$.png", "DOLLAR SIGN");
            Assert.IsTrue(result);
        }
         */

        [TestMethod]
        [TestCategory("1 - Template")]
        public void TestSymbolLearning()
        {
            bool result;
            creator = null;

            string response;

            // Dollar Sign
            response = GetSymbolResponse(ResourcesFolderName + @"Learning\Symbols\$.png");
            Assert.IsFalse(response.ContainsCurrentCulture("DOLLAR SIGN"));

            TestEngine.ExecutiveFunctions.GetResponse(TestEngine.AI.LanguageModel, "The Symbol is DOLLAR SIGN");

            // Pound Sign
            response = GetSymbolResponse(ResourcesFolderName + @"Learning\Symbols\#.png");
            Assert.IsFalse(response.ContainsCurrentCulture("POUND SIGN"));

            TestEngine.ExecutiveFunctions.GetResponse(TestEngine.AI.LanguageModel, "It's Pound Sign!");

            // Asterisk
            response = GetSymbolResponse(ResourcesFolderName + @"Learning\Symbols\asterisk.png");
            Assert.IsFalse(response.ContainsCurrentCulture("ASTERISK"));

            TestEngine.ExecutiveFunctions.GetResponse(TestEngine.AI.LanguageModel, "It's an Asterisk!");

            // MIX IT UP
            TestSymbols();

            // NOW CHECK IF SYMBOLS WERE LEARNED
            creator = TestEngine.User;

            result = TestSymbolResponse(ResourcesFolderName + @"Learning\Symbols\$.png", "DOLLAR SIGN");
            Assert.IsTrue(result);
            result = TestSymbolResponse(ResourcesFolderName + @"Learning\Symbols\#.png", "Pound Sign");
            Assert.IsTrue(result);
            result = TestSymbolResponse(ResourcesFolderName + @"Learning\Symbols\Asterisk.png", "Asterisk");
            Assert.IsTrue(result);
        }

        private bool TestSymbolResponse(string filename, string expectedColorName)
        {
            string response = GetSymbolResponse(filename);
            return CheckSymbolResponse(response, expectedColorName);
        }
        private string GetSymbolResponse(string filename)
        {
            ImagingContextStream stream = GetImagingContextStream(filename);
 
            TestEngine.ExecutiveFunctions.StreamContext(TestEngine, TestEngine.Vision.ImageAnalyzer,
                ContextType.Machine, InputType.Visual, stream, typeof(Bitmap));

            string result = TestEngine.ExecutiveFunctions.GetResponse(TestEngine.AI.LanguageModel, "What Symbol?");

            // Verify Creator
            Assert.AreEqual(creator, NamedTemplate.Creator);

            // Finalize
            return result;
        }
        private static bool CheckSymbolResponse(string response, string expectedColorName)
        {
            string expectedResponse = $"The Symbol is {expectedColorName}.";
            return response.Equals(expectedResponse, StringComparison.CurrentCultureIgnoreCase);
        }
    }
}
