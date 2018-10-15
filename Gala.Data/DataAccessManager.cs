using System;
using System.ComponentModel;
using Galatea.AI.Abstract;
using Galatea.AI.Characterization;
using Galatea.Runtime;

//http://social.msdn.microsoft.com/forums/en-US/Vsexpressvb/thread/875952fc-cd2c-4e74-9cf2-d38910bde613/

namespace Gala.Data
{
    using Gala.Data.Runtime;
    using Galatea.AI;

    internal abstract class DataAccessManager : Memory, ILibrary, IDataAccessManager
    {
        protected DataAccessManager(string connectionString) : base()
        {
            _connectionString = connectionString;

            // Reset TemplateID cache
            Galatea.AI.Abstract.Memory.Reset();

            // Initialize Query Delegates
            Gala.Data.Runtime.Queries.Initialize();
        }

        #region IFoundation
        public void Initialize(IEngine engine)
        {
            _engine = engine;
            _engine.Add(this);

            _isInitialized = true;
        }
        bool IFoundation.IsInitialized { get { return _isInitialized; } }
        public IEngine Engine { get { return _engine; } }
        #endregion

        #region Other Tables

        public FeedbackCounterTable FeedbackCounterTable { get { return _feedbackCounterTable; } }
        
        protected void SetFeedbackCounterTable (FeedbackCounterTable feedbackCounterTable)
        {
            _feedbackCounterTable = feedbackCounterTable;
        }

        #endregion  

        protected string ConnectionString { get { return _connectionString; } }

        #region Component Model

        ISite IComponent.Site
        {
            get { return _site; }
            set { _site = value; }
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        public event EventHandler Disposed;

        #endregion

        /*
        public static void LoadPersonalityTraits(Galatea.AI.CognitiveModelingSystem AI)
        {
            // Load Personality Settings
            SelfAwareness.PersonalityTraits.Discerning    = PersonalitySettings.Default.Discerning;
            SelfAwareness.PersonalityTraits.Loquacious    = PersonalitySettings.Default.Loquacious;
            SelfAwareness.PersonalityTraits.Gregarious    = PersonalitySettings.Default.Gregarious;
            SelfAwareness.PersonalityTraits.Autonomous    = PersonalitySettings.Default.Autonomous;
            SelfAwareness.PersonalityTraits.Executive     = PersonalitySettings.Default.Executive;
            SelfAwareness.PersonalityTraits.Abstract      = PersonalitySettings.Default.Abstract;
            SelfAwareness.PersonalityTraits.FavoriteColor = AI.ShortTermMemory.ColorTemplates[PersonalitySettings.Default.FavoriteColor];
        }
        public static void SavePersonalityTraits(Galatea.AI.CognitiveModelingSystem AI)
        {         
            PersonalitySettings.Default.Discerning    = SelfAwareness.PersonalityTraits.Discerning;
            PersonalitySettings.Default.Loquacious    = SelfAwareness.PersonalityTraits.Loquacious;
            PersonalitySettings.Default.Gregarious    = SelfAwareness.PersonalityTraits.Gregarious;
            PersonalitySettings.Default.Autonomous    = SelfAwareness.PersonalityTraits.Autonomous;
            PersonalitySettings.Default.Executive     = SelfAwareness.PersonalityTraits.Executive;
            PersonalitySettings.Default.Abstract      = SelfAwareness.PersonalityTraits.Abstract;
            PersonalitySettings.Default.FavoriteColor = SelfAwareness.PersonalityTraits.FavoriteColor.Name;
        }
         */

        private FeedbackCounterTable _feedbackCounterTable;
        private string _connectionString;
        private IEngine _engine;
        private bool _isInitialized;
        private ISite _site;
    }
}