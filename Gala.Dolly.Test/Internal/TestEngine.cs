using System.Collections.Generic;
using Galatea;
using Galatea.AI;
using Galatea.AI.Imaging;
using Galatea.Diagnostics;
using Galatea.Runtime;
using Galatea.Runtime.Services;

namespace Gala.Dolly.Test
{
    using Galatea.AI.Abstract;
    using Gala.Data;
    using Gala.Data.Properties;
    using Gala.Data.Runtime;

    internal class TestEngine : RuntimeEngine, IRuntimeEngine, IEngine
    {
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
            Galatea.AI.Robotics.SensoryMotorSystem machine = null;
            VisualProcessor vision = null;
            IRobot robot = null;
            ContextCache newContextCache = null;
            IChatbotManager chatbots = null;

            // Initialize Foundation Components
            try
            {
                machine = new Galatea.AI.Robotics.Machine();
                machine.Initialize(this);

                vision = new VisualProcessor(Settings.Default.ImagingSettings);
                vision.Initialize(this);

                // Become Self-Aware
                robot = SelfAwareness.BecomeSelfAware(this, "Skynet");
                newContextCache = new ContextCache();
                newContextCache.Initialize(this.ExecutiveFunctions);
                // Verify that ContextCache is instantiated
                System.Diagnostics.Debug.Assert(ExecutiveFunctions.ContextCache != null);

                // Initialize Language Module
                this.User = new User(Settings.Default.DefaultUserName);
                chatbots = new ChatbotManager();
                robot.LanguageModel.LoadChatbots(chatbots);

                var substitutions = new List<string>
                {
                    "I ,eye ",
                    ".,.  ",
                    "Ayuh,If you say so|false"
                };
                robot.LanguageModel.LoadSubstitutions(substitutions);

                _ai = robot;
            }
            catch
            {
                if (machine == null) throw new TeaArgumentNullException("machine");
                if (vision == null) throw new TeaArgumentNullException("vision");
                if (robot == null) throw new TeaArgumentNullException("robot");
                if (newContextCache == null) throw new TeaArgumentNullException("newContextCache");
                if (chatbots == null) throw new TeaArgumentNullException("chatbots");

            }

            InitializeDatabase();
        }
        public void InitializeDatabase()
        {
            // Add Memory
            _dataAccessManager.Initialize(this);
            _dataAccessManager.InitializeMemoryBank();

            _ai.InitializeMemory(_dataAccessManager);

            // Set Application Settings
            ((ColorTemplateCollection)_dataAccessManager[TemplateType.Color]).HybridResultThreshold = Settings.Default.ColorTemplateHybridResultThreshold;
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

        internal static new DataAccessManager DataAccessManager
        {
            get { return _dataAccessManager; }
            set { _dataAccessManager = value; }
        }
        internal static new IRobot AI { get { return _ai; } }

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
