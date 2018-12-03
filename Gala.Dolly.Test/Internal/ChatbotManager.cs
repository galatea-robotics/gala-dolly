#if NETFX_CORE
extern alias GCM;
#endif

using System;
using System.Collections.ObjectModel;
using Galatea.Runtime;
using Galatea.Runtime.Services;

namespace Gala.Dolly.Test
{
    using Gala.Dolly.Test.Properties;

#if NETFX_CORE
    using CM = GCM.System.ComponentModel;
#else
    using CM = System.ComponentModel;
#endif

    /// <summary>
    /// Contains a collection of hard-coded defined Chatbots downloaded from the internet.
    /// </summary>
    internal sealed class ChatbotManager : KeyedCollection<string, IChatbot>, IChatbotManager
    {
        internal ChatbotManager()
        {
            // Initialize Default
            _chatbot = new DefaultChatbot(Settings.Default.DefaultChatbotName);
        }

        IChatbot IChatbotManager.Current
        {
            get { return _chatbot; }
            set { _chatbot = value; }
        }

        #region IComponent

        CM.ISite CM.IComponent.Site
        {
            get { return _site; }
            set { _site = value; }
        }
        void IDisposable.Dispose() {
            foreach (IChatbot chatbot in this)
                chatbot.Dispose();

            if (Disposed != null) Disposed(this, EventArgs.Empty);
        }

        public event EventHandler Disposed;

        #endregion

        protected override string GetKeyForItem(IChatbot item)
        {
            return item.Name;
        }

        private class DefaultChatbot : Galatea.Runtime.Services.Chatbot
        {
            public DefaultChatbot(string name) : base(name)
            {
            }

            public override string Greeting { get => Settings.Default.DefaultChatbotResponse; }
        }

        private IChatbot _chatbot;
        private CM.ISite _site;
    }
}
