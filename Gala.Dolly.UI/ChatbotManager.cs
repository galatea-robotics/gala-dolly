using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Galatea.AI.Abstract;
using Galatea;
using Galatea.Runtime;

namespace Gala.Dolly.UI
{
    using Chatbots.Properties;

    /// <summary>
    /// Contains a collection of hard-coded defined Chatbots downloaded from the internet.
    /// </summary>
    [CLSCompliant(false)]
    public abstract class ChatbotManager : KeyedCollection<string, IChatbot>, IChatbotManager, IComponent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Gala.Dolly.UI.ChatbotManager"/> class.
        /// </summary>
        protected ChatbotManager()
        {
            // Initialize Default
            _chatbot = new DefaultChatbot(Settings.Default.ChatbotDefaultName);
        }

        /// <summary>
        /// Gets or sets the <see cref="IChatbot"/> contained in the collection that is actively 
        /// responding to User text.
        /// </summary>
        public IChatbot Current
        {
            get { return _chatbot; }
            set { _chatbot = value; }
        }

        #region IComponent

        ISite IComponent.Site
        {
            get { return _site; }
            set { _site = value; }
        }

        /// <summary>
        /// Represents the method that handles the <see cref="System.ComponentModel.IComponent.Disposed"/>
        /// event of a <see cref="ChatbotManager"/> component.
        /// </summary>
        public event EventHandler Disposed;
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting
        /// unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases the unmanaged resources used by the System.ComponentModel.Component
        /// and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing">
        /// true to release both managed and unmanaged resources; false to release only unmanaged
        /// resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            foreach (IChatbot chatbot in this)
                chatbot.Dispose();

            Disposed?.Invoke(this, EventArgs.Empty);
        }
        /// <summary>
        ///  Gets or sets the <see cref="System.ComponentModel.ISite"/> associated with the 
        ///  <see cref="ChatbotManager"/> component.
        /// </summary>
        protected virtual ISite Site
        {
            get { return _site; }
            set { _site = value; }
        }

        #endregion

        /// <summary>
        /// Extracts the Name key field from the specified <see cref="IChatbot"/>.
        /// </summary>
        /// <param name="item">
        /// The <see cref="IChatbot"/> from which to extract the key.
        /// </param>
        /// <returns>
        /// The Name key field of the specified <see cref="IChatbot"/>.
        /// </returns>
        protected override string GetKeyForItem(IChatbot item)
        {
            if (item == null) throw new TeaArgumentNullException(nameof(item));
            return item.Name;
        }

        private class DefaultChatbot : Galatea.Runtime.Services.Chatbot
        {
            public DefaultChatbot(string name):base(name)
            {
            }

            public override string Greeting { get => Settings.Default.ChatbotDefaultResponse; }
        }

        private IChatbot _chatbot;
        private ISite _site;
    }
}
