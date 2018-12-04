#if NETFX_CORE
//extern alias GCM;
#endif
using System;
using System.ComponentModel;
using Galatea;
using Galatea.AI.Abstract;
using Galatea.AI.Characterization;
using Galatea.Runtime;

//http://social.msdn.microsoft.com/forums/en-US/Vsexpressvb/thread/875952fc-cd2c-4e74-9cf2-d38910bde613/

namespace Gala.Data
{
    using Gala.Data.Runtime;

    internal abstract class DataAccessManager : Memory, ILibrary, IDataAccessManager
    {
        protected DataAccessManager(string connectionString) : base()
        {
            _connectionString = connectionString;

            // Reset TemplateID cache
            Galatea.AI.Abstract.Memory.Reset();
        }

        #region IFoundation
        public virtual void Initialize(IEngine engine)
        {
            _engine = engine ?? throw new TeaArgumentNullException("engine");
            _engine.Add(this);
        }
        bool IFoundation.IsInitialized { get { return this.IsInitialized; } }
        public IEngine Engine { get { return _engine; } }
        #endregion
        
        #region Other Tables and Settings

        /*
        internal short ColorTemplateHybridResultThreshold
        {
            get { return ((ColorTemplateCollection)this[TemplateType.Color]).HybridResultThreshold; }
            set { ((ColorTemplateCollection)this[TemplateType.Color]).HybridResultThreshold = value; }
        }
         */

        public FeedbackCounterTable FeedbackCounterTable { get { return _feedbackCounterTable; } }
        
        protected void SetFeedbackCounterTable (FeedbackCounterTable feedbackCounterTable)
        {
            _feedbackCounterTable = feedbackCounterTable;
        }
        
        #endregion

        protected string ConnectionString { get { return _connectionString; } }

#if NETFX_CORE
        #region Component Model
        /*
        CM.ISite CM.IComponent.Site
        {
            get { return _site; }
            set { _site = value; }
        }
        protected internal override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            Disposed?.Invoke(this, EventArgs.Empty);
        }
        internal void Dispose()
        {
            Dispose(true);
        }
        private CM.ISite _site;
         */
        #endregion
#endif

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
        private readonly string _connectionString;
        private IEngine _engine;
    }
}