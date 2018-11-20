using Galatea.AI;
using Galatea.AI.Imaging;
using Galatea.Diagnostics;
using Galatea.Runtime;
using Galatea.Runtime.Services;

namespace Gala.Dolly
{
    using Gala.Data;
    using Gala.Data.Databases;

    internal class SmartEngine : Engine, IRuntimeEngine, IEngine
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
            // Initialize Foundation Components
            Galatea.AI.Robotics.SensoryMotorSystem machine = new Galatea.AI.Robotics.Machine();
            machine.Initialize(this);

            VisualProcessor vision = new VisualProcessor(Properties.Settings.Default.ImagingSettings);
            vision.Initialize(this);

            _dataAccessManager.Initialize(this);
            _dataAccessManager.InitializeMemoryBank();

            // Become Self-Aware
            IRobot robot = SelfAwareness.BecomeSelfAware(this, Properties.Settings.Default.ChatbotName);
            Gala.Data.Runtime.ContextCache newContextCache = new Data.Runtime.ContextCache();
            newContextCache.Initialize(this.ExecutiveFunctions);
            // Verify that ContextCache is instantiated
            System.Diagnostics.Debug.Assert(ExecutiveFunctions.ContextCache != null);

            // Initialize Language Module
            this.User = new Galatea.Runtime.Services.User(Properties.Settings.Default.DefaultUserName);
            IChatbotManager chatbots = Gala.Dolly.Chatbots.ChatbotManager.GetChatbots(this.User);
            robot.LanguageModel.LoadChatBots(chatbots);

            System.Collections.Specialized.StringCollection substitutions = new System.Collections.Specialized.StringCollection();
            substitutions.Add("I ,eye ");
            substitutions.Add(".,.  ");
            substitutions.Add("Ayuh,If you say so|false");
            robot.LanguageModel.LoadSubstitutions(substitutions);

            Galatea.Speech.ISpeechModule speech = new Galatea.Speech.SpeechModule();
            speech.Initialize(robot.LanguageModel);
            speech.StaySilent = Properties.Settings.Default.SpeechIsSilent;

            // Add Text to Speech (even if silent)
            Galatea.Speech.TextToSpeech5 tts5 = new Galatea.Speech.TextToSpeech5(speech);
            speech.TextToSpeech = tts5;


            // ********** WIN 10 ZIRA ********** // 
            SpeechLib.SpVoice spV = tts5.GetSpeechObject() as SpeechLib.SpVoice;

            int defaultVoiceIndex = Properties.Settings.Default.TextToSpeechDefaultVoiceIndex;
            try
            {
                SpeechLib.SpObjectToken spVoice = tts5.GetVoice(defaultVoiceIndex) as SpeechLib.SpObjectToken;
                spV.Voice = spVoice;

                //string d = spV.Voice.GetDescription();
                //Debugger.Log(Galatea.Diagnostics.DebuggerLogLevel.Log, string.Format("SpeechLib.SpVoice: {0}", d));
            }
            catch (Galatea.Speech.TeaSpeechException ex)
            {
                string msg = "Error loading Text to Speech.  Silencing Speech Module.";
                Galatea.Speech.TeaSpeechException ex1 = new Galatea.Speech.TeaSpeechException(msg, ex);
                Debugger.HandleTeaException(ex1);

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
            this.User = new User(Properties.Settings.Default.DefaultUserName);
            this.User.FriendlyName = Properties.Settings.Default.DefaultUserName;
        }

        public override void Shutdown()
        {
            // Save Template database
            _dataAccessManager.SaveAll();

            // Finalize
            base.Shutdown();
        }

        internal new DataAccessManager DataAccessManager
        {
            get { return _dataAccessManager; }
            set { _dataAccessManager = value; }
        }
        internal new IRobot AI { get { return _ai; } }

        private static DataAccessManager _dataAccessManager;
        private static IRobot _ai;
    }
}
