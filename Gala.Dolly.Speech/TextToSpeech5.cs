using System;
using System.Collections.Generic;
using Galatea.Diagnostics;
using Galatea.AI.Robotics;

namespace Galatea.Speech
{
    using Properties;

    [System.Runtime.InteropServices.ComVisible(false)]
    internal sealed class TextToSpeech5 : Galatea.Runtime.RuntimeComponent, ITextToSpeech
    {
        private SpeechLib.SpVoice spVoice;
        private readonly SpeechLib.SpeechVoiceSpeakFlags speakFlags;
        private bool speaking, paused;
        private ISpeechModule _speechModule;

        private IVoice _current;
        private List<IVoice> _voices = new List<IVoice>();
        internal class Tts5Voice : IVoice
        {
            public Gender Gender { get; set; }
            public string Name { get; set; }
            public string Locale { get; set; }
            public object VoiceObject { get; set; }
        }

        public TextToSpeech5(ISpeechModule speechModule)
        {
            // Do Component
            _speechModule = speechModule;
            _speechModule.TextToSpeech = this;
            _speechModule.Add(this);

            // Initialize Voice
            spVoice = new SpeechLib.SpVoice();

            spVoice.Viseme += SpVoice_Viseme;
            spVoice.Word += SpVoice_Word;

            _phonemes = Speech.Phonemes.GetPhonemesSapi5();

            // From VB
            speakFlags = SpeechLib.SpeechVoiceSpeakFlags.SVSFlagsAsync
                & SpeechLib.SpeechVoiceSpeakFlags.SVSFPurgeBeforeSpeak
                & SpeechLib.SpeechVoiceSpeakFlags.SVSFIsXML;

            // Get Voices
            var voices = spVoice.GetVoices();
            foreach (SpeechLib.SpObjectToken v in voices)
            {
                string vname = null;
                vname = v.GetDescription();

                if (vname != null)
                {
                    _voices.Add(new Tts5Voice { Gender = Gender.Other, Name = vname, VoiceObject = v });
                }
            }

            //// Turn off Listener events
            //paused = false;
            //speaking = false;
            //this._speechModule.IsSpeaking = false;

            // Write Debug Log
            speechModule.LanguageModel.AI.Engine.Debugger.Log(DebuggerLogLevel.Log, Resources.TTS_Initialized);
        }
        public object GetSpeechObject()
        {
            return spVoice;
        }

        public IVoice GetVoice(int index)
        {
            return _voices[index];
        }
        IVoice ITextToSpeech.CurrentVoice
        {
            get { return _current; }
            set
            {
                _current = value ?? throw new ArgumentNullException(nameof(value));
                spVoice.Voice = _current.VoiceObject as SpeechLib.SpObjectToken;
            }
        }

        #region ITextToSpeech Members

        public int Rate
        {
            get { return spVoice.Rate; }
            set { spVoice.Rate = value; }
        }
        public int Volume
        {
            get { return spVoice.Volume; }
            set { spVoice.Volume = value; }
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

            // Exit if speech is disabled
            if (_speechModule.StaySilent) return;

            try
            {
                _speechModule.LanguageModel.AI.Engine.Debugger.Log(DebuggerLogLevel.Log,
                    Resources.TTS_On_Begin_Speaking);

                // If it's paused and some text still remains to be spoken, Speak button
                // acts the same as Resume button. However a programmer can choose to
                // speak from the beginning again or any other behavior.  In other cases, 
                // we speak the text with given flags.

                if (!paused)
                {
                    if (!speaking)
                    {
                        // just speak the text with the given flags
                        spVoice.Speak(response, speakFlags);
                    }
                }
                else
                {
                    // Resume if Voice is paused
                    spVoice.Resume();
                }
            }
            catch (Exception ex)
            {
                // Deactivate Speech 
                _speechModule.StaySilent = true;

                // Handle
                throw new TeaSpeechException("Error occurred in TTS.  Deactivating Speech.", ex);
            }
        }
        public void PauseTTS()
        {
            spVoice.Pause();
            paused = true;

            // Log 
            _speechModule.LanguageModel.AI.Engine.Debugger.Log(DebuggerLogLevel.Log, Resources.TTS_On_Paused);
        }
        public void ResumeTTS()
        {
            spVoice.Resume();
            paused = false;

            // Log
            _speechModule.LanguageModel.AI.Engine.Debugger.Log(DebuggerLogLevel.Log, Resources.TTS_On_Resumed);
        }
        public void StopTTS()
        {
            try
            {
                _speechModule.LanguageModel.AI.Engine.Debugger.Log(DebuggerLogLevel.Log, Resources.TTS_Stopping);

                // when string to speak is NULL and dwFlags is set to SPF_PURGEBEFORESPEAK
                // it indicates to SAPI that any remaining data to be synthesized should
                // be discarded.
                spVoice.Speak(string.Empty, SpeechLib.SpeechVoiceSpeakFlags.SVSFPurgeBeforeSpeak);
                if (paused) spVoice.Resume();

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

        private void SpVoice_Viseme(int StreamNumber, object StreamPosition, int Duration,
            SpeechLib.SpeechVisemeType NextVisemeId, SpeechLib.SpeechVisemeFeature Feature,
            SpeechLib.SpeechVisemeType CurrentVisemeId)
        {
            short phonemeId = (short)CurrentVisemeId;
            Phoneme phoneme = Phonemes[phonemeId];

            // Fire Event
            MouthPositionChange?.Invoke(this, new MouthPositionEventArgs(phoneme.MouthPosition));

            // Log Event
            string logMessage = string.Format(System.Globalization.CultureInfo.CurrentCulture,
                    Resources.TTS5_On_MouthPositionChange_Log_Format,
                    this.MouthPosition);

            _speechModule.LanguageModel.AI.Engine.Debugger.Log(DebuggerLogLevel.Log, logMessage);
        }

        private void SpVoice_Word(int StreamNumber, object StreamPosition, int CharacterPosition, int Length)
        {
            Word?.Invoke(this, new WordEventArgs(CharacterPosition, Length));
        }

        public event EventHandler<MouthPositionEventArgs> MouthPositionChange;
        public event EventHandler<WordEventArgs> Word;
        public event EventHandler SpeechEnded;

        private readonly PhonemeCollection _phonemes;
        private MouthPosition _mouthPosition;
    }
}