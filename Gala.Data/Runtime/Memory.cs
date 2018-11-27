using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Galatea.AI.Abstract;
using Galatea.AI.Characterization;
using Galatea.Runtime;

namespace Gala.Data.Runtime
{
    internal abstract class Memory : KeyedCollection<TemplateType, IBaseTemplateCollection>, ILibrary
    {
        public Memory()
        {
            // Preserve IMemory reference
            Galatea.AI.Abstract.Memory.Default = this;

            // Initialize
            var t = this.GetType();
            ProviderID = t.FullName;
            ProviderName = t.Name;

            if (_feedbackCache == null) _feedbackCache = new FeedbackCache();
            if (_creators == null) _creators = new CreatorCollection();
        }

        [System.Diagnostics.DebuggerStepThrough]
        protected override TemplateType GetKeyForItem(IBaseTemplateCollection item)
        {
            if (item == null) throw new Galatea.TeaArgumentNullException("item");
            return item.TemplateType;
        }

        int ILibrary.GetNextIdentifier<TTemplate>(IBaseTemplateCollection<TTemplate> items)
        {
            int result = Galatea.AI.Abstract.Memory.IndexOfTemplateIDs.Keys.Max();
            result++;

            return result;
        }


        IList<BaseTemplate> ILibrary.GetBaseTemplateItems<TTemplate>(IBaseTemplateCollection<TTemplate> collection)
        {

            return collection.Cast<BaseTemplate>().ToList();
        }



        IFeedbackCache ILibrary.FeedbackCache { get { return _feedbackCache; } }
        KeyedCollection<string, ICreator> ILibrary.Creators { get { return _creators; } }

        protected internal abstract void InitializeMemoryBank();
        protected internal abstract bool IsInitialized { get; set; }
        protected virtual CreatorCollection Creators { get { return _creators;} }

        public abstract void SaveAll();


        #region IDisposable
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    foreach (IBaseTemplateCollection templateCollection in this)
                    {
                        templateCollection.Dispose();
                    }
                }

                disposedValue = true;
            }

            Disposed?.Invoke(this, EventArgs.Empty);
        }

        // This code added to correctly implement the disposable pattern.
        void IDisposable.Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
        }

        public event EventHandler Disposed;
        #endregion

        ISite IComponent.Site { get; set; }

        /// <summary>
        /// Gets a unique value representing the Data Access Manager.
        /// </summary>
        public string ProviderID { get; }

        /// <summary>
        /// The name that is displayed by UI and logging functions.
        /// </summary>
        public string ProviderName { get; }


        private static IFeedbackCache _feedbackCache;
        private static CreatorCollection _creators;
    }
}