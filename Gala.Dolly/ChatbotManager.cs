using System;
using Galatea.AI.Abstract;
using Galatea.Runtime;


namespace Gala.Dolly.Chatbots
{
    /// <summary>
    /// Contains a collection of hard-coded defined Chatbots downloaded from the internet.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Chatbot")]
    internal sealed class ChatbotManager : Gala.Dolly.UI.ChatbotManager
    {
        /// <summary>
        /// Gets a collection of hard-coded defined Chatbots downloaded from the internet.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Chatbots")]
        public static ChatbotManager GetChatbots(IUser user)
        {
            ChatbotManager result = new ChatbotManager();
            try
            {
                Alice alice = new Alice(user, "Alice");
                result.Add(alice);

                Eliza eliza = new Eliza("Eliza");
                result.Add(eliza);
            }
            catch
            {
                result.Dispose(true);
                throw;
            }

            // Finalize
            return result;
        }

        /// <summary>
        /// Releases the unmanaged resources used by the System.ComponentModel.Component
        /// and optionally releases the managed resources.
        /// </summary>b
        /// <param name="disposing">
        /// true to release both managed and unmanaged resources; false to release only unmanaged
        /// resources.
        /// </param>
        protected override void Dispose(bool disposing)
        {
            //if (_alice != null) _alice.Dispose();
            //if (_eliza != null) _eliza.Dispose();
            base.Dispose(disposing);
        }
    }
}
