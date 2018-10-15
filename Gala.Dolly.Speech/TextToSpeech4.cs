using System;
using System.Collections.Generic;
using Galatea.Diagnostics;
using Galatea.AI.Robotics;

namespace Galatea.Speech
{
    [System.Runtime.InteropServices.ComVisible(false)]
    internal class TextToSpeech4 : Galatea.Runtime.RuntimeComponent, ITextToSpeech
    {
        private HTTSLib.TextToSpeech voice4;
        private bool speaking, paused;
        private ISpeechModule _speechModule;

        public TextToSpeech4(ISpeechModule speechModule)
        {
            // Do Component
            speechModule.Add(this);
            _speechModule = speechModule;
            _speechModule.TextToSpeech = this;

            // Initialize Voice
            voice4 = new HTTSLib.TextToSpeech();
            voice4.Select(1);
            voice4.Speed = 156;
            voice4.Visual += Voice4_Visual;

            _phonemes = Speech.Phonemes.GetPhonemesSapi4();

            // Turn off Listener events
            //_speechModule.IsSpeaking = false;

            // Write Debug Log
            _speechModule.LanguageModel.AI.Engine.Debugger.Log(DebuggerLogLevel.Log,
                "The Text-To-Speech Interface was successfully Initialized");
        }

        public object GetSpeechObject()
        {
            return voice4;
        }
        public object GetVoice(int index)
        {
            return voice4.Speaker(index);
        }

        //public bool IsSpeaking { get { return voice4.IsSpeaking > 0; } }

        public event EventHandler<MouthPositionEventArgs> MouthPositionChange;
        public event EventHandler<WordEventArgs> Word { add { throw new NotSupportedException(); } remove { } }

        #region ITextToSpeech Members

        public int Rate
        {
            get { return voice4.Speed; }
            set { voice4.Speed = value; }
        }
        public int Volume
        {
            get { return -1; }
            set { throw new System.NotImplementedException(); }
        }
        public MouthPosition MouthPosition
        {
            get { return _mouthPosition; }
            set { _mouthPosition = value; }
        }

        public virtual void Speak(string response)
        {
            // exit if there's nothing to speak
            if (string.IsNullOrEmpty(response))
            {
                _speechModule.LanguageModel.AI.Engine.Debugger.Log(DebuggerLogLevel.Warning, "The Speech Text is empty.");
                return;
            }

            try
            {
                _speechModule.LanguageModel.AI.Engine.Debugger.Log(DebuggerLogLevel.Log, "Speech TTS Starting.");

                if (!paused)
                {
                    if (!speaking)
                    {
                        // Speak the text
                        (voice4 as HTTSLib.ITextToSpeech).Speak(response);
                    }
                }
                else
                {
                    // Resume if paused
                    voice4.Resume();
                }
            }
            catch (Exception ex)
            {
                throw new TeaSpeechException("Error occurred in TTS.", ex);
            }
        }
        public virtual void PauseTTS()
        {
            voice4.Pause();
            paused = true;

            // Log 
            _speechModule.LanguageModel.AI.Engine.Debugger.Log(DebuggerLogLevel.Log, "Speech TTS Paused.");
        }
        public virtual void ResumeTTS()
        {
            voice4.Resume();
            paused = false;

            // Log 
            _speechModule.LanguageModel.AI.Engine.Debugger.Log(DebuggerLogLevel.Log, "Speech TTS Resumed.");
        }
        public virtual void StopTTS()
        {
            try
            {
                _speechModule.LanguageModel.AI.Engine.Debugger.Log(DebuggerLogLevel.Log, "Speech TTS Stopping.");

                voice4.StopSpeaking();
                if (paused) voice4.Resume();

                speaking = false;
                paused = false;
            }
            catch (Exception ex)
            {
                throw new TeaSpeechException("Error occurred in TTS.", ex);
            }
        }

        #endregion

        #region Component Designer generated code

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        #endregion

        public PhonemeCollection Phonemes { get { return _phonemes; } }
        private void Voice4_Visual(short Phoneme, short EnginePhoneme, int hints, short MouthHeight, short bMouthWidth, short bMouthUpturn, short bJawOpen, short TeethUpperVisible, short TeethLowerVisible, short TonguePosn, short LipTension)
        {
            _speechModule.LanguageModel.AI.Engine.Debugger.Log(DebuggerLogLevel.Log,
                "Voice Phoneme - " + "ph:" + Phoneme +
                ", eph:" + EnginePhoneme +
                ", mh:" + MouthHeight +
                ", mw:" + bMouthWidth +
                ", mu:" + bMouthUpturn +
                ", jaw:" + bJawOpen +
                ", upper:" + TeethUpperVisible +
                ", lower:" + TeethLowerVisible +
                ", tongue:" + TonguePosn +
                ", lip:" + LipTension);

            SetMouthPosition(Phoneme);
        }
        private void SetMouthPosition(short PhonemeId)
        {
            Phoneme phoneme = Phonemes[PhonemeId];

            if (MouthPositionChange != null)
                MouthPositionChange(this, new MouthPositionEventArgs(phoneme.MouthPosition));
        }

        private PhonemeCollection _phonemes;
        private MouthPosition _mouthPosition;
    }
}