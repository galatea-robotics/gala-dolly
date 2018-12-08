using System;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using dshow;
using dshow.Core;
using motion;
using VideoSource;
using Galatea;
using Galatea.AI.Abstract;
using Galatea.AI.Imaging;
using Galatea.Imaging;
using Galatea.Imaging.IO;
using Galatea.IO;

namespace Gala.Dolly.UI
{
    using Gala.Dolly.Properties;

    internal partial class VisionCapture : UserControl, IProvider
    {
        private IMotionDetector detector = null;
        //private int detectorType = 0;
        //private int intervalsToSave = 0;
        private Tiger.Video.VFW.AVIWriter writer = null;

        private const int PANTILT_CONSTANT = 100;

        public VisionCapture()
        {
            InitializeComponent();

            #region CA1303
            btnPOV.Text = Resources.VisionCapture_btnPOV_Text;
            btnCapture.Text = Resources.VisionCapture_btnCapture_Text;
            btnRelease.Text = Resources.VisionCapture_btnRelease_Text;
            cbReverseY.Text = Resources.VisionCapture_cbReverseY_Text;
            lblDisplaySize.Text = Resources.VisionCapture_lblDisplaySize_Text;
            lblMousePosition.Text = Resources.VisionCapture_lblMousePosition_Text;
            lblLimits.Text = Resources.VisionCapture_lblLimits_Text;
            lblPan.Text = Resources.VisionCapture_lblPan_Text;
            lblTilt.Text = Resources.VisionCapture_lblTilt_Text;
            ui.Text = Resources.VisionCapture_ui_Text;
            btnResetCamera.Text = Resources.VisionCapture_btnResetCamera_Text;
            btnLoad.Text = Resources.VisionCapture_btnLoad_Text;
            #endregion

            //maxWidth = cameraWindow.Width - cameraWindow.Margin.Left - cameraWindow.Margin.Right;
            //maxHeight = cameraWindow.Margin.Top - cameraWindow.Margin.Bottom;

            // Load Application Settings
            txtPanMin.Text = Settings.Default.VisionCapturePanMin.ToString();
            txtPanMax.Text = Settings.Default.VisionCapturePanMax.ToString();
            txtTiltMin.Text = Settings.Default.VisionCaptureTiltMin.ToString();
            txtTiltMax.Text = Settings.Default.VisionCaptureTiltMax.ToString();
        }

        string IProvider.ProviderID => "Gala.Dolly.UI.VisionCapture";
        string IProvider.ProviderName => "VisionCapture";

