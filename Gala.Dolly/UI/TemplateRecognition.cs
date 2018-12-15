using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using Galatea;
using Galatea.AI.Abstract;
using Galatea.AI.Imaging;
using Galatea.Imaging;
using Galatea.Imaging.IO;
using Galatea.IO;

namespace Gala.Dolly.UI
{
    using Gala.Dolly.Properties;

    internal partial class TemplateRecognition : UserControl, IProvider
    {
        public TemplateRecognition()
        {
            InitializeComponent();

            #region CA1303
            btnLoad.Text = Resources.TemplateRecognition_btnLoad_Text;
            lbl_R.Text = Resources.TemplateRecognition_lbl_R_Text;
            lbl_G.Text = Resources.TemplateRecognition_lbl_G_Text;
            lbl_B.Text = Resources.TemplateRecognition_lbl_B_Text;
            lbl_H.Text = Resources.TemplateRecognition_lbl_H_Text;
            lbl_S.Text = Resources.TemplateRecognition_lbl_S_Text;
            lbl_L.Text = Resources.TemplateRecognition_lbl_L_Text;
            btnRandom.Text = Resources.TemplateRecognition_btnRandom_Text;
            btnBlobify.Text = Resources.TemplateRecognition_btnBlobify_Text;
            #endregion
        }

        [System.ComponentModel.Category("Gala Dolly Events")]
        public event System.EventHandler TemplateLoaded;

        string IProvider.ProviderId => "Gala.Dolly.UI.TemplateRecognition";
        string IProvider.ProviderName => "TemplateRecognition";

        //private IObjectAnalyzer recognition;
        //private VisionProcessingSystem vision;
        //private Color uiColor;

        private void TemplateRecognition_Load(object sender, System.EventArgs e)
        {
            display.BackgroundImageLayout = ImageLayout.Zoom;
            btnRandom.Visible = false;
            btnBlobify.Visible = true;
            btnBlobify.Enabled = false;
            if (this.DesignMode) return;

            if (!Program.Started) return;

            // Create EventHandler
            Program.Engine.ExecutiveFunctions.ContextRecognition += ExecutiveFunctions_ContextRecognition;            

            /*
            displayImage = new Bitmap(display.Width, display.Height);
            display.BackgroundImageLayout = ImageLayout.Zoom;

            // Set initial color
            _r = 255;
            _g = 255;
            _b = 255;
            SetAllRGB();

            // Set Focus
            ToggleRandomBlobify(true);
            chatBotControl.txtInput.Focus();
             */
        }

        private void BtnLoad_Click(object sender, System.EventArgs e)
        {
            openFileDialog1.Filter = Resources.OpenFileDialogImageFilter;

            // Get the file
            openFileDialog1.ShowDialog();
            string file = openFileDialog1.FileName;

            ValidateAndLoadImage(file);
            blobFillType = 0;
            btnBlobify.Enabled = true;

            //// Focus the Chat text
            //chatBotControl.Focus();
            //chatBotControl.txtInput.Focus();

            TemplateLoaded(this, System.EventArgs.Empty);
        }

        #region Color Recognition

        private byte _r, _g, _b;

        /*
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        private void BtnRandom_Click(object sender, System.EventArgs e)
        {
            System.Random rand = new System.Random();
            byte[] rgbInfo = new byte[3];
            rand.NextBytes(rgbInfo);
            _r = rgbInfo[0];
            _g = rgbInfo[1];
            _b = rgbInfo[2];
            SetAllRGB();
            //chatBotControl.Focus();
            //chatBotControl.txtInput.Focus();
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        private void SetAllRGB()
        {
            txt_R.Text = _r.ToString(CultureInfo.CurrentCulture);
            txt_G.Text = _g.ToString(CultureInfo.CurrentCulture);
            txt_B.Text = _b.ToString(CultureInfo.CurrentCulture);
            txt_Validated(null, null);
        }
         */

