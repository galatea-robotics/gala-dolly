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
    public class SymbolRecognitionTest : TestBase
    {
        ICreator creator;

        [TestMethod]
        [TestCategory("1 - Template")]
        public void TestSymbols()
        {
            bool result;
            creator = null;

            result = TestSymbolResponse(@"..\..\..\Resources\Learning\Symbols\A.png", "Letter A"); Assert.IsTrue(result);
            result = TestSymbolResponse(@"..\..\..\Resources\Learning\Symbols\B.png", "Letter B"); Assert.IsTrue(result);
            result = TestSymbolResponse(@"..\..\..\Resources\Learning\Symbols\C.png", "Letter C"); Assert.IsTrue(result);
            result = TestSymbolResponse(@"..\..\..\Resources\Learning\Symbols\D.png", "Letter D"); Assert.IsTrue(result);
            result = TestSymbolResponse(@"..\..\..\Resources\Learning\Symbols\E.png", "Letter E"); Assert.IsTrue(result);
            result = TestSymbolResponse(@"..\..\..\Resources\Learning\Symbols\F.png", "Letter F"); Assert.IsTrue(result);
            result = TestSymbolResponse(@"..\..\..\Resources\Learning\Symbols\G.png", "Letter G"); Assert.IsTrue(result);
            result = TestSymbolResponse(@"..\..\..\Resources\Learning\Symbols\H.png", "Letter H"); Assert.IsTrue(result);
            result = TestSymbolResponse(@"..\..\..\Resources\Learning\Symbols\I.png", "Letter I"); Assert.IsTrue(result);
            result = TestSymbolResponse(@"..\..\..\Resources\Learning\Symbols\J.png", "Letter J"); Assert.IsTrue(result);
            result = TestSymbolResponse(@"..\..\..\Resources\Learning\Symbols\K.png", "Letter K"); Assert.IsTrue(result);
            result = TestSymbolResponse(@"..\..\..\Resources\Learning\Symbols\L.png", "Letter L"); Assert.IsTrue(result);
            result = TestSymbolResponse(@"..\..\..\Resources\Learning\Symbols\M.png", "Letter M"); Assert.IsTrue(result);
            result = TestSymbolResponse(@"..\..\..\Resources\Learning\Symbols\N.png", "Letter N"); Assert.IsTrue(result);
            result = TestSymbolResponse(@"..\..\..\Resources\Learning\Symbols\O.png", "Letter O"); Assert.IsTrue(result);
            result = TestSymbolResponse(@"..\..\..\Resources\Learning\Symbols\P.png", "Letter P"); Assert.IsTrue(result);
            result = TestSymbolResponse(@"..\..\..\Resources\Learning\Symbols\Q.png", "Letter Q"); Assert.IsTrue(result);
            result = TestSymbolResponse(@"..\..\..\Resources\Learning\Symbols\R.png", "Letter R"); Assert.IsTrue(result);
            result = TestSymbolResponse(@"..\..\..\Resources\Learning\Symbols\S.png", "Letter S"); Assert.IsTrue(result);
            result = TestSymbolResponse(@"..\..\..\Resources\Learning\Symbols\T.png", "Letter T"); Assert.IsTrue(result);
            result = TestSymbolResponse(@"..\..\..\Resources\Learning\Symbols\U.png", "Letter U"); Assert.IsTrue(result);
            result = TestSymbolResponse(@"..\..\..\Resources\Learning\Symbols\V.png", "Letter V"); Assert.IsTrue(result);
            result = TestSymbolResponse(@"..\..\..\Resources\Learning\Symbols\W.png", "Letter W"); Assert.IsTrue(result);
            result = TestSymbolResponse(@"..\..\..\Resources\Learning\Symbols\X.png", "Letter X"); Assert.IsTrue(result);
            result = TestSymbolResponse(@"..\..\..\Resources\Learning\Symbols\Y.png", "Letter Y"); Assert.IsTrue(result);
            result = TestSymbolResponse(@"..\..\..\Resources\Learning\Symbols\Z.png", "Letter Z"); Assert.IsTrue(result);
            result = TestSymbolResponse(@"..\..\..\Resources\Learning\Symbols\1.png", "Number 1"); Assert.IsTrue(result);
            result = TestSymbolResponse(@"..\..\..\Resources\Learning\Symbols\2.png", "Number 2"); Assert.IsTrue(result);
            result = TestSymbolResponse(@"..\..\..\Resources\Learning\Symbols\3.png", "Number 3"); Assert.IsTrue(result);
            result = TestSymbolResponse(@"..\..\..\Resources\Learning\Symbols\4.png", "Number 4"); Assert.IsTrue(result);
            result = TestSymbolResponse(@"..\..\..\Resources\Learning\Symbols\5.png", "Number 5"); Assert.IsTrue(result);
            result = TestSymbolResponse(@"..\..\..\Resources\Learning\Symbols\6.png", "Number 6"); Assert.IsTrue(result);
            result = TestSymbolResponse(@"..\..\..\Resources\Learning\Symbols\7.png", "Number 7"); Assert.IsTrue(result);
            result = TestSymbolResponse(@"..\..\..\Resources\Learning\Symbols\8.png", "Number 8"); Assert.IsTrue(result);
            result = TestSymbolResponse(@"..\..\..\Resources\Learning\Symbols\9.png", "Number 9"); Assert.IsTrue(result);
        }


        [TestMethod]
        [TestCategory("1 - Template")]
        public void TestSymbolLearning()
        {
            bool result;
            creator = null;

            string response;

            // Dollar Sign
            response = GetSymbolResponse(@"..\..\..\Resources\Learning\Symbols\$.png");
            Assert.IsFalse(response.ToUpper().Contains("DOLLAR SIGN"));

            TestEngine.ExecutiveFunctions.GetResponse(TestEngine.AI.LanguageModel, User, "The Symbol is DOLLAR SIGN");

            // NOW CHECK IF SYMBOLS WERE LEARNED
            creator = TestEngine.User;

            result = TestSymbolResponse(@"..\..\..\Resources\Learning\Symbols\$.png", "DOLLAR SIGN");
            Assert.IsTrue(result);
        }

        /*
        [TestMethod]
        [TestCategory("Learning"), TestCategory("Symbol")]
        public void TestSymbolLearning()
        {
            bool result;
            creator = null;

            string response;

            // Dollar Sign
            response = GetSymbolResponse(@"..\..\..\Resources\Learning\Symbols\$.png");
            Assert.IsFalse(response.ToUpper().Contains("DOLLAR SIGN"));

            Engine.ExecutiveFunctions.GetResponse(Engine.AI.LanguageModel, User, "The Symbol is DOLLAR SIGN");

            // Pound Sign
            response = GetSymbolResponse(@"..\..\..\Resources\Learning\Symbols\#.png");
            Assert.IsFalse(response.ToUpper().Contains("POUND SIGN"));

            Engine.ExecutiveFunctions.GetResponse(Engine.AI.LanguageModel, User, "It's Pound Sign!");

            // Asterisk
            response = GetSymbolResponse(@"..\..\..\Resources\Learning\Symbols\asterisk.png");
            Assert.IsFalse(response.ToUpper().Contains("ASTERISK"));

            Engine.ExecutiveFunctions.GetResponse(Engine.AI.LanguageModel, User, "It's an Asterisk!");

            //// MIX IT UP
            //TestSymbols();

            // NOW CHECK IF SYMBOLS WERE LEARNED
            creator = Engine.User;

            result = TestSymbolResponse(@"..\..\..\Resources\Learning\Symbols\$.png", "DOLLAR SIGN");
            Assert.IsTrue(result);
            result = TestSymbolResponse(@"..\..\..\Resources\Learning\Symbols\#.png", "Pound Sign");
            Assert.IsTrue(result);
            result = TestSymbolResponse(@"..\..\..\Resources\Learning\Symbols\Asterisk.png", "Asterisk");
            Assert.IsTrue(result);
        }
         */

        private bool TestSymbolResponse(string fileName, string expectedColorName)
        {
            string response = GetSymbolResponse(fileName);
            return CheckSymbolResponse(response, expectedColorName);
        }
        private string GetSymbolResponse(string fileName)
        {
            Bitmap bitmap = new Bitmap(fileName);
            ImagingContextStream stream = ImagingContextStream.FromBitmap(bitmap);

            TestEngine.ExecutiveFunctions.StreamContext(TestEngine, TestEngine.Vision.ImageAnalyzer,
                ContextType.Machine, InputType.Visual, stream, typeof(Bitmap));

            string result = TestEngine.ExecutiveFunctions.GetResponse(TestEngine.AI.LanguageModel, User, "What Symbol?");

            // Verify Creator
            Assert.AreEqual(creator, this.NamedTemplate.Creator);

            // Finalize
            return result;
        }
        private bool CheckSymbolResponse(string response, string expectedColorName)
        {
            string expectedResponse = string.Format("The Symbol is {0}.", expectedColorName);
            return response.Equals(expectedResponse, StringComparison.CurrentCultureIgnoreCase);
        }
    }
}