        public Point Offset
        {
            get { return _offset; }
            set { _offset = value; }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        public Image GetLastFrame()
        {
            Bitmap result = null;
            try
            {
                result = new Bitmap(cameraWindow.Camera.LastFrame);
#if DEBUG
                // Debugging
                if (Settings.Default.ImagingSettings.DebugRecognitionSaveImages)
                {
                    result.Save("lastframe.png", System.Drawing.Imaging.ImageFormat.Png);
                }
#endif
                SetDisplayImage(result);
                StaticMode = true;

                return result;
            }
            catch
            {
                result.Dispose();
                throw;
            }
        }

        private void VisionCapture_Load(object sender, EventArgs e)
        {
            if (!Program.Started) return;

            try
            {
                FilterCollection filters = new FilterCollection(FilterCategory.VideoInputDevice);

                // create video source
                CaptureDevice localSource = new CaptureDevice()
                {
                    VideoSource = filters[0].MonikerString
                };

                // open it
                OpenVideoSource(localSource);
            }
            catch (ApplicationException ex)
            {
                // TODO:  Implement in UIDebugger
                throw new TeaException(ex.Message, ex);
            }

            // Do UI Stuff
            _offset = new Point(11, 58);

            CenterMousePosition();
            this.ParentForm.KeyDown += Form_KeyDown;

            // Create EventHandler
            Program.Engine.ExecutiveFunctions.ContextRecognition += ExecutiveFunctions_ContextRecognition;
        }

        #region Source Code stuff

        private void OpenVideoSource(VideoSource.IVideoSource source)
        {
            // set busy cursor
            this.Cursor = Cursors.WaitCursor;

            // close previous file
            CloseFile();

            /*
            // enable/disable motion alarm
            if (detector != null)
            {
                detector.MotionLevelCalculation = motionAlarmItem.Checked;
            }
             */

            // create camera
            Camera camera = new Camera(source, detector);
            // start camera
            camera.Start();

            // attach camera to camera window
            cameraWindow.Camera = camera;

            /*
            // reset statistics
            statIndex = statReady = 0;
             */

            // set event handlers
            camera.NewFrame += Camera_NewFrame;
            camera.Alarm += Camera_Alarm;

            /*
            // start timer
            timer.Start();
             */

            this.Cursor = Cursors.Default;
        }

        private void CloseFile()
        {
            motion.Camera camera = cameraWindow.Camera;

            if (camera != null)
            {
                // detach camera from camera window
                cameraWindow.Camera = null;

                // signal camera to stop
                camera.SignalToStop();
                // wait for the camera
                camera.WaitForStop();

                camera = null;

                if (detector != null)
                    detector.Reset();
            }

            if (writer != null)
            {
                writer.Dispose();
                writer = null;
            }
            //intervalsToSave = 0;
        }

        //int frameIndex = 1;
        private void Camera_NewFrame(object sender, System.EventArgs e)
        {
            /*
            if ((intervalsToSave != 0) && (saveOnMotion == true))
            {
                // lets save the frame
                if (writer == null)
                {
                    // create file name
                    DateTime date = DateTime.Now;
                    String fileName = String.Format("{0}-{1}-{2} {3}-{4}-{5}.avi",
                        date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second);

                    try
                    {
                        // create AVI writer
                        writer = new AVIWriter("wmv3");
                        // open AVI file
                        writer.Open(fileName, cameraWindow.Camera.Width, cameraWindow.Camera.Height);
                    }
                    catch (ApplicationException ex)
                    {
                        if (writer != null)
                        {
                            writer.Dispose();
                            writer = null;
                        }
                    }
                }

                // save the frame
                Camera camera = cameraWindow.Camera;

                camera.Lock();
                writer.AddFrame(camera.LastFrame);
                camera.Unlock();
            }
             */

            /*
            // Temporary "Video" stream
            string frameLabel = string.Format(@"C:\GALA\Media\FRAME!{0:00#}!{1:HH_mm_ss_fff}.png", frameIndex, DateTime.Now);
            System.Diagnostics.Debug.WriteLine(frameLabel);

            try
            {
                using (Bitmap frame = new Bitmap(cameraWindow.Camera.LastFrame))
                {
                    frame.Save(frameLabel, System.Drawing.Imaging.ImageFormat.Png);
                    frame.Dispose();
                }
            }
            catch { }

            frameIndex++;
             */
        }

        private void Camera_Alarm(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Windows.Forms.Control.set_Text(System.String)")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object)")]
        private void VisionCapture_Resize(object sender, EventArgs e)
        {
            // Get the maximum available Height and Width 
            int maxHeight = this.Height - panControl.Height;
            int maxWidth = this.Width - tiltControl.Width - buttonPanel.Width;

            //int maxHeight = containerSize.Height - (visionCapture.cameraWindow.Margin.Top + visionCapture.cameraWindow.Margin.Bottom);
            //int maxWidth = containerSize.Width - (visionCapture.cameraWindow.Margin.Left + visionCapture.cameraWindow.Margin.Right);

            // Determine the relative Width of the maximum Height
            int relativeWidth = maxHeight * 4 / 3;

            if (relativeWidth < maxWidth)
            {
                cameraWindow.Height = maxHeight;
                cameraWindow.Width = relativeWidth;
            }
            else
            {
                cameraWindow.Height = maxWidth * 3 / 4;
                cameraWindow.Width = maxWidth;
            }

            // Resize the Static Image control
            pictureBox1.Size = cameraWindow.Size;

            // Size and position Widgets
            panControl.Width = cameraWindow.Width;
            panControl.Top = cameraWindow.Height;

            tiltControl.Height = cameraWindow.Height;
            tiltControl.Left = cameraWindow.Width;

            //// Reposition Mouse
            //Point newPosition = new Point();
            //newPosition.X = _mousePosition.X * pan / 100;
            //newPosition.Y = _mousePosition.Y * tilt / 100;
            //InputMousePosition = newPosition;

            // Debugging
            txtDisplaySize.Text = string.Format(CultureInfo.CurrentCulture, "{0},{1}", cameraWindow.Width, cameraWindow.Height);
        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.PageUp:
                    CaptureMode = true;
                    break;
                case Keys.PageDown:
                    CaptureMode = false;
                    break;
                case Keys.Home:
                    CenterMousePosition();
                    break;
                case Keys.Insert:
                    StaticMode = false;
                    break;
            }
        }
        private void BtnCapture_Click(object sender, EventArgs e)
        {
            CaptureMode = true;
        }
        private void BtnRelease_Click(object sender, EventArgs e)
        {
            CaptureMode = false;
        }
        private void BtnPOV_Click(object sender, EventArgs e)
        {
            CenterMousePosition();
        }
        private void BtnResetCamera_Click(object sender, EventArgs e)
        {
            StaticMode = false;
        }

