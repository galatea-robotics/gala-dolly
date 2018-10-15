using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Galatea.IO;
using Galatea.Runtime;

namespace Gala.Data.Runtime
{
    internal class ContextCache : KeyedCollection<ContextKey, ContextNode>, IContextCache
    {
        [System.Diagnostics.DebuggerStepThrough]
        protected override ContextKey GetKeyForItem(ContextNode item)
        {
            if (item == null) throw new Galatea.TeaArgumentNullException("item");
            return item.Key;
        }
        internal void Initialize(IExecutiveFunctions taskManager)
        {
            if (taskManager == null) throw new Galatea.TeaArgumentNullException("taskManager");
            taskManager.InitializeContextCache(this);
        }

        ContextNode IContextCache.GetItem(ContextKey key)
        {
            return CacheBase.GetItem(this, key);
        }
        IContextCache IContextCache.GetPatternEntityNodes()
        {
            return CacheBase.GetPatternEntityNodes(this);
        }
        IContextCache IContextCache.GetRecognitionPatternNodes(InputType inputType)
        {
            return CacheBase.GetRecognitionPatternNodes(this, inputType);
        }
        IContextCache IContextCache.GetContextNodes(ContextType contextType)
        {
            return CacheBase.GetContextNodes(this, contextType);
        }
        IContextCache IContextCache.GetContextNodes(ContextType contextType, InputType inputType)
        {
            return CacheBase.GetContextNodes(this, contextType, inputType);
        }
        IContextCache IContextCache.GetHandledContextNodes(bool handled)
        {
            return CacheBase.GetHandledContextNodes(this, handled);
        }
        ContextNode IContextCache.GetMostRecent()
        {
            return CacheBase.GetMostRecent(this);
        }

        #region Component Model

        private bool disposedValue = false; // To detect redundant calls

        ISite IComponent.Site
        {
            get { return _site; }
            set { _site = value; }
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    foreach (ContextNode node in this)
                    {
                        node.Dispose();
                    }
                }

                disposedValue = true;

                // Raise the Disposed Event??
                if (Disposed != null)
                    Disposed(this, EventArgs.Empty);
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
        }

        public event EventHandler Disposed;

        private ISite _site;

        #endregion

        private class CacheBase : Collection<ContextNode>, IContextCache
        {
            [System.Diagnostics.DebuggerStepThrough]
            private CacheBase(IList<ContextNode> items) : base(items) { }

            #region Interface
            ContextNode IContextCache.GetItem(ContextKey key)
            {
                return CacheBase.GetItem(this, key);
            }
            IContextCache IContextCache.GetContextNodes(ContextType contextType)
            {
                return GetContextNodes(this, contextType);
            }
            IContextCache IContextCache.GetContextNodes(ContextType contextType, InputType inputType)
            {
                return GetContextNodes(this, contextType, inputType);
            }
            IContextCache IContextCache.GetHandledContextNodes(bool handled)
            {
                return GetHandledContextNodes(this, handled);
            }
            IContextCache IContextCache.GetPatternEntityNodes()
            {
                return GetPatternEntityNodes(this);
            }
            IContextCache IContextCache.GetRecognitionPatternNodes(InputType inputType)
            {
                return GetRecognitionPatternNodes(this, inputType);
            }
            ContextNode IContextCache.GetMostRecent()
            {
                return GetMostRecent(this);
            }
            #endregion

            public static ContextNode GetItem(IContextCache cache, ContextKey key)
            {
                return cache.ToDictionary(node => node.Key)[key];
            }
            public static IContextCache GetContextNodes(IContextCache cache,
                ContextType contextType)
            {
                IEnumerable<ContextNode> result =
                    from n in cache
                    where n.Key.ContextType == contextType
                    select n;

                return new CacheBase(result.ToList());
            }
            public static IContextCache GetContextNodes(IContextCache cache,
                ContextType contextType, InputType inputType)
            {
                IEnumerable<ContextNode> result =
                    from n in cache
                    where n.Key.ContextType == contextType && n.Key.InputType == inputType
                    select n;

                return new CacheBase(result.ToList());
            }
            public static IContextCache GetHandledContextNodes(IContextCache cache, bool handled)
            {
                IEnumerable<ContextNode> result =
                    from n in cache
                    where n.Handled == handled
                    select n;

                return new CacheBase(result.ToList());
            }
            public static IContextCache GetPatternEntityNodes(IContextCache cache)
            {
                IEnumerable<ContextNode> result = from n in cache
                                                  where n.PatternEntity != null
                                                  select n;

                return new CacheBase(result.ToList());
            }

            public static IContextCache GetRecognitionPatternNodes(IContextCache cache, InputType inputType = InputType.Undefined)
            {
                IEnumerable<ContextNode> result =
                    from n in cache
                    where (n.TemplateType != Galatea.AI.Abstract.TemplateType.Null || n.NamedEntity != null || n.NamedTemplate != null)
                    select n;

                if (inputType != InputType.Undefined)
                    result = from n in result
                             where n.Key.InputType == inputType
                             select n;

                return new CacheBase(result.ToList());
            }
            public static ContextNode GetMostRecent(IContextCache cache)
            {
                return cache.OrderBy(node => node.Key.Timestamp).FirstOrDefault();
            }

            #region Component Model

            private bool disposedValue = false; // To detect redundant calls
            protected virtual void Dispose(bool disposing)
            {
                if (!disposedValue)
                {
                    if (disposing)
                    {
                        foreach (ContextNode node in this)
                            node.Dispose();
                    }

                    disposedValue = true;
                }
            }

            // This code added to correctly implement the disposable pattern.
            public void Dispose()
            {
                // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
                Dispose(true);
            }
            public ISite Site
            {
                get { return _site; }
                set { _site = value; }
            }

            private ISite _site;

            #endregion

            public virtual event EventHandler Disposed { add { throw new NotSupportedException(); } remove { } }
        }
    }
}
