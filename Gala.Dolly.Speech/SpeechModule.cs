using Galatea.AI;
using Galatea.Runtime;

// TODO: Make speech loader with Drop Down for TTS5 and TTS4.

namespace Galatea.Speech
{
    /// <summary>
    /// Contains Speech Recognition and Text-to-Speech components.
    /// </summary>
    [System.Runtime.InteropServices.ComVisible(false)]
    [System.CLSCompliant(false)]
    public sealed class SpeechModule : RuntimeContainer, ISpeechModule
    {
        /// <summary>
        /// Creates a new instance of the <see cref="SpeechModule"/> class.
        /// </summary>
        public SpeechModule() { }
        /// <summary>
        /// Adds the <see cref="SpeechModule"/> component to the <see cref="CognitiveModelingSystem.LanguageModel"/>
        /// container.
        /// </summary>
        /// <param name="languageAnalyzer">
        /// the <see cref="CognitiveModelingSystem.LanguageModel"/> container.
        /// </param>
        public void Initialize(ILanguageAnalyzer languageAnalyzer)
        {
            _languageAnalyzer = languageAnalyzer ?? throw new Galatea.TeaArgumentNullException(nameof(languageAnalyzer));

            // Component Model
            languageAnalyzer.SpeechModule = this;
        }
        /// <summary>
        /// The Speech Recognition component of the <see cref="SpeechModule"/>.
        /// </summary>
        public ISpeechRecognition SpeechRecognition
        {
            get { return _speechRecognition; }
            set { _speechRecognition = value; }
        }
        /// <summary>
        /// The Text-to-Speech component of the <see cref="SpeechModule"/>.
        /// </summary>
        public ITextToSpeech TextToSpeech
        {
            [System.Diagnostics.DebuggerStepThrough] get { return _textToSpeech; }
            [System.Diagnostics.DebuggerStepThrough] set { _textToSpeech = value; }
        }

        bool ISpeechModule.StaySilent
        {
            [System.Diagnostics.DebuggerStepThrough] get { return _silent; }
            [System.Diagnostics.DebuggerStepThrough] set { _silent = value; }
        }

        ILanguageAnalyzer ISpeechModule.LanguageModel { [System.Diagnostics.DebuggerStepThrough] get { return _languageAnalyzer; } }


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

        private ILanguageAnalyzer _languageAnalyzer;
        private ISpeechRecognition _speechRecognition;
        private ITextToSpeech _textToSpeech;
        private bool _silent;
    }
}