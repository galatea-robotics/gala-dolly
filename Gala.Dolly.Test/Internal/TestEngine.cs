using System.Collections.Generic;
using Galatea.AI;
using Galatea.AI.Imaging;
using Galatea.Diagnostics;
using Galatea.Runtime;
using Galatea.Runtime.Services;

namespace Gala.Dolly.Test
{
    using Galatea.AI.Abstract;
    using Gala.Data;

    internal class TestEngine : RuntimeEngine, IRuntimeEngine, IEngine
    {
        /*
        #region Config
#if NETFX_CORE
        private static Gala.Data.Configuration.Settings config;
        internal static Gala.Data.Configuration.Settings UWPSettings { get { return config; } }
#endif
        private static string defaultUserName, chatbotName;
        private static short colorTemplateHybridResultThreshold;
        internal static void LoadConfig()
        {
#if NETFX_CORE
            config = Gala.Data.Configuration.Settings.Load().Result;
            if (config == null) throw new TeaInitializationException("TestEngine initialization failed - UWPSettings is null.");
            defaultUserName = config.DefaultUserName;
            chatbotName = config.ChatbotName;
            colorTemplateHybridResultThreshold = config.ColorTemplateHybridResultThreshold;
            // Imaging Settings from .NETCORE Config
            Galatea.AI.Imaging.Properties.NetCoreSettings.ColorBrightnessThreshold = config.ColorBrightnessThreshold;
            Galatea.AI.Imaging.Properties.NetCoreSettings.ShapeOblongThreshold = config.ShapeOblongThreshold;
            Galatea.AI.Imaging.Properties.NetCoreSettings.ShapeOblongRecognitionLevel = config.ShapeOblongRecognitionLevel;
            Galatea.AI.Imaging.Properties.NetCoreSettings.ShapeOblongRecognitionNormalization = config.ShapeOblongRecognitionNormalization;
#else
            defaultUserName =  Properties.Settings.Default.DefaultUserName;
            chatbotName = Properties.Settings.Default.DefaultChatbotName;
            colorTemplateHybridResultThreshold = Properties.Settings.Default.ColorTemplateHybridResultThreshold;
#endif
        }
        #endregion

        static TestEngine()
        {
            LoadConfig();
        }
         */

        public TestEngine(IDebugger debugger, DataAccessManager dataAccessManager)
        {
            //LoadConfig();

            // Initialize Debugger before anything else
            this.Debugger = debugger;

            // Initialize Database
            _dataAccessManager = dataAccessManager;

            // Initialize Engine
            Initialize();
        }

        internal void Initialize()
        {
            // Initialize Foundation Components
            Galatea.AI.Robotics.SensoryMotorSystem machine = new Galatea.AI.Robotics.Machine();
            machine.Initialize(this);

/*
#if !NETFX_CORE
            imagingSettings = Galatea.AI.Imaging.ImagingSettings.Create();
#else
            imagingSettings = UWPSettings.ImagingSettings;
#endif
 */
            VisualProcessor vision = new VisualProcessor(Properties.Settings.Default.ImagingSettings);
            vision.Initialize(this);

            // Become Self-Aware
            IRobot robot = SelfAwareness.BecomeSelfAware(this, "Skynet");
            Gala.Data.Runtime.ContextCache newContextCache = new Data.Runtime.ContextCache();
            newContextCache.Initialize(this.ExecutiveFunctions);
            // Verify that ContextCache is instantiated
            System.Diagnostics.Debug.Assert(ExecutiveFunctions.ContextCache != null);

            // Initialize Language Module
            this.User = new Galatea.Runtime.Services.User(Properties.Settings.Default.DefaultUserName);
            IChatbotManager chatbots = new Gala.Dolly.Test.ChatbotManager();
            robot.LanguageModel.LoadChatbots(chatbots);

            var substitutions = new List<string>();
            substitutions.Add("I ,eye ");
            substitutions.Add(".,.  ");
            substitutions.Add("Ayuh,If you say so|false");
            robot.LanguageModel.LoadSubstitutions(substitutions);

            /*
            Galatea.Speech.ISpeechModule speech = new Galatea.Speech.SpeechModule();
            speech.Initialize(robot.LanguageModel);
            speech.StaySilent = Properties.Settings.Default.SpeechIsSilent;

            if (!speech.StaySilent)
            {
                /*
                //_engine.Machine.SpeechModule = _speech;
                Galatea.Speech.TextToSpeech5 tts5 = new Galatea.Speech.TextToSpeech5(speech);


                // ********** WIN 10 ZIRA ********** // 
                SpeechLib.SpVoice spV = tts5.GetSpeechObject() as SpeechLib.SpVoice;

                int defaultVoiceIndex = Properties.Settings.Default.TextToSpeechDefaultVoiceIndex;
                SpeechLib.SpObjectToken spVoice = tts5.GetVoice(defaultVoiceIndex) as SpeechLib.SpObjectToken;
                spV.Voice = spVoice;

                string d = spV.Voice.GetDescription();
                Debugger.Log(Galatea.Diagnostics.DebuggerLogLevel.Log, string.Format("SpeechLib.SpVoice: {0}", d));
                 * /
            }
             */

            _ai = robot;

            /*
            // Apply default conversational labels
            _ai.LanguageModel.ChatbotManager.Current = new Chatbot(chatbotName);
             */

            InitializeDatabase();
        }
        public void InitializeDatabase()
        {
            // Add Memory
            _dataAccessManager.Initialize(this);
            _dataAccessManager.InitializeMemoryBank();

            _ai.InitializeMemory(_dataAccessManager);

            // Set Application Settings
            ((ColorTemplateCollection)_dataAccessManager[TemplateType.Color]).HybridResultThreshold = Properties.Settings.Default.ColorTemplateHybridResultThreshold;
        }

        public override void Startup()
        {
            base.Startup();
        }

        public override void Shutdown()
        {
            _dataAccessManager.SaveAll();
            base.Shutdown();

            /*
            _dataAccessManager.FileLogger.StopLogging();
             */
        }

        internal new DataAccessManager DataAccessManager
        {
            get { return _dataAccessManager; }
            set { _dataAccessManager = value; }
        }
        internal new IRobot AI { get { return _ai; } }

        protected override void Dispose(bool disposing)
        {
            /*
#if NETFX_CORE
            config.Save();
#endif
             */

            base.Dispose(disposing);
        }


        private static DataAccessManager _dataAccessManager;
        private static IRobot _ai;
        //private static Galatea.Speech.TextToSpeech5 _tts5;
    }
}
