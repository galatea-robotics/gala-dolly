using System;
using System.IO;
using System.Threading.Tasks;
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
    public class MonochromeBlobFilterTest : TestBase
    {
        [TestMethod]
        [TestCategory("Galatea.Drawing.Filters.MonochromeBlobFilterTest")]
        public async Task TestVectorImages()
        {
            await ApplyMonochromeBlobFilter(@"..\..\..\Resources\Learning\STOP.png");
            await ApplyMonochromeBlobFilter(@"..\..\..\Resources\Learning\pacman.png");
            await ApplyMonochromeBlobFilter(@"..\..\..\Resources\Learning\green_circle.png");
            await ApplyMonochromeBlobFilter(@"..\..\..\Resources\Learning\Symbols\B.png");
        }

        [TestMethod]
        [TestCategory("Galatea.Drawing.Filters.MonochromeBlobFilterTest")]
        public void TestWebcamImages()
        {

        }
        private async Task ApplyMonochromeBlobFilter(string path)
        {
            // Apply Filter
            Bitmap bitmap = GetMonochromeBlobFilterResult(path);

            // Save result to Roaming Folder
            FileInfo fi = new FileInfo(path);
            StorageFile file = await ApplicationData.Current.RoamingFolder.CreateFileAsync(fi.Name, CreationCollisionOption.ReplaceExisting);
            using (Stream fileStream = await file.OpenStreamForWriteAsync())
            {
                bitmap.Save(fileStream, ImageFormat.Png);
            }
        }   

        private Bitmap GetMonochromeBlobFilterResult(string path)
        {
            // Get Bitmap from path
            var factor = TestEngine.UWPSettings.ImagingSettings.MonochromeBlobFilterSettings.ContrastCorrectionFactor;
            var filter = new Galatea.Drawing.Filters.GrayscaleFilter(factor);

            using (var stream = Resources.GetResourceStream(path))
            {
                using (var bmp = new Bitmap(stream))
                {
                    return filter.Apply(bmp);
                }
            }
        }
    }
}