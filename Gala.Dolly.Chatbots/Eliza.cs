//using Galatea.Services;

using System;
using System.ComponentModel;
using System.IO;
using Galatea.Globalization;
using Galatea.Runtime.Services;

namespace Gala.Dolly.Chatbots
{
    using Properties;

    /// <summary>
    /// A ChatBot interface component class running the 
    /// infamous ELIZA artificial intelligence program.
    /// </summary>
    internal sealed class Eliza : Chatbot
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Eliza"></see> class.
        /// </summary>
        public Eliza() : base("Eliza") { Initialize(); }
        /// <summary>
        /// Initializes a new instance of the <see cref="Eliza"></see> class.
        /// </summary>
        public Eliza(string name) : base(name, name) { Initialize(); }

        public override string Greeting { get { return ChatbotResources.ChatbotElizaGreeting; } }

        private void Initialize()
        {
            elizaBot = new ElizaBot.Doctor(
                Path.Combine(Settings.Default.ChatbotResourcesFolder, "eliza.dat"), 
                defaultEmptyResponse);
        }

        /// <summary>
        /// An artificial intelligence method that responds to a text input based on the classic ELIZA program.
        /// </summary>
        /// <param name="input"> The text input to process. </param>
        /// <returns> The text response. </returns>
        protected override string GetResponse(string input)
        {
            return elizaBot.Ask(input);
        }

        internal string defaultEmptyResponse = "";
        private ElizaBot.Doctor elizaBot;
    }
}
