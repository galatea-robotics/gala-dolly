using System;
using System.Collections.Generic;
using Galatea.Diagnostics;
using Galatea.AI.Robotics;

namespace Galatea.Speech
{
    using Galatea.Speech.Properties;

    [System.Runtime.InteropServices.ComVisible(false)]
    internal sealed class TextToSpeech4 : Galatea.Runtime.RuntimeComponent, ITextToSpeech
    {
        private HTTSLib.TextToSpeech voice4;
        private bool speaking, paused;
        private ISpeechModule _speechModule;

        private readonly IVoice _current;
        private struct Tts4Voice : IVoice
        {
            public Gender Gender { get; set; }
            public string Name { get; set; }
            public string Locale { get; set; }
            public object VoiceObject { get; set; }
        }

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

            // Get Voices
            _current = new Tts4Voice { Gender = (Gender)voice4.Gender(1), Name = voice4.Speaker(1) };

            // Turn off Listener events
            //_speechModule.IsSpeaking = false;

            // Write Debug Log
            _speechModule.LanguageModel.AI.Engine.Debugger.Log(DebuggerLogLevel.Log, Resources.TTS_Initialized);
        }

        public object GetSpeechObject()
        {
            return voice4;
        }
        public IVoice GetVoice(int index)
        {
            return _current;
        }
        IVoice ITextToSpeech.CurrentVoice { get; set; }

        public event EventHandler<MouthPositionEventArgs> MouthPositionChange;
        public event EventHandler<WordEventArgs> Word;
        public event EventHandler SpeechEnded;

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

        public void Speak(string response, IProvider sender)
        {
            // exit if there's nothing to speak
            if (string.IsNullOrEmpty(response))
            {
                _speechModule.LanguageModel.AI.Engine.Debugger.Log(DebuggerLogLevel.Warning,
                    Resources.TTS_Speech_Text_Is_Empty);
                return;
            }

            try
            {
                _speechModule.LanguageModel.AI.Engine.Debugger.Log(DebuggerLogLevel.Log,
                    Resources.TTS_On_Begin_Speaking);

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
        public void PauseTTS()
        {
            voice4.Pause();
            paused = true;

            // Log
            _speechModule.LanguageModel.AI.Engine.Debugger.Log(DebuggerLogLevel.Log, Resources.TTS_On_Paused);
        }
        public void ResumeTTS()
        {
            voice4.Resume();
            paused = false;

            // Log
            _speechModule.LanguageModel.AI.Engine.Debugger.Log(DebuggerLogLevel.Log, Resources.TTS_On_Resumed);
        }
        public void StopTTS()
        {
            try
            {
                _speechModule.LanguageModel.AI.Engine.Debugger.Log(DebuggerLogLevel.Log, Resources.TTS_Stopping);

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

        public bool IsSpeaking { get; set; }

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
            /*
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
             */

            // Log event
            string logMessage = string.Format(System.Globalization.CultureInfo.CurrentCulture,
                Resources.TTS4_On_Visual_Log_Format, EnginePhoneme, MouthHeight, bMouthWidth,
                bJawOpen, TeethUpperVisible, TeethLowerVisible, TonguePosn, LipTension);

            _speechModule.LanguageModel.AI.Engine.Debugger.Log(DebuggerLogLevel.Log, logMessage);

            // Process event
            SetMouthPosition(Phoneme);
        }
        private void SetMouthPosition(short PhonemeId)
        {
            Phoneme phoneme = Phonemes[PhonemeId];

            MouthPositionChange?.Invoke(this, new MouthPositionEventArgs(phoneme.MouthPosition));
        }

        private readonly PhonemeCollection _phonemes;
        private MouthPosition _mouthPosition;
    }
}