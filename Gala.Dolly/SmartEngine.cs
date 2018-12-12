using System.Collections.Specialized;
using Galatea.AI;
using Galatea.AI.Imaging;
using Galatea.AI.Robotics;
using Galatea.Diagnostics;
using Galatea.Runtime;
using Galatea.Runtime.Services;

namespace Gala.Dolly
{
    using Gala.Data;
    using Gala.Data.Databases;
	using Gala.Data.Runtime;

	internal class SmartEngine : RuntimeEngine, IRuntimeEngine, IEngine
    {
		public SmartEngine(IDebugger debugger, DataAccessManager dataAccessManager)
        {
            // Initialize Debugger before anything else
            this.Debugger = debugger;

            // Initialize Database
            _dataAccessManager = dataAccessManager;

            // Initialize Engine
            Initialize();

        }
        public SmartEngine(IDebugger debugger) : base()
        {
            // Initialize Debugger before anything else
            this.Debugger = debugger;

            // Initialize Engine
            _dataAccessManager = new SerializedDataAccessManager(Properties.Settings.Default.DataAccessManagerConnectionString);

            // Initialize Engine
            Initialize();
        }

		private void Initialize()
        {
			IRobot robot = null;

			// Initialize Foundation Components
            try
			{
                machine = new Machine();
				machine.Initialize(this);

                vision = new VisualProcessor(Properties.Settings.Default.ImagingSettings);
				vision.Initialize(this);

                _dataAccessManager.Initialize(this);
                _dataAccessManager.InitializeMemoryBank();

                newContextCache = new ContextCache();
                newContextCache.Initialize(this.ExecutiveFunctions);

                // Verify that ContextCache is instantiated
                System.Diagnostics.Debug.Assert(ExecutiveFunctions.ContextCache != null);
            }
            catch
            {
                machine.Dispose();
                vision.Dispose();
                throw;
            }

			// Become Self-Aware
			robot = SelfAwareness.BecomeSelfAware(this, Properties.Settings.Default.ChatbotName);

            // Initialize Language Module
            this.User = new Galatea.Runtime.Services.User(Properties.Settings.Default.DefaultUserName);
            IChatbotManager chatbots = Gala.Dolly.Chatbots.ChatbotManager.GetChatbots(this.User);
            robot.LanguageModel.LoadChatbots(chatbots);

            var substitutions = new []
            {
                "I ,eye ",
                ".,.  ",
                "Ayuh,If you say so|false"
            };
			robot.LanguageModel.LoadSubstitutions(substitutions);

            speech = new Galatea.Speech.SpeechModule();
			speech.Initialize(robot.LanguageModel);
			speech.StaySilent = Properties.Settings.Default.SpeechIsSilent;
            
            // Add Text to Speech (even if silent)
            Galatea.Speech.ITextToSpeech tts5 = new Galatea.Speech.TextToSpeech5(speech);
			speech.TextToSpeech = tts5;

            // ********** WIN 10 ZIRA ********** //
            //SpeechLib.SpVoice spV = tts5.GetSpeechObject() as SpeechLib.SpVoice;

            int defaultVoiceIndex = Properties.Settings.Default.TextToSpeechDefaultVoiceIndex;
            try
            {
                Galatea.Speech.IVoice voice = tts5.GetVoice(defaultVoiceIndex);
                tts5.CurrentVoice = voice;
            }
            catch (Galatea.Speech.TeaSpeechException ex)
            {
                string msg = "Error loading Text to Speech.  Silencing Speech Module.";
                Galatea.Speech.TeaSpeechException ex1 = new Galatea.Speech.TeaSpeechException(msg, ex);
                Debugger.HandleTeaException(ex1, speech);

                Program.Console.SendResponse(msg);
				speech.StaySilent = true;
            }

            // Add Memory
            robot.InitializeMemory(_dataAccessManager);
        }

        public override void Startup()
        {
            base.Startup();
            _ai = base.AI as IRobot;

			// Apply default conversational labels
			User _user = null;
			_user = new User(Properties.Settings.Default.DefaultUserName);
			User = _user;
			User.FriendlyName = Properties.Settings.Default.DefaultUserName;
		}

        public override void Shutdown()
        {
            // Save Template database
            _dataAccessManager.SaveAll();

            // Finalize
            base.Shutdown();
        }

        protected override void Dispose(bool disposing)
        {
            machine?.Dispose();
            vision?.Dispose();
            _ai?.Dispose();
            newContextCache?.Dispose();
            this.User?.Dispose();
            speech?.Dispose();
            Debugger?.Dispose();

            base.Dispose(disposing);
        }

        internal new DataAccessManager DataAccessManager
        {
            get { return _dataAccessManager; }
            set { _dataAccessManager = value; }
        }
        internal new IRobot AI { get { return _ai; } }

        private static SensoryMotorSystem machine;
        private static VisualProcessor vision;
        private static DataAccessManager _dataAccessManager;
        private static ContextCache newContextCache;
        private static Galatea.Speech.ISpeechModule speech;

        private static IRobot _ai;
    }
}
