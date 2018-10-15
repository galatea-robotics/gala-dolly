using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Gala.Dolly.UI
{
    public partial class SpeechRecognition : UserControl
    {
        public SpeechRecognition()
        {
            InitializeComponent();
        }

        private void btnMicOn_Click(object sender, EventArgs e)
        {

        }
        private void btnMicOff_Click(object sender, EventArgs e)
        {

        }


        private void StartMic()
        {

        }

        private void StopMic()
        {

        }



        internal bool MicOn { get { return _micOn; } }

        internal bool MicPaused { get { return _micPaused; } }

        private bool _micOn;
        private bool _micPaused;
    }
}