        private static bool GetRGB(TextBox textBox, ref byte value)
        {
            bool result;
            try
            {
                value = byte.Parse(textBox.Text, CultureInfo.CurrentCulture);
                result = true;
            }
            catch (System.FormatException)
            {
                textBox.Text = value.ToString(CultureInfo.CurrentCulture);
                result = false;
            }

            return result;
        }
        private void Txt_Click(object sender, System.EventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }
        private void Txt_R_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!GetRGB(txt_R, ref _r)) e.Cancel = true;
        }
        private void Txt_G_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!GetRGB(txt_G, ref _g)) e.Cancel = true;
        }
        private void Txt_B_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!GetRGB(txt_B, ref _b)) e.Cancel = true;
        }
        private void Txt_Validated(object sender, System.EventArgs e)
        {
            /*
            // Get Color from input textbox
            Color newColor = Color.FromArgb(_r, _g, _b);
            if (uiColor == newColor) return;

            uiColor = newColor;

            // Update HSB
            txt_H.Text = ((int)uiColor.GetHue()).ToString();
            txt_S.Text = ((int)100 * uiColor.GetSaturation()).ToString("f0");
            txt_L.Text = ((int)100 * uiColor.GetBrightness()).ToString("f0");

            // Set Display
            FillImage();
             */

            // Update Context
            //byte[] rgbInfo = new byte[] { _r, _g, _b };
            //Program.Engine.AI.RecognitionModel.ContextStream = new MemoryStream(rgbInfo);
        }

        #endregion

        private Bitmap sourceImage;
        private Bitmap displayImage;
        private BlobImage blobImage;
        //private BitmapBlob bitmapBlob = null;

        #region Shape Recognition

        private int blobFillType = -1;

        private void ValidateAndLoadImage(string file)
        {
            if (string.IsNullOrEmpty(file)) return;

            // Validate the file
            try
            {
                sourceImage = new Bitmap(file);
            }
            catch (System.ArgumentException ex)
            {
                throw new Galatea.TeaArgumentException("Invalid image type.", ex);
            }

            // Display in the User Control
            SetDisplayImage(sourceImage);

            // Stream Bitmap to Recognition Manager
            ImagingContextStream stream = ImagingContextStream.FromImage(displayImage);
            Program.Engine.ExecutiveFunctions.StreamContext(this, Program.Engine.Vision.ImageAnalyzer,
                ContextType.Machine, InputType.Visual, stream, typeof(Bitmap));

            // Get Blob
            blobImage = Program.Engine.Vision.ImageAnalyzer.ContextBlobImage;
        }

        private void BtnBlobify_Click(object sender, System.EventArgs e)
        {
            blobFillType++;

            //// Reset the Thresholds
            //Galatea.AI.Imaging.Properties.BlobPointSettings.LineSegmentThreshold = 9;     // Iteration 1.1.3
            //VisualProcessor.ImagingSettings.AI.Imaging.Properties.BlobPointSettings.LineAngleThreshold = 4;
            //Galatea.AI.Imaging.Properties.BlobPointSettings.CurveAngleThreshold = 28;

            //Program.ImagingSettings.BlobPointSettings.LineSegmentThreshold = 18;
            //Program.ImagingSettings.BlobPointSettings.LineAngleThreshold = 9;
            //Program.ImagingSettings.BlobPointSettings.CurveAngleThreshold = 27;

            VisualProcessor.ImagingSettings.BlobPointSettings.LineSegmentThreshold = 18;
            VisualProcessor.ImagingSettings.BlobPointSettings.LineAngleThreshold = 9;
            VisualProcessor.ImagingSettings.BlobPointSettings.CurveAngleThreshold = 27;

            try
            {
                SwitchFillType();
            }
            catch (Galatea.Imaging.TeaImagingException)
            {
                blobFillType = 0;
                throw;
            }
        }

        private void SwitchFillType()
        {
            Bitmap bitmap;
            Color fillColor = Color.FromArgb(64, blobImage.MeanColor);
            Color backgroundColor = Color.FromArgb(232, 232, 232);
            bool hasLines, hasCurves, isCircle;

            switch (blobFillType)
            {
                case 0: // Get the Monochrome Bitmap Blob
                    bitmap = GuiImaging.GetBitmapBlobImage(blobImage.BitmapBlob, blobImage.MeanColor, backgroundColor);
                    FillDisplayImage(bitmap);
                    return;

                case 1: // Get BitmapBlob Initial Points
                    bitmap = GuiImaging.GetBitmapBlobInitialPointsImage(blobImage.BitmapBlob, fillColor, backgroundColor,
                        Color.Cyan, GuiPointShape.Cross);
                    FillDisplayImage(bitmap);
                    return;

                case 2: // Get BitmapBlob Threshold Points
                    bitmap = GuiImaging.GetThresholdBlobPointsImage(blobImage.BitmapBlob, fillColor, backgroundColor, Color.Cyan,
                        Color.Magenta, Color.Green, Color.Yellow, out hasLines, out hasCurves, out isCircle);
                    FillDisplayImage(bitmap);

                    if (isCircle) blobFillType += 2;
                    //if (!hasLines) blobFillType++;
                    return;

                //case 3: // Get Vertex Intersection Points
                //    bitmap = GuiImaging.GetVertexIntersectionBlobPointsImage(blobImage.BitmapBlob, fillColor, Color.Cyan,
                //        Color.Magenta, Color.Green, Color.Yellow, out hasLines, out hasCurves, out isCircle);
                //    FillDisplayImage(bitmap);

                //    if (!hasCurves) blobFillType++;
                //    return;

                case 3: // Get BitmapBlob Tangents
                    bitmap = GuiImaging.GetTangentBlobPointsImage(blobImage.BitmapBlob, fillColor, backgroundColor, Color.Cyan,
                        Color.Magenta, Color.Green, Color.Yellow, out hasLines, out hasCurves, out isCircle);
                    FillDisplayImage(bitmap);
                    return;

                ////case 5: // Get Adjusted BitmapBlob Points
                ////    bitmap = GuiImaging.GetAdjustedBlobPointsImage(blobImage.BitmapBlob, fillColor,
                ////        Color.Cyan, Color.Magenta, Color.Green, Color.Yellow, out isCircle);

                ////    if (isCircle)
                ////    {
                ////        Program.Engine.Debugger.Log(Galatea.Runtime.Services.DebuggerLogLevel.Message, "It's a Circle!");
                ////        blobFillType = 0;
                ////    }
                ////    else
                ////    {
                ////        FillDisplayImage(bitmap);
                ////    }
                ////    return;

                ////case 6: // Get BitmapBlob Points with Properties
                ////    bitmap = GuiImaging.GetBlobPointsPropertiesImage(blobImage.BitmapBlob, fillColor, Color.Cyan,
                ////        Color.Orange, Color.DarkGreen, Color.Yellow, out isCircle);

                ////    if (isCircle)
                ////    {
                ////        Program.Engine.Debugger.Log(Galatea.Runtime.Services.DebuggerLogLevel.Message, "It's a Circle!");
                ////        blobFillType = 0;
                ////    }
                ////    else
                ////    {
                ////        FillDisplayImage(bitmap);
                ////    }
                ////    return;

                default:
                    // Set the original Source Image
                    SetDisplayImage(sourceImage);

                    // Reset the Incrementer to zero
                    blobFillType = 0;

                    return;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        private void ExecutiveFunctions_ContextRecognition(object sender, ContextRecognitionEventArgs e)
        {
            if (e.TemplateType != TemplateType.Shape || !(e.NamedTemplate is ShapeTemplate))
                return;

            //if (e.NamedTemplate.ID != 0)    // The ShapeTemplate has already been identified
            //    return;e

            ShapeTemplate st = e.NamedTemplate as ShapeTemplate;
            Color fillColor = Color.FromArgb(255, blobImage.MeanColor);
            Color backgroundColor = blobImage.BitmapBlob.BackgroundIsBlack ? Color.Black : Color.White;

            // Cheat a little bit
            Accord.Imaging.HSL hsl = new Accord.Imaging.HSL((int)fillColor.GetHue(), fillColor.GetSaturation(), fillColor.GetBrightness());
            hsl.NormalizeSaturation();

            fillColor = hsl.ToRGB().Color;

            Color pointColor = Color.FromArgb(64, Color.Cyan);
            Color firstPointColor = Color.FromArgb(64, Color.Red);
            Color lastPointColor = Color.FromArgb(64, Color.Blue);
            Bitmap bitmap = GuiImaging.GetBitmapBlobImage(st.BitmapBlob, fillColor, backgroundColor);
#if DEBUG
            if (Settings.Default.ImagingSettings.DebugRecognitionSaveImages)
            {
                bitmap.Save("bitmap.png", System.Drawing.Imaging.ImageFormat.Png);
            }
#endif
            //// Reverse any Rotation
            //if (st.BitmapBlob.Rotation > 0)
            //    bitmap = Galatea.AI.Imaging.Filters.RotateFilter.Rotate(bitmap, st.BitmapBlob.Rotation * -1);

            if (st.BlobPoints != null)
            {
                // Get Initial Points
                GuiPointsGraphics.DrawInitialPoints(pointColor, firstPointColor, lastPointColor, GuiPointShape.Cross, st.BlobPoints.InitialPoints, bitmap);

                // Get final Blob Points
                GuiPointsGraphics.DrawBlobPoints(bitmap, st.BlobPoints, Color.Magenta, Color.Red, Color.Blue, Color.Green, Color.Yellow);
            }

            // Put the blob in the rectangle
            Bitmap newBitmap = null;

            try
            {
                newBitmap = new Bitmap(sourceImage.Width, sourceImage.Height);
                Graphics gfx = Graphics.FromImage(newBitmap);
                gfx.Clear(backgroundColor);

                //if (blobImage.BitmapBlob.BackgroundIsBlack) gfx.Clear(Color.Black);
                gfx.DrawImage(bitmap, blobImage.Location);

                // Update Display
                SetDisplayImage(newBitmap);
                //display.BackColor = backgroundColor;
            }
            catch
            {
                if (newBitmap != null) newBitmap.Dispose();
                throw;
            }
        }

        /*
        private void DrawRectangle()
        {
            // Draw Blob Rectangle
            Pen rectPen = blobImage.BitmapBlob.BackgroundIsBlack ? Pens.White : Pens.Black;

            using (Bitmap bmpTempBlob = new Bitmap(displayImage))
            {
                Graphics gfx = Graphics.FromImage(bmpTempBlob);
                gfx.DrawRectangle(rectPen, blobImage.Location.X, blobImage.Location.Y, blobImage.BitmapBlob.Width, blobImage.BitmapBlob.Height);

                // Update the Display Image
                SetDisplayImage(bmpTempBlob);
            }
        }
        //private void DrawHSL()
        //{
        //}
        //private void FillBlobImage(BitmapBlob bitmapBlob)
        //{
        //    using (Bitmap bmpTemp = GuiImaging.GetBitmapBlobImage(bitmapBlob, blobImage.MeanColor))
        //    {
        //        FillDisplayImage(bmpTemp);
        //    }
        //}
         */

        private void FillDisplayImage(Bitmap bmpTemp)
        {
            // Get Location from Context
            Rectangle rect = new Rectangle(blobImage.Location.X, blobImage.Location.Y, blobImage.BitmapBlob.Width, blobImage.BitmapBlob.Height);

            // Create BRAND FUCKING NEW Bitmap
            Bitmap newBitmap = null;
            try
            {
                using (newBitmap = new Bitmap(sourceImage.Width, sourceImage.Height))
                using (Graphics gfx = Graphics.FromImage(newBitmap))
                {
                    gfx.Clear(blobImage.BitmapBlob.BackgroundIsBlack ? Color.Black : Color.White);
                    gfx.DrawImage(bmpTemp, rect);
                }

                // Update the Display
                SetDisplayImage(newBitmap);
            }
            catch
            {
                newBitmap?.Dispose();
                throw;
            }
        }
        private void SetDisplayImage(Bitmap bitmap)
        {
            displayImage = bitmap;
            display.BackgroundImage = displayImage;
            display.Refresh();
        }

        #endregion

        internal Size DisplaySize { get { return display.Size; } }
        internal Point DisplayLocation { get { return display.Location; } }
    }
}
