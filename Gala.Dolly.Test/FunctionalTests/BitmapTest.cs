using System.IO;
using Windows.Storage;
#if !NETFX_CORE
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
#else
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Galatea.Drawing;
using Galatea.Drawing.Imaging;
#endif

namespace Gala.Dolly.Test
{
    [TestClass]
    public class BitmapTest 
    {
        [TestMethod]
        [TestCategory("Galatea.Drawing.BitmapTest")]
        public void TestSave()
        {
            var stream = Resources.GetResourceStream(@"..\..\..\Resources\Learning\pacman.png");
            Bitmap bmp = new Bitmap(stream);
            string path = ApplicationData.Current.RoamingFolder.Path;
            bmp.Save(Path.Combine(path, "pacman.png"), ImageFormat.Png);
        }

        [TestMethod]
        [TestCategory("Galatea.Drawing.BitmapTest")]
        public void TestDemo()
        {
            var stream = Resources.GetResourceStream(@"..\..\..\Resources\Learning\pacman.png");
            Bitmap bmp = new Bitmap(stream);

            var testFilter = new Galatea.Drawing.Filters.TestFilter();
            Bitmap result = testFilter.Apply(bmp);

            result.Save(@"C:\Users\shudson\AppData\Local\Packages\7eb2d800-9939-47af-87c8-70df96de77d0_rw0ydbvn7tdxa\RoamingState\pacman.png", ImageFormat.Png);
        }
    }
}
