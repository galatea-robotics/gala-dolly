
namespace Gala.Dolly.UI
{
    internal static class Extension
    {
        public static void NormalizeSaturation(this AForge.Imaging.HSL hsl)
        {
            if (hsl.Saturation < 0.25F)
                hsl.Saturation *= 3.6F;
            else if (hsl.Saturation < 0.5F)
                hsl.Saturation *= 2.0F;
            else if (hsl.Saturation < 0.7F)
                hsl.Saturation *= 1.6F;
            else if (hsl.Saturation < 0.8F)
                hsl.Saturation *= 1.24F;
            else if (hsl.Saturation < 0.9F)
                hsl.Saturation *= 1.12F;
        }
    }
}