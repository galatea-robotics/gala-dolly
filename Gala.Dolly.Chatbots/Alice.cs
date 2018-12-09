using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using AIMLBot.Utils;

namespace Gala.Dolly.Chatbots
{
    using Galatea.Runtime.Services;
    using Galatea.Globalization;
    using Properties;

    /// <summary>
    /// A ChatBot interface running the ALICE artificial intelligence algorithm.
    /// </summary>
    internal class Alice : Chatbot
    {
        /// <summary>
        /// Initializes a new instance of the <see>Galatea.ChatBots.Alice</see> class.
        /// </summary>
        public Alice(Galatea.AI.Abstract.IUser user, string chatbotName): this(user, chatbotName, null, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see>Galatea.ChatBots.Alice</see> class.
        /// </summary>
        public Alice(Galatea.AI.Abstract.IUser user, string chatbotName, string chatbotAliceConfigFolder, string chatbotResourcesFolder) : base(chatbotName)
        {
            userName = user.Name;
            aimlBot = new AIMLBot.Bot();
            if (chatbotAliceConfigFolder == null) chatbotAliceConfigFolder = GetChatbotAliceConfigFolder();
            if (chatbotResourcesFolder == null) chatbotResourcesFolder = GetChatbotResourcesFolder();

            // Validate Folders
            ValidateFolders(chatbotAliceConfigFolder, chatbotResourcesFolder);

            // Initialize AIMLBot2.5 Properties
            aimlBot.loadSettings(chatbotAliceConfigFolder);
            aimlBot.PathToAIML = Path.Combine(Settings.Default.ChatbotResourcesFolder, "alice");

            aimlBot.loadAIMLFromFiles();
            aimlBot.DefaultPredicates.updateSetting("name", userName);

            // Set Chatbot Settings

            // Initialize Chat runtime
            aimlUser = new AIMLBot.User(userName, aimlBot);
        }

        private static void ValidateFolders(string chatbotAliceConfigFolder, string chatbotResourcesFolder)
        {
            if (!Directory.Exists(chatbotAliceConfigFolder))
                throw new FileNotFoundException(
                    string.Format(CultureInfo.CurrentCulture,
                        Resources.ChatbotAliceConfigFolder_Not_Found,
                        chatbotAliceConfigFolder));

            if (!Directory.Exists(chatbotResourcesFolder))
                throw new FileNotFoundException(
                    string.Format(CultureInfo.CurrentCulture,
                        Resources.ChatbotResourcesFolder_Not_Found,
                        chatbotResourcesFolder));
        }

        public override string Greeting { get { return Resources.ChatBotAliceGreeting; } }

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

        private static string GetChatbotAliceConfigFolder()
        {
            return Properties.Settings.Default.ChatbotAliceConfigFolder;
        }
        private static string GetChatbotResourcesFolder()
        {
            return Properties.Settings.Default.ChatbotResourcesFolder;
        }

        private string userName;
        private AIMLBot.Bot aimlBot;
        private AIMLBot.User aimlUser;
    }
}