        /*
        private void FillCanvas()
        {
            // Fill the canvas
            Bitmap bmp = new Bitmap(cameraWindow.Camera.LastFrame);

            // Set the Vertical
            for (int x = 0; x < bmp.Width; x++)
                bmp.SetPixel(x, _mousePosition.Y, Color.Red);

            // Set the Horizontal
            for (int y = 0; y < bmp.Height; y++)
                bmp.SetPixel(_mousePosition.X, y, Color.Red);

            // -- Draw the Cross Hair --
            cameraWindow.Camera.n
            /*
        With Me.ctlDisplay

            ' Fill the canvas
            Dim bmp As New Bitmap(.Width - 5, .Height - 5)
            Dim gfx As Graphics = Graphics.FromImage(bmp)
            gfx.FillRectangle(Brushes.White, 0, 0, bmp.Width, bmp.Height)

            ' Set the Vertical
            For x As Long = 0 To .Width - 6
                bmp.SetPixel(x, _mousepos.Y, Color.Red)
            Next

            ' Set the Horizontal
            For y As Long = 0 To .Height - 6
                bmp.SetPixel(_mousepos.X, y, Color.Red)
            Next

            ' -- Draw the Cross Hair --
            .Image = bmp
        End With
             * /
        }
         */

        private void CameraWindow_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_captureMode) return;

            // Get relative mouse position
            int x, y;
            x = MousePosition.X - ParentForm.Left - _offset.X;
            y = MousePosition.Y - ParentForm.Top - _offset.Y;

            InputMousePosition = new Point(x, y);
        }
        private void CameraWindow_MouseLeave(object sender, EventArgs e)
        {
            if (!_captureMode) return;

            // Capture mouse within control until "Page Down"
            int x, y;
            x = MousePosition.X - ParentForm.Left - _offset.X;
            y = MousePosition.Y - ParentForm.Top - _offset.Y;

            if (x < 0) x = 0;
            if (x > cameraWindow.Width - 1) x = cameraWindow.Width - 1;
            if (y < 0) y = 0;
            if (y > cameraWindow.Height - 1) y = cameraWindow.Height - 1;

            InputMousePosition = new Point(x, y);
            SetCursorFromMouseCapture();
        }

        internal void InitialResize()
        {
            VisionCapture_Resize(null, null);
        }

        internal void CenterMousePosition()
        {
            Point center = new Point()
            {
                //center.X = 50;
                //center.Y = 50;
                X = (cameraWindow.Width) / 2,
                Y = (cameraWindow.Height) / 2
            };
            InputMousePosition = center;
            ////SetCursorFromMouseCapture();
        }

        internal void SetCursorFromMouseCapture()
        {
            Point cursorPoint = new Point()
            {
                //cursorPoint.X = this.cameraWindow.Width * InputMousePosition.X / 100;
                //cursorPoint.X += this.ParentForm.Left + this.cameraWindow.Left + _centerOffset.X;
                //cursorPoint.Y = this.cameraWindow.Height * InputMousePosition.Y / 100;
                //cursorPoint.Y += this.ParentForm.Top + this.cameraWindow.Top + _centerOffset.Y;

                X = this.ParentForm.Left + this.cameraWindow.Left + InputMousePosition.X + _offset.X,
                Y = this.ParentForm.Top + this.cameraWindow.Top + InputMousePosition.Y + _offset.Y
            };
            System.Windows.Forms.Cursor.Position = cursorPoint;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "Galatea.Diagnostics.IDebugger.Log(Galatea.Diagnostics.DebuggerLogLevel,System.String)")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object)")]
        internal void SendCommand()
        {
            // Convert the X value
            int x = (pan * (_panMax - _panMin) / 100) + _panMin;
            int y = (tilt * (_tiltMax - _tiltMin) / 100) + _tiltMin;

            // Invert the Y value
            int yInverse = y;
            y = _tiltMin + (_tiltMax - y);

            panControl.Value = x * 10;
            tiltControl.Value = y * 10;

            if (cbReverseY.Checked) y = yInverse;   // Y Axis was already inverted

            // Log for Debug
            if (Program.Engine.Debugger.LogLevel == Galatea.Diagnostics.DebuggerLogLevel.Diagnostic)
            {
                string msg = string.Format(CultureInfo.CurrentCulture, "Pan: {0}, Tilt: {1}", x, y);
                Program.Engine.Debugger.Log(Galatea.Diagnostics.DebuggerLogLevel.Diagnostic, msg);
            }

            // Send Command
            Wait(Program.Engine.Machine.SerialPortController.WaitInterval);
            Program.Engine.Machine.SerialPortController.SendCommand(1000 + x);

            // Send the next command after wait interval elapses
            Wait(Program.Engine.Machine.SerialPortController.WaitInterval);
            Program.Engine.Machine.SerialPortController.SendCommand(2000 + y);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Windows.Forms.Control.set_Text(System.String)")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object)")]
        internal Point InputMousePosition
        {
            get { return _mousePosition; }
            set
            {
                // Set the value with Constraints
                if (value.X < 0) value.X = 0;
                if (value.X > cameraWindow.Width) value.X = cameraWindow.Width;
                //if (value.X > displayAdjustedWidth) value.X = displayAdjustedWidth;

                if (value.Y < 0) value.Y = 0;
                if (value.Y > cameraWindow.Height) value.Y = cameraWindow.Height;
                //if (value.Y > displayAdjustedHeight) value.Y = displayAdjustedHeight;

                _mousePosition = value;

                // Send the Basic Stamp command
                pan = (_mousePosition.X * PANTILT_CONSTANT) / cameraWindow.Width;
                tilt = (_mousePosition.Y * PANTILT_CONSTANT) / cameraWindow.Height;

                // Debugging
                txtMousePosition.Text = string.Format(CultureInfo.CurrentCulture, "{0},{1}", _mousePosition.X, _mousePosition.Y);
                txtPan.Text = pan.ToString(CultureInfo.CurrentCulture);
                txtTilt.Text = tilt.ToString(CultureInfo.CurrentCulture);

                /*
                    ' Draw the Mouse Position
                    FillCanvas()

                    ' Send the Basic Stamp Command
                    SendCommand(_mousepos)
                 */

                // Send the Basic Stamp command
                SendCommand();
            }
        }

        internal bool CaptureMode
        {
            get { return _captureMode; }
            set
            {
                // Lock or Unlock the Mouse for Input
                _captureMode = value;

                if (_captureMode)
                {
                    StaticMode = false;
                    cameraWindow.Cursor = Cursors.Cross;

                    // Adjust Mouse Position
                    Point adjustedPosition = new Point()
                    {
                        X = (cameraWindow.Width * pan) / PANTILT_CONSTANT,
                        Y = (cameraWindow.Height * tilt) / PANTILT_CONSTANT
                    };
                    InputMousePosition = adjustedPosition;

                    // Capture mouse input
                    this.cameraWindow.MouseMove += CameraWindow_MouseMove;
                    this.cameraWindow.MouseLeave += CameraWindow_MouseLeave;
                    SetCursorFromMouseCapture();
                }
                else
                {
                    cameraWindow.Cursor = Cursors.Default;

                    // Release mouse input events
                    this.cameraWindow.MouseMove -= CameraWindow_MouseMove;
                    this.cameraWindow.MouseLeave -= CameraWindow_MouseLeave;
                }

                btnRelease.Visible = _captureMode;
                btnCapture.Visible = !_captureMode;
                panControl.Enabled = _captureMode;
                tiltControl.Enabled = _captureMode;

                // Turn off the Light pins if any of them got lit up accidentally
                Wait(Program.Engine.Machine.SerialPortController.WaitInterval);
                Program.Engine.Machine.SerialPortController.SendCommand((int)Robotics.BS2Commands.SpeechCommand.MouthPositionClosed);

                //nextCommand = (int)Robotics.Bs2Commands.SpeechCommands.MouthPositionClosed;
                //timer.Interval = Program.Engine.Machine.SerialPortController.WaitInterval;
                //timer.Elapsed += Timer_SendCommand;

                //SerialInterface.Wait(20)
                //SerialInterface.SendCommand(BS2Commands.SpeechCommands.CMD_Mouth_Pos_CLO)
            }
        }

        #region Template Recognition

        internal bool StaticMode
        {
            get { return _staticMode; }
            set
            {
                _staticMode = value;

                pictureBox1.Size = cameraWindow.Size;
                pictureBox1.Location = cameraWindow.Location;
                pictureBox1.Visible = _staticMode;
                cameraWindow.Visible = !_staticMode;

                if (_staticMode)
                {
                    CaptureMode = false;
                }
            }
        }

        private void ExecutiveFunctions_ContextRecognition(object sender, ContextRecognitionEventArgs e)
        {
            Bitmap bitmap;
            BlobImage blobImage = Program.Engine.Vision.ImageAnalyzer.ContextBlobImage;
            Color fillColor = Color.FromArgb(255, blobImage.MeanColor);
            Color backgroundColor = Color.FromArgb(232, 232, 232);

            // Cheat a little bit
            if (!StaticMode)
            {
                AForge.Imaging.HSL hsl = new AForge.Imaging.HSL((int)fillColor.GetHue(), fillColor.GetSaturation(), fillColor.GetBrightness());
                hsl.NormalizeSaturation();
                fillColor = hsl.ToRGB().Color;
            }

            if (e.TemplateType != TemplateType.Shape || !(e.NamedTemplate is ShapeTemplate))
            {
                bitmap = GuiImaging.GetBitmapBlobImage(blobImage.BitmapBlob, fillColor, backgroundColor);
                ImageDump(bitmap, "Color");
            }
            else
            {
                ShapeTemplate st = e.NamedTemplate as ShapeTemplate;
                Color pointColor = Color.FromArgb(64, Color.Cyan);
                Color firstPointColor = Color.FromArgb(64, Color.Red);
                Color lastPointColor = Color.FromArgb(64, Color.Blue);
                bitmap = GuiImaging.GetBitmapBlobImage(st.BitmapBlob, fillColor, backgroundColor);

                if (st.BlobPoints != null)
                {
                    // Get Initial Points
                    GuiPointsGraphics.DrawInitialPoints(pointColor, firstPointColor, lastPointColor, GuiPointShape.Cross, st.BlobPoints.InitialPoints, bitmap);

                    // Get final Blob Points
                    GuiPointsGraphics.DrawBlobPoints(bitmap, st.BlobPoints, Color.Magenta, Color.Red, Color.Blue, Color.Green, Color.Yellow);
                }

                ImageDump(bitmap, "Shape");
            }

            // Update Display
            FillDisplayImage(blobImage, bitmap);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        private void FillDisplayImage(BlobImage blobImage, Bitmap bmpTemp)
        {
            // Get Location from Context
            Rectangle rect = new Rectangle(blobImage.Location.X, blobImage.Location.Y, bmpTemp.Width, bmpTemp.Height);

            // Create BRAND FUCKING NEW Bitmap
            using (Bitmap newBitmap = new Bitmap(blobImage.Source.Size.Width, blobImage.Source.Size.Height))
            {
                newBitmap = new Bitmap(blobImage.Source.Size.Width, blobImage.Source.Size.Height);

                Graphics gfx = Graphics.FromImage(newBitmap);
                gfx.Clear(blobImage.BitmapBlob.BackgroundIsBlack ? Color.Black : Color.White);
                gfx.DrawImage(bmpTemp, rect);

                // Update the Display
                SetDisplayImage(newBitmap);
#if DEBUG
                // Debugging
                if (Settings.Default.ImagingSettings.DebugRecognitionSaveImages)
                {
                    ImageDump(newBitmap, "Post");
                }
#endif
            }
        }

        private void SetDisplayImage(Bitmap bitmap)
        {
            displayImage = bitmap;
            pictureBox1.Image = bitmap;
            pictureBox1.Refresh();
        }

        private void BtnLoad_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = Resources.OpenFileDialogImageFilter;

            // Get the file
            openFileDialog1.ShowDialog();
            string file = openFileDialog1.FileName;

            ValidateAndLoadImage(file);
            //blobFillType = 0;
        }

        private void ValidateAndLoadImage(string file)
        {
            if (string.IsNullOrEmpty(file)) return;
            string errorMessage = null;

            // Validate the file
            try
            {
                sourceImage = new Bitmap(file);
            }
            catch (System.ArgumentException ex)
            {
                errorMessage = "Invalid image type.";
                Program.Engine.Debugger.HandleTeaException(new Galatea.TeaArgumentException(errorMessage, ex), this);
            }

            if (Settings.Default.ImagingSettings.DebugRecognitionSaveImages)
                ImageDump(sourceImage, "Load");

            // Display in the User Control
            SetDisplayImage(sourceImage);
            StaticMode = true;

            // Stream Bitmap to Recognition Manager
            ImagingContextStream stream = ImagingContextStream.FromBitmap(displayImage);
            try
            {
                Program.Engine.ExecutiveFunctions.StreamContext(this, Program.Engine.Vision.ImageAnalyzer,
                    ContextType.Machine, InputType.Visual, stream, typeof(Bitmap));
            }
            catch (TeaImagingException ex)
            {
                Program.Engine.Debugger.HandleTeaException(ex, this);
            }

            // Notify if Error
            if (Program.Engine.Debugger.Exception != null)
            {
                if (errorMessage == null)
                {
                    errorMessage = string.Format(
                        CultureInfo.CurrentCulture,
                        Program.Engine.Debugger.ErrorMessage,
                        Galatea.Globalization.ProviderResources.ImagingModel_Provider_Name);
                }

                // Send Error Notification to Speech Module
                Program.Engine.AI.LanguageModel.SpeechModule.TextToSpeech.Speak(errorMessage, this);

                //Program.BaseForm.Chatbot.SendResponse(errorMessage);

                // Restore Debugger Error Status
                Program.Engine.Debugger.ClearError();
            }
        }


        private static void ImageDump(Image image, string prefix = null)
        {
            if (!Settings.Default.ImagingSettings.DebugRecognitionSaveImages) return;

            string filename = string.Format(CultureInfo.CurrentCulture, "{0:yyyymmdd_HHmmss_fff}.png", DateTime.Now);
            if (!string.IsNullOrEmpty(prefix)) filename = string.Format(CultureInfo.CurrentCulture, "{0}_{1}", prefix, filename);

            image.Save(System.IO.Path.Combine(Settings.Default.ImagingSettings.DebugRecognitionSaveFolder, filename), System.Drawing.Imaging.ImageFormat.Png);
        }


        private Bitmap sourceImage;
        private Bitmap displayImage;
        //private BlobImage blobImage;
        //private int blobFillType = -1;

        #endregion

        private static void Wait(int ms)
        {
            DateTime start = DateTime.Now;

            do { /* Loop */ }
            while (DateTime.Now >= start.AddMilliseconds(ms));
        }

        //private void Timer_SendCommand(object sender, System.Timers.ElapsedEventArgs e)
        //{
        //    timer.Elapsed -= Timer_SendCommand;
        //    Program.Engine.Machine.SerialPortController.SendCommand(nextCommand);
        //}

        //private int nextCommand;


        #region Pan/Tilt Limits

        public int PanMin { get { return _panMin; } set { _panMin = value; } }
        public int PanMax { get { return _panMax; } set { _panMax = value; } }
        public int TiltMin { get { return _tiltMin; } set { _tiltMin = value; } }
        public int TiltMax { get { return _tiltMax; } set { _tiltMax = value; } }

        /*
        private void MinMax_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }
         */

        private void MinMax_Validating(object sender, CancelEventArgs e)
        {
            // Validate value
            TextBox ctl = (TextBox)sender;
            int value = Convert.ToInt32(ctl.Text, CultureInfo.CurrentCulture);

            if (value < 0 || value > 150)
            {
                var options = this.RightToLeft == RightToLeft.Yes ? MessageBoxOptions.RtlReading : MessageBoxOptions.DefaultDesktopOnly;

                MessageBox.Show(Resources.VisionCaptureMinMaxInvalid, this.FindForm().Text, MessageBoxButtons.OK, 
                    MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, options);

                e.Cancel = true;
            }
        }
        private void MinMax_TextChanged(object sender, EventArgs e)
        {
            // Validate value
            TextBox ctl = (TextBox)sender;
            int value = Convert.ToInt32(ctl.Text, CultureInfo.CurrentCulture);

            switch (ctl.Name)
            {
                case "txtPanMin":
                    _panMin = value;
                    break;
                case "txtPanMax":
                    _panMax = value;
                    break;
                case "txtTiltMin":
                    _tiltMin = value;
                    break;
                case "txtTiltMax":
                    _tiltMax = value;
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        private int _panMin, _panMax, _tiltMin, _tiltMax;
        #endregion


        private int pan, tilt;

        private Point _offset;
        private Point _mousePosition;

        private bool _captureMode, _staticMode;
    }
}
