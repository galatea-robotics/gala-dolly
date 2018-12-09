using System;
using System.Linq;
using System.Runtime.Serialization;
using System.Windows.Forms;
using Galatea.Speech;

namespace Gala.Dolly.UI
{
    using Gala.Dolly.Properties;
    using Gala.Dolly.Robotics.BS2Commands;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "TTS")]
    [CLSCompliant(false)]
    public partial class TTS : UserControl //, Galatea.Runtime.Speech.ITextToSpeech
    {
        private ISpeechModule speechModule;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
        [Serializable]
        protected class MouthPositionList : System.Collections.Generic.Dictionary<int, string>
        {
            public MouthPositionList()
            {
                this.Add(0, "Closed");
                this.Add(1, "Open 1");
                this.Add(2, "Open 2");
                this.Add(3, "Big Ooh");
                this.Add(4, "Open 4");
                //this.Add(3, "Little Smile");
                this.Add(5, "Little Ooh");
                this.Add(6, "Big Smile");
                //this.Add(8, "Big Ooh");
                this.Add(9, "Little Frown");
                this.Add(10, "Big Frown");
            }

            protected MouthPositionList(SerializationInfo info, StreamingContext context) : base(info, context)
            {
        }
        }

        public TTS()
        {
            InitializeComponent();

            #region CA1303
            btn4a.Text = Resources.TTS_btn4_Text;
            btn5.Text = Resources.TTS_btn5_Text;
            btn4b.Text = Resources.TTS_btn4_Text;
            btn1a.Text = Resources.TTS_btn1_Text;
            btn2.Text = Resources.TTS_btn2_Text;
            btn1b.Text = Resources.TTS_btn1_Text;
            #endregion

            this.LEDColor = LEDColor.Red;           // Default is Red
            this.EyeCamColor = EyeCamColor.Blue;    // Default is Blue

            // Initialize widget
            cbMouthPositions.DataSource = new MouthPositionList().ToList();
            cbMouthPositions.DisplayMember = "Value";
            cbMouthPositions.ValueMember = "Key";
            cbMouthPositions.SelectedValueChanged += CbMouthPositions_SelectedValueChanged;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "LEDs")]
        public void SetLEDsOff()
        {
            SetLED(0, false);
        }

        #region Design Properties

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "LED")]
        [System.ComponentModel.DefaultValue(LEDColor.Red)]
        public LEDColor LEDColor
        {
            get { return _ledColor; }
            set {
                _ledColor = value;

                // Initialize the Image Resource
                System.Drawing.Image ledImage = _ledColor == LEDColor.Red ?
                    Resources.red_led :
                    Resources.green_led;

                // Set each Image Control
                var imgs = new[] { led1a, led1b, led2a, led2b, led2c, led4a, led4b, led5a, led5b, led5c };

                foreach (var img in imgs)
                {
                    img.Image = ledImage;
                    img.Visible = true;
                }
            }
        }

        [System.ComponentModel.DefaultValue(EyeCamColor.Blue)]
        public EyeCamColor EyeCamColor
        {
            get { return _eyeCamColor; }
            set
            {
                _eyeCamColor = value;

                // Initialize the Image Resource
                System.Drawing.Image eyecamImage = _eyeCamColor == EyeCamColor.Blue ?
                    Resources.eyecam :
                    Resources.b_eyecam;

                // Set each Image Control
                var imgs = new[] { eyecam1, eyecam2 };

                foreach (var img in imgs)
                {
                    img.Image = eyecamImage;
                }
            }
        }

        #endregion

        #region Runtime Properties

        public int Rate { get { return slSpeed.Value; } set { slSpeed.Value = value; } }
        public int Volume { get { return slVolume.Value; } set { slVolume.Value = value; } }

        public event EventHandler RateChanged;
        public event EventHandler VolumeChanged;

        #endregion

        public void SetMouthPosition(MouthPosition mouthPosition)
        {
            TextToSpeech_MouthPositionChange(mouthPosition);
        }

        private void TTS_Load(object sender, System.EventArgs e)
        {
            if (Program.Engine == null || !Program.Started) return;

            // Implement Speech Module
            try
            {
                speechModule = new Galatea.Speech.SpeechModule();
                speechModule.Initialize(Program.Engine.AI.LanguageModel);

                TextToSpeech5 tts5 = null;
                try
                {
                    tts5 = new TextToSpeech5(speechModule);
                }
                catch
                {
                    tts5.Dispose();
                    throw;
                }
                
                speechModule.TextToSpeech = tts5;
                speechModule.TextToSpeech.Rate = -3;
                speechModule.TextToSpeech.MouthPositionChange += TextToSpeech_MouthPositionChange;
            }
            catch (Galatea.TeaException ex)
            {
                MessageBoxOptions options = (this.RightToLeft == RightToLeft.Yes) ?
                    MessageBoxOptions.RightAlign & MessageBoxOptions.RtlReading
                    : MessageBoxOptions.DefaultDesktopOnly;

                MessageBox.Show(ex.Message, this.FindForm().Text, MessageBoxButtons.OK, MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1, options);
            }

            // Set Keyboard Events
            this.ParentForm.KeyDown += MainForm2_KeyDown;
            this.ParentForm.KeyUp += MainForm2_KeyUp;

            // Close the mouth!
            SetLEDsOff();
        }

        private void CbMouthPositions_SelectedValueChanged(object sender, EventArgs e)
        {
            // Determine Mouth Position
            int value = (int)cbMouthPositions.SelectedValue;
            MouthPosition mouthPosition = (MouthPosition)value;

            // Set Mouth Position
            TextToSpeech_MouthPositionChange(mouthPosition);
        }


        private void TextToSpeech_MouthPositionChange(object sender, MouthPositionEventArgs e)
        {
            TextToSpeech_MouthPositionChange(e.MouthPosition);
        }
        private void TextToSpeech_MouthPositionChange(MouthPosition mouthPosition)
        {
            switch (mouthPosition)
            {
                case MouthPosition.Closed:
                    this.led5a.Visible = false;
                    this.led5b.Visible = false;
                    this.led5c.Visible = false;
                    this.led4a.Visible = false;
                    this.led4b.Visible = false;
                    this.led2a.Visible = false;
                    this.led2b.Visible = false;
                    this.led2c.Visible = false;
                    this.led1a.Visible = false;
                    this.led1b.Visible = false;
                    break;
                case MouthPosition.Open1:
                    this.led5a.Visible = false;
                    this.led5b.Visible = false;
                    this.led5c.Visible = false;
                    this.led4a.Visible = false;
                    this.led4b.Visible = false;
                    this.led2a.Visible = true;
                    this.led2b.Visible = true;
                    this.led2c.Visible = true;
                    this.led1a.Visible = false;
                    this.led1b.Visible = false;
                    break;
                case MouthPosition.Open2:
                    this.led5a.Visible = false;
                    this.led5b.Visible = false;
                    this.led5c.Visible = false;
                    this.led4a.Visible = false;
                    this.led4b.Visible = false;
                    this.led2a.Visible = true;
                    this.led2b.Visible = true;
                    this.led2c.Visible = true;
                    this.led1a.Visible = true;
                    this.led1b.Visible = true;
                    break;
                case MouthPosition.Open3:
                    this.led5a.Visible = true;
                    this.led5b.Visible = true;
                    this.led5c.Visible = true;
                    this.led4a.Visible = false;
                    this.led4b.Visible = false;
                    this.led2a.Visible = true;
                    this.led2b.Visible = true;
                    this.led2c.Visible = true;
                    this.led1a.Visible = true;
                    this.led1b.Visible = true;
                    break;
                case MouthPosition.Open4:
                    this.led5a.Visible = false;
                    this.led5b.Visible = false;
                    this.led5c.Visible = false;
                    this.led4a.Visible = true;
                    this.led4b.Visible = true;
                    this.led2a.Visible = true;
                    this.led2b.Visible = true;
                    this.led2c.Visible = true;
                    this.led1a.Visible = true;
                    this.led1b.Visible = true;
                    break;
                case MouthPosition.LittleOoh:
                    this.led5a.Visible = true;
                    this.led5b.Visible = true;
                    this.led5c.Visible = true;
                    this.led4a.Visible = false;
                    this.led4b.Visible = false;
                    this.led2a.Visible = true;
                    this.led2b.Visible = true;
                    this.led2c.Visible = true;
                    this.led1a.Visible = false;
                    this.led1b.Visible = false;
                    break;
                case MouthPosition.BigSmile:
                    this.led5a.Visible = true;
                    this.led5b.Visible = true;
                    this.led5c.Visible = true;
                    this.led4a.Visible = true;
                    this.led4b.Visible = true;
                    this.led2a.Visible = true;
                    this.led2b.Visible = true;
                    this.led2c.Visible = true;
                    this.led1a.Visible = true;
                    this.led1b.Visible = true;
                    break;
                /*case MouthPosition.BigOoh:
                    this.led5a.Visible = true;
                    this.led5b.Visible = true;
                    this.led5c.Visible = true;
                    this.led4a.Visible = false;
                    this.led4b.Visible = false;
                    this.led2a.Visible = true;
                    this.led2b.Visible = true;
                    this.led2c.Visible = true;
                    this.led1a.Visible = true;
                    this.led1b.Visible = true;
                    break;*/
                case MouthPosition.LittleFrown:
                    this.led5a.Visible = true;
                    this.led5b.Visible = true;
                    this.led5c.Visible = true;
                    this.led4a.Visible = false;
                    this.led4b.Visible = false;
                    this.led2a.Visible = false;
                    this.led2b.Visible = false;
                    this.led2c.Visible = false;
                    this.led1a.Visible = false;
                    this.led1b.Visible = false;
                    break;
                case MouthPosition.BigFrown:
                    this.led5a.Visible = true;
                    this.led5b.Visible = true;
                    this.led5c.Visible = true;
                    this.led4a.Visible = false;
                    this.led4b.Visible = false;
                    this.led2a.Visible = false;
                    this.led2b.Visible = false;
                    this.led2c.Visible = false;
                    this.led1a.Visible = true;
                    this.led1b.Visible = true;
                    break;
                default:
                    throw new NotImplementedException();
            }

            // Fucking Duuuuuuuh
            if (Program.Started && Program.Engine != null)
                Program.Engine.Machine.SerialPortController.SendCommand(90 + (int)mouthPosition);
        }

        private void SlSpeed_ValueChanged(object sender, EventArgs e)
        {
            if (speechModule != null && speechModule.TextToSpeech != null)
                speechModule.TextToSpeech.Rate = slSpeed.Value;

            RateChanged(this, null);
        }
        private void SlVolume_ValueChanged(object sender, EventArgs e)
        {
            if (speechModule != null && speechModule.TextToSpeech != null)
                speechModule.TextToSpeech.Volume = slVolume.Value;

            VolumeChanged(this, null);
        }

        #region Keyboard
        private void MainForm2_KeyDown(object sender, KeyEventArgs e)
        {
            ProcessKeys(e.KeyCode, true);
        }
        private void MainForm2_KeyUp(object sender, KeyEventArgs e)
        {
            ProcessKeys(e.KeyCode, false);
        }
        private void ProcessKeys(Keys keys, bool onOff)
        {
            //if (mainForm2.templateRecognition.chatBotControl.Inputting) return;

            switch (keys)
            {
                case Keys.NumPad4:
                case Keys.D4:
                case Keys.U:
                    SetLED(4, onOff);
                    break;

                case Keys.NumPad5:
                case Keys.D5:
                case Keys.I:
                    SetLED(5, onOff);
                    break;

                case Keys.NumPad1:
                case Keys.D1:
                case Keys.J:
                    SetLED(1, onOff);
                    break;

                case Keys.NumPad2:
                case Keys.D2:
                case Keys.K:
                    SetLED(2, onOff);
                    break;
            }
        }
        #endregion

        #region Button Up/Down
        private void Btn1_MouseDown(object sender, MouseEventArgs e)
        {
            SetLED(1, true);
        }
        private void Btn1_MouseUp(object sender, MouseEventArgs e)
        {
            SetLED(1, false);
        }
        private void Btn2_MouseDown(object sender, MouseEventArgs e)
        {
            SetLED(2, true);
        }
        private void Btn2_MouseUp(object sender, MouseEventArgs e)
        {
            SetLED(2, false);
        }
        private void Btn4_MouseDown(object sender, MouseEventArgs e)
        {
            SetLED(4, true);
        }
        private void Btn4_MouseUp(object sender, MouseEventArgs e)
        {
            SetLED(4, false);
        }
        private void Btn5_MouseDown(object sender, MouseEventArgs e)
        {
            SetLED(5, true);
        }
        private void Btn5_MouseUp(object sender, MouseEventArgs e)
        {
            SetLED(5, false);
        }
        #endregion

        /*
        public int Rate
        {
            get
            {
                throw new System.Exception("The method or operation is not implemented.");
            }
            set
            {
                throw new System.Exception("The method or operation is not implemented.");
            }
        }

        public int Volume
        {
            get
            {
                throw new System.Exception("The method or operation is not implemented.");
            }
            set
            {
                throw new System.Exception("The method or operation is not implemented.");
            }
        }

        public Galatea.Runtime.Speech.MouthPosition MouthPosition
        {
            get
            {
                throw new System.Exception("The method or operation is not implemented.");
            }
            set
            {
                throw new System.Exception("The method or operation is not implemented.");
            }
        }

        public void Speak(string response)
        {
            throw new System.Exception("The method or operation is not implemented.");
        }

        public void Pause()
        {
            throw new System.Exception("The method or operation is not implemented.");
        }

        public void Resume()
        {
            throw new System.Exception("The method or operation is not implemented.");
        }

        public void Stop()
        {
            throw new System.Exception("The method or operation is not implemented.");
        }
         */

        private void SetLED(short section, bool onOff)
        {
            SpeechCommands command;

            switch (section)
            {
                case 0:
                    this.led4a.Visible = onOff;
                    this.led4b.Visible = onOff;
                    this.led5a.Visible = onOff;
                    this.led5b.Visible = onOff;
                    this.led5c.Visible = onOff;
                    this.led1a.Visible = onOff;
                    this.led1b.Visible = onOff;
                    this.led2a.Visible = onOff;
                    this.led2b.Visible = onOff;
                    this.led2c.Visible = onOff;
                    command = onOff ? SpeechCommands.MouthPositionOpen4 : SpeechCommands.MouthPositionClosed;
                    break;
                case 4:
                    this.led4a.Visible = onOff;
                    this.led4b.Visible = onOff;
                    command = onOff ? SpeechCommands.Pin4On : SpeechCommands.Pin4Off;
                    break;
                case 5:
                    this.led5a.Visible = onOff;
                    this.led5b.Visible = onOff;
                    this.led5c.Visible = onOff;
                    command = onOff ? SpeechCommands.Pin5On : SpeechCommands.Pin5Off;
                    break;
                case 1:
                    this.led1a.Visible = onOff;
                    this.led1b.Visible = onOff;
                    command = onOff ? SpeechCommands.Pin1On : SpeechCommands.Pin1Off;
                    break;
                case 2:
                    this.led2a.Visible = onOff;
                    this.led2b.Visible = onOff;
                    this.led2c.Visible = onOff;
                    command = onOff ? SpeechCommands.Pin2On : SpeechCommands.Pin2Off;
                    break;
                default:
                    throw new NotImplementedException();
            }

            if (Program.Started && Program.Engine != null)
                Program.Engine.Machine.SerialPortController.SendCommand((int)command);
        }

        private LEDColor _ledColor;
        private EyeCamColor _eyeCamColor;
    }

    public enum LEDColor
    {
        Red,
        Green
    }

    public enum EyeCamColor
    {
        Blue,
        Grey
    }
}
