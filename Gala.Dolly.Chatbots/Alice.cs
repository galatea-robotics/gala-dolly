using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using AIMLBot.Utils;
using Galatea.Runtime.Services;

namespace Gala.Dolly.Chatbots
{
    using Properties;

    /// <summary>
    /// A ChatBot interface running the ALICE artificial intelligence algorithm.
    /// </summary>
    internal class Alice : Chatbot
    {
        /// <summary>
        /// Initializes a new instance of the <see>Galatea.ChatBots.Alice</see> class.
        /// </summary>
        public Alice(Galatea.AI.Abstract.IUser user, string chatbotName) : base(chatbotName)
        {
            userName = user.Name;
            aimlBot = new AIMLBot.Bot();

            // Initialize AIMLBot2.5 Properties
            if (!Directory.Exists(Settings.Default.ChatbotAliceConfigFolder))
                throw new FileNotFoundException(
                    string.Format(CultureInfo.CurrentCulture,
                        Resources.ChatbotAliceConfigFolder_Not_Found,
                        Settings.Default.ChatbotAliceConfigFolder));

            if(!Directory.Exists(Settings.Default.ChatbotResourcesFolder))
                throw new FileNotFoundException(
                    string.Format(CultureInfo.CurrentCulture,
                        Resources.ChatbotResourcesFolder_Not_Found,
                        Settings.Default.ChatbotResourcesFolder));

            aimlBot.loadSettings(Settings.Default.ChatbotAliceConfigFolder);
            aimlBot.PathToAIML = Path.Combine(Settings.Default.ChatbotResourcesFolder, "alice");

            aimlBot.loadAIMLFromFiles();
            aimlBot.DefaultPredicates.updateSetting("name", userName);

            // Set Chatbot Settings

            // Initialize Chat runtime
            aimlUser = new AIMLBot.User(userName, aimlBot);
        }

        public override string Greeting { get { return Properties.Resources.ChatBotAliceGreeting; } }

        /// <summary>
        /// An artificial intelligence method that responds to a text input based on the ALICE algorithm.
        /// </summary>
        /// <param name="input"> The text input to process. </param>
        /// <returns> The text response. </returns>
        protected override string GetResponse(string input)
        {
            AIMLBot.Request aimlRequest = new AIMLBot.Request(input, aimlUser, aimlBot, true);
            return aimlBot.Chat(aimlRequest).Output;
        }

        public override string FriendlyName
        {
            get { return base.FriendlyName; }
            set
            {
                aimlBot.GlobalSettings.updateSetting("name", value);
                base.FriendlyName = value;
            }
        }

        private string userName;
        private AIMLBot.Bot aimlBot;
        private AIMLBot.User aimlUser;
    }
}
