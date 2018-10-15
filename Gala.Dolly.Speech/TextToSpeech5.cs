using System;
using Galatea.Diagnostics;
using Galatea.AI.Robotics;

namespace Galatea.Speech
{
    [System.Runtime.InteropServices.ComVisible(false)]
    internal class TextToSpeech5 : Galatea.Runtime.RuntimeComponent, ITextToSpeech
    {
        private SpeechLib.SpVoice spVoice;
        private SpeechLib.SpeechVoiceSpeakFlags speakFlags;
        private bool speaking, paused;
        private ISpeechModule _speechModule;

        public TextToSpeech5(ISpeechModule speechModule)
        {
            // Do Component
            _speechModule = speechModule;
            _speechModule.TextToSpeech = this;
            _speechModule.Add(this);

            // Initialize Voice
            spVoice = new SpeechLib.SpVoice();

            var v = spVoice.Voice;

            spVoice.Viseme += SpVoice_Viseme;
            spVoice.Word += SpVoice_Word;

            _phonemes = Speech.Phonemes.GetPhonemesSapi5();

            // From VB
            speakFlags = SpeechLib.SpeechVoiceSpeakFlags.SVSFlagsAsync
                & SpeechLib.SpeechVoiceSpeakFlags.SVSFPurgeBeforeSpeak
                & SpeechLib.SpeechVoiceSpeakFlags.SVSFIsXML;

            // Determines Male / Female


            //// Turn off Listener events
            //paused = false;
            //speaking = false;
            //this._speechModule.IsSpeaking = false;

            // Write Debug Log
            speechModule.LanguageModel.AI.Engine.Debugger.Log(DebuggerLogLevel.Log,
                "The Text-To-Speech Interface was successfully Initialized");
        }

        public object GetSpeechObject()
        {
            return spVoice;
        }

        public object GetVoice(int index)
        {
            object result = GetVoice2(index);
            if (result == null) result = GetVoice2(-1);

            return result;
        }
        private object GetVoice2(int index)
        {
            object result = null;

            try
            {
                int voiceIndex = index == -1 ? 0 : index;
                result = spVoice.GetVoices().Item(voiceIndex);
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                string msg = string.Format(Galatea.AI.Language.LanguageResources.TextToSpeechGetVoicesError, index);
                Galatea.Speech.TeaSpeechException speechException = new Galatea.Speech.TeaSpeechException(msg, ex);

                if (index != -1)
                    _speechModule.LanguageModel.AI.Engine.Debugger.HandleTeaException(speechException);
                else
                {
                    // 2nd Try - Throw the error instead of handling it.
                    throw speechException;
                }

            }

            return result;
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

        public virtual void Speak(string response)
        {
            // exit if there's nothing to speak
            if (string.IsNullOrEmpty(response))
            {
                _speechModule.LanguageModel.AI.Engine.Debugger.Log(DebuggerLogLevel.Warning, "The Speech Text is empty.");
                return;
            }

            // Exit if speech is disabled
            if (_speechModule.StaySilent) return;

            try
            {
                _speechModule.LanguageModel.AI.Engine.Debugger.Log(DebuggerLogLevel.Log, "Speech TTS Starting.", true);

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
            catch(Exception ex)
            {
                // Deactivate Speech 
                _speechModule.StaySilent = true;

                // Handle
                throw new TeaSpeechException("Error occurred in TTS.  Deactivating Speech.", ex);
            }
        }
        public virtual void PauseTTS()
        {
            spVoice.Pause();
            paused = true;

            // Log 
            _speechModule.LanguageModel.AI.Engine.Debugger.Log(DebuggerLogLevel.Log, "Speech TTS Paused.");
        }
        public virtual void ResumeTTS()
        {
            spVoice.Resume();
            paused = false;

            // Log 
            _speechModule.LanguageModel.AI.Engine.Debugger.Log(DebuggerLogLevel.Log, "Speech TTS Resumed.");
        }
        public virtual void StopTTS()
        {
            try
            {
                _speechModule.LanguageModel.AI.Engine.Debugger.Log(DebuggerLogLevel.Log, "Speech TTS Stopping.");

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
            if (MouthPositionChange != null)
                MouthPositionChange(this, new MouthPositionEventArgs(phoneme.MouthPosition));

            // Log Event
            _speechModule.LanguageModel.AI.Engine.Debugger.Log(DebuggerLogLevel.Diagnostic, 
                "Speech TTS Mouth Position: " + this.MouthPosition.ToString());
        }

        private void SpVoice_Word(int StreamNumber, object StreamPosition, int CharacterPosition, int Length)
        {
            if (Word != null)
                Word(this, new WordEventArgs(CharacterPosition, Length));
        }

        public event EventHandler<MouthPositionEventArgs> MouthPositionChange;
        public event EventHandler<WordEventArgs> Word;

        private PhonemeCollection _phonemes;
        private MouthPosition _mouthPosition;
    }
}